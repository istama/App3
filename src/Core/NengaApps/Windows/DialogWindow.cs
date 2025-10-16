using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// ダイアログを操作するクラス。
    /// </summary>
    sealed class DialogWindow : NengaAppWindowBasic
    {
        private readonly DialogConfig _config;


        public DialogWindow(INativeWindowFactory nativeWindowFactory, DialogConfig dialogConfig)
            : base(nativeWindowFactory, dialogConfig.Basic)
        {
            Assert.IsNull(nativeWindowFactory, nameof(nativeWindowFactory));

            _config = dialogConfig;
        }


        /// <summary>
        /// 引数のテキストがダイアログのメッセージに部分的にでも含まれていればtrueを返す。
        /// </summary>
        public Boolean Contains(string text)
        {
            return Contains(new[] { text });
        }

        /// <summary>
        /// 引数のテキストの配列のうち、１つでもダイアログのメッセージに部分的にでも含まれていればtrueを返す。
        /// </summary>
        public Boolean Contains(params string[] texts)
        {
            return Contains(texts);
        }

        /// <summary>
        /// 引数のテキストのコレクションのうち、１つでもダイアログのメッセージに部分的にでも含まれていればtrueを返す。
        /// </summary>
        public Boolean Contains(IEnumerable<string> texts)
        {
            var message = WindowStates
                .GetFormControlStatesArray(_config.LabelPoint_Message)[0]
                .GetText();

            return texts.Any(text => message.Contains(text));
        }

        /// <summary>
        /// ダイアログがエラーメッセージの場合はtrueを返す。
        /// </summary>
        public Boolean IsErrorDialog()
        {
            return Contains(_config.Texts_ErrorMessage);
        }

        /// <summary>
        /// OKボタンを押す。
        /// </summary>
        public Task<Boolean> OkAsync()
        {
            return WindowOperator
                .SendEnterTo(_config.ButtonName_Ok)
                .ThrowIfFailed()
                .DoAsync();
        }
    }
}
