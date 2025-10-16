using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// InputKeyと仮想キーコードの相互変換を行うメソッドを集めた静的クラス。
    /// </summary>
    static class InputKeyConverters
    {
        private static readonly Dictionary<InputKey, byte> _InputKeyAndKeycodeMap;
        private static readonly Dictionary<byte, InputKey> _KeycodeAndInputKeyMap;

        static InputKeyConverters()
        {
            //// キーを表す列挙体と仮想キーコードを紐付けたマップ
            _InputKeyAndKeycodeMap = new Dictionary<InputKey, byte>
            {
                { InputKey.ESC, NativeWindowKeyInput.VK_ESCAPE },

                { InputKey.ZERO,  NativeWindowKeyInput.VK_0 },
                { InputKey.ONE,   NativeWindowKeyInput.VK_1 },
                { InputKey.TWO,   NativeWindowKeyInput.VK_2 },
                { InputKey.THREE, NativeWindowKeyInput.VK_3 },
                { InputKey.FOUR,  NativeWindowKeyInput.VK_4 },
                { InputKey.FIVE,  NativeWindowKeyInput.VK_5 },
                { InputKey.SIX,   NativeWindowKeyInput.VK_6 },
                { InputKey.SEVEN, NativeWindowKeyInput.VK_7 },
                { InputKey.EIGHT, NativeWindowKeyInput.VK_8 },
                { InputKey.NINE , NativeWindowKeyInput.VK_9 },

                { InputKey.T_ZERO,  NativeWindowKeyInput.VK_NUMPAD0 },
                { InputKey.T_ONE,   NativeWindowKeyInput.VK_NUMPAD1 },
                { InputKey.T_TWO,   NativeWindowKeyInput.VK_NUMPAD2 },
                { InputKey.T_THREE, NativeWindowKeyInput.VK_NUMPAD3 },
                { InputKey.T_FOUR,  NativeWindowKeyInput.VK_NUMPAD4 },
                { InputKey.T_FIVE,  NativeWindowKeyInput.VK_NUMPAD5 },
                { InputKey.T_SIX,   NativeWindowKeyInput.VK_NUMPAD6 },
                { InputKey.T_SEVEN, NativeWindowKeyInput.VK_NUMPAD7 },
                { InputKey.T_EIGHT, NativeWindowKeyInput.VK_NUMPAD8 },
                { InputKey.T_NINE , NativeWindowKeyInput.VK_NUMPAD9 },

                { InputKey.A,  NativeWindowKeyInput.VK_A },
                { InputKey.B,  NativeWindowKeyInput.VK_B },
                { InputKey.C,  NativeWindowKeyInput.VK_C },
                { InputKey.D,  NativeWindowKeyInput.VK_D },
                { InputKey.E,  NativeWindowKeyInput.VK_E },
                { InputKey.F,  NativeWindowKeyInput.VK_F },
                { InputKey.G,  NativeWindowKeyInput.VK_G },
                { InputKey.H,  NativeWindowKeyInput.VK_H },
                { InputKey.I,  NativeWindowKeyInput.VK_I },
                { InputKey.J,  NativeWindowKeyInput.VK_J },
                { InputKey.K,  NativeWindowKeyInput.VK_K },
                { InputKey.L,  NativeWindowKeyInput.VK_L },
                { InputKey.M,  NativeWindowKeyInput.VK_M },
                { InputKey.N,  NativeWindowKeyInput.VK_N },
                { InputKey.O,  NativeWindowKeyInput.VK_O },
                { InputKey.P,  NativeWindowKeyInput.VK_P },
                { InputKey.Q,  NativeWindowKeyInput.VK_Q },
                { InputKey.R,  NativeWindowKeyInput.VK_R },
                { InputKey.S,  NativeWindowKeyInput.VK_S },
                { InputKey.T,  NativeWindowKeyInput.VK_T },
                { InputKey.U,  NativeWindowKeyInput.VK_U },
                { InputKey.V,  NativeWindowKeyInput.VK_V },
                { InputKey.W,  NativeWindowKeyInput.VK_W },
                { InputKey.X,  NativeWindowKeyInput.VK_X },
                { InputKey.Y,  NativeWindowKeyInput.VK_Y },
                { InputKey.Z,  NativeWindowKeyInput.VK_Z },

                { InputKey.F1,  NativeWindowKeyInput.VK_F1  },
                { InputKey.F2,  NativeWindowKeyInput.VK_F2  },
                { InputKey.F3,  NativeWindowKeyInput.VK_F3  },
                { InputKey.F4,  NativeWindowKeyInput.VK_F4  },
                { InputKey.F5,  NativeWindowKeyInput.VK_F5  },
                { InputKey.F6,  NativeWindowKeyInput.VK_F6  },
                { InputKey.F7,  NativeWindowKeyInput.VK_F7  },
                { InputKey.F8,  NativeWindowKeyInput.VK_F8  },
                { InputKey.F9,  NativeWindowKeyInput.VK_F9  },
                { InputKey.F10, NativeWindowKeyInput.VK_F10 },
                { InputKey.F11, NativeWindowKeyInput.VK_F11 },
                { InputKey.F12, NativeWindowKeyInput.VK_F12 },

                { InputKey.BACK_SPACE, NativeWindowKeyInput.VK_BACK },

                { InputKey.INSERT, NativeWindowKeyInput.VK_INSERT },
                { InputKey.DELETE, NativeWindowKeyInput.VK_DELETE },
                { InputKey.HOME,   NativeWindowKeyInput.VK_HOME   },
                { InputKey.END,    NativeWindowKeyInput.VK_END    },
                { InputKey.PGUP,   NativeWindowKeyInput.VK_PRIOR  },
                { InputKey.PGDN,   NativeWindowKeyInput.VK_NEXT   },

                { InputKey.UP,    NativeWindowKeyInput.VK_UP    },
                { InputKey.DOWN,  NativeWindowKeyInput.VK_DOWN  },
                { InputKey.LEFT,  NativeWindowKeyInput.VK_LEFT  },
                { InputKey.RIGHT, NativeWindowKeyInput.VK_RIGHT },

                { InputKey.SPACE,         NativeWindowKeyInput.VK_SPACE         },
                { InputKey.ENTER,         NativeWindowKeyInput.VK_RETURN        },
                { InputKey.COMMA,         NativeWindowKeyInput.VK_COMMA         },
                { InputKey.PERIOD,        NativeWindowKeyInput.VK_PERIOD        },
                { InputKey.SLASH,         NativeWindowKeyInput.VK_SLASH         },
                { InputKey.BACK_SLASH,    NativeWindowKeyInput.VK_BACK_SLASH    },
                { InputKey.COLON,         NativeWindowKeyInput.VK_COLON         },
                { InputKey.SEMICOLON,     NativeWindowKeyInput.VK_SEMICOLON     },
                { InputKey.BRACKET_OPEN,  NativeWindowKeyInput.VK_BRACKET_OPEN  },
                { InputKey.BRACKET_CLOSE, NativeWindowKeyInput.VK_BRACKET_CLOSE },
                { InputKey.AT_SIGN,       NativeWindowKeyInput.VK_AT_SIGN       },
                { InputKey.CARET,         NativeWindowKeyInput.VK_CARET         },
                { InputKey.HYPHEN,        NativeWindowKeyInput.VK_HYPHEN        },
                { InputKey.YEN,           NativeWindowKeyInput.VK_YEN           },
                { InputKey.TAB,           NativeWindowKeyInput.VK_TAB           },

                { InputKey.PLUS,     NativeWindowKeyInput.VK_ADD      },
                { InputKey.MINUS,    NativeWindowKeyInput.VK_SUBTRACT },
                { InputKey.MULTIPLY, NativeWindowKeyInput.VK_MULTIPLY },
                { InputKey.DIVIDE,   NativeWindowKeyInput.VK_DIVIDE   },
                { InputKey.DECIMAL,  NativeWindowKeyInput.VK_DECIMAL   },

                { InputKey.NONCONVERT, NativeWindowKeyInput.VK_NONCONVERT },

                { InputKey.NONE, 0 },
            };

            _KeycodeAndInputKeyMap = new Dictionary<byte, InputKey>();

            // 列挙値と仮想キーコードを反対にしてマップに登録する
            foreach (var key in _InputKeyAndKeycodeMap.Keys)
            {
                var code = _InputKeyAndKeycodeMap[key];
                if (_KeycodeAndInputKeyMap.ContainsKey(code))
                {
                    var k = _KeycodeAndInputKeyMap[code];
                    throw new InvalidOperationException($"{code}: 仮想キーコードの値が {k.ToString()} と重複しています。");
                }
                _KeycodeAndInputKeyMap.Add(code, key);
            }
        }

        /// <summary>
        /// InputKeys列挙型の要素が示すキーを、win32api に渡すことのできる
        /// 仮想キーコードに変換する。
        /// </summary>
        public static byte ToVirtualKeyCode(this InputKey key)
        {
            if (_InputKeyAndKeycodeMap.ContainsKey(key))
                return _InputKeyAndKeycodeMap[key];

            throw new ArgumentException($"{key.ToString()} はキーマップに割り当てられてないキーです。");
        }

        /// <summary>
        /// Win32APIに渡す仮想キーコードを対応するInputKeys列挙体の値に変換する。
        /// </summary>
        public static bool TryConvertToInputKeyFrom(byte vkCode, out InputKey key)
        {
            key = ToInputKeyFrom(vkCode);

            return key != InputKey.NONE;
        }

        /// <summary>
        /// Win32APIに渡す仮想キーコードを対応するInputKeys列挙体の値に変換する。
        /// </summary>
        public static InputKey ToInputKeyFrom(byte vkCode)
        {
            if (_KeycodeAndInputKeyMap.ContainsKey(vkCode))
                return _KeycodeAndInputKeyMap[vkCode];
        
            // KeyEventArgsのKeyValueプロパティでは無変換キーは229を返すため
            if (vkCode == 229)
                return InputKey.NONCONVERT;
        
            return InputKey.NONE;
        }

        /// <summary>
        /// InputKeys列挙型の要素が示すキーを、System.Windows.Forms.SendKeys.SendWait() に渡すことのできる
        /// キーを表す文字列に変換する。
        /// </summary>
        public static string ToSendKeysText(InputKey key)
        {
            if (key >= InputKey.ZERO && key <= InputKey.NINE)
            {
                switch (key)
                {
                    case InputKey.ZERO:  return "0";
                    case InputKey.ONE:   return "1";
                    case InputKey.TWO:   return "2";
                    case InputKey.THREE: return "3";
                    case InputKey.FOUR:  return "4";
                    case InputKey.FIVE:  return "5";
                    case InputKey.SIX:   return "6";
                    case InputKey.SEVEN: return "7";
                    case InputKey.EIGHT: return "8";
                    case InputKey.NINE:  return "9";
                }
                throw new ArgumentException();
            }

            if (key >= InputKey.T_ZERO && key <= InputKey.T_NINE)
            {
                switch (key)
                {
                    case InputKey.T_ZERO:  return "0";
                    case InputKey.T_ONE:   return "1";
                    case InputKey.T_TWO:   return "2";
                    case InputKey.T_THREE: return "3";
                    case InputKey.T_FOUR:  return "4";
                    case InputKey.T_FIVE:  return "5";
                    case InputKey.T_SIX:   return "6";
                    case InputKey.T_SEVEN: return "7";
                    case InputKey.T_EIGHT: return "8";
                    case InputKey.T_NINE:  return "9";
                }
                throw new ArgumentException();
            }

            if (key >= InputKey.A && key <= InputKey.Z)
            {
                return key.ToString().ToLower();
            }

            if (key >= InputKey.F1 && key <= InputKey.F12)
            {
                return "{" + key.ToString() + "}";
            }

            if (key == InputKey.UP || key == InputKey.LEFT || key == InputKey.RIGHT || key == InputKey.DOWN)
            {
                return "{" + key.ToString() + "}";
            }

            switch (key)
            {
                case InputKey.SPACE:         return "{SPACE}";
                case InputKey.ENTER:         return "{ENTER}";
                case InputKey.COMMA:         return ",";
                case InputKey.PERIOD:        return ".";
                case InputKey.SLASH:         return "/";
                case InputKey.BACK_SLASH:    return "\\";
                case InputKey.COLON:         return ":";
                case InputKey.SEMICOLON:     return ";";
                case InputKey.BRACKET_OPEN:  return "[";
                case InputKey.BRACKET_CLOSE: return "]";
                case InputKey.AT_SIGN:       return "@";
                case InputKey.CARET:         return "^";
                case InputKey.HYPHEN:        return "-";
                case InputKey.YEN:           return "\\";
                case InputKey.TAB:           return "{TAB}";

                case InputKey.INSERT:        return "{INS}";
                case InputKey.DELETE:        return "{DEL}";
                case InputKey.HOME:          return "{HOME}";
                case InputKey.END:           return "{END}";
                case InputKey.PGUP:          return "{PGUP}";
                case InputKey.PGDN:          return "{PGDN}";

                case InputKey.PLUS:          return "{ADD}";
                case InputKey.MINUS:         return "{SUBTRACT}";
                case InputKey.MULTIPLY:      return "{MULTIPLY}";
                case InputKey.DIVIDE:        return "{DIVIDE}";

                case InputKey.ESC:
                //case InputKey.LWIN:
                case InputKey.NONCONVERT:
                    throw new ArgumentException($"{key} にSendWait()メソッドに渡せる文字列形式はありません。");

                case InputKey.NONE:          return "";
            }

            throw new ArgumentException();
        }

        
    }
}
