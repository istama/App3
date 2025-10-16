using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.Error;
//using NengaBooster.UI.View;
using IsTama.Utils;

namespace IsTama.NengaBooster.AppMain
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// 引数args[0]にはNengaBoosterConfig.jsonのパスを受け取る。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                /*
                 * プロジェクトを開始するにあたっての注意点。
                 * 
                 * Win32Apiを使って他のアプリケーションにSendMessageを送る場合、
                 * 対象のアプリケーションとこのアプリケーションのビット数を同じにする必要がある。
                 * 例えば、対象のアプリケーションが64bitアプリケーションなら、
                 * このアプリケーションも64bitでビルドしないと、SendMessageが正しく動作しない。
                 * 
                 */
                using (var mutex = new ApplicationMutex("__NENGAB00STER7.0__"))
                {
                    // アプリケーションが起動しているか判定し、二重起動を禁止する
                    if (!mutex.Runnable())
                    {
                        MessageBox.Show("既に起動しています。", "NengaBooster");
                        return;
                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var di = new Di();

                    
                    
                    // Diのテスト
                    //di.TestBuild();

                    var mainform = di.BuildMainForm();
                    if (mainform != null)
                    {
                        var userConfigFilepath = args.Length != 0 ? args[0] : string.Empty;
                        mainform.UserConfigFilepath = userConfigFilepath;

                        Application.Run(mainform);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.ToString());
            }
        }
    }
}
