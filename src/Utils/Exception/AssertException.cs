using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class AssertException : Exception
    {
        public override String Message { get; }

        public AssertException(String msg)
        {
            Message = msg;
        }
    }

    class AssertExceptionArgs<T> : Exception where T : ExceptionArgs
    {
        public override String Message { get; }

        public AssertExceptionArgs(T args)
        {
            Message = args.Message;
        }
    }
}



