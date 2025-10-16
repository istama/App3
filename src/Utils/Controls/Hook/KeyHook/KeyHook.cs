using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class KeyHook : KeyHookPrimitive
    {
        public new event EventHandler<KeyHookEventArgs> InputKeyDown;
        public new event EventHandler<KeyHookEventArgs> InputKeyUp;

        public new event EventHandler<KeyHookEventArgs> ModifierKeyDown;
        public new event EventHandler<KeyHookEventArgs> ModifierKeyUp;


        /// 入力されたキーの状態を保持する
        private readonly KeyStateBuffer _keyStateBuffer;


        public KeyHook() : this(new KeyStateBuffer())
        {
        }
        public KeyHook(KeyStateBuffer keyStateBuffer) : base()
        {
            Assert.IsNull(keyStateBuffer, nameof(keyStateBuffer));

            _keyStateBuffer = keyStateBuffer;
        }


        /// <summary>
        /// キーが押下されたイベントを通知する。
        /// イベントハンドリングメソッドがeのHandledプロパティにtrueをセットすると、
        /// 次のウィンドウにキー入力を渡さないようにできる。
        /// </summary>
        protected virtual void OnInputKeyDown(KeyHookEventArgs e)
            => Volatile.Read(ref this.InputKeyDown)?.Invoke(this, e);

        protected virtual void OnInputKeyUp(KeyHookEventArgs e)
            => Volatile.Read(ref this.InputKeyUp)?.Invoke(this, e);

        protected virtual void OnModifierKeyDown(KeyHookEventArgs e)
            => Volatile.Read(ref this.ModifierKeyDown)?.Invoke(this, e);

        protected virtual void OnModifierKeyUp(KeyHookEventArgs e)
            => Volatile.Read(ref this.ModifierKeyUp)?.Invoke(this, e);

        /* 
         * 以下、キー操作を行ったときに呼び出されるKeyHookPrimitiveのイベント発火メソッドのオーバーライド。
         */

        protected override void OnInputKeyDown(KeyHookPrimitiveEventArgs e)
        {
            if (_keyStateBuffer.UpdateByInputKeyDown(e.InputKey))
            {
                var args = new KeyHookEventArgs(_keyStateBuffer.ToKeyState());
                OnInputKeyDown(args);

                // 元のイベントにHandledされたことを伝播する
                if (args.Handled)
                    e.Handled = true;
            }

            base.OnInputKeyDown(e);
        }

        protected override void OnInputKeyUp(KeyHookPrimitiveEventArgs e)
        {
            if (_keyStateBuffer.UpdateByInputKeyUp())
            {
                var args = new KeyHookEventArgs(_keyStateBuffer.ToKeyState());
                OnInputKeyUp(args);

                // 元のイベントにHandledされたことを伝播する
                if (args.Handled)
                    e.Handled = true;
            }

            base.OnInputKeyUp(e);
        }

        protected override void OnModifierKeyDown(KeyHookPrimitiveEventArgs e)
        {
            if(_keyStateBuffer.UpdateByModifierKeysDown(e.ModifierKeys))
            {
                var args = new KeyHookEventArgs(_keyStateBuffer.ToKeyState());
                OnModifierKeyDown(args);

                // 元のイベントにHandledされたことを伝播する
                if (args.Handled)
                    e.Handled = true;
            }

            base.OnModifierKeyDown(e);
        }

        protected override void OnModifierKeyUp(KeyHookPrimitiveEventArgs e)
        {
            if (_keyStateBuffer.UpdateByModifierKeysUp(e.ModifierKeys))
            {
                var args = new KeyHookEventArgs(_keyStateBuffer.ToKeyState());
                OnModifierKeyUp(args);

                // 元のイベントにHandledされたことを伝播する
                if (args.Handled)
                    e.Handled = true;
            }

            base.OnModifierKeyUp(e);
        }
    }
}
