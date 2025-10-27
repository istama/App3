using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    sealed class MyResource
    {
        /// <summary>
        /// バイナリとして実行ファイルに埋め込まれたアイコン画像を取得する。
        /// resource_nameは必ず.ico拡張子のファイルでなければならない。
        /// </summary>
        public static Icon GetIcon(String resource_name)
        {
            Assert.IsNull(resource_name, nameof(resource_name));

            // タスクバーのアイコンを設定する
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            // Visual Studioでビルドするときは NengaBooster.ngb.ico にする
            using (var stream = myAssembly.GetManifestResourceStream(resource_name))
            {
                if (stream != null)
                    return new Icon(stream);
                else
                    return null;
            }
        }
    }
}
