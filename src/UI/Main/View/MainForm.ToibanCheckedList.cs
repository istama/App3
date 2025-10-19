
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.View
{
    /// <summary>
    /// 出力リスト。
    /// </summary>
    partial class MainForm : Form
    {
        private void MainForm_ToibanCheckedList_Load()
        {
        }

        /// <summary>
        /// 出力リストのイベントハンドラを設定。
        /// </summary>
        private void InitializeToibanCheckedListEventHandler()
        {
            ChkListToiban.MouseWheel += ChkListOutputToiban_MouseWheel;
            ChkListToiban.ItemCheck  += ChkListToiban_Check;
        }

        /// <summary>
        /// アイテムの選択状態が変わると呼び出される。
        /// </summary>
        private void ChkListToiban_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx >= 0)
                _controller.SetToibanToToibanTextBoxFromToibanCheckedListAt(idx);
        }

        /// <summary>
        /// アイテムのチェックが変更されると呼び出される。
        /// また、CheckList.Items.Add()メソッドで要素を追加したときも、第二引数にtrueを渡していれば、呼び出される。
        /// </summary>
        private void ChkListToiban_Check(Object sender, ItemCheckEventArgs e)
        {
            _controller.SetCheckStateToToibanCheckedList(e.Index, e.NewValue);
        }

        /// <summary>
        /// 出力リストにフォーカスを当てる。
        /// </summary>
        private void FocusOnToibanCheckedList()
        {
            ChkListToiban.Focus();
            if (ChkListToiban.Items.Count > 0 && ChkListToiban.SelectedIndex <= 0)
                ChkListToiban.SelectedIndex = 0;
        }

        /// <summary>
        /// ↑ボタンを押されたときに呼び出される。
        /// </summary>
        private void BtnRaiseToiban_Click(object sender, EventArgs e)
        {
            RaiseSelectedToibanInCheckedList();
        }
        /// <summary>
        /// 選択されている出力リストの問番の順番を１つ上げる。
        /// </summary>
        private void RaiseSelectedToibanInCheckedList()
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx > 0)
            {
                _controller.RaiseSelectedToibanInCheckedListAt(idx);
                idx -= 1;
                ChkListToiban.SelectedIndex = idx;
            }
        }

        /// <summary>
        /// ↓ボタンを押されたときに呼び出される。
        /// </summary>
        private void BtnLowerToiban_Click(object sender, EventArgs e)
        {
            LowerSelectedToibanInCheckedList();
        }
        /// <summary>
        /// 選択されている出力リストの問番の順番を１つ下げる。
        /// </summary>
        private void LowerSelectedToibanInCheckedList()
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx >= 0 && idx < ChkListToiban.Items.Count - 1)
            {
                _controller.LowerSelectedToibanInCheckedListAt(idx);
                idx += 1;
                ChkListToiban.SelectedIndex = idx;
            }
        }

        /// <summary>
        /// 出力ボタンを押されたときに呼び出される。
        /// </summary>
        private void BtnPrintKouseishi_Click(object sender, System.EventArgs e)
        {
            ShowErrorIfThrowException(() =>  _controller.EnterTextBoxToibanToKouseishiAsync());
        }

        /// <summary>
        /// 削除ボタンを押されたときに呼び出される。
        /// </summary>
        private void BtnRemoveToiban_Click(object sender, EventArgs e)
        {
            RemoveSlectedToibanInCheckedList();
        }
        /// <summary>
        /// 選択されている問番を削除する。
        /// </summary>
        private void RemoveSlectedToibanInCheckedList()
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx >= 0)
            {
                _controller.RemoveSelectedToibanFromCheckedListAt(idx);

                if (idx < ChkListToiban.Items.Count)
                    ChkListToiban.SelectedIndex = idx;
            }
        }

        /// <summary>
        /// クリアボタンを押されたときに呼び出される。
        /// </summary>
        private async void BtnClearToibanCheckedList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("全て削除してよろしいですか？", "NengaBooster.exe", MessageBoxButtons.OKCancel) == DialogResult.OK)
                await _controller.ClearToibanCheckedListAsync();
        }

        /// <summary>
        /// 出力リストの上でマウスホイールが動くと呼び出される。
        /// </summary>
        private void ChkListOutputToiban_MouseWheel(object sender, MouseEventArgs e)
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx < 0)
                return;

            const Int32 WHEEL_DELTA = 120;

            // スクロールした方向に選択された出力リストの問番を動かす
            var scroll_data = e.Delta / WHEEL_DELTA;
            var scroll_count = Math.Abs(scroll_data);
            if (scroll_data > 0)
                for (var i = 0; i < scroll_count; i++)
                    RaiseSelectedToibanInCheckedList();
            else
                for (var i = 0; i < scroll_count; i++)
                    LowerSelectedToibanInCheckedList();
        }

        private void ChkListToiban_KeyDown(object sender, KeyEventArgs e)
        {
            // フォーカスがチェックリストにあるときにでキー入力をしても、
            // MainFormのKeyDownイベントは発生する。
            // なので、ここでviewmodelのショートカットキーのメソッドは呼び出すべきではない。
            //await _viewmodel.CallShortcutKeyCommandAsync(this, e);

            if (e.KeyData == (Keys.Control | Keys.Up))
            {
                //await NengaBooster.Domain.NengaBoosterController.ActivateAndSelectToibanAsync();
            }
            else if (e.KeyData == Keys.Left)
            {
                // メニューを表示する
                var menu = ChkListToiban.ContextMenuStrip;
                menu.Show(ChkListToiban, new Point(ChkListToiban.Width * -1, 0));
                e.Handled = true;
            }
            else if (e.KeyData == Keys.Right)
            {
                //e.Handled = true;
            }
            else if (e.KeyData == Keys.Enter)
            {
                //await _viewmodel.CallNengaApplication1Async();
            }
            //else if (e.KeyData == (Keys.Alt | Keys.Up))
            //{
            //    RaiseSelectedOutputToiban();
            //}
            //else if (e.KeyData == (Keys.Alt | Keys.Down))
            //{
            //    LowerSelectedOutputToiban();
            //}
            //else if (e.KeyData == Keys.Delete)
            //{
            //    RemoveSlectedOutputToiban();
            //}
        }
    }
    /*

 CheckedListBoxの要素に下線を引くコードの例 
using System;
using System.Drawing;
using System.Windows.Forms;

public class MyForm : Form
{
    private CheckedListBox checkedListBox;

    public MyForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.checkedListBox = new CheckedListBox();
        this.checkedListBox.DrawMode = DrawMode.OwnerDrawFixed;
        this.checkedListBox.DrawItem += new DrawItemEventHandler(checkedListBox_DrawItem);
        this.checkedListBox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3" });
        this.checkedListBox.Location = new Point(10, 10);
        this.checkedListBox.Size = new Size(200, 150);
        this.Controls.Add(this.checkedListBox);
        this.Text = "CheckedListBox Example";
        this.Size = new Size(300, 200);
    }

    private void checkedListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.DrawBackground();
        Font font = e.Font;
        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        {
            font = new Font(e.Font, FontStyle.Underline);
        }
        e.Graphics.DrawString(checkedListBox.Items[e.Index].ToString(), font, Brushes.Black, e.Bounds);
        e.DrawFocusRectangle();
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MyForm());
    }
}

     */
}
