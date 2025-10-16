using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.View
{
    partial class MainForm : Form
    {
        /// コンテキストメニュー上でenterキーが押されたかどうか。
        private Boolean _pressedEnterKeyOnContextMenuStrip = false;

        /// <summary>
        /// 右クリックメニューのイベントハンドラを設定する。
        /// </summary>
        private void InitializeContextMenuStripEventHandler()
        {
            // 右クリックメニューのイベントハンドラの設定
            // （メニューの個々のアイテムに対するイベントハンドラではない）
            ContextMenuStrip_Toiban.PreviewKeyDown                   += ContextMenuStrip_PreviewKeyDown;
            ContextMenuStrip_Toiban.Closing                          += ContextMenuStrip_Closing;
                                                                     
            ContextMenuStrip_Hensyu.PreviewKeyDown                   += ContextMenuStrip_PreviewKeyDown;
            ContextMenuStrip_Hensyu.Closing                          += ContextMenuStrip_Closing;
                                                                     
            ContextMenuStrip_Information.PreviewKeyDown              += ContextMenuStrip_PreviewKeyDown;
            ContextMenuStrip_Information.Closing                     += ContextMenuStrip_Closing;
                                                                     
            ContextMenuStrip_PrintToiban.PreviewKeyDown              += ContextMenuStrip_PreviewKeyDown;
            ContextMenuStrip_PrintToiban.Closing                     += ContextMenuStrip_Closing;
                                                                     
            ContextMenuStrip_ToibanCheckedList.PreviewKeyDown        += ContextMenuStrip_PreviewKeyDown;
            ContextMenuStrip_ToibanCheckedList.Closing               += ContextMenuStrip_Closing;
                                                                     
            ContextMenuStrip_ClearToibanCheckedList.PreviewKeyDown   += ContextMenuStrip_PreviewKeyDown;
            ContextMenuStrip_ClearToibanCheckedList.Closing          += ContextMenuStrip_Closing;

            // 個々の右クリックメニューアイテムがクリックされたときにハンドラを設定
            //ToolStripMenuItem_EnterTextBoxToibanToKouseishi.Click    += MenuItemEnterTextBoxToibanToKouseishi_Click;
            //
            //ToolStripMenuItem_ToibanSelectMode_ByClick.Click         += MenuItemToibanSelectModeByClick_Click;
            //ToolStripMenuItem_ToibanSelectMode_ByWClick.Click        += MenuItemToibanSelectModeByWClick_Click;
            //                                                         
            //ToolStripMenuItem_HensyuOpenMode_Menu.Click              += MenuItemHensyuOpenModeMenu_Click;
            //ToolStripMenuItem_HensyuOpenMode_Tegumi.Click            += MenuItemHensyuOpenModeTegumi_Click;
            //
            //ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen.Click += MenuItemShouldAddToibanToCheckedListWhenInformationOpen;
            //
            //ToolStripMenuItem_InformationOpenMode_SearchForm.Click   += MenuItemInformationOpenModeSearchForm_Click;
            //ToolStripMenuItem_InformationOpenMode_DetailWindow.Click += MenuItemInformationOpenModeDetailWindow_Click;
            //ToolStripMenuItem_InformationOpenMode_KouseiPage.Click   += MenuItemInformationOpenModeKouseiPage_Click;
            //ToolStripMenuItem_InformationOpenMode_KumihanPage.Click  += MenuItemInformationOpenModeKumihanPage_Click;
            //
            //ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi.Click += MenuItemShouldUncheckToibanFromCheckedListWhenPrintKouseishi_Click;
            //
            //ToolStripMenuItem_ToibanClearMode_All.Click              += MenuItemToibanCheckedListClearModeAll_Click;
            //ToolStripMenuItem_ToibanClearMode_CheckedOnly.Click      += MenuItemToibanCheckedListClearModeCheckedOnly_Click;
            //ToolStripMenuItem_ToibanClearMode_UncheckedOnly.Click    += MenuItemToibanCheckedListClearModeUncheckedOnly_Click;
        }

        /// <summary>
        /// 右クリックメニューの表示状態とviewmodelをバインディングする。
        /// </summary>
        private void BindContextMenuStripToViewModel()
        {
            // 右クリックメニューのチェック状態とviewmodelのプロパティを紐付ける
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ToibanSelectMode_ByClick,         nameof(_viewmodel.ToibanSelectMode_ByClick));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ToibanSelectMode_ByWClick,        nameof(_viewmodel.ToibanSelectMode_ByWClick));
                                                                                                 
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_HensyuOpenMode_Menu,              nameof(_viewmodel.HensyuOpenMode_Menu));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_HensyuOpenMode_Tegumi,            nameof(_viewmodel.HensyuOpenMode_Tegumi));

            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ShouldAddToibanToCheckedListWhenInformationOpen, nameof(_viewmodel.ShouldAddToibanToCheckedList));

            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_InformationOpenMode_SearchForm,   nameof(_viewmodel.InformationOpenMode_SearchForm));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_InformationOpenMode_DetailWindow, nameof(_viewmodel.InformationOpenMode_DetailWindow));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_InformationOpenMode_KouseiPage,   nameof(_viewmodel.InformationOpenMode_KouseiPage));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_InformationOpenMode_KumihanPage,  nameof(_viewmodel.InformationOpenMode_KumihanPage));

            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ShouldUncheckToibanFromCheckedListWhenPrintKouseishi, nameof(_viewmodel.ShouldUncheckToibanFromCheckedList));

            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ToibanClearMode_All,              nameof(_viewmodel.ToibanCheckedListClearMode_All));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ToibanClearMode_CheckedOnly,      nameof(_viewmodel.ToibanCheckedListClearMode_CheckedOnly));
            _vmBinder.BindMenuItemCheckState(ToolStripMenuItem_ToibanClearMode_UncheckedOnly,    nameof(_viewmodel.ToibanCheckedListClearMode_UncheckedOnly));
        }

        private async void MenuItemEnterTextBoxToibanToKouseishi_Click(object sender, EventArgs e)
        {
            await _controller.EnterTextBoxToibanToKouseishiAsync();
        }

        /// <summary>
        /// 名入れの動作モードのメニューがクリックされたときに呼び出される。
        /// </summary>
        private async void MenuItemToibanSelectModeByClick_Click(object sender, EventArgs e)
        { 
            await _controller.SetToibanSelectModeToByClickAsync().ConfigureAwait(false);
        }
        private async void MenuItemToibanSelectModeByWClick_Click(object sender, EventArgs e)
        { 
            await _controller.SetToibanSelectModeToByWClickAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 編集の動作モードのメニューがクリックされたときに呼び出される。
        /// </summary>
        private async void MenuItemHensyuOpenModeMenu_Click(object sender, EventArgs e)
        { 
            await _controller.SetHensyuOpenModeToMenuWindowAsync().ConfigureAwait(false);
        }
        private async void MenuItemHensyuOpenModeTegumi_Click(object sender, EventArgs e)
        { 
            await _controller.SetHensyuOpenModeToTegumiWindowAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// インフォメーション検索で出力リストに問番を追加するかどうかのメニューがクリックされたときに呼び出される。
        /// </summary>
        private async void MenuItemShouldAddToibanToCheckedListWhenInformationOpen(object sender, EventArgs e)
        { 
            await _controller.ChangeWhetherToAddTobanToCheckedListWhenInformationOpenAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// インフォメーションの動作モードのメニューがクリックされたときに呼び出される。
        /// </summary>
        private async void MenuItemInformationOpenModeSearchForm_Click(object sender, EventArgs e)
        { 
            await _controller.SetInformationOpenModeToSearchFormAsync().ConfigureAwait(false);
        }
        private async void MenuItemInformationOpenModeDetailWindow_Click(object sender, EventArgs e)
        { 
            await _controller.SetInformationOpenModeToDetailWindowAsync().ConfigureAwait(false);
        }
        private async void MenuItemInformationOpenModeKouseiPage_Click(object sender, EventArgs e)
        { 
            await _controller.SetInformationOpenModeToKouseisPageAsync().ConfigureAwait(false);
        }
        private async void MenuItemInformationOpenModeKumihanPage_Click(object sender, EventArgs e)
        { 
            await _controller.SetInformationOpenModeToKumihanPageAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 校正紙出力したときに出力リストのチェックを外すかどうかのメニューをクリックしたときに呼び出される。
        /// </summary>
        private async void MenuItemShouldUncheckToibanFromCheckedListWhenPrintKouseishi_Click(object sender, EventArgs e)
        { 
            await _controller.ChangeWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync();
        }

        /// <summary>
        /// 出力リストのクリアの動作モードのメニューがクリックされたときに呼び出される。
        /// </summary>
        private async void MenuItemToibanCheckedListClearModeAll_Click(object sender, EventArgs e)
        { 
            await _controller.SetToibanCheckedListClearModeToAllAsync().ConfigureAwait(false);
        }
        private async void MenuItemToibanCheckedListClearModeCheckedOnly_Click(object sender, EventArgs e)
        { 
            await _controller.SetToibanCheckedListClearModeToCheckedOnlyAsync().ConfigureAwait(false);
        }
        private async void MenuItemToibanCheckedListClearModeUncheckedOnly_Click(object sender, EventArgs e)
        {
            await _controller.SetToibanCheckedListClearModeToUncheckedOnlyAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// コンテキストメニュー上でキーが押されるとKeyDownイベントが発生する前に呼び出される。
        /// ※Enterキーが押されたときはなぜかKeyDownイベントが発生しないのでこちらを使用している。
        /// </summary>
        private void ContextMenuStrip_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                _pressedEnterKeyOnContextMenuStrip = true;
                // TODO ここにコンテキストメニューの表示状態を更新する処理をいれる
            }
        }

        /// <summary>
        /// コンテキストメニューが閉じるときに呼び出される。
        /// </summary>
        private void ContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            // Enterキーが押されていた場合、コンテキストメニューを閉じないようにする
            if (_pressedEnterKeyOnContextMenuStrip)
                e.Cancel = true;

            _pressedEnterKeyOnContextMenuStrip = false;
        }

        /// <summary>
        /// テキストボックスの問番を校正紙出力へがクリックされたら。
        /// </summary>
        private void ToolStripMenuItemSendToibanToKouseishi_Click(object sender, EventArgs e)
        {
            ShowErrorIfThrowException(() => _controller.EnterTextBoxToibanToKouseishiAsync());
        }

        private void プリンタを一時停止するToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
