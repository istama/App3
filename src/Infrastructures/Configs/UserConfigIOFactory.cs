using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.Configs
{
    /// <summary>
    /// UserConfigの読み書きを行うインスタンスを生成するファクトリクラス。
    /// </summary>
    class UserConfigIOFactory : IUserConfigIOFactory
    {
        private readonly ISerializer _jsonSerializer;
        private readonly IDeserializer _jsonDeserializer;

        private readonly Dictionary<string, IUserConfigDTOReader> _filepathAndReaderMap = new Dictionary<string, IUserConfigDTOReader>();
        private readonly Dictionary<string, IUserConfigDTOWriter> _filepathAndWriterMap = new Dictionary<string, IUserConfigDTOWriter>();


        public UserConfigIOFactory(ISerializer jsonSerializer, IDeserializer jsonDeserializer)
        {
            Assert.IsNull(jsonSerializer, nameof(jsonSerializer));
            Assert.IsNull(jsonDeserializer, nameof(jsonDeserializer));

            _jsonSerializer = jsonSerializer;
            _jsonDeserializer = jsonDeserializer;
        }


        /// <summary>
        /// UserConfigReaderのインスタンスを生成する。
        /// 同じファイルパスで生成済みなら同じインスタンスを返す。
        /// </summary>
        public IUserConfigDTOReader GetOrCreateUserConfigDTOReader(string filepath)
        {
            Assert.IsNullOrWhiteSpace(filepath, nameof(filepath));
            Assert.IsNot(filepath.IsValidAsPath(), $"パスの書式ではありません。 {filepath}");

            if (!_filepathAndReaderMap.ContainsKey(filepath))
            {
                var fileReader = new FileReader(filepath, Encoding.UTF8);
                var jsonReader = new JsonReader(fileReader, _jsonDeserializer);
                var configReader = new UserConfigDTOReader(jsonReader);
                _filepathAndReaderMap.Add(filepath, configReader);
            }

            return _filepathAndReaderMap[filepath];
        }

        /// <summary>
        /// UserConfigWriterのインスタンスを生成する。
        /// 同じファイルパスで生成済みなら同じインスタンスを返す。
        /// </summary>
        public IUserConfigDTOWriter GetOrCreateUserConfigDTOWriter(string filepath)
        {
            Assert.IsNullOrWhiteSpace(filepath, nameof(filepath));
            Assert.IsNot(filepath.IsValidAsPath(), $"パスの書式ではありません。 {filepath}");

            if (!_filepathAndWriterMap.ContainsKey(filepath))
            {
                var fileWriter = new FileWriter(filepath, Encoding.UTF8);
                var jsonWriter = new JsonWriter(fileWriter, _jsonSerializer);
                var configWriter = new UserConfigDTOWriter(jsonWriter);
                _filepathAndWriterMap.Add(filepath, configWriter);
            }

            return _filepathAndWriterMap[filepath];
        }
    }
}
