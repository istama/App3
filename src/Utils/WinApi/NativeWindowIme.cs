using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// ※IMEのAPIは現在非推奨。最新のWindowsでは効かないかも。
    /// 代わりにTSF(Text Service Framework)を使用する。
    /// </summary>
    public static class NativeWindowIme
    {
        public static readonly uint IME_CMODE_ALPHANUMERIC = 0x0000; // 英数字入力モード
        public static readonly uint IME_CMODE_NATIVE       = 0x0001; // ネイティブモード（日本語など）。
        public static readonly uint IME_CMODE_KATAKANA     = 0x0002; // カタカナモード。
        public static readonly uint IME_CMODE_FULLSHAPE    = 0x0008; // 全角モード。
        public static readonly uint IME_CMODE_ROMAN        = 0x0010; // ローマ字入力モード。
        public static readonly uint IME_CMODE_NOCONVERSION = 0x0100; // 変換を行わないモード。


        public static readonly int SM_IMMENABLED = 82;

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// 指定したウィンドウに関連付けられた入力コンテキストを取得する。
        /// 入力コンテキストハンドルのことをHIMCという。
        /// 取得した入力コンテキストは必ず、ImmReleaseContext()で解放する。
        /// </summary>
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        /// <summary>
        /// 入力コンテキストが開いているならtrueを返す。
        /// </summary>
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr hIMC);

        /// <summary>
        /// 入力コンテキストの現在の設定値を変更する。
        /// fdwConversionには入力モードの値を渡す。
        /// fdwSentenceには文の変換に関する設定を指定する。0でよい。
        /// </summary>
        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, uint fdwConversion, uint fdwSentence);

        /// <summary>
        /// 現在の入力コンテキストの状態を取得する。
        /// </summary>
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC, out uint lpdwConversion, out uint lpdwSentence);

        /// <summary>
        /// 入力コンテキストを解放する。
        /// </summary>
        [DllImport("imm32.dll")]
        public static extern bool ImmReleaseContext(HandleRef hWnd, IntPtr hIMC);
    }
}
