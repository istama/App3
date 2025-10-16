using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class JsonWriter
    {
        private readonly FileWriter _textWriter;
        private readonly ISerializer _serializer;

        public JsonWriter(FileWriter textWriter, ISerializer serializer)
        {
            Assert.IsNull(textWriter, nameof(textWriter));
            Assert.IsNull(serializer, nameof(serializer));

            _textWriter = textWriter;
            _serializer = serializer;
        }


        public async Task WriteAsync<T>(T obj)
        {
            Assert.IsNull(obj, nameof(obj));

            var json = _serializer.Serialize(obj);

            await _textWriter.WriteAsync(json).ConfigureAwait(false);
        }
    }
}
