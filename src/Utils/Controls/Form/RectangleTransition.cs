using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// 長方形のリストを座標とサイズを基に遷移するクラス。
    /// </summary>
    class RectangleTransition
    {
        private readonly RectangleList _rects;
        
        private readonly bool _loopable = true;

        public RectangleTransition(IEnumerable<Rectangle> rects)
        {
            _rects = new RectangleList(rects.ToList());
        }

        /// <summary>
        /// 引数のRectangleがこのこのオブジェクトの中に含まれているならtrueを返す。
        /// </summary>
        public bool Contains(Rectangle rect)
        {
            return _rects.Any(r => r == rect);
        }

        public Rectangle Up(Rectangle current_rect, RectangleTransitionAction transition_action)
            => Up(current_rect, new Rectangle[] { }, transition_action);
        public Rectangle Up(Rectangle current_rect, IEnumerable<Rectangle> rects_to_exclude, RectangleTransitionAction transition_action)
        {
            // フォーカス中のコントロールより上にあるコントロールを取得
            //var upper_squares = _squares.OfTopAbove(current_square.Top).OfBottomAbove(current_square.Bottom);            
            var upper_rects = _rects
                .Exclude(rects_to_exclude)
                .OfTopAbove(current_rect.Top)
                .OfBottomAbove(current_rect.Top + 8); // +8は許容する誤差
            if (upper_rects.Count == 0)
            {
                if (!_loopable)
                    return default;

                upper_rects = _rects.OfTopBelow(current_rect.Top).OfBottomBelow(current_rect.Bottom);
                if (upper_rects.Count == 0)
                    return default;
                
                transition_action = RectangleTransitionAction.PreferRows;
            }

            // 列を優先してフォーカスを移動
            if (transition_action == RectangleTransitionAction.PreferColumns)
            {
                // フォーカス中のSquareの垂直方向にかかっているSquareを取得
                var vertical_rects = upper_rects.OfVerticallyContainsOf(current_rect);
                // 横幅が重なっているコントロールがある場合
                if (vertical_rects.Count > 0)
                {
                    // もっとも下にあって重なっているコントロールを取得
                    return vertical_rects.OfLowermost(10).MostHorizontallyOverlappedWith(current_rect);
                }
            }

            // 行を優先して動作（デフォルトの動作）

            // 上にあるコントロールのうち最も下にあるコントロールを取得
            var lowermost_rects = upper_rects.OfLowermost(5);
            
            // 次の行の一番左に移動
            if (transition_action == RectangleTransitionAction.PreferLeft)
            {
                return lowermost_rects.OfLeftmost(0).First();
            }

            // フォーカス中のコントロールより左にあって一番右にあるコントロールにフォーカス移動
            // フォーカス中のコントロールより左にないなら、一番左にあるコントロールにフォーカス移動
            return GetTheClosestRectTo(current_rect, lowermost_rects);
        }

        public Rectangle Down(Rectangle current_rect, RectangleTransitionAction transition_action)
            => Down(current_rect, new Rectangle[] { }, transition_action);
        public Rectangle Down(Rectangle current_rect, IEnumerable<Rectangle> rects_to_exclude, RectangleTransitionAction transition_action)
        {
            // フォーカス中のコントロールより上にあるコントロールを取得
            //var lower_squares = _squares.OfTopBelow(current_square.Top).OfBottomBelow(current_square.Bottom);
            var lower_rects = _rects
                .Exclude(rects_to_exclude)
                .OfBottomBelow(current_rect.Bottom)
                .OfTopBelow(current_rect.Bottom - 8); // +8は許容する誤差
            if (lower_rects.Count == 0)
            {
                if (!_loopable)
                    return default;

                lower_rects = _rects.OfTopAbove(current_rect.Top).OfBottomAbove(current_rect.Bottom);
                if (lower_rects.Count == 0)
                    return default;

                transition_action = RectangleTransitionAction.PreferRows;
            }

            // 列を優先してフォーカスを移動
            if (transition_action == RectangleTransitionAction.PreferColumns)
            {
                // フォーカス中のSquareの垂直方向にかかっているSquareを取得
                var vertical_rects = lower_rects.OfVerticallyContainsOf(current_rect);
                // 横幅が重なっているコントロールがある場合
                if (vertical_rects.Count > 0)
                {
                    // もっとも左上にあるコントロールを取得
                    return vertical_rects.OfUppermost(10).MostHorizontallyOverlappedWith(current_rect);
                }
            }

            // 行を優先して動作（デフォルトの動作）

            // 下にあるコントロールのうち最も上にあるコントロールを取得
            var uppermost_rects = lower_rects.OfUppermost(5);

            // 次の行の一番左に移動
            if (transition_action == RectangleTransitionAction.PreferLeft)
            {
                return uppermost_rects.OfLeftmost(0).First();
            }

            // フォーカス中のコントロールより左にあって一番右にあるコントロールにフォーカス移動
            // フォーカス中のコントロールより左にないなら、一番左にあるコントロールにフォーカス移動
            return GetTheClosestRectTo(current_rect, uppermost_rects);
        }

        /// <summary>
        /// 引数の座標より左にあって最も近いコントロールを引数のリストにフォーカス移動する。
        /// 左にあるコントロールがなければ、右にあって最も近いコントロールにフォーカス移動する。
        /// </summary>
        private Rectangle GetTheClosestRectTo(Rectangle current_rect, RectangleList rect_list)
        {
            var more_left_rects = rect_list.ExceptRightOf(current_rect.Left);

            return more_left_rects.Count > 0
                ? more_left_rects.OfRightmost(0).First()
                : rect_list.OfLeftmost(0).First();
        }

        public Rectangle Left(Rectangle current_rect)
            => Left(current_rect, new Rectangle[] { });
        public Rectangle Left(Rectangle current_rect, IEnumerable<Rectangle> rects_to_exclude)
        {
            // フォーカス中のコントロールより左にあるコントロールを取得
            //var moreleft_squares = _squares.OfLeftOf(current_square.Left);
            var moreleft_rects = _rects
                .Exclude(rects_to_exclude)
                .OfLeftIsMoreLeftThan(current_rect.Left)
                .OfRightIsMoreLeftThan(current_rect.Left + 8); // +8は許容できる誤差
            if (moreleft_rects.Count == 0)
                return default;

            // フォーカス中のコントロールの水平方向にかかっているコントロールを取得
            var horizontal_rects = moreleft_rects.OfHorizontallyContainsOf(current_rect);
            if (horizontal_rects.Count == 0)
                return default;
         
            // もっとも右にあって重なっているコントロールを取得
            return horizontal_rects.OfRightmost(5).MostVerticallyOverlappedWith(current_rect);
        }

        public Rectangle Right(Rectangle current_rect)
            => Right(current_rect, new Rectangle[] { });
        public Rectangle Right(Rectangle current_rect, IEnumerable<Rectangle> rects_to_exclude)
        {
            // フォーカス中のコントロールより→にあるコントロールを取得
            //var moreright_squares = _squares.OfRightOf(current_square.Left);
            var moreright_rects = _rects
                .Exclude(rects_to_exclude)
                .OfRightIsMoreRightThan(current_rect.Right)
                .OfLeftIsMoreRightThan(current_rect.Right - 8);
            if (moreright_rects.Count == 0)
                return default;

            // フォーカス中のコントロールの水平方向にかかっているコントロールを取得
            var horizontal_rects = moreright_rects.OfHorizontallyContainsOf(current_rect);
            if (horizontal_rects.Count == 0)
                return default;
         
            // もっとも左上にあるコントロールを取得
            return horizontal_rects.OfLeftmost(5).MostVerticallyOverlappedWith(current_rect);
        }
    }

    sealed class RectangleList : List<Rectangle>
    {
        public RectangleList(IEnumerable<Rectangle> rects) : base(rects)
        {
        }

        public RectangleList Exclude(IEnumerable<Rectangle> rects)
            => rects == null ? this : new RectangleList(this.Where(r => !rects.Contains(r)));

        public RectangleList OfTopAbove(int y)
            => new RectangleList(this.FindAll(r => r.Top < y));
        public RectangleList OfBottomAbove(int y)
            => new RectangleList(this.FindAll(r => r.Bottom < y));
        public RectangleList OfTopBelow(int y)
            => new RectangleList(this.FindAll(r => r.Top > y));
        public RectangleList OfBottomBelow(int y)
            => new RectangleList(this.FindAll(r => r.Bottom > y));

        public RectangleList OfLeftIsMoreLeftThan(int x)
            => new RectangleList(this.FindAll(r => r.Left < x));
        public RectangleList OfRightIsMoreLeftThan(int x)
            => new RectangleList(this.FindAll(r => r.Right < x));
        public RectangleList OfLeftIsMoreRightThan(int x)
            => new RectangleList(this.FindAll(r => r.Left > x));
        public RectangleList OfRightIsMoreRightThan(int x)
            => new RectangleList(this.FindAll(r => r.Right > x));


        public RectangleList ExceptRightOf(int x)
            => new RectangleList(this.FindAll(r => r.Left <= x));
        public RectangleList ExceptLeftOf(int x)
            => new RectangleList(this.FindAll(r => r.Left >= x));

        public RectangleList OfUppermost(int torelance)
        {
            var y = this.Min(r => r.Top);
            return new RectangleList(this.Where(r => r.Top <= y + torelance));
        }
        public RectangleList OfLowermost(int torelance)
        {
            var y = this.Max(r => r.Top);
            return new RectangleList(this.Where(r => r.Top >= y - torelance));
        }
        public RectangleList OfLeftmost(int torelance)
        {
            var x = this.Min(r => r.Left);
            return new RectangleList(this.Where(r => r.Left <= x + torelance));
        }
        public RectangleList OfRightmost(int torelance)
        {
            var x = this.Max(r => r.Right);
            return new RectangleList(this.Where(r => r.Right >= x - torelance));
        }

        public RectangleList OfVerticallyAlignedWith(Rectangle rect)
            => new RectangleList(this.FindAll(r => r.IsOverlappedByMoreThanHalfWidthWith(rect)));
        public RectangleList OfHorizontallyAlignedWith(Rectangle rect)
            => new RectangleList(this.FindAll(r => r.IsOverlappedByMoreThanHalfHeightWith(rect)));

        /// <summary>
        /// 引数のSquareの垂直方向にかかるSquareのリストを返す。
        /// </summary>
        public RectangleList OfVerticallyContainsOf(Rectangle rect)
        {
            return new RectangleList(this.FindAll(r => 
                r.Left  >= rect.Left && r.Left  <= rect.Right || 
                r.Right >= rect.Left && r.Right <= rect.Right ||
                r.Left  <= rect.Left && r.Right >= rect.Right
                ));
        }
        /// <summary>
        /// 引数のSquareの水平方向にかかるSquareのリストを返す。
        /// </summary>
        public RectangleList OfHorizontallyContainsOf(Rectangle rect)
        {
            return new RectangleList(this.FindAll(r =>
                r.Top    >= rect.Top && r.Top    <= rect.Bottom ||
                r.Bottom >= rect.Top && r.Bottom <= rect.Bottom ||
                r.Top    <= rect.Top && r.Bottom >= rect.Bottom));
        }

        /// <summary>
        /// 引数のSquareと高さが最も重なっているSquareを返す。
        /// </summary>
        public Rectangle MostVerticallyOverlappedWith(Rectangle rect)
        {
            var overlapped_height = 0;
            Rectangle result = default;
            foreach (var r in this)
            {
                var top = Math.Max(r.Top, rect.Top);
                var bottom = Math.Min(r.Bottom, rect.Bottom);
                var height = bottom - top;
                if (height > overlapped_height)
                {
                    overlapped_height = height;
                    result = r;
                }
            }
            return result;
        }

        /// <summary>
        /// 以下の優先順位でSquareを選ぶ。
        /// ・引数のSquareと幅が半分以上重なっている
        /// ・最も左にある。
        /// </summary>
        public Rectangle MostHorizontallyOverlappedWith(Rectangle rect)
        {
            var rects = this.Where(r => rect.IsOverlappedByMoreThanHalfWidthWith(r)).ToList();
            if (rects.Count == 0)
                rects = this;

            var result = rects[0];
            foreach (var r in rects.Skip(1))
            {
                if (r.Left < result.Left)
                    result = r;
            }

            return result;
        }

    }
}
