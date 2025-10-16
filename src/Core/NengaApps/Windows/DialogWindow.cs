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
    /// �_�C�A���O�𑀍삷��N���X�B
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
        /// �����̃e�L�X�g���_�C�A���O�̃��b�Z�[�W�ɕ����I�ɂł��܂܂�Ă����true��Ԃ��B
        /// </summary>
        public Boolean Contains(string text)
        {
            return Contains(new[] { text });
        }

        /// <summary>
        /// �����̃e�L�X�g�̔z��̂����A�P�ł��_�C�A���O�̃��b�Z�[�W�ɕ����I�ɂł��܂܂�Ă����true��Ԃ��B
        /// </summary>
        public Boolean Contains(params string[] texts)
        {
            return Contains(texts);
        }

        /// <summary>
        /// �����̃e�L�X�g�̃R���N�V�����̂����A�P�ł��_�C�A���O�̃��b�Z�[�W�ɕ����I�ɂł��܂܂�Ă����true��Ԃ��B
        /// </summary>
        public Boolean Contains(IEnumerable<string> texts)
        {
            var message = WindowStates
                .GetFormControlStatesArray(_config.LabelPoint_Message)[0]
                .GetText();

            return texts.Any(text => message.Contains(text));
        }

        /// <summary>
        /// �_�C�A���O���G���[���b�Z�[�W�̏ꍇ��true��Ԃ��B
        /// </summary>
        public Boolean IsErrorDialog()
        {
            return Contains(_config.Texts_ErrorMessage);
        }

        /// <summary>
        /// OK�{�^���������B
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
