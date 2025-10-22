
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.View
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    partial class MainForm : Form
    {
        private readonly MainFormViewModel _viewmodel;
        private readonly MainFormController _controller;

        private readonly ViewModelBinder _vmBinder;
        private readonly FocusOrderControlBlocks _focusOrderController;


        public MainForm(MainFormViewModel viewmodel, MainFormController controller)
        {
            InitializeComponent();

            // タスクバーに表示されるアイコンを読み込んで設定する
            //this.Icon = MyResource.GetIcon("ngb.ico");

            _viewmodel = viewmodel;
            _controller = controller;

            _vmBinder = new ViewModelBinder(_viewmodel);
            _focusOrderController = new FocusOrderControlBlocks(true, true);

            InitializeStartPosition();
            InitializeEventHandler();
            BindControls();
        }

        /// <summary>
        /// ユーザー設定ファイルのパス。
        /// </summary>
        public string UserConfigFilepath { get; set; }

        /// <summary>
        /// NengaBoosterのデスクトップの表示位置を設定する。
        /// </summary>
        private void InitializeStartPosition()
        {
            StartPosition = FormStartPosition.Manual;
            var screen_size = Screen.GetBounds(this);
            DesktopLocation = new Point(screen_size.Width - Width, 0);
        }

        private void InitializeEventHandler()
        {
            Shown                 += MainForm_Shown;
            KeyDown               += MainForm_KeyDown;
            TxtBoxToiban.KeyDown  += TxtBoxToiban_KeyDown;
            TxtBoxToiban.Click    += TxtBoxToiban_Click;
            TxtBoxToiban.LostFocus += TxtBoxToiban_LostFocus;

            Btn1.KeyDown += Btn1_KeyDown;
            Btn2.KeyDown += Btn2_KeyDown;
            Btn3.KeyDown += Btn3_KeyDown;
            BtnPrintKouseishi.KeyDown += BtnPrintKouseishi_KeyDown;

            Btn1.PreviewKeyDown += Btn_PreviewKeyDown;
            Btn2.PreviewKeyDown += Btn_PreviewKeyDown;
            Btn3.PreviewKeyDown += Btn_PreviewKeyDown;
            Btn4.PreviewKeyDown += Btn_PreviewKeyDown;
            BtnRaiseToiban.PreviewKeyDown += Btn_PreviewKeyDown;
            BtnLowerToiban.PreviewKeyDown += Btn_PreviewKeyDown;
            BtnPrintKouseishi.PreviewKeyDown += Btn_PreviewKeyDown;
            BtnRemoveToiban.PreviewKeyDown += Btn_PreviewKeyDown;
            BtnClearToibanList.PreviewKeyDown += Btn_PreviewKeyDown;

            ChkListToiban.KeyDown += TxtBoxToiban_KeyDown;

            InitializeContextMenuStripEventHandler();
            InitializeToibanCheckedListEventHandler();
        }

        private void BindControls()
        {
            _vmBinder.BindText(TxtBoxToiban, nameof(_viewmodel.Toiban));
            _vmBinder.BindText(Btn1, nameof(_viewmodel.Button1Name));
            _vmBinder.BindText(Btn2, nameof(_viewmodel.Button2Name));
            _vmBinder.BindText(Btn3, nameof(_viewmodel.Button3Name));

            _vmBinder.BindText(LblCheckedToibanCount, nameof(_viewmodel.CheckedToibanCount));

            _vmBinder.BindBackColor(this, nameof(_viewmodel.FormColor));
            _vmBinder.BindBackColor(Btn1, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(Btn2, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(Btn3, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(Btn4, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(BtnLowerToiban, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(BtnRaiseToiban, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(BtnPrintKouseishi, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(BtnRemoveToiban, nameof(_viewmodel.ButtonColor));
            _vmBinder.BindBackColor(BtnClearToibanList, nameof(_viewmodel.ButtonColor));

            _vmBinder.BindBackColor(LabelLeftCtrlSignal,   nameof(_viewmodel.LabelLeftCtrlSignalColor));
            _vmBinder.BindBackColor(LabelLeftShiftSignal,  nameof(_viewmodel.LabelLeftShiftSignalColor));
            _vmBinder.BindBackColor(LabelLeftAltSignal,    nameof(_viewmodel.LabelLeftAltSignalColor));
            _vmBinder.BindBackColor(LabelRightCtrlSignal,  nameof(_viewmodel.LabelRightCtrlSignalColor));
            _vmBinder.BindBackColor(LabelRightShiftSignal, nameof(_viewmodel.LabelRightShiftSignalColor));
            _vmBinder.BindBackColor(LabelRightAltSignal,   nameof(_viewmodel.LabelRightAltSignalColor));

            _vmBinder.BindForeColor(LabelLeftCtrlSignal, nameof(_viewmodel.LabelSignalTextColor));
            _vmBinder.BindForeColor(LabelLeftShiftSignal, nameof(_viewmodel.LabelSignalTextColor));
            _vmBinder.BindForeColor(LabelLeftAltSignal, nameof(_viewmodel.LabelSignalTextColor));
            _vmBinder.BindForeColor(LabelRightCtrlSignal, nameof(_viewmodel.LabelSignalTextColor));
            _vmBinder.BindForeColor(LabelRightShiftSignal, nameof(_viewmodel.LabelSignalTextColor));
            _vmBinder.BindForeColor(LabelRightAltSignal, nameof(_viewmodel.LabelSignalTextColor));

            _vmBinder.BindEnabled(BtnStartScreenSaverStopper, nameof(_viewmodel.StartScreenSaverStopperButtonEnabled));
            _vmBinder.BindEnabled(BtnStopScreenSaverStopper, nameof(_viewmodel.StopScreenSaverStopperButtonEnabled));

            //_vm_binder.BindBackColor(ChkListOutputToiban, nameof(_viewmodel.ListBoxColor));
            //_vm_binder.BindBackColor(MenuBar, nameof(_viewmodel.MenuBarColor));

            // メニューをviewmodelにバインドする
            BindContextMenuToViewModel();
            // ButtonのConatextMenuStripにviewmodelをバインドする
            BindContextMenuStripToViewModel();

            // CheckedListはバインドできないので、手動で連携する
            _viewmodel.ToibanCheckedList = new List<(bool, string)>();
            _viewmodel.PropertyChanged += _viewmodel_PropertyChanged;
        }

        private void _viewmodel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // 出力リストが更新された場合
            if (e.PropertyName == nameof(_viewmodel.ToibanCheckedList))
            {
                ChkListToiban.Items.Clear();
                foreach (var (check, toiban) in _viewmodel.ToibanCheckedList)
                    ChkListToiban.Items.Add(toiban, check);

                return;
            }

            // 出力リストの文字サイズが更新された場合
            if (e.PropertyName == nameof(_viewmodel.ToibanCheckedListCharSize))
            {
                var size = _viewmodel.ToibanCheckedListCharSize;
                var newFont = new Font(ChkListToiban.Font.FontFamily, size, ChkListToiban.Font.Style);
                ChkListToiban.Font = newFont;
            }
        }

        /// <summary>
        /// フォームが起動される度に呼び出されるハンドラ。
        /// </summary>
        async void MainFormLoad(object sender, System.EventArgs e)
        {
            //NengaBooster.Di.NengaBoosterObjectFactory.Test();
            /*
              キーが押されたとき、Buttonなどのコントロールにキーイベントに渡される前に、
              このフォームがイベントを受け取るようにする
              ただし、カーソルキーが押されたときButtonにフォーカスがあると、
              この設定だけではフォームにイベントが渡されない。
              イベントを受け取るためには次のProcessDialogKey()をオーバーライドする必要がある。
            */
            this.KeyPreview = true;

            // 各コントロールのカーソル移動順を構成する
            _focusOrderController
                .AddRow(TxtBoxToiban, 6)
                .AddRow(Btn1, 6)
                .AddRow(Btn2, 6)
                .AddRow(Btn3, 6)
                .AddRow(Btn4, 6)
                .AddRow(BtnRaiseToiban, 3)
                .AddColumn(BtnLowerToiban, 3)
                .AddRow(BtnPrintKouseishi, 2)
                .AddColumn(BtnRemoveToiban, 2)
                .AddColumn(BtnClearToibanList, 2)
                .Build();
            
            MainForm_ToibanCheckedList_Load();
            MainForm_MenuBar_Load();

            // ユーザー設定をロードする
            await _controller.LoadUserConfigAsync(UserConfigFilepath);
            // 修飾キーの状態を通知するタスクを起動する
            _controller.StartTaskToNotifyModifierKeysState();
            // KeyReplacerを起動する
            await _controller.ExecuteKeyReplacerAsync();

        }

        /// <summary>
        /// フォームが初めて表示された直後に呼び出される。
        /// </summary>
        private void MainForm_Shown(object sender, EventArgs e)
        {
        }

        ///// <summary>
        ///// Buttonにフォーカスがあるときにカーソルキーが押されると呼び出されるメソッド。
        ///// 以降のフォームにキーイベントを手渡したくないときはtrueを返すようにする。
        ///// </summary>
        //protected override bool ProcessDialogKey(Keys key)
        //{
        //    if (key.IsCursorKey())
        //    {
        //        MessageBox.Show("progress");
        //        _control_focus_order.FocusNext(key);
        //        return true;
        //    }
        //    return base.ProcessDialogKey(key);
        //}

        void MainFormClosing(object sender, System.EventArgs e)
        {
            _controller.StopTaskToNotifyModifierKeysState();
            _controller.KillKeyReplacer();
        }
    
        /// <summary>
        /// すべてのコントロールでキーが押されたら呼び出される。
        /// </summary>
        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Shift && !e.Alt && e.KeyCode.IsCursorKey())
            {
                // NengaBooster内のフォーカス位置を移動する
                _focusOrderController.FocusNext(e.KeyCode);
                return;
            }
        }

        private void TxtBoxToiban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowErrorIfThrowException(() => _controller.EnterToibanToNaireAsync());
                return;
            }
            //else if (e.KeyCode == Keys.Left)
            //{
            //    var w = WindowController.Find("^NengaBooster$", 0).FirstOrDefault();
            //    if (w == default)
            //        return;
            //
            //    var sw = w.GetSubWindows(new Point(21, 63)).FirstOrDefault();
            //    if (sw == default)
            //        return;
            //
            //    var (start, end) = sw.GetSelectedRange();
            //    if (start != 0 || end != 0)
            //        return;
            //
            //    ShowControlContextMenuStrip(TxtBoxToiban);
            //}
        }

        /// <summary>
        /// フォームＩＤのテキストボックスが入力されているときに呼び出される値のチェック。
        /// </summary>
        void TxtBoxToiban_KeyPress(object sender, KeyPressEventArgs e)
        {
            var num = (Int32)e.KeyChar;
            if (num == 3 || num == 22 || num == 24)  // ctrl + c か ctrl + v か ctrl + x の場合
                return;
                
            //押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = (e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b';
        }

        private void Btn1_KeyDown(object sender, KeyEventArgs e)
        {
            // 動作モードを変更するメニューを表示する
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.M)
                ShowControlContextMenuStrip(Btn1);
        }

        private void Btn2_KeyDown(object sender, KeyEventArgs e)
        {
            // 動作モードを変更するメニューを表示する
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.M)
                ShowControlContextMenuStrip(Btn2);
        }

        private void Btn3_KeyDown(object sender, KeyEventArgs e)
        {
            // 動作モードを変更するメニューを表示する
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.M)
                ShowControlContextMenuStrip(Btn3);
        }

        private void BtnPrintKouseishi_KeyDown(object sender, KeyEventArgs e)
        {
            // 動作モードを変更するメニューを表示する
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.M)
                ShowControlContextMenuStrip(BtnPrintKouseishi);
        }

        private void ShowControlContextMenuStrip(System.Windows.Forms.Control control)
        {
            var menu = control.ContextMenuStrip;
            if (menu == null)
                return;
            
            menu.Show(control, new Point(menu.Size.Width * -1, 0));
        }

        /// <summary>
        /// Buttonでキー入力したとき、KeyDownイベントが発生する前に呼び出されるメソッド。
        /// デフォルトではButtonでカーソルキーを入力したとき、ButtonのKeyDownイベントは発生せず、
        /// Buttonに設定されたデフォルトの動作が実行される。
        /// このイベントリスナーでその動作を変更できる。
        /// </summary>
        private void Btn_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Buttonでカーソルキーが入力されたときのデフォルトの動作を無効にし、
            // KeyDownイベントが発生するようにする。
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                e.IsInputKey = true;
        }


        private String _selected_text = String.Empty;
        /// <summary>
        /// 問番テキストボックスがクリックされると呼び出される。
        /// </summary>
        private void TxtBoxToiban_Click(object sender, EventArgs e)
        {
            // 選択状態のテキストを再度クリックしたら選択が解除されるようにするため
            if (TxtBoxToiban.Text != _selected_text)
            {
                if (_viewmodel.ToibanSelectMode_ByClick == CheckState.Checked)
                {
                    TxtBoxToiban.SelectAll();
                    _selected_text = TxtBoxToiban.SelectedText;
                }
            }
            else
            {
                _selected_text = String.Empty;
            }
        }

        private void TxtBoxToiban_LostFocus(object sender, EventArgs e)
        {
            _selected_text = string.Empty;
        }

        /// <summary>
        /// ボタン1がクリックされたときに呼び出される。
        /// </summary>
        private void Btn1Click(object sender, EventArgs e)
        {
            ShowErrorIfThrowException(async () => await _controller.EnterToibanToNaireAsync());
        }
    
        /// <summary>
        /// ボタン2がクリックされたときに呼び出される。
        /// </summary>
        private void Btn2Click(object sender, EventArgs e)
        {
            ShowErrorIfThrowException(() => _controller.EnterToibanToHensyuAsync());
        }
    
        /// <summary>
        /// ボタン3がクリックされたときによびだされる。
        /// </summary>
        private void Btn3Click(object sender, EventArgs e)
        {
            ShowErrorIfThrowException(() => _controller.EnterToibanToInformationAsync());
        }

        /// <summary>
        /// ボタン4がクリックされたときに呼び出される。
        /// </summary>
        private void Btn4Click(object sender, EventArgs e)
        {
            ShowErrorIfThrowException(() => _controller.AddToibanToCheckedList());
        }

        private void ShowErrorIfThrowException(Action callback)
        {
            try
            {
                callback();
            }
            catch (NengaBoosterException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ShowErrorIfThrowException(Func<Task> callback)
        {
            try
            {
                await callback();
            }
            catch (NengaBoosterException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// SSSの起動ボタンがクリックされたときに呼び出される。
        /// </summary>
        private async void BtnStopScreenSaver_Click(object sender, EventArgs e)
        {
            await _controller.StartScreenSaverStopperAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// SSSの停止ボタンがクリックされたときに呼び出される。
        /// </summary>
        private void BtnStopScreenSaverStopper_Click(object sender, EventArgs e)
        {
            _controller.StopScreenSaverStopper();
        }

    }
}
