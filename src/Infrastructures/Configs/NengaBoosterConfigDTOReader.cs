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
    /// NengaBoosterの設定を読み込むクラス。
    /// </summary>
    sealed class NengaBoosterConfigDTOReader : INengaBoosterConfigDTOReader
    {
        private readonly JsonReader _reader;
        private NengaBoosterConfigDTO _dtoForCache;
        private DateTime _lastWriteTime = DateTime.MinValue;


        public NengaBoosterConfigDTOReader(JsonReader reader)
        {
            Assert.IsNull(reader, nameof(reader));

            _reader = reader;
        }


        /// <inheritdoc />
        public async Task<NengaBoosterConfigDTO> ReadAsync()
        {
            // 設定ファイルが存在するか
            var fileExists = _reader.FileExists();
        
            // 最後に読み込んでから更新されてないならバッファを返す
            if (fileExists && !IsUpdated(_lastWriteTime) && _dtoForCache != null)
            {
                return _dtoForCache;
            }
        
            // ファイルが存在するなら読み込む、しないならデフォルト値を返す
            var dto = fileExists
                ? await _reader.ReadAsync<NengaBoosterConfigDTO>().ConfigureAwait(false)
                : new NengaBoosterConfigDTO();
        
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

        /// <summary>
        /// 引数に最後にReadAsync()を呼び出した時刻を渡し、DTOファイルが更新された時刻がそれより後ならtrueを返す。
        /// まだReadAsync()を呼び出していないときはdefaultを渡す。
        /// なお、設定ファイルが存在しない場合は、つねにfalseを返す。
        /// </summary>
        public bool IsUpdated(DateTime lastReadTime)
        {
            if (!_reader.FileExists() || lastReadTime == default)
                return false;

            return _reader.GetLastWriteTime() > lastReadTime;
        }
    }
}
