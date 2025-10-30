using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    static class KeysExtensions
    {
        /// <summary>
        /// カーソルキーならtrueを返す。
        /// </summary>
        public static Boolean IsCursorKey(this Keys key)
            => key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right;
    }
}
