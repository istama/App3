using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    [Serializable]
    public abstract class ExceptionArgs
    {
        public virtual String Message
        {
            get => String.Empty;
        }
    }

    /// <summary>
    /// WindowOperationsに失敗したときに投げる例外のパラメータ。
    /// </summary>
    [Serializable]
    class WindowOperationsExceptionArgs : ExceptionArgs
    {
    }
}

