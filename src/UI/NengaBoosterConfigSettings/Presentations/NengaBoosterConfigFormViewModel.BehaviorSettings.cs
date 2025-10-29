
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations
{
    /// <summary>
    /// 動作設定のビューモデル。
    /// </summary>
    class NengaBoosterConfigFormViewModelForBehaviorSettings
    {
        [Description("注文名入れ情報入力でデータを開くとき、ダイアログが開かれるまで待機する時間 ミリ秒単位で指定する")]
        [Category("1. 待ち時間")]
        [LargerThanOrEqual(0)]
        public int WaitTime_Naire_DialogOpen { get; set; }
        [Description("再組版モードで注文名入れ情報入力でデータを開くとき、ダイアログが開かれるまで待機する時間 ミリ秒単位で指定する")]
        [Category("1. 待ち時間")]
        [LargerThanOrEqual(0)]
        public int WaitTime_Naire_DialogOpenOnSaikumi { get; set; }
        [Description("再組版モードで注文名入れ情報に登録するとき、手組原稿だった場合、ダイアログが開かれるまで待機する時間 ミリ秒単位で指定する")]
        [Category("1. 待ち時間")]
        [LargerThanOrEqual(0)]
        public int WaitTime_Naire_SaikumiAlertDialogOpen { get; set; }
        [Description("編集でデータを開くとき、ダイアログが開かれるまで待機する時間 ミリ秒単位で指定する")]
        [Category("1. 待ち時間")]
        [LargerThanOrEqual(0)]
        public int WaitTime_Hensyu_DialogOpen { get; set; }
        //[Description("問番リストを一件ずつ校正紙ダイレクト出力に送るときの待ち時間 ミリ秒単位で指定する")]
        //[Category("1. 待ち時間")]
        //[LargerThanOrEqual(0)]
        //public int WaitTime_Kouseishi_NextToibanSend { get; set; }

        [Description("工程を示すダイアログが表示されたとき、自動でそのダイアログを閉じてもよい工程を指定する")]
        [Category("2. 動作")]
        public string Texts_Dialog_WorkProcessNames { get; set; }

        [Description("ユーザー設定ファイルのパスを指定する")]
        [Category("3. ユーザー設定")]
        public string Path_UserSettingsFile { get; set; }

        [Description("KeyReplacerの実行ファイルのパスを指定する")]
        [Category("4. KeyReplacer")]
        public string Path_KeyReplacerExeFile { get; set; }

        [Description("SSSの設定")]
        [Category("5. SSS")]
        public string Texts_SSSExecutionPeriods { get; set; }
    }
}
