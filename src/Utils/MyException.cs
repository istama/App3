using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// Error: このクラスをthrowしてもなぜかcatchできない。
    /// </summary>
    [Serializable]
    class MyException<T> : Exception, ISerializable where T : ExceptionArgs
    {
        // シリアル化/逆シリアル化に使われる
        private const string ARGS = "args";

        // ジェネリック型によって例外の種類を切り分けるようにすれば、例外の派生クラスをいくつも作らなくてすむ。
        // （ただし、未処理例外のダイアログボックスにはジェネリック型が表示されないというデメリットもある）
        private T Args { get; }

        public MyException(string msg = null, Exception inner = null) : this(null, msg, inner) { }
        public MyException(T args, string msg = null, Exception inner = null) : base(msg, inner)
        {
            this.Args = args;
        }

        // 逆シリアル化用のコンストラクタ。
        // sealedされているのでprivate。sealedされてなければprotectedにすべき。
        // SecurityPermission属性は現在非推奨
        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        protected MyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Args = (T)info.GetValue(ARGS, typeof(T));
        }

        // シリアル化用メソッド。
        // ISerializableに準拠するためpublicになっている。
        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ARGS, this.Args);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get => (this.Args == null) ? base.Message : $"{base.Message} ({this.Args.Message})";
        }

        public override bool Equals(object obj)
          => obj is MyException<T> other && object.Equals(this.Args, other.Args) && base.Equals(obj);

        public override int GetHashCode()
          => base.GetHashCode();
    }
}
