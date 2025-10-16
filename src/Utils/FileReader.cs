using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class FileReader
    {
        public string Filepath { get; }
        private readonly Encoding _encoding;


        public FileReader(string filepath, Encoding encoding)
        {
            Assert.IsNullOrEmpty(filepath, nameof(filepath));
            Assert.IsNull(encoding, nameof(encoding));

            if (!filepath.IsValidAsPath())
                throw new ArgumentException("ファイルパスとして不正な文字列です。", nameof(filepath));

            Filepath = filepath;
            _encoding = encoding;
        }


        public bool FileExists()
        {
            return File.Exists(Filepath);
        }

        public DateTime GetLastWriteTime()
        {
            if (!FileExists())
                throw new InvalidOperationException("ファイルが存在しません。");

            return File.GetLastWriteTime(Filepath);
        }

        public async Task<string> ReadAllAsync()
        {
            using (var fs = new FileStream(Filepath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, true))
            {
                using (var sr = new StreamReader(fs, _encoding))
                {
                    return await sr.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
