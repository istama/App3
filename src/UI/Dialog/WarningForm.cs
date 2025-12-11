using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.NengaBooster.UI.Dialog
{
    public partial class WarningForm : Form
    {
        public WarningForm(string message)
        {
            InitializeComponent();

            this.labelMessage.Text = message;

            // 1. システム標準の警告アイコン（MessageBoxIcon.Warningと同じ）を取得します。
            // SystemIcons.Warning は System.Drawing.Icon 型です。
            System.Drawing.Icon warningIcon = System.Drawing.SystemIcons.Warning;

            // 2. PictureBoxに表示するために、IconをBitmapまたはImageに変換する必要があります。
            // Icon.ToBitmap() メソッドを使用します。
            pictureBox1.Image = warningIcon.ToBitmap();

            // 3. PictureBoxのサイズモードを設定します（推奨）。
            // SizeModeをZoom、StretchImage、またはAutoSizeにすると、PictureBoxのサイズに合わせてアイコンのサイズを調整できます。
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // 💡 PictureBoxのサイズを適切なアイコンサイズに設定してください
            // 通常、システムアイコンは 32x32 や 48x48 などのサイズでレンダリングされます。
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
