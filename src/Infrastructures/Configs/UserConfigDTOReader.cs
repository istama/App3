using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.Configs
{
    class UserConfigDTOReader : IUserConfigDTOReader
    {
        private readonly JsonReader _reader;
        private UserConfigDTO _dtoForCache;
        private DateTime _lastWriteTime = DateTime.MinValue;


        public UserConfigDTOReader(JsonReader reader)
        {
            Assert.IsNull(reader, nameof(reader));

            _reader = reader;
        }

        /// <inheritdoc />
        public async Task<UserConfigDTO> ReadAsync()
        {
            // 設定ファイルが存在するか
            var fileExists = _reader.FileExists();

            // 最後に読み込んでから更新されてないならバッファを返す
            if (fileExists && !IsUpdated() && _dtoForCache != null)
            {
                return _dtoForCache;
            }

            // ファイルが存在するなら読み込む、しないならデフォルト値を返す
            var dto = fileExists
                ? await _reader.ReadAsync<UserConfigDTO>().ConfigureAwait(false)
                : new UserConfigDTO();

            _dtoForCache = dto;

            if (fileExists)
                _lastWriteTime = _reader.GetLastWriteTime();

            return _dtoForCache;
        }

        /// <summary>
        /// 設定ファイルが更新されたならtrueを返す。
        /// </summary>
        public bool IsUpdated()
        {
            var fileExists = _reader.FileExists();
            return fileExists && _reader.GetLastWriteTime() > _lastWriteTime;
        }

        public bool IsUpdated(DateTime lastReadTime)
        {
            if (!_reader.FileExists() || lastReadTime == default)
                return false;

            return _reader.GetLastWriteTime() > lastReadTime;
        }
    }
}
