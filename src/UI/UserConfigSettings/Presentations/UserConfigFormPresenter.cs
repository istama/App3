using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserConfigSettings.Presentations
{
    sealed class UserConfigFormPresenter
    {
        private readonly UserConfigFormViewModel _viewmodel;
        private readonly IUserConfigIOFactory _userConfigIOFactory;
        private readonly UserConfigDtoAndViewModelMapper _mapper;


        public UserConfigFormPresenter(
            UserConfigFormViewModel viewmodel,
            IUserConfigIOFactory userConfigIOFactory, 
            UserConfigDtoAndViewModelMapper mapper)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));
            Assert.IsNull(userConfigIOFactory, nameof(userConfigIOFactory));
            Assert.IsNull(mapper, nameof(mapper));

            _viewmodel = viewmodel;
            _userConfigIOFactory = userConfigIOFactory;
            _mapper = mapper;
        }

        /*
         * <設計の考え方>
         * 設定ファイルの内容をそのままフォームに表示したり、フォームの内容をファイルに書き込むだけの処理の場合、
         * データはビジネスロジックを経由しないのでユースケース以降の層は不要。
         * そして、Controllerもその役割はビューとロジックをつなぐものなので、
         * ただ設定ファイルの読み書きを行うだけのフォームの場合、Controllerがあることは過剰設計となる。
         * その場合は、Presenterにファイルの読み込み/書き込みの処理を記述し、ビューから処理を呼び出すことは一般的な設計。
         */

        /// <summary>
        /// 保存された設定をフォームにロードする。
        /// </summary>
        public async Task LoadAsync(string userConfigFilepath)
        {
            Assert.IsNullOrWhiteSpace(userConfigFilepath, nameof(userConfigFilepath));
            Assert.IsNot(userConfigFilepath.IsValidAsPath(), $"パスの書式が不正です。 {userConfigFilepath}");

            // DTOのすべてのプロパティ名がviewmodelのプロパティに定義されているか
            if (!_mapper.AreAllDtoPropertyNamesDefinedInVm(out var undefinedPropName))
            {
                throw new NengaBoosterException($"{nameof(UserConfigDTO)}.{undefinedPropName} が {nameof(UserConfigFormViewModel)} に定義されていません。");
            }

            // ユーザー設定ファイルを読み込むインスタンスを取得
            var reader = _userConfigIOFactory.GetOrCreateUserConfigDTOReader(userConfigFilepath);
            // ユーザー設定を読み込む
            var dto = await reader.ReadAsync().ConfigureAwait(false);
            // 設定をViewmodelにセットする
            _mapper.SetDtoPropertyValuesToViewModel(dto, _viewmodel);
        }

        /// <summary>
        /// フォームの設定を保存する。
        /// </summary>
        public async Task SaveAsync(string userConfigFilepath)
        {
            Assert.IsNullOrWhiteSpace(userConfigFilepath, nameof(userConfigFilepath));
            Assert.IsNot(userConfigFilepath.IsValidAsPath(), $"パスの書式が不正です。 {userConfigFilepath}");

            // viewmodelのすべてのプロパティ名がDTOのプロパティに定義されているか
            if (!_mapper.AreAllVmPropertyNamesDefinedInDto(out var undefinedPropName))
            {
                throw new NengaBoosterException($"{nameof(UserConfigFormViewModel)}.{undefinedPropName} が {nameof(UserConfigDTO)} に定義されていません。");
            }

            var dto = new UserConfigDTO();
            // viewmodelの値ををDTOにセットする
            _mapper.SetVmPropertyValuesToDto(_viewmodel, dto);
            
            // ユーザー設定ファイルを書き込むインスタンスを取得
            var writer = _userConfigIOFactory.GetOrCreateUserConfigDTOWriter(userConfigFilepath);
            // 設定を保存する
            await writer.SaveAsync(dto).ConfigureAwait(false);
        }

        /// <summary>
        /// ViewModelの値が更新されているならtrueを返す。
        /// </summary>
        public async Task<bool> Updated(string userConfigFilepath)
        {
            Assert.IsNullOrWhiteSpace(userConfigFilepath, nameof(userConfigFilepath));
            Assert.IsNot(userConfigFilepath.IsValidAsPath(), $"パスの書式が不正です。 {userConfigFilepath}");

            var reader = _userConfigIOFactory.GetOrCreateUserConfigDTOReader(userConfigFilepath);
            var dto = await reader.ReadAsync().ConfigureAwait(false);
            return !_mapper.AreEquals(dto, _viewmodel);
        }
    }
}
