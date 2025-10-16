using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
    static class NativeWindowKeyInput
    {
        public static readonly int WM_KEYDOWN = 0x0100;   // キーを押す
        public static readonly int WM_KEYUP = 0x0101;   // キーを離す
        public static readonly int WM_SYSKEYDOWN = 0x0104;   // キーを押す
        public static readonly int WM_SYSKEYUP = 0x0105;   // キーを押す

        public static readonly int WM_COPY = 0x0301; // コピー
        public static readonly int WM_PASTE = 0x0302; // ペースト

        /* キーコード一覧 https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes */

        public static readonly byte VK_LBUTTON = 0x01;     // マウスの左ボタン
        public static readonly byte VK_RBUTTON = 0x02;     // マウスの右ボタン
        public static readonly byte VK_CANCEL = 0x03;     // コントロールブレーク処理
        public static readonly byte VK_MBUTTON = 0x04;     // マウスの中央ボタン(3 ボタンマウス)
        public static readonly byte VK_XBUTTON1 = 0x05;     // X1 マウスボタン
        public static readonly byte VK_XBUTTON2 = 0x06;     // X2 マウスボタン

        //public static readonly byte -	        = 0x07;     // 未定義。
        public static readonly byte VK_BACK    = 0x08;     // BACKSPACE キー
        public static readonly byte VK_TAB     = 0x09;     // Tab キー
        //public static readonly byte -	        = 0x0A-0B;	// 予約済み
        public static readonly byte VK_CLEAR   = 0x0C;     // キーのクリア
        public static readonly byte VK_RETURN  = 0x0D;     // Enter キー
        //public static readonly byte -	        = 0x0E-0F;	// 未定義。
        public static readonly byte VK_SHIFT   = 0x10;     // SHIFT キー
        public static readonly byte VK_CONTROL = 0x11;     // CTRL キー
        public static readonly byte VK_MENU    = 0x12;     // ALT キー
        public static readonly byte VK_PAUSE   = 0x13;     // キーの一時停止
        public static readonly byte VK_CAPITAL = 0x14;     // CAPS LOCK キー

        public static readonly byte VK_KANA    = 0x15;     // IME かなモード
        public static readonly byte VK_HANGUEL = 0x15;     // IME ハングルモード(互換性のために維持されます。を使用します VK_HANGUL)
        public static readonly byte VK_HANGUL  = 0x15;     // IME ハングル モード
        public static readonly byte VK_IME_ON  = 0x16;     // IME オン
        public static readonly byte VK_JUNJA   = 0x17;     // IME Junja モード
        public static readonly byte VK_FINAL   = 0x18;     // IME Final モード
        public static readonly byte VK_HANJA   = 0x19;     // IME Hanja モード
        public static readonly byte VK_KANJI   = 0x19;     // IME 漢字モード
        public static readonly byte VK_IME_OFF = 0x1A;     // IME オフ

        public static readonly byte VK_ESCAPE     = 0x1B;     // ESC キー
        public static readonly byte VK_CONVERT    = 0x1C;     // IME 変換
        public static readonly byte VK_NONCONVERT = 0x1D;     // IME 無変換
        public static readonly byte VK_ACCEPT     = 0x1E;     // IME 使用可能
        public static readonly byte VK_MODECHANGE = 0x1F;     // IME モード変更要求

        public static readonly byte VK_SPACE    = 0x20;     // Space キー

        public static readonly byte VK_PRIOR    = 0x21;     // PAGEUP キー
        public static readonly byte VK_NEXT     = 0x22;     // PAGEDOWN キー
        public static readonly byte VK_END      = 0x23;     // END キー
        public static readonly byte VK_HOME     = 0x24;     // HOME キー

        public static readonly byte VK_LEFT     = 0x25;     // 左方向キー
        public static readonly byte VK_UP       = 0x26;     // 上方向キー
        public static readonly byte VK_RIGHT    = 0x27;     // 右方向キー
        public static readonly byte VK_DOWN     = 0x28;     // 下方向キー

        public static readonly byte VK_SELECT   = 0x29;     // キーの選択
        public static readonly byte VK_PRINT    = 0x2A;     // 印刷キー
        public static readonly byte VK_EXECUTE  = 0x2B;     // キーの実行
        public static readonly byte VK_SNAPSHOT = 0x2C;     // PRINTSCREEN キー

        public static readonly byte VK_INSERT   = 0x2D;     // INS キー
        public static readonly byte VK_DELETE   = 0x2E;     // DEL キー

        public static readonly byte VK_HELP     = 0x2F;     // ヘルプ キー

        public static readonly byte VK_0 = 0x30;	 // 0 キー
        public static readonly byte VK_1 = 0x31;	 // 1 キー
        public static readonly byte VK_2 = 0x32;	 // 2 キー
        public static readonly byte VK_3 = 0x33;	 // 3 キー
        public static readonly byte VK_4 = 0x34;	 // 4 キー
        public static readonly byte VK_5 = 0x35;	 // 5 キー
        public static readonly byte VK_6 = 0x36;	 // 6 キー
        public static readonly byte VK_7 = 0x37;	 // 7 キー
        public static readonly byte VK_8 = 0x38;	 // 8 キー
        public static readonly byte VK_9 = 0x39;	 // 9 キー
        //public static readonly byte -	0x3A-40	未定義。
        public static readonly byte VK_A = 0x41;     // キー
        public static readonly byte VK_B = 0x42;     // B キー
        public static readonly byte VK_C = 0x43;     // C キー
        public static readonly byte VK_D = 0x44;     // D キー
        public static readonly byte VK_E = 0x45;     // E キー
        public static readonly byte VK_F = 0x46;     // F キー
        public static readonly byte VK_G = 0x47;     // G キー
        public static readonly byte VK_H = 0x48;     // H キー
        public static readonly byte VK_I = 0x49;     // I キー
        public static readonly byte VK_J = 0x4A;     // J キー
        public static readonly byte VK_K = 0x4B;     // K キー
        public static readonly byte VK_L = 0x4C;     // L キー
        public static readonly byte VK_M = 0x4D;     // M キー
        public static readonly byte VK_N = 0x4E;     // N キー
        public static readonly byte VK_O = 0x4F;     // O キー
        public static readonly byte VK_P = 0x50;     // P キー
        public static readonly byte VK_Q = 0x51;     // Q キー
        public static readonly byte VK_R = 0x52;     // R キー
        public static readonly byte VK_S = 0x53;     // S キー
        public static readonly byte VK_T = 0x54;     // T キー
        public static readonly byte VK_U = 0x55;     // U キー
        public static readonly byte VK_V = 0x56;     // V キー
        public static readonly byte VK_W = 0x57;     // W キー
        public static readonly byte VK_X = 0x58;     // X キー
        public static readonly byte VK_Y = 0x59;     // Y キー
        public static readonly byte VK_Z = 0x5A;     // Z キー

        public static readonly byte VK_LWIN = 0x5B;     // 左Windows キー(自然キーボード)
        public static readonly byte VK_RWIN = 0x5C;     // 右Windows キー(自然キーボード)
        public static readonly byte VK_APPS = 0x5D;     // アプリケーション キー(自然キーボード)
        //public static readonly byte -	        = 0x5E;     // 予約済み
        public static readonly byte VK_SLEEP = 0x5F;     // コンピューターのスリープ キー

        public static readonly byte VK_NUMPAD0   = 0x60;     // テンキー 0 キー
        public static readonly byte VK_NUMPAD1   = 0x61;     // テンキー 1 キー
        public static readonly byte VK_NUMPAD2   = 0x62;     // テンキー 2 キー
        public static readonly byte VK_NUMPAD3   = 0x63;     // テンキー 3 キー
        public static readonly byte VK_NUMPAD4   = 0x64;     // テンキー 4 キー
        public static readonly byte VK_NUMPAD5   = 0x65;     // テンキーの5キー
        public static readonly byte VK_NUMPAD6   = 0x66;     // テンキー6キー
        public static readonly byte VK_NUMPAD7   = 0x67;     // テンキーの7キー
        public static readonly byte VK_NUMPAD8   = 0x68;     // テンキーの8キー
        public static readonly byte VK_NUMPAD9   = 0x69;     // テンキーの9キー

        public static readonly byte VK_MULTIPLY  = 0x6A;     // キーの乗算
        public static readonly byte VK_ADD       = 0x6B;     // キーの追加
        public static readonly byte VK_SEPARATOR = 0x6C;     // 区切り記号キー
        public static readonly byte VK_SUBTRACT  = 0x6D;     // 減算キー
        public static readonly byte VK_DECIMAL   = 0x6E;     // 小数点キー
        public static readonly byte VK_DIVIDE    = 0x6F;     // キーの除算

        public static readonly byte VK_F1       = 0x70;     // F1 キー
        public static readonly byte VK_F2       = 0x71;     // F2 キー
        public static readonly byte VK_F3       = 0x72;     // F3 キー
        public static readonly byte VK_F4       = 0x73;     // F4 キー
        public static readonly byte VK_F5       = 0x74;     // F5 キー
        public static readonly byte VK_F6       = 0x75;     // F6 キー
        public static readonly byte VK_F7       = 0x76;     // F7 キー
        public static readonly byte VK_F8       = 0x77;     // F8 キー
        public static readonly byte VK_F9       = 0x78;     // F9 キー
        public static readonly byte VK_F10      = 0x79;     // F10 キー
        public static readonly byte VK_F11      = 0X7a;     // F11 キー
        public static readonly byte VK_F12      = 0x7B;     // F12 キー
        public static readonly byte VK_F13      = 0x7C;     // F13 キー
        public static readonly byte VK_F14      = 0x7D;     // F14 キー
        public static readonly byte VK_F15      = 0x7E;     // F15 キー
        public static readonly byte VK_F16      = 0x7F;     // F16 キー
        public static readonly byte VK_F17      = 0x80;     // F17 キー
        public static readonly byte VK_F18      = 0x81;     // F18 キー
        public static readonly byte VK_F19      = 0x82;     // F19 キー
        public static readonly byte VK_F20      = 0x83;     // F20 キー
        public static readonly byte VK_F21      = 0x84;     // F21 キー
        public static readonly byte VK_F22      = 0x85;     // F22 キー
        public static readonly byte VK_F23      = 0x86;     // F23 キー
        public static readonly byte VK_F24      = 0x87;     // F24 キー

        public static readonly byte VK_NUMLOCK  = 0x90;     // NUMLOCK キー

        public static readonly byte VK_SCROLL   = 0x91;     // ロックキーのスクロール
        public static readonly byte VK_LSHIFT   = 0xA0;     // 左 Shift キー
        public static readonly byte VK_RSHIFT   = 0xA1;     // 右 Shift キー
        public static readonly byte VK_LCONTROL = 0xA2;     // 左 Ctrl キー
        public static readonly byte VK_RCONTROL = 0xA3;     // 右 Ctrl キー
        public static readonly byte VK_LMENU    = 0xA4;     // 左 Alt キー
        public static readonly byte VK_RMENU    = 0xA5;     // 右 Alt キー

        public static readonly byte VK_COLON         = 0xBA;	// ':' キー
        public static readonly byte VK_SEMICOLON     = 0xBB;	// ';' キー
        public static readonly byte VK_COMMA         = 0xBC;	// ',' キー
        public static readonly byte VK_HYPHEN        = 0xBD;	// '-' キー
        public static readonly byte VK_PERIOD        = 0xBE;	// '.' キー
        public static readonly byte VK_SLASH         = 0xBF;	//  '/' key
        public static readonly byte VK_AT_SIGN        = 0xC0;	//  '@' キー
        //public static readonly byte -	0xC1-D7 予約されています。
        //public static readonly byte -	0xD8-DA[Unassigned] \(未割り当て)
        public static readonly byte VK_BRACKET_OPEN  = 0xDB;	// '[' キー
        public static readonly byte VK_YEN           = 0xDC;	// '\' キー
        public static readonly byte VK_BRACKET_CLOSE = 0xDD;	// ']' キー
        public static readonly byte VK_CARET         = 0xDE;	// "^" キー
        //public static readonly byte VK_OEM_8 =	0xDF;	// 
        //public static readonly byte -	0xE0	予約されています。
        //public static readonly byte 0xE1	OEM 固有
        public static readonly byte VK_BACK_SLASH    = 0xE2; // ＼キー

        public static readonly int INPUT_KEYBOARD = 1;
        public static readonly int KEYEVENTF_EXTENDEDKEY = 0x0001; // 指定した場合、スキャンコードの前に、値が0xE0(224) のプレフィックスバイトが付いている
        public static readonly int KEYEVENTF_KEYUP = 0x0002;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(
            HandleRef handle,   // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,            // 送信するメッセージ
            long wparam,        // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            long lparam         // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int PostMessage(
            HandleRef handle,   // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,            // 送信するメッセージ
            long wparam,        // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            long lparam         // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        /// <summary>
        /// キーボード操作を送信する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendInput(
            int nInputs,
            INPUT[] pInputs,
            int cvsize
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void keybd_event(
            byte bVk,
            byte bScan,
            uint dwFlags,
            UIntPtr dwExtraInfo
        );

        /// <summary>
        /// 仮想キーコードをスキャンコードまたは文字の値（ASCII 値）へ変換する。またはスキャンコードを仮想コードへ変換する。
        /// uCodeとuMapTypeの値に従って、スキャンコード、仮想キーコード、ASCII値のいずれかが返る。変換されないときは0が返る。
        /// アプリケーションはこの関数を使って、スキャンコードを VK_SHIFT、VK_CONTROL、VK_MENU などの仮想キーコード定数へ変換できる。
        /// また、その逆の変換も行える。これらの変換では、左右の Shift、Ctrl、Alt の各キーを区別しない。
        /// 
        /// スキャンコードとは
        /// キーボードが押されたり離されたりしたとき、キーボードからCPUに送られるコード（符号）のこと。走査コードともいう。
        /// キーボードは例えば「A」が押されたとき、「A」という文字が直接送られるのではなく、キーを物理的に識別する値が送られる。
        /// この値をスキャンコードという。CPUはスキャンコードを受け取ると、キー配列や修飾キー（SHIFTなど）、ロックキーなどの状態を参照して
        /// 「A」という文字を出力する。
        /// 
        /// ここでは主に、SHIFTキーを送信しながらカーソルキーを送信したときに文字列が選択されるようにするために使う。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern byte MapVirtualKey(
            byte uCode,    // キーの仮想キーコードもしくはスキャンコード
            byte uMapType  // uCodeをどのように変換するかを表す値
                           //  0: uCodeは仮想キーコードでありスキャンコードに変換する。
                           //     左右のキーを区別しないキーコードのときは左側のスキャンコードを返す。
                           //  1: uCodeはスキャンコードであり仮想キーコードに変換する。
                           //     この仮想キーコードは左右のキーを区別する。
                           //  2: デッドキー（分音符号）は戻り値の上位ビットをセットすることにより明示される。
            );

        /// <summary>
        /// 引数で指定した仮想キーコードのキーが押されているかどうかを調べる。
        /// 戻り値の最上位ビットが1の場合、そのキーは押されている。0の場合、押されていない。
        /// 最下位ビットが1の場合、前回のGetAsyncKeyState()の呼び出し以降に押された。0の場合、前回のGetAsyncKeyState()の呼び出し以降押されていない。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(
            int vKey
            );

        /// <summary>
        /// 選択されている文字列をクリップボードにコピーする。
        /// </summary>
        public static void Copy(HandleRef handle)
        {
            SendMessage(handle, WM_COPY, 0, 0);
        }

        /// <summary>
        /// クリップボードの文字列をコピーする。
        /// </summary>
        public static void Paste(HandleRef handle)
        {
            SendMessage(handle, WM_PASTE, 0, 0);
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public KEYBDINPUT ki;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public int wVk;
            public int wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }


    }


}
