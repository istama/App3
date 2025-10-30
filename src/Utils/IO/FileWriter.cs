using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    sealed class FileWriter
    {
        private readonly string _filepath;
        private readonly Encoding _encoding;
        

        public FileWriter(string filepath, Encoding encoding)
        {
            Assert.IsNullOrEmpty(filepath, nameof(filepath));

            if (!filepath.IsValidAsPath())
                throw new ArgumentException("ファイルパスとして不正な文字列です。", nameof(filepath));

            _filepath = filepath;
            _encoding = encoding;
        }


        public async Task WriteAsync(String text)
        {
            using (var fs = new FileStream(_filepath, FileMode.Create, FileAccess.Write, FileShare.Read, 1024, true))
            {
                using (var sw = new StreamWriter(fs, _encoding, 1024))
                {
                    await sw.WriteLineAsync(text).ConfigureAwait(false);
                }
            }
        }
    }
}
