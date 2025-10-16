//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Util
//{
//    // TODO
//    // コードを以下のコードに分離する
//    //  ・コントロールの座標と現在フォーカス中のコントロールの座標を取得するコード
//    //  ・コントロールの座標リストの中から次にフォーカスが移動するコントロールの座標を見つけ出すコード
//    //  ・フォーカスを移動するコード
//    //
//    // 現在のフォーカス位置から開いているタブ名を取得できるか？
//    //  タブを開いた瞬間のフォーカスはどこにあるか調べる
//    //    フォーカスのTextがタブ名か？
//    //    フォーカスの親ウィンドウがタブ名か？
//    //  タブのコントロールをSubWindow.ClassName()で識別できるか？


//    /// <summary>
//    /// フォーカス移動の動作を指定するための列挙値。
//    /// </summary>
//    enum FocusTransitionAction
//    {
//        // 縦移動のとき列を優先して移動する。 
//        PreferColumns,  
//        // 縦移動のとき行を優先して移動する。
//        PreferRows,
//        // 縦移動のとき行の一番左へ移動する。
//        PreferLeft
//    }

//    /// <summary>
//    /// 指定されたウィンドウ内のフォーカスを上下左右を表すメソッドで移動させるクラス。
//    /// あるコントロールから次の移動先となるコントロールを自動で割り出す。
//    /// </summary>
//    class SubWindowFocusTransition
//    {
//        private readonly string _window_title_pattern;
//        private readonly bool _loopable;
//        private readonly int   _x_limit;
//        private readonly int   _y_limit;

//        private WindowController _window;
//        private SubWindowList _sub_windows_all;
//        private Dictionary<string, SubWindowList> _focusable_sub_windows_each_parent_text = new Dictionary<string, SubWindowList>();
//        private SubWindowList _tab_sub_windows;

//        public SubWindowFocusTransition(string window_title_pattern, bool loopable=false) : this(window_title_pattern, 99999, 99999, false) {}
//        public SubWindowFocusTransition(string window_title_pattern, int x_limit, int y_limit, bool loopable=false)
//        {
//            _window_title_pattern = window_title_pattern;
//            _loopable = loopable;
//            _x_limit  = x_limit;
//            _y_limit  = y_limit;
//        }

//        private WindowController GetWindow()
//        {
//            if (_window == null)
//            {
//                var windows = WindowController.FindAll(_window_title_pattern, 0);
//                if (windows.Count == 0)
//                    throw new InvalidOperationException($"{_window_title_pattern} に該当するウィンドウが見つかりません。");
//                if (windows.Count > 1)
//                    throw new InvalidOperationException($"{_window_title_pattern} に該当するウィンドウが複数あります。");
                
//                _window = windows[0];
//            }
//            return _window;
//        }

//        private SubWindowList GetSubWindowsAll(bool reload=false)
//        {
//            if (_sub_windows_all == null || reload)
//            {
//                var window = GetWindow();
//                var sub_windows = window.GetSubWindows().Where(sw => 
//                {
//                    var square = sw.GetSquare();
//                    return square.Left <= _x_limit && square.Top <= _y_limit;
//                });
//                _sub_windows_all = new SubWindowList(sub_windows);
//            }
//            return _sub_windows_all;
//        }

//        /// <summary>
//        /// フォーカス可能なコントロールのリストを取得する。
//        /// </summary>
//        private SubWindowList GetFocusableSubWindowList(string parent_text_pattern, params string[] ng_words)
//        {
//            if (!_focusable_sub_windows_each_parent_text.ContainsKey(parent_text_pattern))
//            {
//                var window = GetWindow();
//                var sub_windows = string.IsNullOrEmpty(parent_text_pattern)
//                    ? GetSubWindowsAll(true).ToList()
//                    : GetSubWindowsAll(true).Where(sw => sw.IsTextMatchingInParents(parent_text_pattern)).ToList();
                    
//                var focusable_sub_windows = sub_windows
//                    .Where(sw =>
//                    {
//                        // 無効なコントロールは除外する
//                        if (!sw.Enabled())
//                            return false;

//                        // 以下のコントロールクラス以外のコントロールは除外する
//                        var className = sw.GetClassName();
//                        if (className != nameof(System.Windows.Forms.Button) &&
//                            className != nameof(System.Windows.Forms.TextBox) &&
//                            className != nameof(System.Windows.Forms.ComboBox))
//                            return false;

//                        // NGワードが含まれるテキストを持つコントロールも除外する
//                        var sw_text = sw.GetText();
//                        foreach (var word in ng_words)
//                        {
//                            var regex = new Regex(word);
//                            if (regex.IsMatch(sw_text))
//                                return false;
//                        }
//                        return true;
//                    }).ToList();
                    
//                if (focusable_sub_windows.Count == 0)
//                    return new SubWindowList(new List<SubWindow>());

//                _focusable_sub_windows_each_parent_text.Add(parent_text_pattern, new SubWindowList(focusable_sub_windows));
//            }

//            return _focusable_sub_windows_each_parent_text[parent_text_pattern];
//        }

//        private List<SubWindow> GetTabSubWindows()
//        {
//            if (_tab_sub_windows == null || _tab_sub_windows.Count == 0)
//            {
//                _tab_sub_windows = new SubWindowList(GetSubWindowsAll().Where(sw => sw.GetClassName() == nameof(System.Windows.Forms.TabControl)));
//            }
//            return _tab_sub_windows;
//        }

//        /// <summary>
//        /// フォーカス中のコントロールを取得する。
//        /// </summary>
//        private SubWindow GetFocusedSubWindow(string parent_text_pattern, params string[] ng_words)
//        {
//            var sub_windows = GetFocusableSubWindowList(parent_text_pattern, ng_words);
//            var focused = sub_windows.FirstOrDefault(sw => sw.IsFocused());
//            if (focused != default)
//                return focused;

//            if (string.IsNullOrEmpty(parent_text_pattern))
//                return default;
                
//            var tab_sub_windows = GetTabSubWindows();
//            return tab_sub_windows.FirstOrDefault(sw => sw.IsFocused());
//        }

//        /// <summary>
//        /// フォーカス中のコントロールの座標とサイズを取得する。
//        /// </summary>
//        private Square GetFocusedSubWindowSquare(string parent_text_pattern, params string[] ng_words)
//        {
//            var focused_sub_window = GetFocusedSubWindow(parent_text_pattern, ng_words);
//            if (focused_sub_window == default)
//                return default;

//            var class_name = focused_sub_window.GetClassName();
//            // フォーカスの位置がタブでない場合
//            if (class_name != nameof(System.Windows.Forms.TabControl))
//                return focused_sub_window.GetSquare();

//            // タブにフォーカスがあるとタブのウィンドウ全体がサイズになってしまうため、幅と高さを修正する
//            var sq = focused_sub_window.GetSquare();
//            var p = new Point(sq.Left, sq.Top);
//            return new Square(p, new Size(1, 1));
//        }

//        /// <summary>
//        /// 現在フォーカス中のコントロールの上にあるコントロールにフォーカスを移動する。
//        /// parent_text_patternを指定すると、このパターンを持つコントロールを親に持つコントロールのみが対象となる。
//        /// ng_wordsを指定すると、このワードを持つコントロールは対象外となる。
//        /// </summary>
//        public void Up(FocusTransitionAction action, string parent_text_pattern = "", params string[] ng_words)
//        {
//            var sub_windows = GetFocusableSubWindowList(parent_text_pattern, ng_words);
//            var f_square = GetFocusedSubWindowSquare(parent_text_pattern, ng_words);

//            // フォーカス中のコントロールより上にあるコントロールを取得
//            var upper_sub_windows = sub_windows.OfBottomAbove(f_square.Top);            
//            if (upper_sub_windows.Count == 0)
//            {
//                if (!_loopable)
//                    return;

//                upper_sub_windows = sub_windows.OfTopBelow(f_square.Bottom);
//                if (upper_sub_windows.Count == 0)
//                    return;
                
//                action = FocusTransitionAction.PreferRows;
//            }

//            // 列を優先してフォーカスを移動
//            if (action == FocusTransitionAction.PreferColumns)
//            {
//                // 横幅がフォーカス中のコントロールの横幅と半分以上重なっているコントロールを取得
//                var vertical_sub_windows = upper_sub_windows.OfVerticallyAlignedWith(f_square);
//                // 横幅が重なっているコントロールがある場合
//                if (vertical_sub_windows.Count > 0)
//                {
//                    // もっとも左下にあるコントロールを取得
//                    var most_low_and_left_sub_window = vertical_sub_windows.OfLowermost(0).OfLeftmost(0).First();
//                    most_low_and_left_sub_window.Focus();
//                    return;
//                }
//            }

//            // 行を優先して動作（デフォルトの動作）

//            // 上にあるコントロールのうち最も下にあるコントロールを取得
//            var lowermost_sub_windows = upper_sub_windows.OfLowermost(5);
            
//            // 次の行の一番左に移動
//            if (action == FocusTransitionAction.PreferLeft)
//            {
//                var leftmost_sub_window = lowermost_sub_windows.OfLeftmost(0).First();
//                leftmost_sub_window.Focus();
//                return;
//            }

//            // フォーカス中のコントロールより左にあって一番右にあるコントロールにフォーカス移動
//            // フォーカス中のコントロールより左にないなら、一番左にあるコントロールにフォーカス移動
//            FocusTheClosestSubWindowTo(f_square, lowermost_sub_windows);
//        }

//        public void Down(FocusTransitionAction action, string parent_text_pattern = "", params string[] ng_words)
//        {
//            var sub_windows = GetFocusableSubWindowList(parent_text_pattern, ng_words);
//            var f_square = GetFocusedSubWindowSquare(parent_text_pattern, ng_words);

//            // フォーカス中のコントロールより下にあるコントロールを取得
//            var lower_sub_windows = sub_windows.OfTopBelow(f_square.Bottom);
//            if (lower_sub_windows.Count == 0)
//            {
//                if (!_loopable)
//                    return;

//                lower_sub_windows = sub_windows.OfBottomAbove(f_square.Top);
//                if (lower_sub_windows.Count == 0)
//                    return;

//                action = FocusTransitionAction.PreferRows;
//            }

//            if (action == FocusTransitionAction.PreferColumns)
//            {
//                // 横幅がフォーカス中のコントロールの横幅と半分以上重なっているコントロールを取得
//                var vertical_sub_windows = lower_sub_windows.OfVerticallyAlignedWith(f_square);
//                // 横幅が重なっているコントロールがある場合
//                if (vertical_sub_windows.Count > 0)
//                {
//                    // もっとも左上にあるコントロールを取得
//                    var most_top_and_left_sub_window = vertical_sub_windows.OfUppermost(0).OfLeftmost(0).First();
//                    most_top_and_left_sub_window.Focus();
//                    return;
//                }
//            }

//            // 行を優先して動作（デフォルトの動作）

//            // 下にあるコントロールのうち最も上にあるコントロールを取得
//            var uppermost_sub_windows = lower_sub_windows.OfUppermost(5);

//            // 次の行の一番左に移動
//            if (action == FocusTransitionAction.PreferLeft)
//            {
//                var most_left_sub_windows = uppermost_sub_windows.OfLeftmost(0).First();
//                most_left_sub_windows.Focus();
//                return;
//            }

//            // フォーカス中のコントロールより左にあって一番右にあるコントロールにフォーカス移動
//            // フォーカス中のコントロールより左にないなら、一番左にあるコントロールにフォーカス移動
//            FocusTheClosestSubWindowTo(f_square, uppermost_sub_windows);
//        }

//        /// <summary>
//        /// 引数の座標より左にあって最も近いコントロールを引数のリストにフォーカス移動する。
//        /// 左にあるコントロールがなければ、右にあって最も近いコントロールにフォーカス移動する。
//        /// </summary>
//        private void FocusTheClosestSubWindowTo(Square square, SubWindowList sub_window_list)
//        {
//            var more_left_sub_windows = sub_window_list.ExceptRightOf(square.Left);

//            var target = more_left_sub_windows.Count > 0
//                ? more_left_sub_windows.OfRightmost(0).First()
//                : sub_window_list.OfLeftmost(0).First();

//            target.Focus();
//        }

//        public void Left(string parent_text_pattern = "", params string[] ng_words)
//        {
//            var sub_windows = GetFocusableSubWindowList(parent_text_pattern, ng_words);
            
//            var focused_sub_window = GetFocusedSubWindow(parent_text_pattern, ng_words);
//            if (focused_sub_window == default)
//                return;
                
//            var f_square = focused_sub_window.GetSquare();

//            // フォーカス中のコントロールより左にあるコントロールを取得
//            var moreleft_sub_windows = sub_windows.OfLeftOf(f_square.Left);

//            // 高さがフォーカス中のコントロールの高さと半分以上重なっているコントロールを取得
//            var horizontal_sub_windows = moreleft_sub_windows.OfHorizontallyAlignedWith(f_square);

//            // 高さが重なっているコントロールがある場合
//            if (horizontal_sub_windows.Count > 0)
//            {
//                // もっとも右上にあるコントロールを取得
//                var target = horizontal_sub_windows.OfRightmost(0).OfUppermost(0).First();
//                target.Focus();
//            }
//        }

//        public void Right(string parent_text_pattern = "", params string[] ng_words)
//        {
//            var sub_windows = GetFocusableSubWindowList(parent_text_pattern, ng_words);
            
//            var focused_sub_window = GetFocusedSubWindow(parent_text_pattern, ng_words);
//            if (focused_sub_window == default)
//                return;

//            var f_square = focused_sub_window.GetSquare();

//            // フォーカス中のコントロールより右にあるコントロールを取得
//            var moreleft_sub_windows = sub_windows.OfRightOf(f_square.Right);

//            // 高さがフォーカス中のコントロールの高さと半分以上重なっているコントロールを取得
//            var horizontal_sub_windows = moreleft_sub_windows.OfHorizontallyAlignedWith(f_square);

//            // 高さが重なっているコントロールがある場合
//            if (horizontal_sub_windows.Count > 0)
//            {
//                // もっとも左上にあるコントロールを取得
//                var target = horizontal_sub_windows.OfLeftmost(0).OfUppermost(0).First();
//                target.Focus();
//            }
//        }

//        /// <summary>
//        /// 取得したウィンドウ情報のバッファをリセットする。
//        /// バッファを利用することで速度が上がるが、操作対象のウィンドウを閉じたりするとバッファは使い物にならなくなる。
//        /// 定期的にリセットしてウィンドウ情報を読み込みなおした方がよい。
//        /// </summary>
//        public void ResetBuffer()
//        {
//            _window = null;
//            _sub_windows_all = null;
//            _focusable_sub_windows_each_parent_text.Clear();
//            _tab_sub_windows = null;
//        }
//    }

//    sealed class SubWindowList : List<SubWindow>
//    {
//        public SubWindowList(IEnumerable<SubWindow> sub_windows) : base(sub_windows)
//        {
//        }
//        public SubWindowList OfBottomAbove(int y)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().Bottom < y));
//        public SubWindowList OfTopBelow(int y)
//            => new SubWindowList(this.FindAll(sw =>  sw.GetSquare().Top > y));

//        public SubWindowList OfLeftOf(int x)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().Left < x));
//        public SubWindowList OfRightOf(int x)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().Left > x));

//        public SubWindowList ExceptRightOf(int x)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().Left <= x));
//        public SubWindowList ExceptLeftOf(int x)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().Left >= x));

//        public SubWindowList OfUppermost(int torelance)
//        {
//            var y = this.Min(sw => sw.GetRelativePoint().Y);
//            return new SubWindowList(this.Where(sw => sw.GetRelativePoint().Y <= y + torelance));
//        }
//        public SubWindowList OfLowermost(int torelance)
//        {
//            var y = this.Max(sw => sw.GetRelativePoint().Y);
//            return new SubWindowList(this.Where(sw => sw.GetRelativePoint().Y >= y - torelance));
//        }
//        public SubWindowList OfLeftmost(int torelance)
//        {
//            var x = this.Min(sw => sw.GetRelativePoint().X);
//            return new SubWindowList(this.Where(sw => sw.GetRelativePoint().X <= x + torelance));
//        }
//        public SubWindowList OfRightmost(int torelance)
//        {
//            var x = this.Max(sw => sw.GetRelativePoint().X);
//            return new SubWindowList(this.Where(sw => sw.GetSquare().Right >= x - torelance));
//        }

//        public SubWindowList OfVerticallyAlignedWith(Square square)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().IsOverlappedByMoreThanHalfWidthWith(square)));
//        public SubWindowList OfHorizontallyAlignedWith(Square square)
//            => new SubWindowList(this.FindAll(sw => sw.GetSquare().IsOverlappedByMoreThanHalfHeightWith(square)));
//    }
//}
