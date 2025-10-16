//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Util
//{
//    /// <summary>
//    /// 指定されたウィンドウ内のフォーカスを、登録した座標のコントロールに移動させるクラス。
//    /// 前後を表すメソッドで、現在のフォーカスの位置から最も近い登録した座標のコントロールにフォーカスを移動する。
//    /// </summary>
//    class SubWindowFocusJumper
//    {
//        private readonly string _window_title_pattern;
//        // 登録する座標の順番が複数列になるなら、複数のリストに分けて格納する
//        private readonly List<List<Rectangle>> _sub_window_rects_list = new List<List<Rectangle>>();
//        // 座標の順番を２列に分けた時の境目のx座標
//        private int _separator_x_point;

//        public SubWindowFocusJumper(string window_title_pattern, params Rectangle[] sub_window_rects)
//        {
//            _window_title_pattern = window_title_pattern;
//            _sub_window_rects_list.Add(sub_window_rects.ToList());
//            _separator_x_point = 9999;
//        }
//        // 登録する座標の順番が複数列になるなら、複数のリストに分けて格納する
//        public SubWindowFocusJumper(string window_title_pattern, int separator_x_point, params IEnumerable<Rectangle>[] sub_window_rects_list)
//        {
//            _window_title_pattern = window_title_pattern;
//            foreach (var rects in sub_window_rects_list)
//                _sub_window_rects_list.Add(rects.ToList());
//            _separator_x_point = separator_x_point;
//        }

//        private WindowController GetWindow()
//        {
//            var windows = WindowController.FindAll(_window_title_pattern, 0);
//            if (windows.Count == 0)
//                throw new InvalidOperationException($"{_window_title_pattern} に該当するウィンドウが見つかりません。");
//            if (windows.Count > 1)
//                throw new InvalidOperationException($"{_window_title_pattern} に該当するウィンドウが複数あります。");

//            return windows[0];
//        }

//        /// <summary>
//        /// フォーカス中のコントロールを取得する。
//        /// </summary>
//        private SubWindow GetFocusedSubWindow()
//        {
//            var window = GetWindow();
//            var sub_windows = window.GetSubWindows();
//            return sub_windows.FirstOrDefault(sw => sw.IsFocused());
//        }

//        private SubWindow GetSubWindow(Rectangle r, string parent_text_pattern)
//        {
//            var window = GetWindow();
//            return window.GetSubWindows(r, sw => sw.IsTextMatchingInParents(parent_text_pattern)).FirstOrDefault();
//        }

//        public void Jump(int idx, string parent_text_pattern = "")
//        {
//            if (idx < 0)
//                throw new IndexOutOfRangeException();

//            var i = idx;
//            foreach (var rects in _sub_window_rects_list)
//            {
//                if (i < rects.Count)
//                {
//                    FocusTo(rects[i], parent_text_pattern);
//                    return;
//                }

//                i -= rects.Count;
//            }
//        }

//        public void Prev(string parent_text_pattern="")
//        {
//            var focused_sub_window = GetFocusedSubWindow();
//            var f_square = focused_sub_window.GetSquare();
//            var col_idx = GetRectColumnIndex(f_square.Left);

//            // フォーカス中のコントロールより上にあるコントロールを取得
//            var rects = _sub_window_rects_list[col_idx];
//            var upper_rects = rects.Where(r =>
//                r.Top  < f_square.Top ||
//                r.Top == f_square.Top && r.Left < f_square.Left) // 自分自身のRectangleを含めないようにするため
//                .ToList();
//            if (upper_rects.Count > 0)
//            {
//                // 上にあるコントロールの中で一番下にあるコントロールを取得
//                var lowest_rects = MaxOf(upper_rects, r => r.Bottom);
//                if (lowest_rects.Count == 1)
//                {
//                    FocusTo(lowest_rects[0], parent_text_pattern);
//                }
//                else
//                {
//                    var rect = GetTheClosestRectTo(f_square, lowest_rects);
//                    FocusTo(rect, parent_text_pattern);
//                }
//            }
//            // フォーカス中のコントロールより上にあるコントロールが１つもない場合
//            else
//            {
//                // 一番後ろのコントロールにフォーカスを移動する
//                var rect = rects.LastOrDefault();
//                if (rect != default)
//                    FocusTo(rect, parent_text_pattern);
//            }
//        }

//        public void Next(string parent_text_pattern="")
//        {
//            var focused_sub_window = GetFocusedSubWindow();
//            var f_square = focused_sub_window.GetSquare();
//            var col_idx = GetRectColumnIndex(f_square.Left);

//            // フォーカス中のコントロールより下にあるコントロールを取得
//            var rects = _sub_window_rects_list[col_idx];
//            var lower_rects = rects.Where(r => r.Top > f_square.Top).ToList();
//            if (lower_rects.Count > 0)
//            {
//                // 下にあるコントロールの中で一番上にあるコントロールを取得
//                var most_top_rects = MinOf(lower_rects, r => r.Top);
//                if (most_top_rects.Count == 1)
//                {
//                    FocusTo(most_top_rects[0], parent_text_pattern);
//                }
//                else
//                {
//                    var rect = GetTheClosestRectTo(f_square, most_top_rects);
//                    FocusTo(rect, parent_text_pattern);
//                }
//            }
//            // フォーカス中のコントロールより下にあるコントロールが１つもない場合
//            else
//            {
//                // 次のリストの一番前のコントロールにフォーカスを移動する
//                var rect = rects.FirstOrDefault();
//                if (rect != default)
//                    FocusTo(rect, parent_text_pattern);
//            }
//        }

//        /// <summary>
//        /// 指定したRectangleのリストのうち、指定したSquareより左にあって最も近いRectangleを取得する。
//        /// 左にあるRectangleがない場合は、右にあるRectangleの中で最も近いものを取得する。
//        /// </summary>
//        private Rectangle GetTheClosestRectTo(Square square, List<Rectangle> rects)
//        {
//            var moreleft_rects = rects.Where(r => r.Left <= square.Left).ToList();
//            if (moreleft_rects.Count > 0)
//            {
//                var mostright_rects = MaxOf(moreleft_rects, r => r.Left);
//                return mostright_rects[0];
//            }
//            else
//            {
//                var mostleft_rects = MinOf(rects, r => r.Left);
//                return mostleft_rects[0];
//            }
//        }

//        public void Right(string parent_text_pattern = "")
//        {
//            var focused_sub_window = GetFocusedSubWindow();
//            var f_square = focused_sub_window.GetSquare();
//            var col_idx = GetRectColumnIndex(f_square.Left);

//            var right_col_idx = (col_idx + 1) % _sub_window_rects_list.Count;

//            // フォーカス中のコントロール以下にあるコントロールを取得
//            var rects = _sub_window_rects_list[right_col_idx];
//            var lower_rects = rects.Where(r => r.Top >= f_square.Top).ToList();
//            if (lower_rects.Count > 0)
//            {
//                // 以下にあるコントロールの中で一番上にあるコントロールを取得
//                var most_top_rects = MinOf(lower_rects, r => r.Top);
//                if (most_top_rects.Count == 1)
//                {
//                    FocusTo(most_top_rects[0], parent_text_pattern);
//                }
//                else
//                {
//                    var most_left_rects = MinOf(most_top_rects, r => r.Left);
//                    FocusTo(most_left_rects[0], parent_text_pattern);
//                }
//            }
//            // フォーカス中のコントロール以下にあるコントロールが１つもない場合
//            else
//            {
//                // 一番下にあるコントロールに移動する
//                var lowest_rects = MaxOf(rects, r => r.Top);
//                var most_left_rects = MinOf(lower_rects, r => r.Left);
//                FocusTo(most_left_rects[0], parent_text_pattern);
//            }
//        }

//        public void Left(string parent_text_pattern = "")
//        {
//            var focused_sub_window = GetFocusedSubWindow();
//            var f_square = focused_sub_window.GetSquare();
//            var col_idx = GetRectColumnIndex(f_square.Left);

//            var left_col_idx = (col_idx - 1 + _sub_window_rects_list.Count) % _sub_window_rects_list.Count;

//            // フォーカス中のコントロール以下にあるコントロールを取得
//            var rects = _sub_window_rects_list[left_col_idx];
//            var lower_rects = rects.Where(r => r.Top >= f_square.Top).ToList();
//            if (lower_rects.Count > 0)
//            {
//                // 以下にあるコントロールの中で一番上にあるコントロールを取得
//                var most_top_rects = MinOf(lower_rects, r => r.Top);
//                if (most_top_rects.Count == 1)
//                {
//                    FocusTo(most_top_rects[0], parent_text_pattern);
//                }
//                else
//                {
//                    var most_right_rects = MaxOf(most_top_rects, r => r.Left);
//                    FocusTo(most_right_rects[0], parent_text_pattern);
//                }
//            }
//            // フォーカス中のコントロール以下にあるコントロールが１つもない場合
//            else
//            {
//                // 一番下にあるコントロールに移動する
//                var lowest_rects = MaxOf(rects, r => r.Top);
//                var most_right_rects = MaxOf(lower_rects, r => r.Left);
//                FocusTo(most_right_rects[0], parent_text_pattern);
//            }
//        }

//        /// <summary>
//        /// 指定されたx座標が、登録されたRectangleリストの何列目にあたるかのインデックスを返す。
//        /// </summary>
//        private int GetRectColumnIndex(int x)
//        {
//            return x < _separator_x_point ? 0 : 1;
//        }

//        private List<Rectangle> MaxOf(List<Rectangle> rects, Func<Rectangle, int> f_field)
//        {
//            var max = rects.Max(f_field);
//            return rects.Where(r => f_field(r) == max).ToList();
//        }

//        private List<Rectangle> MinOf(List<Rectangle> rects, Func<Rectangle, int> f_field)
//        {
//            var min = rects.Min(f_field);
//            return rects.Where(r => f_field(r) == min).ToList();
//        }

//        private void FocusTo(Rectangle target_rect, string parent_text_pattern)
//        {
//            var target = GetSubWindow(target_rect, parent_text_pattern);
//            if (target != default)
//                target.Focus();
//        }

//        /// <summary>
//        /// 座標を追加する。
//        /// </summary>
//        public void Add(int col, int row, Rectangle rect)
//        {
//            if (col < 0 || col > _sub_window_rects_list.Count)
//                throw new IndexOutOfRangeException();
//            else if (col == _sub_window_rects_list.Count && row > 0)
//                throw new IndexOutOfRangeException();
//            else if (col < _sub_window_rects_list.Count && row > _sub_window_rects_list[col].Count)
//                throw new IndexOutOfRangeException();

//            if (col == _sub_window_rects_list.Count)
//                _sub_window_rects_list.Add(new List<Rectangle>());

//            var rects = _sub_window_rects_list[col];
//            rects.Insert(row, rect);
//        }

//        /// <summary>
//        /// 座標を指定した座標に入れ替える。
//        /// </summary>
//        public void Replace(params Rectangle[] sub_window_rects)
//        {
//            _sub_window_rects_list.Clear();
//            _sub_window_rects_list.Add(sub_window_rects.ToList());
//            _separator_x_point = 9999;
//        }
//        /// <summary>
//        /// 座標を指定した座標に入れ替える。
//        /// </summary>
//        public void Replace(int separator_x_point, params IEnumerable<Rectangle>[] sub_window_rects_list)
//        {
//            _sub_window_rects_list.Clear();
//            foreach (var rects in sub_window_rects_list)
//                _sub_window_rects_list.Add(rects.ToList());
//            _separator_x_point = separator_x_point;
//        }
//    }
//}
