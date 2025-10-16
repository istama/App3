using System;
using System.Threading;

namespace IsTama.Utils
{
    /// <summary>
    /// システムレベルでミューテックスを行うクラス。
    /// </summary>
    sealed class ApplicationMutex : IDisposable
    {
        private readonly string _mutext_name;

        private Mutex   _mutex;
        private bool _has_handle;

        public ApplicationMutex(string mutex_name)
        {
            _mutext_name = mutex_name;
        }

        /// <summary>
        /// システムレベルでシグナルを取得できるならtrueを返す。
        /// </summary>
        public bool Runnable()
        {
            _mutex = new Mutex(false, _mutext_name);

            try
            {
                // ミューテックスの所有権を取得する
                _has_handle = _mutex.WaitOne(0, false);
            }
            // 別のアプリケーションがミューテックスを解放しないで終了した場合
            catch (AbandonedMutexException)
            {
                _has_handle = true;
            }

            return _has_handle;
        }

        /// <summary>
        /// 取得したミューテックスを解放する。
        /// </summary>
        public void Dispose()
        {
            if (_mutex != null)
            {
                if (_has_handle)
                    _mutex.ReleaseMutex();

                _mutex.Close();
            }
        }
    }
}
