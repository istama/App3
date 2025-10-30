

namespace IsTama.NengaBooster.UI.Main.View
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox TxtBoxToiban;
        private System.Windows.Forms.Button Btn1;
        private System.Windows.Forms.Button Btn2;
        private System.Windows.Forms.Button Btn3;
        private System.Windows.Forms.Button BtnPrintKouseishi;
        private System.Windows.Forms.Button BtnRemoveToiban;
        private System.Windows.Forms.Button BtnClearToibanList;
        private System.Windows.Forms.Button BtnRaiseToiban;
        private System.Windows.Forms.Button BtnLowerToiban;
        private System.Windows.Forms.CheckedListBox ChkListToiban;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_ToibanCheckedList;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EnterSelectedToibanToKouseishi;
        private System.Windows.Forms.Button Btn4;
        private System.Windows.Forms.Label LblCheckedToibanCount;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem MenuOperationModes;
        private System.Windows.Forms.ToolStripMenuItem MenuNaireOpenMode_Normal;
        private System.Windows.Forms.ToolStripMenuItem MenuNaireOpenMode_Saikumi;
        private System.Windows.Forms.ToolStripMenuItem MenuShowUserAccountForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuShowNengaBoosterConfigForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi;
        private System.Windows.Forms.ToolStripMenuItem MenuPrintToibanHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuUnlockOperationMode;
        private System.Windows.Forms.ToolStripMenuItem MenuBarcodeScanOperationMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ReverseCheckStateAll;
    
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TxtBoxToiban = new System.Windows.Forms.TextBox();
            this.ContextMenuStrip_Toiban = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_ToibanSelectMode_ByClick = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_EnterTextBoxToibanToKouseishi = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn1 = new System.Windows.Forms.Button();
            this.Btn2 = new System.Windows.Forms.Button();
            this.ContextMenuStrip_Hensyu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_HensyuOpenMode_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn3 = new System.Windows.Forms.Button();
            this.ContextMenuStrip_Information = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_InformationOpenMode_SearchForm = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_InformationOpenMode_DetailWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_InformationOpenMode_KouseiPage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_InformationOpenMode_KumihanPage = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnPrintKouseishi = new System.Windows.Forms.Button();
            this.ContextMenuStrip_PrintToiban = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnRemoveToiban = new System.Windows.Forms.Button();
            this.BtnClearToibanList = new System.Windows.Forms.Button();
            this.ContextMenuStrip_ClearToibanCheckedList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_ToibanClearMode_All = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ToibanClearMode_UncheckedOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnRaiseToiban = new System.Windows.Forms.Button();
            this.BtnLowerToiban = new System.Windows.Forms.Button();
            this.ChkListToiban = new System.Windows.Forms.CheckedListBox();
            this.ContextMenuStrip_ToibanCheckedList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_EnterSelectedToibanToKouseishi = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_ReverseCheckStateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn4 = new System.Windows.Forms.Button();
            this.LblCheckedToibanCount = new System.Windows.Forms.Label();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOperationModes = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNaireOpenMode_Normal = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNaireOpenMode_Saikumi = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaikumiByOutputListOperationMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnlockOperationMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBarcodeScanOperationMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuPrintToibanHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuSaveOutputToibanList = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLoadOutputToibanList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuShowUserAccountForm = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuShowUserConfigForm = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuShortcutKeySetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenShortcutKeySettingFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSelectShortcutKeySettingFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReloadShortcutKeySetting = new System.Windows.Forms.ToolStripMenuItem();
            this.再起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuShowNengaBoosterConfigForm = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnStartScreenSaverStopper = new System.Windows.Forms.Button();
            this.ContextMenuStrip_Kyokudome = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.依頼主都道府県住所電話番号別住所住所ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.依頼主都道府県住所別住所住所ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.依頼主住所電話番号別住所住所ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.依頼主住所別住所住所ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.依頼主姓名別住所名前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.名入れToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.名入れ住所依頼主住所ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.依頼主姓名名入れ姓名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelLeftCtrlSignal = new System.Windows.Forms.Label();
            this.LabelLeftShiftSignal = new System.Windows.Forms.Label();
            this.LabelLeftAltSignal = new System.Windows.Forms.Label();
            this.LabelRightAltSignal = new System.Windows.Forms.Label();
            this.LabelRightShiftSignal = new System.Windows.Forms.Label();
            this.LabelRightCtrlSignal = new System.Windows.Forms.Label();
            this.BtnStopScreenSaverStopper = new System.Windows.Forms.Button();
            this.ContextMenuStrip_Toiban.SuspendLayout();
            this.ContextMenuStrip_Hensyu.SuspendLayout();
            this.ContextMenuStrip_Information.SuspendLayout();
            this.ContextMenuStrip_PrintToiban.SuspendLayout();
            this.ContextMenuStrip_ClearToibanCheckedList.SuspendLayout();
            this.ContextMenuStrip_ToibanCheckedList.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.ContextMenuStrip_Kyokudome.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtBoxToiban
            // 
            this.TxtBoxToiban.BackColor = System.Drawing.SystemColors.Info;
            this.TxtBoxToiban.ContextMenuStrip = this.ContextMenuStrip_Toiban;
            this.TxtBoxToiban.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBoxToiban.Location = new System.Drawing.Point(14, 32);
            this.TxtBoxToiban.MaxLength = 10;
            this.TxtBoxToiban.Name = "TxtBoxToiban";
            this.TxtBoxToiban.Size = new System.Drawing.Size(162, 28);
            this.TxtBoxToiban.TabIndex = 0;
            this.TxtBoxToiban.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBoxToiban_KeyPress);
            // 
            // ContextMenuStrip_Toiban
            // 
            this.ContextMenuStrip_Toiban.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ToibanSelectMode_ByClick,
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick,
            this.toolStripSeparator6,
            this.ToolStripMenuItem_EnterTextBoxToibanToKouseishi});
            this.ContextMenuStrip_Toiban.Name = "ContextMenuStrip_Toiban";
            this.ContextMenuStrip_Toiban.Size = new System.Drawing.Size(239, 76);
            // 
            // ToolStripMenuItem_ToibanSelectMode_ByClick
            // 
            this.ToolStripMenuItem_ToibanSelectMode_ByClick.Name = "ToolStripMenuItem_ToibanSelectMode_ByClick";
            this.ToolStripMenuItem_ToibanSelectMode_ByClick.Size = new System.Drawing.Size(238, 22);
            this.ToolStripMenuItem_ToibanSelectMode_ByClick.Text = "クリックで問番を選択状態にする";
            this.ToolStripMenuItem_ToibanSelectMode_ByClick.Click += new System.EventHandler(this.MenuItemToibanSelectModeByClick_Click);
            // 
            // ToolStripMenuItem_ToibanSelectMode_ByWClick
            // 
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick.Checked = true;
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick.Name = "ToolStripMenuItem_ToibanSelectMode_ByWClick";
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick.Size = new System.Drawing.Size(238, 22);
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick.Text = "Wクリックで問番を選択状態にする";
            this.ToolStripMenuItem_ToibanSelectMode_ByWClick.Click += new System.EventHandler(this.MenuItemToibanSelectModeByWClick_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(235, 6);
            // 
            // ToolStripMenuItem_EnterTextBoxToibanToKouseishi
            // 
            this.ToolStripMenuItem_EnterTextBoxToibanToKouseishi.Name = "ToolStripMenuItem_EnterTextBoxToibanToKouseishi";
            this.ToolStripMenuItem_EnterTextBoxToibanToKouseishi.Size = new System.Drawing.Size(238, 22);
            this.ToolStripMenuItem_EnterTextBoxToibanToKouseishi.Text = "問番を校正紙出力へ";
            this.ToolStripMenuItem_EnterTextBoxToibanToKouseishi.Click += new System.EventHandler(this.ToolStripMenuItemSendToibanToKouseishi_Click);
            // 
            // Btn1
            // 
            this.Btn1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Btn1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Btn1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn1.Location = new System.Drawing.Point(13, 67);
            this.Btn1.Name = "Btn1";
            this.Btn1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn1.Size = new System.Drawing.Size(164, 33);
            this.Btn1.TabIndex = 1;
            this.Btn1.Text = "注文・名入れ情報";
            this.Btn1.UseVisualStyleBackColor = false;
            this.Btn1.Click += new System.EventHandler(this.Btn1Click);
            // 
            // Btn2
            // 
            this.Btn2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Btn2.ContextMenuStrip = this.ContextMenuStrip_Hensyu;
            this.Btn2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Btn2.Location = new System.Drawing.Point(13, 103);
            this.Btn2.Name = "Btn2";
            this.Btn2.Size = new System.Drawing.Size(164, 33);
            this.Btn2.TabIndex = 2;
            this.Btn2.Text = "編集";
            this.Btn2.UseVisualStyleBackColor = false;
            this.Btn2.Click += new System.EventHandler(this.Btn2Click);
            // 
            // ContextMenuStrip_Hensyu
            // 
            this.ContextMenuStrip_Hensyu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_HensyuOpenMode_Menu,
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi});
            this.ContextMenuStrip_Hensyu.Name = "ContextMenuStrip_Hensyu";
            this.ContextMenuStrip_Hensyu.Size = new System.Drawing.Size(237, 48);
            // 
            // ToolStripMenuItem_HensyuOpenMode_Menu
            // 
            this.ToolStripMenuItem_HensyuOpenMode_Menu.Name = "ToolStripMenuItem_HensyuOpenMode_Menu";
            this.ToolStripMenuItem_HensyuOpenMode_Menu.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuItem_HensyuOpenMode_Menu.Text = "編集項目の選択ウィンドウまで開く";
            this.ToolStripMenuItem_HensyuOpenMode_Menu.Click += new System.EventHandler(this.MenuItemHensyuOpenModeMenu_Click);
            // 
            // ToolStripMenuItem_HensyuOpenMode_Tegumi
            // 
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi.Checked = true;
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi.Name = "ToolStripMenuItem_HensyuOpenMode_Tegumi";
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi.Text = "手組はがき編集ウィンドウまで開く";
            this.ToolStripMenuItem_HensyuOpenMode_Tegumi.Click += new System.EventHandler(this.MenuItemHensyuOpenModeTegumi_Click);
            // 
            // Btn3
            // 
            this.Btn3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Btn3.ContextMenuStrip = this.ContextMenuStrip_Information;
            this.Btn3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Btn3.Location = new System.Drawing.Point(13, 139);
            this.Btn3.Name = "Btn3";
            this.Btn3.Size = new System.Drawing.Size(164, 33);
            this.Btn3.TabIndex = 3;
            this.Btn3.Text = "インフォメーション検索";
            this.Btn3.UseVisualStyleBackColor = false;
            this.Btn3.Click += new System.EventHandler(this.Btn3Click);
            // 
            // ContextMenuStrip_Information
            // 
            this.ContextMenuStrip_Information.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen,
            this.toolStripSeparator7,
            this.ToolStripMenuItem_InformationOpenMode_SearchForm,
            this.ToolStripMenuItem_InformationOpenMode_DetailWindow,
            this.ToolStripMenuItem_InformationOpenMode_KouseiPage,
            this.ToolStripMenuItem_InformationOpenMode_KumihanPage});
            this.ContextMenuStrip_Information.Name = "ContextMenuStrip_Information";
            this.ContextMenuStrip_Information.Size = new System.Drawing.Size(209, 120);
            // 
            // ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen
            // 
            this.ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen.Name = "ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen";
            this.ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen.Size = new System.Drawing.Size(208, 22);
            this.ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen.Text = "出力リストに問番を追加する";
            this.ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen.Click += new System.EventHandler(this.MenuItemShouldAddToibanToCheckedListWhenInformationOpen);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // ToolStripMenuItem_InformationOpenMode_SearchForm
            // 
            this.ToolStripMenuItem_InformationOpenMode_SearchForm.Checked = true;
            this.ToolStripMenuItem_InformationOpenMode_SearchForm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_InformationOpenMode_SearchForm.Name = "ToolStripMenuItem_InformationOpenMode_SearchForm";
            this.ToolStripMenuItem_InformationOpenMode_SearchForm.Size = new System.Drawing.Size(208, 22);
            this.ToolStripMenuItem_InformationOpenMode_SearchForm.Text = "検索結果まで開く";
            this.ToolStripMenuItem_InformationOpenMode_SearchForm.Click += new System.EventHandler(this.MenuItemInformationOpenModeSearchForm_Click);
            // 
            // ToolStripMenuItem_InformationOpenMode_DetailWindow
            // 
            this.ToolStripMenuItem_InformationOpenMode_DetailWindow.Name = "ToolStripMenuItem_InformationOpenMode_DetailWindow";
            this.ToolStripMenuItem_InformationOpenMode_DetailWindow.Size = new System.Drawing.Size(208, 22);
            this.ToolStripMenuItem_InformationOpenMode_DetailWindow.Text = "詳細画面まで開く";
            this.ToolStripMenuItem_InformationOpenMode_DetailWindow.Click += new System.EventHandler(this.MenuItemInformationOpenModeDetailWindow_Click);
            // 
            // ToolStripMenuItem_InformationOpenMode_KouseiPage
            // 
            this.ToolStripMenuItem_InformationOpenMode_KouseiPage.Name = "ToolStripMenuItem_InformationOpenMode_KouseiPage";
            this.ToolStripMenuItem_InformationOpenMode_KouseiPage.Size = new System.Drawing.Size(208, 22);
            this.ToolStripMenuItem_InformationOpenMode_KouseiPage.Text = "校正まで開く";
            this.ToolStripMenuItem_InformationOpenMode_KouseiPage.Click += new System.EventHandler(this.MenuItemInformationOpenModeKouseiPage_Click);
            // 
            // ToolStripMenuItem_InformationOpenMode_KumihanPage
            // 
            this.ToolStripMenuItem_InformationOpenMode_KumihanPage.Name = "ToolStripMenuItem_InformationOpenMode_KumihanPage";
            this.ToolStripMenuItem_InformationOpenMode_KumihanPage.Size = new System.Drawing.Size(208, 22);
            this.ToolStripMenuItem_InformationOpenMode_KumihanPage.Text = "組版まで開く";
            this.ToolStripMenuItem_InformationOpenMode_KumihanPage.Click += new System.EventHandler(this.MenuItemInformationOpenModeKumihanPage_Click);
            // 
            // BtnPrintKouseishi
            // 
            this.BtnPrintKouseishi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrintKouseishi.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnPrintKouseishi.ContextMenuStrip = this.ContextMenuStrip_PrintToiban;
            this.BtnPrintKouseishi.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BtnPrintKouseishi.Location = new System.Drawing.Point(12, 490);
            this.BtnPrintKouseishi.Name = "BtnPrintKouseishi";
            this.BtnPrintKouseishi.Size = new System.Drawing.Size(50, 23);
            this.BtnPrintKouseishi.TabIndex = 15;
            this.BtnPrintKouseishi.Text = "出力";
            this.BtnPrintKouseishi.UseVisualStyleBackColor = false;
            this.BtnPrintKouseishi.Click += new System.EventHandler(this.BtnPrintKouseishi_Click);
            // 
            // ContextMenuStrip_PrintToiban
            // 
            this.ContextMenuStrip_PrintToiban.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi});
            this.ContextMenuStrip_PrintToiban.Name = "contextMenuStrip1";
            this.ContextMenuStrip_PrintToiban.Size = new System.Drawing.Size(214, 26);
            // 
            // ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi
            // 
            this.ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi.Name = "ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi";
            this.ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi.Size = new System.Drawing.Size(213, 22);
            this.ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi.Text = "出力した問番のチェックを外す";
            this.ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi.Click += new System.EventHandler(this.MenuItemShouldUncheckToibanFromCheckedListWhenPrintKouseishi_Click);
            // 
            // BtnRemoveToiban
            // 
            this.BtnRemoveToiban.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnRemoveToiban.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnRemoveToiban.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BtnRemoveToiban.Location = new System.Drawing.Point(68, 490);
            this.BtnRemoveToiban.Name = "BtnRemoveToiban";
            this.BtnRemoveToiban.Size = new System.Drawing.Size(50, 23);
            this.BtnRemoveToiban.TabIndex = 16;
            this.BtnRemoveToiban.Text = "削除";
            this.BtnRemoveToiban.UseVisualStyleBackColor = false;
            this.BtnRemoveToiban.Click += new System.EventHandler(this.BtnRemoveToiban_Click);
            // 
            // BtnClearToibanList
            // 
            this.BtnClearToibanList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnClearToibanList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnClearToibanList.ContextMenuStrip = this.ContextMenuStrip_ClearToibanCheckedList;
            this.BtnClearToibanList.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BtnClearToibanList.Location = new System.Drawing.Point(124, 490);
            this.BtnClearToibanList.Name = "BtnClearToibanList";
            this.BtnClearToibanList.Size = new System.Drawing.Size(50, 23);
            this.BtnClearToibanList.TabIndex = 17;
            this.BtnClearToibanList.Text = "クリア";
            this.BtnClearToibanList.UseVisualStyleBackColor = false;
            this.BtnClearToibanList.Click += new System.EventHandler(this.BtnClearToibanCheckedList_Click);
            // 
            // ContextMenuStrip_ClearToibanCheckedList
            // 
            this.ContextMenuStrip_ClearToibanCheckedList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ToibanClearMode_All,
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly,
            this.ToolStripMenuItem_ToibanClearMode_UncheckedOnly});
            this.ContextMenuStrip_ClearToibanCheckedList.Name = "ContextMenuStrip_ClearOutputToibanList";
            this.ContextMenuStrip_ClearToibanCheckedList.Size = new System.Drawing.Size(257, 70);
            // 
            // ToolStripMenuItem_ToibanClearMode_All
            // 
            this.ToolStripMenuItem_ToibanClearMode_All.Name = "ToolStripMenuItem_ToibanClearMode_All";
            this.ToolStripMenuItem_ToibanClearMode_All.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_ToibanClearMode_All.Text = "すべての問番を削除する";
            this.ToolStripMenuItem_ToibanClearMode_All.Click += new System.EventHandler(this.MenuItemToibanCheckedListClearModeAll_Click);
            // 
            // ToolStripMenuItem_ToibanClearMode_CheckedOnly
            // 
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly.Checked = true;
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly.Name = "ToolStripMenuItem_ToibanClearMode_CheckedOnly";
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly.Text = "チェックの付いた問番のみ削除する";
            this.ToolStripMenuItem_ToibanClearMode_CheckedOnly.Click += new System.EventHandler(this.MenuItemToibanCheckedListClearModeCheckedOnly_Click);
            // 
            // ToolStripMenuItem_ToibanClearMode_UncheckedOnly
            // 
            this.ToolStripMenuItem_ToibanClearMode_UncheckedOnly.Name = "ToolStripMenuItem_ToibanClearMode_UncheckedOnly";
            this.ToolStripMenuItem_ToibanClearMode_UncheckedOnly.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_ToibanClearMode_UncheckedOnly.Text = "チェックの付いてない問番のみ削除する";
            this.ToolStripMenuItem_ToibanClearMode_UncheckedOnly.Click += new System.EventHandler(this.MenuItemToibanCheckedListClearModeUncheckedOnly_Click);
            // 
            // BtnRaiseToiban
            // 
            this.BtnRaiseToiban.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnRaiseToiban.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnRaiseToiban.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BtnRaiseToiban.Location = new System.Drawing.Point(13, 462);
            this.BtnRaiseToiban.Name = "BtnRaiseToiban";
            this.BtnRaiseToiban.Size = new System.Drawing.Size(78, 23);
            this.BtnRaiseToiban.TabIndex = 10;
            this.BtnRaiseToiban.Text = "↑";
            this.BtnRaiseToiban.UseVisualStyleBackColor = false;
            this.BtnRaiseToiban.Click += new System.EventHandler(this.BtnRaiseToiban_Click);
            // 
            // BtnLowerToiban
            // 
            this.BtnLowerToiban.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnLowerToiban.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnLowerToiban.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.BtnLowerToiban.Location = new System.Drawing.Point(96, 462);
            this.BtnLowerToiban.Name = "BtnLowerToiban";
            this.BtnLowerToiban.Size = new System.Drawing.Size(78, 23);
            this.BtnLowerToiban.TabIndex = 11;
            this.BtnLowerToiban.Text = "↓";
            this.BtnLowerToiban.UseVisualStyleBackColor = false;
            this.BtnLowerToiban.Click += new System.EventHandler(this.BtnLowerToiban_Click);
            // 
            // ChkListToiban
            // 
            this.ChkListToiban.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkListToiban.ContextMenuStrip = this.ContextMenuStrip_ToibanCheckedList;
            this.ChkListToiban.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ChkListToiban.FormattingEnabled = true;
            this.ChkListToiban.Location = new System.Drawing.Point(14, 217);
            this.ChkListToiban.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ChkListToiban.Name = "ChkListToiban";
            this.ChkListToiban.ScrollAlwaysVisible = true;
            this.ChkListToiban.Size = new System.Drawing.Size(162, 200);
            this.ChkListToiban.TabIndex = 8;
            this.ChkListToiban.SelectedIndexChanged += new System.EventHandler(this.ChkListToiban_SelectedIndexChanged);
            // 
            // ContextMenuStrip_ToibanCheckedList
            // 
            this.ContextMenuStrip_ToibanCheckedList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_EnterSelectedToibanToKouseishi,
            this.ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi,
            this.toolStripSeparator4,
            this.ToolStripMenuItem_ReverseCheckStateAll});
            this.ContextMenuStrip_ToibanCheckedList.Name = "contextMenuStrip1";
            this.ContextMenuStrip_ToibanCheckedList.Size = new System.Drawing.Size(227, 76);
            this.ContextMenuStrip_ToibanCheckedList.Text = "この問番から下を校正紙出力へ";
            // 
            // ToolStripMenuItem_EnterSelectedToibanToKouseishi
            // 
            this.ToolStripMenuItem_EnterSelectedToibanToKouseishi.Name = "ToolStripMenuItem_EnterSelectedToibanToKouseishi";
            this.ToolStripMenuItem_EnterSelectedToibanToKouseishi.Size = new System.Drawing.Size(226, 22);
            this.ToolStripMenuItem_EnterSelectedToibanToKouseishi.Text = "この問番のみ校正紙出力へ";
            this.ToolStripMenuItem_EnterSelectedToibanToKouseishi.Click += new System.EventHandler(this.MenuItemEnterSelectedToibanToKouseishi_Click);
            // 
            // ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi
            // 
            this.ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi.Name = "ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi";
            this.ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi.Size = new System.Drawing.Size(226, 22);
            this.ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi.Text = "この問番から下を校正紙出力へ";
            this.ToolStripMenuItem_EnterToibanListBelowSelectedToKouseishi.Click += new System.EventHandler(this.MenuItemEnterToibanListBelowSelectedToKouseishi_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(223, 6);
            // 
            // ToolStripMenuItem_ReverseCheckStateAll
            // 
            this.ToolStripMenuItem_ReverseCheckStateAll.Name = "ToolStripMenuItem_ReverseCheckStateAll";
            this.ToolStripMenuItem_ReverseCheckStateAll.Size = new System.Drawing.Size(226, 22);
            this.ToolStripMenuItem_ReverseCheckStateAll.Text = "チェック状態をすべて反転させる";
            this.ToolStripMenuItem_ReverseCheckStateAll.Click += new System.EventHandler(this.MenuItemReverseCheckStateAll_Click);
            // 
            // Btn4
            // 
            this.Btn4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Btn4.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Btn4.Location = new System.Drawing.Point(13, 175);
            this.Btn4.Name = "Btn4";
            this.Btn4.Size = new System.Drawing.Size(164, 33);
            this.Btn4.TabIndex = 4;
            this.Btn4.Text = "出力リストへ";
            this.Btn4.UseVisualStyleBackColor = false;
            this.Btn4.Click += new System.EventHandler(this.Btn4Click);
            // 
            // LblCheckedToibanCount
            // 
            this.LblCheckedToibanCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblCheckedToibanCount.AutoSize = true;
            this.LblCheckedToibanCount.Location = new System.Drawing.Point(145, 444);
            this.LblCheckedToibanCount.Name = "LblCheckedToibanCount";
            this.LblCheckedToibanCount.Size = new System.Drawing.Size(17, 12);
            this.LblCheckedToibanCount.TabIndex = 11;
            this.LblCheckedToibanCount.Text = "00";
            this.LblCheckedToibanCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MenuBar
            // 
            this.MenuBar.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions});
            this.MenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(191, 26);
            this.MenuBar.TabIndex = 12;
            this.MenuBar.Text = "menuStrip1";
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOperationModes,
            this.toolStripSeparator3,
            this.MenuPrintToibanHistory,
            this.toolStripSeparator2,
            this.MenuSaveOutputToibanList,
            this.MenuLoadOutputToibanList,
            this.toolStripSeparator1,
            this.MenuShowUserAccountForm,
            this.MenuShowUserConfigForm,
            this.MenuShortcutKeySetting,
            this.MenuShowNengaBoosterConfigForm});
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(99, 22);
            this.menuOptions.Text = "オプション(O)";
            // 
            // MenuOperationModes
            // 
            this.MenuOperationModes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNaireOpenMode_Normal,
            this.MenuNaireOpenMode_Saikumi,
            this.MenuSaikumiByOutputListOperationMode,
            this.MenuUnlockOperationMode,
            this.toolStripSeparator5,
            this.MenuBarcodeScanOperationMode});
            this.MenuOperationModes.Name = "MenuOperationModes";
            this.MenuOperationModes.Size = new System.Drawing.Size(244, 22);
            this.MenuOperationModes.Text = "動作モード";
            // 
            // MenuNaireOpenMode_Normal
            // 
            this.MenuNaireOpenMode_Normal.Checked = true;
            this.MenuNaireOpenMode_Normal.CheckOnClick = true;
            this.MenuNaireOpenMode_Normal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuNaireOpenMode_Normal.Name = "MenuNaireOpenMode_Normal";
            this.MenuNaireOpenMode_Normal.Size = new System.Drawing.Size(244, 22);
            this.MenuNaireOpenMode_Normal.Text = "通常モード";
            this.MenuNaireOpenMode_Normal.Click += new System.EventHandler(this.MenuNaireOpenModeNormal_Click);
            // 
            // MenuNaireOpenMode_Saikumi
            // 
            this.MenuNaireOpenMode_Saikumi.CheckOnClick = true;
            this.MenuNaireOpenMode_Saikumi.Name = "MenuNaireOpenMode_Saikumi";
            this.MenuNaireOpenMode_Saikumi.Size = new System.Drawing.Size(244, 22);
            this.MenuNaireOpenMode_Saikumi.Text = "再組版モード";
            this.MenuNaireOpenMode_Saikumi.Click += new System.EventHandler(this.MenuNaireOpenModeSaikumi_Click);
            // 
            // MenuSaikumiByOutputListOperationMode
            // 
            this.MenuSaikumiByOutputListOperationMode.Enabled = false;
            this.MenuSaikumiByOutputListOperationMode.Name = "MenuSaikumiByOutputListOperationMode";
            this.MenuSaikumiByOutputListOperationMode.Size = new System.Drawing.Size(244, 22);
            this.MenuSaikumiByOutputListOperationMode.Text = "再組版モード（出力リスト用）";
            this.MenuSaikumiByOutputListOperationMode.Visible = false;
            // 
            // MenuUnlockOperationMode
            // 
            this.MenuUnlockOperationMode.Enabled = false;
            this.MenuUnlockOperationMode.Name = "MenuUnlockOperationMode";
            this.MenuUnlockOperationMode.Size = new System.Drawing.Size(244, 22);
            this.MenuUnlockOperationMode.Text = "ロック解除モード";
            this.MenuUnlockOperationMode.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(241, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // MenuBarcodeScanOperationMode
            // 
            this.MenuBarcodeScanOperationMode.Enabled = false;
            this.MenuBarcodeScanOperationMode.Name = "MenuBarcodeScanOperationMode";
            this.MenuBarcodeScanOperationMode.Size = new System.Drawing.Size(244, 22);
            this.MenuBarcodeScanOperationMode.Text = "問番スキャン";
            this.MenuBarcodeScanOperationMode.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(241, 6);
            // 
            // MenuPrintToibanHistory
            // 
            this.MenuPrintToibanHistory.Enabled = false;
            this.MenuPrintToibanHistory.Name = "MenuPrintToibanHistory";
            this.MenuPrintToibanHistory.Size = new System.Drawing.Size(244, 22);
            this.MenuPrintToibanHistory.Text = "出力履歴";
            this.MenuPrintToibanHistory.Visible = false;
            this.MenuPrintToibanHistory.Click += new System.EventHandler(this.MenuPrintToibanHistory_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(241, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // MenuSaveOutputToibanList
            // 
            this.MenuSaveOutputToibanList.Enabled = false;
            this.MenuSaveOutputToibanList.Name = "MenuSaveOutputToibanList";
            this.MenuSaveOutputToibanList.Size = new System.Drawing.Size(244, 22);
            this.MenuSaveOutputToibanList.Text = "出力リストの保存";
            this.MenuSaveOutputToibanList.Visible = false;
            this.MenuSaveOutputToibanList.Click += new System.EventHandler(this.MenuSaveToibanCheckedList_Click);
            // 
            // MenuLoadOutputToibanList
            // 
            this.MenuLoadOutputToibanList.Enabled = false;
            this.MenuLoadOutputToibanList.Name = "MenuLoadOutputToibanList";
            this.MenuLoadOutputToibanList.Size = new System.Drawing.Size(244, 22);
            this.MenuLoadOutputToibanList.Text = "出力リストの読込";
            this.MenuLoadOutputToibanList.Visible = false;
            this.MenuLoadOutputToibanList.Click += new System.EventHandler(this.MenuLoadToibanCheckedList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(241, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // MenuShowUserAccountForm
            // 
            this.MenuShowUserAccountForm.Name = "MenuShowUserAccountForm";
            this.MenuShowUserAccountForm.Size = new System.Drawing.Size(244, 22);
            this.MenuShowUserAccountForm.Text = "ユーザー名とパスワードの登録";
            this.MenuShowUserAccountForm.Click += new System.EventHandler(this.MenuRegisterUserAccount_Click);
            // 
            // MenuShowUserConfigForm
            // 
            this.MenuShowUserConfigForm.Name = "MenuShowUserConfigForm";
            this.MenuShowUserConfigForm.Size = new System.Drawing.Size(244, 22);
            this.MenuShowUserConfigForm.Text = "ユーザー設定";
            this.MenuShowUserConfigForm.Click += new System.EventHandler(this.MenuShowUserConfigForm_Click);
            // 
            // MenuShortcutKeySetting
            // 
            this.MenuShortcutKeySetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpenShortcutKeySettingFile,
            this.MenuSelectShortcutKeySettingFile,
            this.MenuReloadShortcutKeySetting,
            this.再起動ToolStripMenuItem});
            this.MenuShortcutKeySetting.Enabled = false;
            this.MenuShortcutKeySetting.Name = "MenuShortcutKeySetting";
            this.MenuShortcutKeySetting.Size = new System.Drawing.Size(244, 22);
            this.MenuShortcutKeySetting.Text = "ショートカットキーの設定";
            this.MenuShortcutKeySetting.Visible = false;
            // 
            // MenuOpenShortcutKeySettingFile
            // 
            this.MenuOpenShortcutKeySettingFile.Name = "MenuOpenShortcutKeySettingFile";
            this.MenuOpenShortcutKeySettingFile.Size = new System.Drawing.Size(220, 22);
            this.MenuOpenShortcutKeySettingFile.Text = "設定ファイルを開く";
            this.MenuOpenShortcutKeySettingFile.Visible = false;
            this.MenuOpenShortcutKeySettingFile.Click += new System.EventHandler(this.MenuOpenShortcutKeySettingFile_Click);
            // 
            // MenuSelectShortcutKeySettingFile
            // 
            this.MenuSelectShortcutKeySettingFile.Name = "MenuSelectShortcutKeySettingFile";
            this.MenuSelectShortcutKeySettingFile.Size = new System.Drawing.Size(220, 22);
            this.MenuSelectShortcutKeySettingFile.Text = "設定ファイルを変更する：";
            this.MenuSelectShortcutKeySettingFile.Visible = false;
            this.MenuSelectShortcutKeySettingFile.Click += new System.EventHandler(this.MenuSelectShortcutKeySettingFile_Click);
            // 
            // MenuReloadShortcutKeySetting
            // 
            this.MenuReloadShortcutKeySetting.Name = "MenuReloadShortcutKeySetting";
            this.MenuReloadShortcutKeySetting.Size = new System.Drawing.Size(220, 22);
            this.MenuReloadShortcutKeySetting.Text = "設定をリロードする";
            this.MenuReloadShortcutKeySetting.Visible = false;
            this.MenuReloadShortcutKeySetting.Click += new System.EventHandler(this.MenuReloadShortcutKeySetting_Click);
            // 
            // 再起動ToolStripMenuItem
            // 
            this.再起動ToolStripMenuItem.Name = "再起動ToolStripMenuItem";
            this.再起動ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.再起動ToolStripMenuItem.Text = "再起動";
            this.再起動ToolStripMenuItem.Click += new System.EventHandler(this.MenuRestartKeyReplacer_Click);
            // 
            // MenuShowNengaBoosterConfigForm
            // 
            this.MenuShowNengaBoosterConfigForm.Name = "MenuShowNengaBoosterConfigForm";
            this.MenuShowNengaBoosterConfigForm.Size = new System.Drawing.Size(244, 22);
            this.MenuShowNengaBoosterConfigForm.Text = "プロパティ";
            this.MenuShowNengaBoosterConfigForm.Click += new System.EventHandler(this.MenuShowNengaBoosterConfigForm_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BtnStartScreenSaverStopper
            // 
            this.BtnStartScreenSaverStopper.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BtnStartScreenSaverStopper.Location = new System.Drawing.Point(217, 67);
            this.BtnStartScreenSaverStopper.Name = "BtnStartScreenSaverStopper";
            this.BtnStartScreenSaverStopper.Size = new System.Drawing.Size(82, 33);
            this.BtnStartScreenSaverStopper.TabIndex = 18;
            this.BtnStartScreenSaverStopper.Text = "SSS起動";
            this.BtnStartScreenSaverStopper.UseVisualStyleBackColor = false;
            this.BtnStartScreenSaverStopper.Click += new System.EventHandler(this.BtnStopScreenSaver_Click);
            // 
            // ContextMenuStrip_Kyokudome
            // 
            this.ContextMenuStrip_Kyokudome.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContextMenuStrip_Kyokudome.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.依頼主都道府県住所電話番号別住所住所ToolStripMenuItem,
            this.依頼主都道府県住所別住所住所ToolStripMenuItem,
            this.依頼主住所電話番号別住所住所ToolStripMenuItem,
            this.依頼主住所別住所住所ToolStripMenuItem,
            this.依頼主姓名別住所名前ToolStripMenuItem,
            this.toolStripSeparator8,
            this.toolStripMenuItem1,
            this.名入れToolStripMenuItem,
            this.toolStripMenuItem2,
            this.名入れ住所依頼主住所ToolStripMenuItem,
            this.toolStripSeparator9,
            this.toolStripMenuItem3,
            this.依頼主姓名名入れ姓名ToolStripMenuItem,
            this.toolStripSeparator10,
            this.閉じるToolStripMenuItem});
            this.ContextMenuStrip_Kyokudome.Name = "ContextMenu_Kyokudome";
            this.ContextMenuStrip_Kyokudome.Size = new System.Drawing.Size(389, 286);
            // 
            // 依頼主都道府県住所電話番号別住所住所ToolStripMenuItem
            // 
            this.依頼主都道府県住所電話番号別住所住所ToolStripMenuItem.Name = "依頼主都道府県住所電話番号別住所住所ToolStripMenuItem";
            this.依頼主都道府県住所電話番号別住所住所ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.依頼主都道府県住所電話番号別住所住所ToolStripMenuItem.Text = "依頼主（都道府県・住所・電話番号）→ 別住所 住所";
            // 
            // 依頼主都道府県住所別住所住所ToolStripMenuItem
            // 
            this.依頼主都道府県住所別住所住所ToolStripMenuItem.Name = "依頼主都道府県住所別住所住所ToolStripMenuItem";
            this.依頼主都道府県住所別住所住所ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.依頼主都道府県住所別住所住所ToolStripMenuItem.Text = "依頼主（都道府県・住所　　　　　）→ 別住所 住所";
            // 
            // 依頼主住所電話番号別住所住所ToolStripMenuItem
            // 
            this.依頼主住所電話番号別住所住所ToolStripMenuItem.Name = "依頼主住所電話番号別住所住所ToolStripMenuItem";
            this.依頼主住所電話番号別住所住所ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.依頼主住所電話番号別住所住所ToolStripMenuItem.Text = "依頼主（　　　　　住所・電話番号）→ 別住所 住所";
            // 
            // 依頼主住所別住所住所ToolStripMenuItem
            // 
            this.依頼主住所別住所住所ToolStripMenuItem.Name = "依頼主住所別住所住所ToolStripMenuItem";
            this.依頼主住所別住所住所ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.依頼主住所別住所住所ToolStripMenuItem.Text = "依頼主（　　　　　住所　　　　　）→ 別住所 住所";
            // 
            // 依頼主姓名別住所名前ToolStripMenuItem
            // 
            this.依頼主姓名別住所名前ToolStripMenuItem.Name = "依頼主姓名別住所名前ToolStripMenuItem";
            this.依頼主姓名別住所名前ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.依頼主姓名別住所名前ToolStripMenuItem.Text = "依頼主　姓名　　　　　　　　　　　→ 別住所 名前";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(385, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(388, 22);
            this.toolStripMenuItem1.Text = "名入れ（都道府県・住所）電話番号 → 依頼主 住所・電話";
            // 
            // 名入れToolStripMenuItem
            // 
            this.名入れToolStripMenuItem.Name = "名入れToolStripMenuItem";
            this.名入れToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.名入れToolStripMenuItem.Text = "名入れ（都道府県・住所）　　　　 → 依頼主 住所";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(388, 22);
            this.toolStripMenuItem2.Text = "名入れ（　　　　　住所）電話番号 → 依頼主 住所・電話";
            // 
            // 名入れ住所依頼主住所ToolStripMenuItem
            // 
            this.名入れ住所依頼主住所ToolStripMenuItem.Name = "名入れ住所依頼主住所ToolStripMenuItem";
            this.名入れ住所依頼主住所ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.名入れ住所依頼主住所ToolStripMenuItem.Text = "名入れ（　　　　　住所）　　　　 → 依頼主 住所";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(385, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(388, 22);
            this.toolStripMenuItem3.Text = "依頼主 住所 → 名入れ 住所１";
            // 
            // 依頼主姓名名入れ姓名ToolStripMenuItem
            // 
            this.依頼主姓名名入れ姓名ToolStripMenuItem.Name = "依頼主姓名名入れ姓名ToolStripMenuItem";
            this.依頼主姓名名入れ姓名ToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.依頼主姓名名入れ姓名ToolStripMenuItem.Text = "依頼主 姓名 → 名入れ 姓名１";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(385, 6);
            // 
            // 閉じるToolStripMenuItem
            // 
            this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
            this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.閉じるToolStripMenuItem.Text = "閉じる";
            // 
            // LabelLeftCtrlSignal
            // 
            this.LabelLeftCtrlSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelLeftCtrlSignal.AutoSize = true;
            this.LabelLeftCtrlSignal.BackColor = System.Drawing.Color.Teal;
            this.LabelLeftCtrlSignal.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelLeftCtrlSignal.Location = new System.Drawing.Point(13, 520);
            this.LabelLeftCtrlSignal.Name = "LabelLeftCtrlSignal";
            this.LabelLeftCtrlSignal.Padding = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.LabelLeftCtrlSignal.Size = new System.Drawing.Size(18, 16);
            this.LabelLeftCtrlSignal.TabIndex = 19;
            this.LabelLeftCtrlSignal.Text = "C";
            // 
            // LabelLeftShiftSignal
            // 
            this.LabelLeftShiftSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelLeftShiftSignal.AutoSize = true;
            this.LabelLeftShiftSignal.BackColor = System.Drawing.Color.Teal;
            this.LabelLeftShiftSignal.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelLeftShiftSignal.Location = new System.Drawing.Point(36, 520);
            this.LabelLeftShiftSignal.Name = "LabelLeftShiftSignal";
            this.LabelLeftShiftSignal.Padding = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.LabelLeftShiftSignal.Size = new System.Drawing.Size(17, 16);
            this.LabelLeftShiftSignal.TabIndex = 20;
            this.LabelLeftShiftSignal.Text = "S";
            // 
            // LabelLeftAltSignal
            // 
            this.LabelLeftAltSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelLeftAltSignal.AutoSize = true;
            this.LabelLeftAltSignal.BackColor = System.Drawing.Color.Teal;
            this.LabelLeftAltSignal.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelLeftAltSignal.Location = new System.Drawing.Point(59, 520);
            this.LabelLeftAltSignal.Name = "LabelLeftAltSignal";
            this.LabelLeftAltSignal.Padding = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.LabelLeftAltSignal.Size = new System.Drawing.Size(18, 16);
            this.LabelLeftAltSignal.TabIndex = 21;
            this.LabelLeftAltSignal.Text = "A";
            // 
            // LabelRightAltSignal
            // 
            this.LabelRightAltSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelRightAltSignal.AutoSize = true;
            this.LabelRightAltSignal.BackColor = System.Drawing.Color.Teal;
            this.LabelRightAltSignal.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelRightAltSignal.Location = new System.Drawing.Point(156, 520);
            this.LabelRightAltSignal.Name = "LabelRightAltSignal";
            this.LabelRightAltSignal.Padding = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.LabelRightAltSignal.Size = new System.Drawing.Size(18, 16);
            this.LabelRightAltSignal.TabIndex = 22;
            this.LabelRightAltSignal.Text = "A";
            // 
            // LabelRightShiftSignal
            // 
            this.LabelRightShiftSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelRightShiftSignal.AutoSize = true;
            this.LabelRightShiftSignal.BackColor = System.Drawing.Color.Teal;
            this.LabelRightShiftSignal.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelRightShiftSignal.Location = new System.Drawing.Point(133, 520);
            this.LabelRightShiftSignal.Name = "LabelRightShiftSignal";
            this.LabelRightShiftSignal.Padding = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.LabelRightShiftSignal.Size = new System.Drawing.Size(17, 16);
            this.LabelRightShiftSignal.TabIndex = 23;
            this.LabelRightShiftSignal.Text = "S";
            // 
            // LabelRightCtrlSignal
            // 
            this.LabelRightCtrlSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelRightCtrlSignal.AutoSize = true;
            this.LabelRightCtrlSignal.BackColor = System.Drawing.Color.Teal;
            this.LabelRightCtrlSignal.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelRightCtrlSignal.Location = new System.Drawing.Point(110, 520);
            this.LabelRightCtrlSignal.Name = "LabelRightCtrlSignal";
            this.LabelRightCtrlSignal.Padding = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.LabelRightCtrlSignal.Size = new System.Drawing.Size(18, 16);
            this.LabelRightCtrlSignal.TabIndex = 24;
            this.LabelRightCtrlSignal.Text = "C";
            // 
            // BtnStopScreenSaverStopper
            // 
            this.BtnStopScreenSaverStopper.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BtnStopScreenSaverStopper.Enabled = false;
            this.BtnStopScreenSaverStopper.Location = new System.Drawing.Point(305, 67);
            this.BtnStopScreenSaverStopper.Name = "BtnStopScreenSaverStopper";
            this.BtnStopScreenSaverStopper.Size = new System.Drawing.Size(82, 33);
            this.BtnStopScreenSaverStopper.TabIndex = 25;
            this.BtnStopScreenSaverStopper.Text = "SSS停止";
            this.BtnStopScreenSaverStopper.UseVisualStyleBackColor = false;
            this.BtnStopScreenSaverStopper.Click += new System.EventHandler(this.BtnStopScreenSaverStopper_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(191, 542);
            this.Controls.Add(this.BtnStopScreenSaverStopper);
            this.Controls.Add(this.LabelRightCtrlSignal);
            this.Controls.Add(this.LabelRightShiftSignal);
            this.Controls.Add(this.LabelRightAltSignal);
            this.Controls.Add(this.LabelLeftAltSignal);
            this.Controls.Add(this.LabelLeftShiftSignal);
            this.Controls.Add(this.LabelLeftCtrlSignal);
            this.Controls.Add(this.BtnStartScreenSaverStopper);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.LblCheckedToibanCount);
            this.Controls.Add(this.Btn4);
            this.Controls.Add(this.ChkListToiban);
            this.Controls.Add(this.BtnLowerToiban);
            this.Controls.Add(this.BtnRaiseToiban);
            this.Controls.Add(this.BtnClearToibanList);
            this.Controls.Add(this.BtnRemoveToiban);
            this.Controls.Add(this.BtnPrintKouseishi);
            this.Controls.Add(this.Btn3);
            this.Controls.Add(this.Btn2);
            this.Controls.Add(this.Btn1);
            this.Controls.Add(this.TxtBoxToiban);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuBar;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NengaBooster";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ContextMenuStrip_Toiban.ResumeLayout(false);
            this.ContextMenuStrip_Hensyu.ResumeLayout(false);
            this.ContextMenuStrip_Information.ResumeLayout(false);
            this.ContextMenuStrip_PrintToiban.ResumeLayout(false);
            this.ContextMenuStrip_ClearToibanCheckedList.ResumeLayout(false);
            this.ContextMenuStrip_ToibanCheckedList.ResumeLayout(false);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ContextMenuStrip_Kyokudome.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Hensyu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_HensyuOpenMode_Menu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_HensyuOpenMode_Tegumi;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveOutputToibanList;
        private System.Windows.Forms.ToolStripMenuItem MenuLoadOutputToibanList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuSaikumiByOutputListOperationMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuShortcutKeySetting;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenShortcutKeySettingFile;
        private System.Windows.Forms.ToolStripMenuItem MenuSelectShortcutKeySettingFile;
        private System.Windows.Forms.ToolStripMenuItem MenuReloadShortcutKeySetting;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Toiban;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ToibanSelectMode_ByClick;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ToibanSelectMode_ByWClick;
        private System.Windows.Forms.Button BtnStartScreenSaverStopper;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Kyokudome;
        private System.Windows.Forms.ToolStripMenuItem 依頼主都道府県住所電話番号別住所住所ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 依頼主都道府県住所別住所住所ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 依頼主住所電話番号別住所住所ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 依頼主住所別住所住所ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 依頼主姓名別住所名前ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem 閉じるToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 名入れToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 名入れ住所依頼主住所ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 依頼主姓名名入れ姓名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem MenuShowUserConfigForm;
        private System.Windows.Forms.ToolStripMenuItem 再起動ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_ClearToibanCheckedList;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ToibanClearMode_All;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ToibanClearMode_CheckedOnly;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Information;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_InformationOpenMode_SearchForm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_InformationOpenMode_DetailWindow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_InformationOpenMode_KouseiPage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_InformationOpenMode_KumihanPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EnterTextBoxToibanToKouseishi;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_PrintToiban;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ToibanClearMode_UncheckedOnly;
        private System.Windows.Forms.Label LabelLeftCtrlSignal;
        private System.Windows.Forms.Label LabelLeftShiftSignal;
        private System.Windows.Forms.Label LabelLeftAltSignal;
        private System.Windows.Forms.Label LabelRightAltSignal;
        private System.Windows.Forms.Label LabelRightShiftSignal;
        private System.Windows.Forms.Label LabelRightCtrlSignal;
        private System.Windows.Forms.Button BtnStopScreenSaverStopper;
    }
}
