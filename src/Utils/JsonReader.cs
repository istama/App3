using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class JsonReader
    {
        private readonly FileReader _reader;
        private readonly IDeserializer _deserializer;

        public JsonReader(FileReader reader, IDeserializer deserializer)
        {
            Assert.IsNull(reader, nameof(reader));
            Assert.IsNull(deserializer, nameof(deserializer));

            _reader = reader;
            _deserializer = deserializer;
        }


        public string Filepath => _reader.Filepath;


        public bool FileExists()
        {
            return _reader.FileExists();
        }

        public DateTime GetLastWriteTime()
        {
            return _reader.GetLastWriteTime();
        }

        public async Task<T> ReadAsync<T>()
        {
            var json = await _reader.ReadAllAsync().ConfigureAwait(false);
            var settings = _deserializer.Deserialize<T>(json);

            if (settings == null)
                throw new InvalidOperationException(".jsonファイルが読み込めません。");

            return settings;
        }
    }
}
