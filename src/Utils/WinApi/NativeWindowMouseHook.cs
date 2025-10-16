using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
    class NativeWindowMouseHook
    {
        public static readonly int WH_MOUSE_LL      = 0x000E;

        public static readonly int WM_PRESSED_CTRL  = 0x0008;
        public static readonly int WM_PRESSED_SHIFT = 0x0004;
        public static readonly int WM_LBUTTONDOWN   = 0x0201;
        public static readonly int WM_LBUTTONUP     = 0x0202;
        public static readonly int WM_LBUTTONDBLCLK = 0x0203;
        public static readonly int WM_RBUTTONDOWN   = 0x0204;
        public static readonly int WM_RBUTTONUP     = 0x0205;
        public static readonly int WM_RBUTTONDBLCLK = 0x0206;
        public static readonly int WM_MBUTTONDOWN   = 0x0207;
        public static readonly int WM_MBUTTONUP     = 0x0208;
        public static readonly int WM_MBUTTONDBLCLK = 0x0209;
        public static readonly int WM_MOUSEMOVE     = 0x0200;
        public static readonly int WM_MOUSEWHEEL    = 0x020A;
        public static readonly int WM_MOUSEHWHEEL   = 0x020E;

        /// <summary>
        /// マウス入力があると第二引数に渡したデリゲートが呼び出されるようにする。
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProcDelegate lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// SetWindowsHookExに登録したデリゲートを解除する。
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// SetWindowsHookExに登録したデリゲート内で呼び出されるべき関数。
        /// マウス入力を次のデリゲートに渡す。
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// カスタムのウィンドウメッセージを作成する関数。
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);

        public delegate IntPtr LowLevelMouseProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MSLLHOOKSTRUCT
    {
        // カーソルの画面座標
        public POINT pt;
        // メッセージがWM_MOUSEWHEELの場合、上位ワード(16bit)がホイールデータとなる。前方回転なら1以上の値、後方回転なら0未満の値がセットされる。
        public uint mouseData;
        // イベントが挿入されたことを表すフラグ。
        public uint flags;
        // このメッセージのタイムスタンプ。
        public uint time;
        // 追加情報。
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}
