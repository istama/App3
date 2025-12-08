
using System;
using System.Drawing;
using System.ComponentModel;

using IsTama.Utils;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations
{
    /// <summary>
    /// 年賀アプリケーションとの連携の設定のビューモデル。
    /// </summary>
    class NengaBoosterConfigFormViewModelForApplicationSettings
    {
        [Description("年賀アプリのプロセス名")]
        [Category("1. プロセス名")]
        [NotNullAndEmpty]
        public string ProcessName_NengaMenu { get; set; }
        [Description("注文・名入れ情報入力アプリのプロセス名")]
        [Category("1. プロセス名")]
        [NotNullAndEmpty]
        public string ProcessName_Naire { get; set; }
        [Description("編集アプリのプロセス名")]
        [Category("1. プロセス名")]
        [NotNullAndEmpty]
        public string ProcessName_Hensyu { get; set; }
        [Description("インフォメーション検索アプリのプロセス名")]
        [Category("1. プロセス名")]
        [NotNullAndEmpty]
        public string ProcessName_Information { get; set; }
        [Description("校正紙ダイレクト出力アプリのプロセス名")]
        [Category("1. プロセス名")]
        [NotNullAndEmpty]
        public string ProcessName_Kouseishi { get; set; }
        
        
        [Description("年賀メニュー上の注文・名入れ情報入力アプリを起動するボタンの名前")]
        [Category("2. 年賀メニュー")]
        [NotNullAndEmpty]
        public string ApplicationNameOnNengaMenu_Naire { get; set; }
        [Description("年賀メニュー上の編集アプリを起動するボタンの名前")]
        [Category("2. 年賀メニュー")]
        [NotNullAndEmpty]
        public string ApplicationNameOnNengaMenu_Hensyu { get; set; }
        [Description("年賀メニュー上のインフォメーション検索アプリを起動するボタンの名前")]
        [Category("2. 年賀メニュー")]
        [NotNullAndEmpty]
        public string ApplicationNameOnNengaMenu_Information { get; set; }
        [Description("年賀メニュー上の校正紙ダイレクト出力アプリを起動するボタンの名前")]
        [Category("2. 年賀メニュー")]
        [NotNullAndEmpty]
        public string ApplicationNameOnNengaMenu_Kouseishi { get; set; }
        
        
        [Description("年賀メニューアプリのウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_NengaMenu { get; set; }
        [Description("注文・名入れ情報入力アプリのウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_Naire { get; set; }
        [Description("編集アプリのウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_Hensyu { get; set; }
        [Description("インフォメーション検索アプリのウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_Information { get; set; }
        [Description("インフォメーションの詳細ウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_InformationDetail { get; set; }
        [Description("校正紙ダイレクト出力アプリのウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_Kouseishi { get; set; }
        [Description("年賀アプリのログインフォームのウィンドウタイトル")]
        [Category("3. ウィンドウタイトル")]
        [NotNullAndEmpty]
        public string WindowTitlePattern_LoginForm { get; set; }
        
        
        [Description("年賀メニューアプリのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_NengaMenu { get; set; }
        [Description("注文・名入れ情報入力アプリのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_Naire { get; set; }
        [Description("編集アプリのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_Hensyu { get; set; }
        [Description("インフォメーション検索アプリのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_Information { get; set; }
        [Description("インフォメーション詳細アプリのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_InformationDetail { get; set; }
        [Description("校正紙ダイレクト出力アプリのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_Kouseishi { get; set; }
        [Description("ログインのウィンドウ幅　指定した幅より小さいウィンドウのうち最大のウィンドウを取得する")]
        [Category("4. ウィンドウ幅")]
        public int WindowWidth_LoginForm { get; set; }

        
        [Description("注文・名入れ情報入力アプリのダイアログメッセージのウィンドウタイトル")]
        [Category("5. ダイアログウィンドウタイトル")]
        [NotNullAndEmpty]
        public string DialogTitlePattern_Naire { get; set; }
        [Description("編集アプリのダイアログメッセージのウィンドウタイトル")]
        [Category("5. ダイアログウィンドウタイトル")]
        [NotNullAndEmpty]
        public string DialogTitlePattern_Hensyu { get; set; }
        [Description("インフォメーション検索のダイアログメッセージのウィンドウタイトル")]
        [Category("5. ダイアログウィンドウタイトル")]
        [NotNullAndEmpty]
        public string DialogTitlePattern_Information { get; set; }
        [Description("校正紙ダイレクト出力アプリのダイアログメッセージのウィンドウタイトル")]
        [Category("5. ダイアログウィンドウタイトル")]
        [NotNullAndEmpty]
        public string DialogTitlePattern_Kouseishi { get; set; }
        
        
        [Description("注文・名入れ情報入力アプリのダイアログの幅")]
        [Category("6. ダイアログの幅")]
        public int DialogWidth_Naire { get; set; }
        [Description("編集アプリのダイアログの幅")]
        [Category("6. ダイアログの幅")]
        public int DialogWidth_Hensyu { get; set; }
        [Description("インフォメーション検索のダイアログの幅")]
        [Category("6. ダイアログの幅")]
        public int DialogWidth_Information { get; set; }
        [Description("校正紙ダイレクト出力のダイアログの幅")]
        [Category("6. ダイアログの幅")]
        public int DialogWidth_Kouseishi { get; set; }


        [Description("注文・名入れ情報アプリを構成するコントロール一覧のうち、問番を入力するテキストボックスの座標")]
        [Category("7. 注文・名入れ情報設定")]
        public Point TextBoxPoint_Naire_Toiban { get; set; }
        [Description("データを開くボタンの名前")]
        [Category("7. 注文・名入れ情報設定")]
        public string ButtonName_Naire_Open { get; set; }
        [Description("データを登録するボタンの名前")]
        [Category("7. 注文・名入れ情報設定")]
        public string ButtonName_Naire_Save { get; set; }
        [Description("アプリを閉じるボタンの名前")]
        [Category("7. 注文・名入れ情報設定")]
        public string ButtonName_Naire_Close { get; set; }
        [Description("名入れを更新するラジオボタンの名前")]
        [Category("7. 注文・名入れ情報設定")]
        public string RadioButtonName_Naire_KumihanIrai { get; set; }


        [Description("編集アプリを構成するコントロール一覧のうち、問番を入力するテキストボックスの座標")]
        [Category("8. 編集設定")]
        public Point TextBoxPoint_Hensyu_Toiban { get; set; }
        [Description("データを開くボタンの名前")]
        [Category("8. 編集設定")]
        public string ButtonName_Hensyu_Open { get; set; }
        [Description("名入れ編集画面を開くボタンの名前")]
        [Category("8. 編集設定")]
        public string ButtonName_Hensyu_Tegumi { get; set; }
        [Description("閉じるボタンの名前")]
        [Category("8. 編集設定")]
        public string ButtonName_Hensyu_Close { get; set; }

        
        [Description("インフォメーション検索アプリを構成するコントロール一覧のうち、問番を入力するテキストボックスの座標")]
        [Category("9. インフォメーション検索設定")]
        public Point TextBoxPoint_Information_Toiban { get; set; }
        [Description("データを検索するボタンの名前")]
        [Category("9. インフォメーション検索設定")]
        public string ButtonName_Information_Open { get; set; }
        [Description("インフォメーションを閉じるボタンの名前")]
        [Category("9. インフォメーション検索設定")]
        public string ButtonName_Information_Close { get; set; }
        [Description("詳細画面を開くボタンの名前")]
        [Category("9. インフォメーション検索設定")]
        public string ButtonName_Information_Detail { get; set; }
        
        
        [Description("校正紙画像を表示するボタンの名前")]
        [Category("a. インフォメーション検索設定")]
        public string ButtonName_InformationDetail_KouseiPage { get; set; }
        [Description("組版画像を表示するボタンの名前")]
        [Category("a. インフォメーション検索設定")]
        public string ButtonName_InformationDetail_KumihanPage { get; set; }
        [Description("インフォメーション詳細を閉じるボタンの名前")]
        [Category("a. インフォメーション検索設定")]
        public string ButtonName_InformationDetail_Close { get; set; }


        [Description("校正紙ダイレクト出力アプリを構成するコントロール一覧のうち、問番を入力するテキストボックスのインデックス")]
        [Category("b. 校正紙ダイレクト出力設定")]
        public Point TextBoxPoint_Kouseishi_Toiban { get; set; }
        [Description("閉じるボタンの名前")]
        [Category("b. 校正紙ダイレクト出力設定")]
        public string ButtonName_Kouseishi_Close { get; set; }
        

        [Description("ログインダイアログを構成するコントロール一覧のうち、ユーザー名を入力するテキストボックスの座標")]
        [Category("c. ログイン")]
        public Point TextBoxPoint_LoginForm_UserName { get; set; }
        [Description("ログインダイアログを構成するコントロール一覧のうち、パスワードを入力するテキストボックスの座標")]
        [Category("c. ログイン")]
        public Point TextBoxPoint_LoginForm_Password { get; set; }
        [Description("ログインダイアログを構成するコントロール一覧のうち、OKボタンの名前")]
        [Category("c. ログイン")]
        public string ButtonName_LoginForm_Ok { get; set; }
        

        [Description("ダイアログを構成するコントロール一覧のうち、メッセージが書かれたラベルの座標")]
        [Category("d. ダイアログ")]
        public Point LabelPoint_Dialog_Message { get; set; }
        [Description("OKボタンの名前")]
        [Category("d. ダイアログ")]
        public string ButtonName_Dialog_Ok { get; set; }
        
        [Description("注文・名入れ情報のエラーダイアログに含まれるワード")]
        [Category("d. ダイアログ")]
        public string Texts_NaireDialog_ErrorMessage { get; set; }
        [Description("編集のエラーダイアログに含まれるワード")]
        [Category("d. ダイアログ")]
        public string Texts_HensyuDialog_ErrorMessage { get; set; }
        [Description("インフォメーション検索のエラーダイアログに含まれるワード")]
        [Category("d. ダイアログ")]
        public string Texts_InformationDialog_ErrorMessage { get; set; }
        [Description("校正紙ダイレクト出力のエラーダイアログに含まれるワード")]
        [Category("d. ダイアログ")]
        public string Texts_KouseishiDialog_ErrorMessage { get; set; }

        [Description("工程が先に進んでいる場合にエラーダイアログに含まれるワード")]
        [Category("d. ダイアログ")]
        public string Text_Dialog_MovedForwardWorkProcessMessage { get; set; }
    }
}
