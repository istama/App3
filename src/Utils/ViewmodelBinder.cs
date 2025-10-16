using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    /// <summary>
    /// ビューモデルのプロパティとコントロールのプロパティをバインディングする。
    /// 以下のような使い方を想定している。
    /// 
    ///   var binder = new ViewModelBinder(viewmodel);
    ///   binder.BindText(LabelUserName, nameof(viewmodel.UserName));
    ///   
    /// LabelUserName.Textとviewmodel.UserNameの値が、どちらかが更新されるともう一方も更新されるようになる。
    /// </summary>
    sealed class ViewModelBinder
    {
        private readonly ViewModelBase _viewmodel;

        public ViewModelBinder(ViewModelBase viewmodel)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));

            _viewmodel = viewmodel;
        }

        public void BindEnabled(Control control, String vm_property_name)
            => BindProperty(control, nameof(control.Enabled), vm_property_name);

        public void BindText(Control control, String vm_property_name)
            => BindProperty(control, nameof(control.Text), vm_property_name);

        public void BindForeColor(Control control, String vm_property_name)
            => BindProperty(control, nameof(control.ForeColor), vm_property_name);

        public void BindBackColor(Control control, String vm_property_name)
            => BindProperty(control, nameof(control.BackColor), vm_property_name);

        public void BindChecked(CheckBox checkbox, String vm_property_name)
            => BindProperty(checkbox, nameof(checkbox.Checked), vm_property_name);

        public void BindProperty(Control control, String control_property_name, String viewmodel_property_name)
        {
            // コントロールの初期値をviewmodelのプロパティにセットする
            var vm_property = _viewmodel.GetType().GetProperty(viewmodel_property_name);
            if (vm_property == null)
                return;

            var control_property = control.GetType().GetProperty(control_property_name);
            var control_value = control_property.GetValue(control);

            vm_property.SetValue(_viewmodel, control_value);

            // DataSourceUpdateModeの値によって挙動が変わる
            // OnPropertyChangedの場合 : controlに値がセットされるたびに、バインディングされたviewmodelに値がセットされる
            // OnValidationの場合      : viewmodelの値が参照されるときに初めてcontrolの値をviewmodelにセットする
            control.DataBindings.Add(control_property_name, _viewmodel, viewmodel_property_name, false, DataSourceUpdateMode.OnPropertyChanged);
        }


        private Dictionary<String, RadioButton> _vm_property_name_and_radio_button;

        public void BindChecked(RadioButton radio_button, String vm_property_name)
        {
            if (_vm_property_name_and_radio_button == null)
            {
                _vm_property_name_and_radio_button = new Dictionary<string, RadioButton>();

                _viewmodel.PropertyChanged += (sender, e) =>
                {
                    // 変更されたviewmodelのプロパティの名前がDictionaryに登録されているか確認
                    var property_name = e.PropertyName;
                    if (!_vm_property_name_and_radio_button.ContainsKey(property_name))
                        return;

                    // 変更されたviewmodelのプロパティの値を取得する
                    var vm_prop = _viewmodel.GetType().GetProperty(property_name);
                    var value = (Boolean)vm_prop.GetValue(_viewmodel);

                    // 取得した値をメニューのチェック状態にセットする
                    var menu = _vm_property_name_and_radio_button[property_name];
                    menu.Checked = value;
                };
            }

            /*
             * RadioButton.CheckedをRadioButton.DataBindings.Add()でビューモデルとバインドすると、
             * ラジオボタンを２回クリックしないとラジオボタンのチェックが移動しないようになってしまう。
             * そのため、イベントリスナを使って自前でバインドしている。
             */
            radio_button.CheckedChanged += (sender, args) =>
            {
                var vm_prop = _viewmodel.GetType().GetProperty(vm_property_name);
                if (vm_prop != null)
                    vm_prop.SetValue(_viewmodel, radio_button.Checked);
            };

            _vm_property_name_and_radio_button.Add(vm_property_name, radio_button);

            // コントロールの初期値をviewmodelのプロパティにセットする
            var vm_property = _viewmodel.GetType().GetProperty(vm_property_name);
            if (vm_property != null)
                vm_property.SetValue(_viewmodel, radio_button.Checked);
        }

        private Dictionary<String, ToolStripMenuItem> _vm_property_name_and_menu_items;

        public void BindMenuItemCheckState(ToolStripMenuItem menu_item, String vm_property_name)
        {
            if (_vm_property_name_and_menu_items == null)
            {
                _vm_property_name_and_menu_items = new Dictionary<string, ToolStripMenuItem>();

                _viewmodel.PropertyChanged += (sender, e) =>
                {
                    // 変更されたviewmodelのプロパティの名前がDictionaryに登録されているか確認
                    var property_name = e.PropertyName;
                    if (!_vm_property_name_and_menu_items.ContainsKey(property_name))
                        return;

                    // 変更されたviewmodelのプロパティの値を取得する
                    var vm_prop = _viewmodel.GetType().GetProperty(property_name);
                    var value = (CheckState)vm_prop.GetValue(_viewmodel);

                    // 取得した値をメニューのチェック状態にセットする
                    var menu = _vm_property_name_and_menu_items[property_name];
                    menu.CheckState = value;
                };
            }

            _vm_property_name_and_menu_items.Add(vm_property_name, menu_item);

            // コントロールの初期値をviewmodelのプロパティにセットする
            var vm_property = _viewmodel.GetType().GetProperty(vm_property_name);
            if (vm_property != null)
                vm_property.SetValue(_viewmodel, menu_item.CheckState);
        }
    }
}
