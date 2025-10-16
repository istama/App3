//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Util
//{
//    struct Square
//    {
//        public int Top    { get; }
//        public int Bottom { get; }
//        public int Left   { get; }
//        public int Right  { get; }
//        public int Width  { get; }
//        public int Height { get; }
//        public Square(Point p, Size size)
//        {
            
//            Top    = p.Y;
//            Bottom = p.Y + size.Height;
//            Left   = p.X;
//            Right  = p.X + size.Width;
//            Width  = size.Width;
//            Height = size.Height;
//        }

//        /// <summary>
//        /// 引数のSquareと縦に並んでいるならtrueを返す。
//        /// 厳密には、どちらかのSquareの横幅が相手の横幅の半分以上、座標が重なっていればtrueを返す。
//        /// </summary>
//        public bool IsOverlappedByMoreThanHalfWidthWith(Square sq)
//        {
//            var left   = Math.Max(Left,  sq.Left);
//            var right  = Math.Min(Right, sq.Right);
//            var length = right - left;

//            return length >= Width / 2 || length >= sq.Width / 2;
//        }

//        /// <summary>
//        /// 引数のSquareと横に並んでいるならtrueを返す。
//        /// 厳密には、どちらかのSquareの高さが相手の高さの半分以上、座標が重なっていればtrueを返す。
//        /// </summary>
//        public bool IsOverlappedByMoreThanHalfHeightWith(Square sq)
//        {
//            var top    = Math.Max(Top, sq.Top);
//            var bottom = Math.Min(Bottom, sq.Bottom);
//            var length = bottom - top;

//            return length >= Height / 2 || length >= sq.Height / 2;
//        }

//        /// <summary>
//        /// 引数のSquareと重なっている部分があるならtrueを返す。
//        /// </summary>
//        public bool IsOverlappedWith(Square square, int vertical_torelance = 0, int horizontal_torelance = 0)
//        {
//            return
//                !(square.Bottom <= Top    + vertical_torelance || 
//                  square.Top    >= Bottom - vertical_torelance || 
//                  square.Right  <= Left   + horizontal_torelance || 
//                  square.Left   >= Right  - horizontal_torelance);
//        }
//        /// <summary>
//        /// 引数の座標がこのオブジェクトの座標に含まれるならtrueを返す。
//        /// </summary>
//        public bool Contains(Point p)
//        {
//            return p.X >= Left && p.X <= Right && p.Y >= Top && p.Y <= Bottom;
//        }
//        public bool Contains(int x, int y)
//            => Contains(new Point(x, y));

//        public static bool operator ==(Square a, Square b)
//            => a.Equals(b);
//        public static bool operator !=(Square a, Square b)
//            => !a.Equals(b);

//        public bool Equals(Square other)
//            => Top == other.Top && Bottom == other.Bottom && Left == other.Left && Right == other.Right;
//        public override bool Equals(object obj)
//            => obj is Square other && Equals(other);
//        public override int GetHashCode()
//            => new Point(Top, Left).GetHashCode() ^ new Size(Width, Height).GetHashCode();
//        public override string ToString()
//            => $"Top: {Top}, Bottom: {Bottom}, Left: {Left}, Right: {Right}, Width: {Width}, Height: {Height}";
//    }
//}
