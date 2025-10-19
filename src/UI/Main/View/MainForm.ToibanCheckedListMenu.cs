
using System;
using System.Windows.Forms;
using System.Linq;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.View
{
    /// <summary>
    /// 出力リストの右クリックメニュー。
    /// </summary>
    partial class MainForm : Form
    {
        /// <summary>
        /// 選択した問番を出力するをクリックしたとき。
        /// </summary>
        void MenuItemEnterSelectedToibanToKouseishi_Click(object sender, EventArgs e)
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx >= 0)
            {
                ShowErrorIfThrowException(() => _controller.EnterToibanToKouseishiAsyncAt(idx));
            }
        }

        /// <summary>
        /// 選択した問番以降を出力するをクリックしたとき。
        /// </summary>
        void MenuItemEnterToibanListBelowSelectedToKouseishi_Click(object sender, EventArgs e)
        {
            var idx = ChkListToiban.SelectedIndex;
            if (idx >= 0)
            {
                ShowErrorIfThrowException(() => _controller.EnterToibanListToKouseishiAsyncAfter(idx));
            }
        }

        /// <summary>
        /// チェック状態を反転させるをクリックしたとき。
        /// </summary>
        void MenuItemReverseCheckStateAll_Click(object sender, EventArgs e)
		{
            _controller.ReverseCheckStateAllInCheckedList();
		}
    
    }
}
