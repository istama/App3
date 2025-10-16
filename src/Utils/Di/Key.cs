using System;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// キー付きサービスを識別するためのキーを表します。
    /// </summary>
    public readonly struct Key : IEquatable<Key>
    {
        /// <summary>
        /// 空のキーを表します。キーなしサービスに使用されます。
        /// </summary>
        public static readonly Key Empty = new Key(null);

        private readonly string _value;

        /// <summary>
        /// 指定された値で <see cref="Key"/> 構造体の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">キーの値。</param>
        public Key(string value)
        {
            _value = value;
        }

        /// <summary>
        /// 現在のインスタンスが別の <see cref="Key"/> インスタンスと等しいかどうかを判断します。
        /// </summary>
        /// <param name="other">比較対象の <see cref="Key"/> インスタンス。</param>
        /// <returns>2つのインスタンスが等しい場合は <c>true</c>、それ以外の場合は <c>false</c>。</returns>
        public bool Equals(Key other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// 現在のインスタンスが別のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト。</param>
        /// <returns>オブジェクトが <see cref="Key"/> であり、現在のインスタンスと等しい場合は <c>true</c>、それ以外の場合は <c>false</c>。</returns>
        public override bool Equals(object obj)
        {
            return obj is Key other && Equals(other);
        }

        /// <summary>
        /// このインスタンスのハッシュコードを返します。
        /// </summary>
        /// <returns>このインスタンスのハッシュコード。</returns>
        public override int GetHashCode()
        {
            return _value != null ? _value.GetHashCode() : 0;
        }

        /// <summary>
        /// このインスタンスの文字列表現を返します。
        /// </summary>
        /// <returns>このインスタンスの文字列表現。</returns>
        public override string ToString()
        {
            return _value;
        }

        /// <summary>
        /// 2つの <see cref="Key"/> インスタンスが等しいかどうかを判断します。
        /// </summary>
        /// <param name="left">1番目の <see cref="Key"/> インスタンス。</param>
        /// <param name="right">2番目の <see cref="Key"/> インスタンス。</param>
        /// <returns>2つのインスタンスが等しい場合は <c>true</c>、それ以外の場合は <c>false</c>。</returns>
        public static bool operator ==(Key left, Key right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 2つの <see cref="Key"/> インスタンスが等しくないかどうかを判断します。
        /// </summary>
        /// <param name="left">1番目の <see cref="Key"/> インスタンス。</param>
        /// <param name="right">2番目の <see cref="Key"/> インスタンス。</param>
        /// <returns>2つのインスタンスが等しくない場合は <c>true</c>、それ以外の場合は <c>false</c>。</returns>
        public static bool operator !=(Key left, Key right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// 文字列を <see cref="Key"/> に暗黙的に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        public static implicit operator Key(string value)
        {
            return new Key(value);
        }
    }
}