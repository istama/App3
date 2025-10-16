using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.Error
{
    sealed class NengaBoosterExceptionArgs : ExceptionArgs
    {
        public override String Message { get; }

        public NengaBoosterExceptionArgs(String message)
        {
            Message = message;
        }
    }
}
