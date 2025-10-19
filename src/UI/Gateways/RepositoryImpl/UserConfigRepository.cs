using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.NengaApps;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// ユーザー設定のリポジトリ。
    /// </summary>
    sealed class UserConfigRepository : IUserConfigRepositoryExtended
    {
        private readonly IOtherConfigRepository _otherConfigRepository;
        private readonly IUserConfigIOFactory _userConfigIOFactory;

        private string _userConfigFilepathBuffer;
        private UserConfigReader _userConfigReaderBuffer;
        private UserConfigBuffer _userConfigBuffer;


        public UserConfigRepository(IOtherConfigRepository otherConfigRepository, IUserConfigIOFactory userConfigIOFactory)
        {
            Assert.IsNull(otherConfigRepository, nameof(otherConfigRepository));
            Assert.IsNull(userConfigIOFactory, nameof(userConfigIOFactory));

            _otherConfigRepository = otherConfigRepository;
            _userConfigIOFactory = userConfigIOFactory;
        }


        /// <summary>
        /// ユーザーアカウント情報を取得する。
        /// </summary>
        public async Task<UserAccount> GetUserAccountAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.UserAccount;
        }
        public async Task SetUserAccountAsync(UserAccount userAccount)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.UserAccount = userAccount;
        }

        /// <summary>
        /// KeyReplacerの設定ファイルのパスを返す。
        /// </summary>
        public async Task<string> GetKeyReplacerSettingsFilepathAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.KeyReplacerSettingsFilepath;
        }

        /// <summary>
        /// 問番テキストボックスを選択状態にするための操作モード。
        /// </summary>
        public async Task<ToibanSelectMode> GetToibanSelectModeAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.ToibanSelectMode;
        }
        public async Task SetToibanSelectModeAsync(ToibanSelectMode mode)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.ToibanSelectMode = mode;
        }

        /// <summary>
        /// 注文名入れを開いたときの動作モードを取得する。
        /// </summary>
        /// <returns></returns>
        public async Task<NaireOpenMode> GetNaireOpenModeAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.NaireOpenMode;
        }
        public async Task SetNaireOpenModeAsync(NaireOpenMode mode)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.NaireOpenMode = mode;
        }

        /// <summary>
        /// 編集を開いたときの動作モードを取得する。
        /// </summary>
        public async Task<HensyuOpenMode> GetHensyuOpenModeAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.HensyuOpenMode;
        }
        public async Task SetHensyuOpenModeAsync(HensyuOpenMode mode)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.HensyuOpenMode = mode;
        }

        /// <summary>
        /// インフォメーションを開いたときの動作モードを取得する。
        /// </summary>
        public async Task<InformationOpenMode> GetInformationOpenModeAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.InformationOpenMode;
        }
        public async Task SetInformationOpenModeAsync(InformationOpenMode mode)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.InformationOpenMode = mode;
        }

        /// <summary>
        /// インフォメーションを開いたときに問番を出力リストに追加するかどうか。
        /// </summary>
        public async Task<bool> ShouldAddToibanToCheckedListWhenInformationSearchAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.ShouldAddToibanToCheckedList;
        }
        public async Task SetWhetherToAddToibanToCheckedListWhenInformationSearchAsync(bool shouldAdd)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.ShouldAddToibanToCheckedList = shouldAdd;
        }

        /// <summary>
        /// 校正紙に出力した出力リストの問番からチェックを外すかどうか。
        /// </summary>
        public async Task<bool> ShouldUncheckToibanFromCheckedListWhenEnterToKouseishiAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.ShouldUncheckToiban;
        }
        public async Task SetWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(bool shouldUncheck)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.ShouldUncheckToiban = shouldUncheck;
        }

        /// <summary>
        /// 校正紙に出力した出力リストの問番からチェックを外すかどうか。
        /// </summary>
        public async Task<ToibanCheckedListClearMode> GetToibanCheckedListClearModeAsync()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.ToibanCheckedListClearMode;
        }
        public async Task SetToibanCheckedListClearModeAsync(ToibanCheckedListClearMode mode)
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            config.ToibanCheckedListClearMode = mode;
        }

        /// <summary>
        /// 出力リストの文字サイズ。
        /// </summary>
        public async Task<int> GetToibanCheckedListCharSize()
        {
            var config = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return config.ToibanCheckedListCharSize;
        }

        /// <summary>
        /// バッファに保存したユーザー設定を返すか新たに読み込む。
        /// </summary>
        private async Task<UserConfigBuffer> GetOrReadUserConfigAsync()
        {
            // ユーザーコンフィグファイルパスを取得
            var configFilepath = await _otherConfigRepository.GetUserConfigFilepathAsync().ConfigureAwait(false);

            // コンフィグファイルパスが前回の読み込みと同じ場合
            if (_userConfigFilepathBuffer != null && _userConfigFilepathBuffer == configFilepath)
            {
                // コンフィグファイルが更新された場合
                if (_userConfigReaderBuffer.IsUpdated())
                {
                    _userConfigBuffer = await ReadUserConfigAsync(_userConfigReaderBuffer).ConfigureAwait(false);
                }

                return _userConfigBuffer;
            }

            // コンフィグファイルが変更された場合
            var dtoReader = _userConfigIOFactory.GetOrCreateUserConfigDTOReader(configFilepath);
            var reader = new UserConfigReader(dtoReader);
            var config = await ReadUserConfigAsync(reader).ConfigureAwait(false);

            _userConfigFilepathBuffer = configFilepath;
            _userConfigReaderBuffer = reader;
            _userConfigBuffer = config;

            return _userConfigBuffer;
        }

        private async Task<UserConfigBuffer> ReadUserConfigAsync(UserConfigReader reader)
        {
            return new UserConfigBuffer
            {
                UserAccount = await reader.GetUserAccountAsync(),
                KeyReplacerSettingsFilepath = await reader.GetKeyReplacerSettingsFilepathAsync(),

                ToibanSelectMode = await reader.GetToibanSelectModeAsync(),

                NaireOpenMode = NaireOpenMode.Normal,
                HensyuOpenMode = await reader.GetHensyuOpenModeAsync(),
                InformationOpenMode = await reader.GetInformationOpenModeAsync(),
                ShouldAddToibanToCheckedList = await reader.ShouldAddToibanToCheckedListWhenInformationSearchAsync(),
                ShouldUncheckToiban = await reader.ShouldUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(),

                ToibanCheckedListClearMode = await reader.GetToibanCheckedListClearModeAsync(),
                ToibanCheckedListCharSize = await reader.GetToibanCheckedListCharSize(),
            };
        }

        private class UserConfigBuffer
        {
            public UserAccount UserAccount { get; set; }
            public string KeyReplacerSettingsFilepath { get; set; }

            public ToibanSelectMode ToibanSelectMode { get; set; }

            public NaireOpenMode NaireOpenMode { get; set; }
            public HensyuOpenMode HensyuOpenMode { get; set; }
            public InformationOpenMode InformationOpenMode { get; set; }
            public bool ShouldAddToibanToCheckedList { get; set; }
            public bool ShouldUncheckToiban { get; set; }

            public ToibanCheckedListClearMode ToibanCheckedListClearMode { get; set; }
            public int ToibanCheckedListCharSize { get; set; }
        }
    }
}
