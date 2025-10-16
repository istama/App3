using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.View
{
    sealed class NengaBoosterConfigFormExecutor : INengaBoosterConfigForm
    {
        private readonly NengaBoosterConfigFormViewModel _viewmodel;
        private readonly NengaBoosterConfigFormPresenter _presenter;


        public NengaBoosterConfigFormExecutor(NengaBoosterConfigFormViewModel viewmodel, NengaBoosterConfigFormPresenter presenter)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));
            Assert.IsNull(presenter, nameof(presenter));

            _viewmodel = viewmodel;
            _presenter = presenter;
        }


        public void Show(Form owner)
        {
            var form = new NengaBoosterConfigForm(_viewmodel, _presenter);
            form.Show(owner);
        }

        public void Show(Form owner, Action<object, FormClosedEventArgs> finalization)
        {
            var form = new NengaBoosterConfigForm(_viewmodel, _presenter);

            // フォームが閉じたときに実行する処理
            form.FormClosed += (object sender, FormClosedEventArgs e) =>
            {
                finalization(sender, e);
            };

            form.Show(owner);
        }
    }
}
