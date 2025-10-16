using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;

namespace IsTama.Utils
{
    /// <summary>
    /// アサーションクラス。
    /// </summary>
    public static class Assert
    {
        [Conditional("DEBUG")]
        public static void IsNull<V>(V obj, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            if (obj == null)
                Raise(CreateMessage(name + " is null", memberName, filepath, lineNumber));
        }

        [Conditional("DEBUG")]
        public static void IsNullOrEmpty(string str, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            IsNull(str, name, memberName, filepath, lineNumber);
            if (str.Length == 0)
                Raise(CreateMessage(name + " is empty", memberName, filepath, lineNumber));
        }

        [Conditional("DEBUG")]
        public static void IsNullOrWhiteSpace(string str, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            IsNullOrEmpty(str, name, memberName, filepath, lineNumber);
            if (str.All(c => c == ' '))
                Raise(CreateMessage(name + " is whitespace", memberName, filepath, lineNumber));
        }

        [Conditional("DEBUG")]
        public static void IsNot(bool condition, String msg,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            if (!condition)
                Raise(CreateMessage(msg, memberName, filepath, lineNumber));
        }

        [Conditional("DEBUG")]
        public static void IsSmallerThan (Int32 value, Int32 lowLimit, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            if (value < lowLimit)
                Raise(CreateMessage(name + " is smaller than " + lowLimit.ToString() + " / " + value.ToString(), memberName, filepath, lineNumber));
        }

        [Conditional("DEBUG")]
        public static void IsLargerThan (Int32 value, Int32 highLimit, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            if (value > highLimit)
                Raise(CreateMessage(name + " is larger than " + highLimit.ToString() + " / " + value.ToString(), memberName, filepath, lineNumber));
        }

        [Conditional("DEBUG")]
        public static void IsOutRange (Int32 value, Int32 lowLimit, Int32 highLimit, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            IsSmallerThan(value, lowLimit, name, memberName, filepath, lineNumber);
            IsLargerThan(value, highLimit, name, memberName, filepath, lineNumber);
        }

        [Conditional("DEBUG")]
        public static void IsInvalidPath(String path, String name,
            [CallerMemberName] String memberName = "",
            [CallerFilePath]   String filepath = "",
            [CallerLineNumber] Int32 lineNumber = -1)
        {
            IsNullOrWhiteSpace(path, name);
            try {
                System.IO.Path.GetFullPath(path);
            } catch (Exception) {
                Raise(CreateMessage($"{name} is invalid path / {path}", memberName, filepath, lineNumber));
            }
        }
        
        private static String CreateMessage(String msg, String memberName, String filepath, Int32 lineNumber)
        {
            return string.Format(
                "{0}" + Environment.NewLine + Environment.NewLine +
                "[発生場所]" + Environment.NewLine +
                " method  : {1}()" + Environment.NewLine +
                " filepath: {2} {3}行", msg, memberName, filepath, lineNumber.ToString());
        }

        private static void Raise(String msg)
        {
            throw new AssertException(msg);
        }
    }
}
