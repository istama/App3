using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    sealed class UserConfigReader
    {
        private readonly IUserConfigDTOReader _dtoReader;

        private UserConfigDTO _userConfigDtoBuffer;
        private DateTime _lastReadTime = default;


        public UserConfigReader(IUserConfigDTOReader dtoReader)
        {
            Assert.IsNull(dtoReader, nameof(dtoReader));

            _dtoReader = dtoReader;
        }


        /// <summary>
        /// ユーザーアカウント情報を取得する。
        /// </summary>
        public async Task<UserAccount> GetUserAccountAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            return new UserAccount(dto.UserName, dto.Password);
        }

        /// <summary>
        /// KeyReplacerの設定ファイルのパス。
        /// </summary>
        public async Task<string> GetKeyReplacerSettingsFilepathAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            return dto.KeyReplaceSettingsFilePath;
        }

        /// <summary>
        /// 問番テキストボックスを選択状態にするための操作モード。
        /// </summary>
        public async Task<ToibanSelectMode> GetToibanSelectModeAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            if (dto.SelectToibanByClickChecked)
                return ToibanSelectMode.ByClick;

            if (dto.SelectToibanByWClickChecked)
                return ToibanSelectMode.ByWClick;

            return ToibanSelectMode.ByClick;
        }

        //public Task<NaireOpenMode> GetNaireOpenModeAsync()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 編集を開いたときの動作モードを取得する。
        /// </summary>
        public async Task<HensyuOpenMode> GetHensyuOpenModeAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            if (dto.OpenHensyuMenuOnlyChecked)
                return HensyuOpenMode.MenuWindow;

            if (dto.OpenHensyuTegumiWindowChecked)
                return HensyuOpenMode.TegumiWindow;

            // どちらにもチェックが入ってない場合
            return HensyuOpenMode.MenuWindow;
        }

        /// <summary>
        /// インフォメーションを開いたときの動作モードを取得する。
        /// </summary>
        public async Task<InformationOpenMode> GetInformationOpenModeAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            if (dto.OpenInformationSearchFormOnlyChecked)
                return InformationOpenMode.SearchForm;

            if (dto.OpenInformationDetailWindowChecked)
                return InformationOpenMode.DetailWindow;

            if (dto.OpenInformationKouseiPageChecked)
                return InformationOpenMode.KouseiPage;

            if (dto.OpenInformationKumihanPageChecked)
                return InformationOpenMode.KumihanPage;

            // どれにもチェックが入っていない場合
            return InformationOpenMode.SearchForm;
        }

        /// <summary>
        /// インフォメーションを開いたときに問番を出力リストに追加するかどうか。
        /// </summary>
        public async Task<bool> ShouldAddToibanToCheckedListWhenInformationSearchAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            return dto.ShouldAddToibanToCheckedListWhenInformationOpenChecked;
        }

        /// <summary>
        /// 校正紙に出力した出力リストの問番からチェックを外すかどうか。
        /// </summary>
        public async Task<bool> ShouldUncheckToibanFromCheckedListWhenEnterToKouseishiAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            return dto.ShouldUncheckToibanFromCheckedListChecked;
        }

        public async Task<ToibanCheckedListClearMode> GetToibanCheckedListClearModeAsync()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            if (dto.RemoveToibanAllChecked)
                return ToibanCheckedListClearMode.All;

            if (dto.RemoveToibanCheckedChecked)
                return ToibanCheckedListClearMode.CheckedOnly;

            if (dto.RemoveToibanUncheckedChecked)
                return ToibanCheckedListClearMode.UncheckedOnly;

            return ToibanCheckedListClearMode.CheckedOnly;
        }

        /// <summary>
        /// 出力リストの文字サイズ。
        /// </summary>
        public async Task<int> GetToibanCheckedListCharSize()
        {
            var dto = await GetUserConfigDTOAsync().ConfigureAwait(false);

            return dto.CheckedToibanListCharSize;
        }
        

        private async Task<UserConfigDTO> GetUserConfigDTOAsync()
        {
            if (_userConfigDtoBuffer == null || IsUpdated())
            {
                _userConfigDtoBuffer = await _dtoReader.ReadAsync().ConfigureAwait(false);
                _lastReadTime = DateTime.Now;
            }

            return _userConfigDtoBuffer;
        }

        public bool IsUpdated()
        {
            return _dtoReader.IsUpdated(_lastReadTime);
        }



        //private async Task<UserConfigDTO> GetUserConfigBufferAsync()
        //{
        //    if (_userConfigDtoBuffer != null && !_reader.IsUpdated())
        //        return _userConfigDtoBuffer;
        //
        //    _userConfigDtoBuffer = await _reader.ReadAsync().ConfigureAwait(false);
        //
        //    return _userConfigDtoBuffer;
        //}
    }
}
