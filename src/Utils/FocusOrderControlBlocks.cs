using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    /// <summary>
    /// このクラスのオブジェクトにControlクラスを配置していくことで、
    /// Control上でカーソルキーを押したとき、次のフォーカスを得るControlを自動で決めてくれるクラス。
    /// Controlの配置は行と幅を指定することで行う。
    /// 左右のカーソルが押されたときは、同じ行の隣のControlにフォーカスが移り、
    /// 上下のカーソルが押されたときは、指定された幅をもとに前後の行の真上・真下にあるControlにフォーカスが移る。
    /// </summary>
    sealed class FocusOrderControlBlocks
    {
        private class Node
        {
            public System.Windows.Forms.Control Control    { get; }
            public Int32   IndexX     { get; }
            public Int32   LastIndexX { get; }
            public Node    Up         { get; set; } = null;
            public Node    Down       { get; set; } = null;
            public Node    Left       { get; set; } = null;
            public Node    Right      { get; set; } = null;
            public Node(System.Windows.Forms.Control control, Int32 idx_x, Int32 width)
            {
                Control    = control;
                IndexX     = idx_x;
                LastIndexX = IndexX + width - 1;
            }
        }

        // 各行の左端に配置されているControlのリスト
        private readonly List<Node> _most_left_control_each_rows = new List<Node>();

        private readonly Boolean _is_loop_x;
        private readonly Boolean _is_loop_y;

        private Boolean _built = false;

        public FocusOrderControlBlocks(Boolean is_loop_x, Boolean is_loop_y)
        {
            _is_loop_x = is_loop_x;
            _is_loop_y = is_loop_y;
        }

        /// <summary>
        /// 新しい行にControlを追加する。
        /// Controlの幅も指定する。
        /// </summary>
        public FocusOrderControlBlocks AddRow(System.Windows.Forms.Control control, Int32 width)
        {
            var new_node = new Node(control, 0, width);
            _most_left_control_each_rows.Add(new_node);

            if (_is_loop_x)
            {
                new_node.Left  = new_node;
                new_node.Right = new_node;
            }

            _built = false;

            return this;
        }

        /// <summary>
        /// 最後の行の右端にControlを追加する。
        /// Controlの幅も指定する。
        /// </summary>
        public FocusOrderControlBlocks AddColumn(System.Windows.Forms.Control control, Int32 width)
        {
            var last_idx = _most_left_control_each_rows.Count - 1;
            if (last_idx < 0)
                return AddRow(control, width);

            // 左端のノードを取得する
            var left_node = _most_left_control_each_rows[last_idx];
            // 同じ行の右端のノードを取得する
            var next_node = left_node;
            while (next_node.Right != null && next_node.Right != left_node)
            {
                next_node = next_node.Right;
            }
            // 右端のノードと新しいノードをつなぐ
            var new_node = new Node(control, next_node.LastIndexX + 1, width);
            next_node.Right = new_node;
            new_node.Left   = next_node;
            // カーソル移動をループさせるなら、左端のノードと新しいノードをつなぐ
            if (_is_loop_x)
            {
                new_node.Right = left_node;
                left_node.Left = new_node;
            }

            _built = false;

            return this;
        }

        /// <summary>
        /// Controlのフォーカスの経路を構築する。
        /// Controlをすべて追加した後で必ず実行すること。
        /// </summary>
        public void Build()
        {
            for (var idx = 0; idx < _most_left_control_each_rows.Count - 1; idx++)
            {
                var up_node_row   = _most_left_control_each_rows[idx];
                var down_node_row = _most_left_control_each_rows[idx + 1];

                ChainNodeFromUpToDown(up_node_row, down_node_row);
                ChainNodeFromDownToUp(down_node_row, up_node_row);
            }

            if (_is_loop_y && _most_left_control_each_rows.Count > 0)
            {
                var top_row    = _most_left_control_each_rows[0];
                var bottom_row = _most_left_control_each_rows[_most_left_control_each_rows.Count - 1];

                ChainNodeFromUpToDown(bottom_row, top_row);
                ChainNodeFromDownToUp(top_row, bottom_row);
            }

            _built = true;
        }

        /// <summary>
        /// 上のノードが下のノードの参照を保持する。
        /// </summary>
        private void ChainNodeFromUpToDown(Node first_up_node, Node first_down_node)
        {
            var up_next   = first_up_node;
            var down_next = first_down_node;
            while (up_next != null)
            {
                if (up_next.IndexX <= down_next.LastIndexX)
                {
                    up_next.Down = down_next;
                }
                else
                {
                    var tmp = down_next.Right;
                    if (tmp != null && tmp != first_down_node)
                        down_next = tmp;

                    up_next.Down = down_next;
                }
                up_next = up_next.Right;

                if (up_next == first_up_node)
                    break;
            }
        }

        private void ChainNodeFromDownToUp(Node first_down_node, Node first_up_node)
        {
            var down_next = first_down_node;
            var up_next   = first_up_node;
            while (down_next != null)
            {
                if (down_next.IndexX <= up_next.LastIndexX)
                {
                    down_next.Up = up_next;
                }
                else
                {
                    var tmp = up_next.Right;
                    if (tmp != null && tmp != first_up_node)
                        up_next = tmp;

                    down_next.Up = up_next;
                }
                down_next = down_next.Right;

                if (down_next == first_down_node)
                    break;
            }
        }

        /// <summary>
        /// 渡された方向キーの方向にあるControlにフォーカスを移動する。
        /// フォーカスが移動したらtrueを返す。
        /// このメソッドを最初に呼び出す前に必ずBuild()メソッドを呼び出しておくこと。
        /// </summary>
        public Boolean FocusNext(Keys key_code)
        {
            if (!_built)
                throw new InvalidOperationException("まだBuild()が実行されていません。");

            if (TryGetFocusedNode(out var focused_node))
            {
                Node next_node = null;
                if (key_code == Keys.Up)
                    next_node = focused_node.Up;
                else if (key_code == Keys.Down)
                    next_node = focused_node.Down;
                else if (key_code == Keys.Left)
                    next_node = focused_node.Left;
                else if (key_code == Keys.Right)
                    next_node = focused_node.Right;

                if (next_node != null)
                {
                    next_node.Control.Focus();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// フォーカス中のノードを取得する。
        /// </summary>
        private Boolean TryGetFocusedNode(out Node focused_node)
        {
            foreach (var most_left_node in _most_left_control_each_rows)
            {
                var node = most_left_node;
                while (node != null)
                {
                    if (node.Control.Focused)
                    {
                        focused_node = node;
                        return true;
                    }

                    node = node.Right;
                    // ノードが一周していないか判定する
                    if (node == most_left_node)
                        break;
                }
            }

            focused_node = null;
            return false;
        }

    }
}
