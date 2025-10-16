using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    readonly struct Toiban : IComparable<Toiban>, IComparable, IEquatable<Toiban>
    {
        public static readonly Toiban Empty = new Toiban(String.Empty);

        private static readonly Regex DigitsPattern = new Regex("^[0-9]+");

        public static Toiban Create(String toiban_text)
        {
            if (String.IsNullOrWhiteSpace(toiban_text))
                return Toiban.Empty;

            Assert.IsNot(DigitsPattern.IsMatch(toiban_text), $"{nameof(toiban_text)} が数値ではありません。 / {toiban_text}");
            Assert.IsNot(toiban_text.Length <= 10, $"{nameof(toiban_text)} が11文字以上です。 / {toiban_text}");

            return new Toiban(toiban_text);
        }

        public static Boolean TryCreate(String toiban_text, out Toiban toiban)
        {
            if (String.IsNullOrWhiteSpace(toiban_text))
            {
                toiban = Empty;
                return true;
            }

            toiban = default;
            if (!DigitsPattern.IsMatch(toiban_text))
                return false;
            if (toiban_text.Length > 10)
                return false;

            toiban = new Toiban(toiban_text);
            return true;
        }

        public String Text { get; }

        private Toiban(String toiban_text)
        {
            Text = toiban_text;
        }

        public Boolean IsValid()
        {
            if (String.IsNullOrWhiteSpace(Text) || Text.Length > 10)
                return false;
                
            if (!DigitsPattern.IsMatch(Text))
                return false;
            
            return true;
        }

        public Int32 Length
            => Text.Length;

        public static implicit operator String(Toiban toiban)
            => toiban.Text;
        public static implicit operator Toiban(String toiban)
            => new Toiban(toiban);

        public static Boolean operator ==(Toiban a, Toiban b)
            => a.Equals(b);
        public static Boolean operator !=(Toiban a, Toiban b)
            => !(a == b);

        public int CompareTo(object obj)
            => CompareTo((Toiban)obj);
        public int CompareTo(Toiban other)
            => Text.CompareTo(other.Text);

        public override Boolean Equals(Object obj)
            => obj != null && obj is Toiban other && Equals(other);
        public Boolean Equals(Toiban other)
        {
            if (Text == null || other.Text == null)
                return false;
                
            return Text.Equals(other.Text);
        }

        public override int GetHashCode()
            => Text.GetHashCode();
        public override string ToString()
            => Text;
    }
}
