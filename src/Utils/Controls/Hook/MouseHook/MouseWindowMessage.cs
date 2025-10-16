using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    static class MouseWindowMessage
    {
        public const uint LBUTTON_DOWN   = 0x8001;
        public const uint LBUTTON_UP     = 0x8002;
        //public const uint LBUTTON_DCLICK = 0x8003;

        public const uint RBUTTON_DOWN   = 0x8004;
        public const uint RBUTTON_UP     = 0x8005;
        //public const uint RBUTTON_DCLICK = 0x8006;

        public const uint MBUTTON_DOWN   = 0x8007;
        public const uint MBUTTON_UP     = 0x8008;
        //public const uint MBUTTON_DCLICK = 0x8009;

        public const uint MOVE           = 0x800A;
        public const uint WHEEL_DOWN     = 0x800B;
        public const uint WHEEL_UP       = 0x800C;
        public const uint HWHEEL         = 0x800C;


        private static readonly Dictionary<int, uint> _nativeAndCustomwindowMessageMap;

        static MouseWindowMessage()
        {
            _nativeAndCustomwindowMessageMap = new Dictionary<int, uint>
            {
                { NativeWindowMouseHook.WM_LBUTTONDOWN,   LBUTTON_DOWN },
                { NativeWindowMouseHook.WM_LBUTTONUP,     LBUTTON_UP },
                //{ NativeWindowMouseHook.WM_LBUTTONDBLCLK, LBUTTON_DCLICK },
                { NativeWindowMouseHook.WM_RBUTTONDOWN,   RBUTTON_DOWN },
                { NativeWindowMouseHook.WM_RBUTTONUP,     RBUTTON_UP},
                //{ NativeWindowMouseHook.WM_RBUTTONDBLCLK, RBUTTON_DCLICK},
                { NativeWindowMouseHook.WM_MBUTTONDOWN,   MBUTTON_DOWN },
                { NativeWindowMouseHook.WM_MBUTTONUP,     MBUTTON_UP },
                //{ NativeWindowMouseHook.WM_MBUTTONDBLCLK, MBUTTON_DCLICK },
                //{ NativeWindowMouseHook.WM_MOUSEMOVE,     MOUSE_MOVE },
                //{ NativeWindowMouseHook.WM_MOUSEWHEEL,    MOUSE_WHEEL },
                //{ NativeWindowMouseHook.WM_MOUSEHWHEEL,   MOUSE_HWHEEL },
            };
        }


        public static bool TryConvertToCustomMouseWindowMessageFrom(int nativeWindowMassage, out uint customMouseWindowMessage)
        {
            customMouseWindowMessage = 0;

            if (!_nativeAndCustomwindowMessageMap.ContainsKey(nativeWindowMassage))
                return false;

            customMouseWindowMessage = _nativeAndCustomwindowMessageMap[nativeWindowMassage];

            return true;
        }

        public static bool IsWindowMessageButtonDown(uint mouseWindowMessage)
        {
            return
                mouseWindowMessage == LBUTTON_DOWN ||
                mouseWindowMessage == MBUTTON_DOWN ||
                mouseWindowMessage == RBUTTON_DOWN;
        }

        public static bool IsWindowMessageButtonUp(uint mouseWindowMessage)
        {
            return
                mouseWindowMessage == LBUTTON_UP ||
                mouseWindowMessage == MBUTTON_UP ||
                mouseWindowMessage == RBUTTON_UP;
        }

        //public static bool IsWindowMessageDoubleClick(uint mouseWindowMessage)
        //{
        //    return
        //        mouseWindowMessage == LBUTTON_DCLICK ||
        //        mouseWindowMessage == MBUTTON_DCLICK ||
        //        mouseWindowMessage == RBUTTON_DCLICK;
        //}

        public static bool IsWindowMessageLButton(uint mouseWindowMessage)
        {
            return
                mouseWindowMessage == LBUTTON_DOWN ||
                mouseWindowMessage == LBUTTON_UP;
        }

        public static bool IsWindowMessageMButton(uint mouseWindowMessage)
        {
            return
                mouseWindowMessage == MBUTTON_DOWN ||
                mouseWindowMessage == MBUTTON_UP;
        }

        public static bool IsWindowMessageRButton(uint mouseWindowMessage)
        {
            return
                mouseWindowMessage == RBUTTON_DOWN ||
                mouseWindowMessage == RBUTTON_UP;
        }

        public static bool IsWindowMessageWheel(uint mouseWindowMessage)
        {
            return
                mouseWindowMessage == WHEEL_DOWN ||
                mouseWindowMessage == WHEEL_UP;
        }

        public static bool IsWindowMessageMove(uint mouseWindowMessage)
        {
            return mouseWindowMessage == MOVE;
        }

        public static bool IsWindowMessage(uint mouseWindowMessage)
        {
            return
                IsWindowMessageButtonDown(mouseWindowMessage)  ||
                IsWindowMessageButtonUp(mouseWindowMessage)    ||
                //IsWindowMessageDoubleClick(mouseWindowMessage) ||
                IsWindowMessageWheel(mouseWindowMessage)       ||
                IsWindowMessageMove(mouseWindowMessage);
        }
    }
}
