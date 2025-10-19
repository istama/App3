
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.View
{
    /// <summary>
    /// Description of MainForm_MenuBar.
    /// </summary>
    partial class MainForm : Form
    {
        private void MainForm_MenuBar_Load()
        {
            
        }

        private void BindContextMenuToViewModel()
        {
            _vmBinder.BindMenuItemCheckState(MenuNaireOpenMode_Normal, nameof(_viewmodel.NaireOpenMode_Normal));
            _vmBinder.BindMenuItemCheckState(MenuNaireOpenMode_Saikumi, nameof(_viewmodel.NaireOpenMode_Saikumi));
        }

        /// <summary>
        /// 通常モードが選択されたときに呼び出される。
        /// </summary>
        private async void MenuNaireOpenModeNormal_Click(object sender, EventArgs e)
        {
            await _controller.SetNaireOpenModeToNormalAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 再組版モードが選択されたときに呼び出される。
        /// </summary>
        private async void MenuNaireOpenModeSaikumi_Click(object sender, EventArgs e)
        {
            await _controller.SetNaireOpenModeToSaikumiAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// ユーザー名とパスワードの登録がクリックされたときに呼び出される。
        /// </summary>
        private async void MenuRegisterUserAccount_Click(object sender, EventArgs e)
        {
            await _controller.OpenUserAccountFormAsync(this).ConfigureAwait(false);
        }

        /// <summary>
        /// ユーザー設定のウィンドウを開く。
        /// </summary>
        private async void MenuShowUserConfigForm_Click(object sender, EventArgs e)
        {
            await _controller.OpenUserConfigForm(this).ConfigureAwait(false);
        }

        /// <summary>
        /// プロパティがクリックされたときに呼び出される。
        /// </summary>
        private void MenuShowNengaBoosterConfigForm_Click(object sender, EventArgs e)
        {
            _controller.OpenNengaBoosterConfigForm(this);
        }



        /// <summary>
        /// 出力履歴がクリックされたときに呼び出される。
        /// </summary>
        private void MenuPrintToibanHistory_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 出力リストの保存がクリックされたら呼び出される。
        /// </summary>
        private void MenuSaveToibanCheckedList_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 出力リストの読込がクリックされたら呼び出される。
        /// </summary>
        private void MenuLoadToibanCheckedList_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ショートカットキーの設定ファイルを開く。
        /// </summary>
        private void MenuOpenShortcutKeySettingFile_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ショートカットキーの設定ファイルを変更する。
        /// </summary>
        private void MenuSelectShortcutKeySettingFile_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ショートカットキーの設定をリロードする。
        /// </summary>
        private void MenuReloadShortcutKeySetting_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// KeyReplacerを再起動する。
        /// </summary>
        private void MenuRestartKeyReplacer_Click(object sender, EventArgs e)
        {
        }
    }
}
