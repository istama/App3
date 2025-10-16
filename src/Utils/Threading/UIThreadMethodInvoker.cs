using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// コールバックをメインスレッドで実行するためのクラス。
    /// メインスレッド以外でUIコンポーネントを呼び出すときなどに使用する。
    /// 
    /// 通常、メインスレッド以外のスレッドから一部の処理をメインスレッドで実行させるには、
    /// System.Windows.Forms.ControlのインスタンスのInvoke()メソッドにコールバックを渡す必要がある。
    /// しかしこの方法の問題は、Invoke()を呼び出すメソッドがControlインスタンスを保持している必要があり、
    /// さらにそのインスタンスがすでに表示済みでウィンドウハンドルを作成済みである必要がある。
    /// 
    /// このクラスは、クラス内部でControlインスタンスを保持し、ウィンドウハンドルを作成しておき、
    /// staticメソッドでどこからでもInvoke()を呼び出せるようにした。
    /// </summary>
    sealed class UIThreadMethodInvoker
    {
        private static System.Windows.Forms.Control _control;

        static UIThreadMethodInvoker()
        {
            _control = new System.Windows.Forms.Control();
        }

        /// <summary>
        /// Invoke()を呼び出すための初期処理を行う。
        /// このメソッドはメインスレッドから呼び出す必要がある。
        /// </summary>
        public static void Initialize()
        {
            // 強制的にウィンドウハンドルを作成する
            if (!_control.IsHandleCreated)
                _control.CreateControl();
        }

        public static TResult Invoke<TResult>(Func<TResult> callback)
        {
            if (!_control.IsHandleCreated)
                throw new InvalidOperationException("Initialize()メソッドが呼び出されていません。");

            return (TResult)_control.Invoke(new Func<TResult>(callback));
        }

        public static TResult Invoke<T, TResult>(Func<T, TResult> callback, T arg)
        {
            if (!_control.IsHandleCreated)
                throw new InvalidOperationException("Initialize()メソッドが呼び出されていません。");

            return (TResult)_control.Invoke(new Func<T, TResult>(callback), arg);
        }

        public static void Invoke(Action callback)
        {
            if (!_control.IsHandleCreated)
                throw new InvalidOperationException("Initialize()メソッドが呼び出されていません。");

            if (!_control.IsDisposed)
                _control.Invoke(new Action(callback));
        }

        public static void Invoke<T>(Action<T> callback, T arg)
        {
            if (!_control.IsHandleCreated)
                throw new InvalidOperationException("Initialize()メソッドが呼び出されていません。");

            if (!_control.IsDisposed)
                _control.Invoke(new Action<T>(callback), arg);
        }

        public static void Dispose()
        {
            _control.Dispose();
        }
    }
}
