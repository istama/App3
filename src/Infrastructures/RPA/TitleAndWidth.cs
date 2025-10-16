using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    /// <summary>
    /// NativeWindowのファクトリクラスのバッファのキーとして使うクラス。
    /// </summary>
    struct TitleAndWidth : IEquatable<TitleAndWidth>
    {
        public string Title { get; }
        public int Width { get; }

        public TitleAndWidth(string title, int width)
        {
            Title = title;
            Width = width;
        }

        public static bool operator==(TitleAndWidth a, TitleAndWidth b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(TitleAndWidth a, TitleAndWidth b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is TitleAndWidth other && Equals(other);
        }
        
        public bool Equals(TitleAndWidth other)
        {
            return Title == other.Title && Width == other.Width;
        }
        
        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Width.GetHashCode();
        }
        
        public override string ToString()
        {
            return $"Title:{Title} Width:{Width}";
        }
    }
}
