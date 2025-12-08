using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    /// <summary>
    /// ダイアログとの連携設定。
    /// </summary>
    sealed class DialogConfig
    {
        /*
         * BasicとTexts_ErrorMessagesは年賀アプリケーションごとに内容が変わるプロパティ。
         */
        public ApplicationBasicConfig Basic { get; set; }
        public string[] Texts_ErrorMessage { get; set; }

        public int LabelIndex_Message { get; set; }
        public Point LabelPoint_Message { get; set; }
        public string ButtonName_Ok { get; set; }

        public string Text_MovedForwardWorkProcessMessage { get; set; }
    }
}
