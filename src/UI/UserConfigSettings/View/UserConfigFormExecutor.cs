using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.NengaBooster.UI.UserConfigSettings.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserConfigSettings.View
{
    sealed class UserConfigFormExecutor : IUserConfigForm
    {
        private readonly UserConfigFormViewModel _viewmodel;
        private readonly UserConfigFormPresenter _presenter;


        public UserConfigFormExecutor(UserConfigFormViewModel viewmodel, UserConfigFormPresenter presenter)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));
            Assert.IsNull(presenter, nameof(presenter));

            _viewmodel = viewmodel;
            _presenter = presenter;
        }


        public DialogResult ShowDialog(Form owner, string filepath)
        {
            if (String.IsNullOrWhiteSpace(filepath))
                return DialogResult.Cancel;

            if (!filepath.IsValidAsPath())
                throw new ArgumentException($"パスの書式が正しくありません。 {filepath}");

            var form = new UserConfigForm(filepath, _viewmodel, _presenter);
            form.ShowDialog(owner);

            return form.DialogResult;
        }
    }
}
