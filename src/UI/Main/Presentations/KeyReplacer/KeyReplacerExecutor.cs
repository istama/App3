using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed class KeyReplacerExecutor
    {
        private readonly IOtherConfigRepository _repository;

        private Process _keyReplacerProcess;


        public KeyReplacerExecutor(IOtherConfigRepository repository)
        {
            Assert.IsNull(repository, nameof(repository));

            _repository = repository;
        }


        public async Task ExecuteKeyReplacerAsync(string keyReplacerSettingFilepath)
        {
            var keyReplacerExeFilepath = await _repository.GetKeyReplacerExeFilepathAsync();

            // 実行ファイルが存在しない
            if (!File.Exists(keyReplacerExeFilepath))
            {
                return;
            }

            var workingDirectory = Path.GetDirectoryName(keyReplacerExeFilepath);

            // 起動するプログラムの情報
            var processStartInfo = new ProcessStartInfo
            {
                FileName = keyReplacerExeFilepath,
                Arguments = $"\"\" \"{keyReplacerSettingFilepath}\"", // 引数を複数渡すときはスペースで区切る
                WorkingDirectory = workingDirectory,
                UseShellExecute = false  // シェルを使用しない
            };

            // プロセスを生成
            var keyReplacerProcess = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true,  // イベントを発生させる
            };

            // プロセス終了時のイベント
            keyReplacerProcess.Exited += (sender, e) => {
                // Process.Kill()メソッドが非同期なため、同期をとらずにprocessオブジェクトをnullにすると
                // NullReferenceExceptionになるリスクがある
                //_key_replacer_process = null;
            };

            // すでにKeyReplacerが起動している場合
            //if (!keyReplacerProcess.HasExited)
            //{
            //    return;
            //}

            _keyReplacerProcess = keyReplacerProcess;
            

            await Task.Factory.StartNew(() =>
            {
                // プロセスを開始
                _keyReplacerProcess.Start();

                // KeyReplacerが終了するまで待機する
                _keyReplacerProcess.WaitForExit();
            });
        }

        public void KillKeyReplacer()
        {
            if (_keyReplacerProcess == null)
                return;

            _keyReplacerProcess.Kill();
        }
    }
}
