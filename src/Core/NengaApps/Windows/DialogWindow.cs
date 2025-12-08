using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Error;
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
        /// 引数のテキストのコレクションのうち、１つでもダイアログのメッセージに部分的にでも含まれていればtrueを返す。
        /// </summary>
        public Boolean Contains(IEnumerable<string> texts)
        {
            var controls = WindowStates.GetFormControlStatesArray(_config.LabelPoint_Message);
            if (controls.Length == 0)
                throw new NengaBoosterException("ダイアログのラベルを取得できません。");

            var control = controls.First();
            var message = control.GetText();

            return texts.Any(text => message.Contains(text));
        }

        /// <summary>
        /// 引数の工程名のコレクションのうち、１つでもダイアログのメッセージに含まれていればtrueを返す。
        /// また、コレクションが空ならfalseを返す。
        /// Contains()メソッドとの違いは、渡された工程名の前後に自動で、()と（）を付けて判定すること。
        /// 年賀アプリのダイアログによって、カッコの表記が全角か半角か揺れているためこのように対処している。
        /// </summary>
        public Boolean ContainsWorkProcessNames(IEnumerable<string> texts)
        {
            if (texts.Count() == 0)
                return false;

            var controls = WindowStates.GetFormControlStatesArray(_config.LabelPoint_Message);
            if (controls.Length == 0)
                throw new NengaBoosterException("ダイアログのラベルを取得できません。");

            var control = controls.First();
            var message = control.GetText();

            return texts.Any(text => message.Contains($"({text})") || message.Contains($"（{texts}）"));
        }

        /// <summary>
        /// ダイアログがエラーメッセージの場合はtrueを返す。
        /// </summary>
        public Boolean IsErrorDialog()
        {
            return Contains(_config.Texts_ErrorMessage);
        }

        /// <summary>
        /// 工程が先にすすでいるメッセージの場合はtrueを返す
        /// </summary>
        /// <returns></returns>
        public Boolean IsMovedForwardWorkProcessDialog()
        {
            return Contains(_config.Text_MovedForwardWorkProcessMessage);
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
