using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// すべてのキーから修飾キーを除いた列挙体。
    /// </summary>
    enum InputKey
    {
        /*
         * ※注意
         * 新しいキーを追加するときは、InputKeyConverterクラスの静的コストラクタに
         * 列挙値とキーコードのマッピングの設定を追加すること。
         */
         
        /*
         * 列挙値の一番最初の値は0として扱われる。これは予約語の default と同じ値となる。
         * なので例えば、列挙値の一番最初の値やboolのfalseのみをフィールドとして持つオブジェクトや構造体は、
         * defaultと比較するとtrueを返してしまうので注意。
         * 列挙値の一番最初の値は使わない値や、値がないことを意味する値を設定しておいた方が良いかも。
         */
        NONE,

        ESC,

        ONE, TWO, THREE, FOUR, FIVE,
        SIX, SEVEN, EIGHT, NINE, ZERO,

        T_ZERO,
        T_ONE,
        T_TWO,
        T_THREE,
        T_FOUR,
        T_FIVE,
        T_SIX,
        T_SEVEN,
        T_EIGHT,
        T_NINE,

        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,

        F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,

        UP,
        DOWN,
        LEFT,
        RIGHT,

        SPACE,
        ENTER,
        COMMA,
        PERIOD,
        SLASH,
        BACK_SLASH,
        COLON,
        SEMICOLON,
        BRACKET_OPEN,
        BRACKET_CLOSE,
        AT_SIGN,
        CARET,
        HYPHEN,
        YEN,
        TAB,

        BACK_SPACE,

        INSERT,
        DELETE,
        HOME,
        END,
        PGUP,
        PGDN,

        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        DECIMAL,

        //LWIN,
        NONCONVERT,
    }

    static class InputKeyExtensions
    {
        private static readonly InputKey[] _EditKeys = new[]
        {
            InputKey.INSERT, InputKey.DELETE, InputKey.HOME, InputKey.END, InputKey.PGUP, InputKey.PGDN
        };

        private static readonly InputKey[] _CursorKeys = new[]
        {
            InputKey.UP, InputKey.LEFT, InputKey.RIGHT, InputKey.DOWN
        };

        /// <summary>
        /// キーが編集キーならtrueを返す。
        /// </summary>
        public static bool IsEditKey(this InputKey key)
            => _EditKeys.Contains(key);

        /// <summary>
        /// キーがカーソルキーならtrueを返す。
        /// </summary>
        public static bool IsCursorKey(this InputKey key)
            => _CursorKeys.Contains(key);
    }
}
