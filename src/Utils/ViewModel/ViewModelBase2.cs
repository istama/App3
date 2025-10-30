using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace IsTama.Utils
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<String, Object> _properties = new Dictionary<string, object>();

        protected ViewModelBase()
        {
        }

        /// <summary>
        /// valueをpropertyNameをキーとしたDictionaryで保存し、PropertyChangedEventHandlerを発生させる。
        /// valueはプロパティの値であることを想定しており、プロパティの変更と同時に変更したことを通知するためのメソッド。
        /// 主な使用目的は、Control.DataBindings()メソッドと併用しプロパティが変更されると同時にコントロールの表示も変更するようバインドするためのもの。
        /// 引数propertyNameには[CallMemberName]属性を付与している。これによって、このメソッドを呼び出したプロパティやメソッドの名前が自動的にこの引数にセットされる。
        /// プロパティ名やメソッド名が変更されてもこのメソッドの引数を変更しなくてすむようになる。
        /// </summary>
        /// <param name="value">プロパティにセットする値</param>
        /// <param name="shouldAlreadyNotify">valueが同じ値でもPropertyChangedイベントを発生させるかどうか</param>
        /// <param name="propertyName">プロパティ名</param>
        protected bool SetProperty<T>(T value,  bool shouldAlreadyNotify=false, [CallerMemberName] string propertyName = "")
        {
            if (String.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("propertyName is null, empty or whitespace.");

            if (_properties.ContainsKey(propertyName))
            {
                var current_value = GetProperty<T>(propertyName);
                if (current_value.Equals(value) && !shouldAlreadyNotify)
                    return false;

                _properties[propertyName] = value;
            }
            else
                _properties.Add(propertyName, value);

            RaisePropertyChanged(propertyName);
            return true;
        }

        protected T GetProperty<T>([CallerMemberName] string propertyName = "")
        {
            if (String.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("propertyName is null, empty or whitespace.");

            if (!_properties.ContainsKey(propertyName))
                return default;

            return (T)_properties[propertyName];
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Volatile.Read(ref this.PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseAllPropertiesChanged()
        {
            foreach (var property_info in GetType().GetProperties())
                RaisePropertyChanged(property_info.Name);
        }

        /// <summary>
        /// PropertyChangedイベントを伝播させるためのメソッド。
        /// 以下のような使い方をする。
        /// class SomeViewModel : ViewModelBase {
        ///  private UserInfoService _userinfo;
        ///  public String Id { 
        ///    get => _userinfo.Id;
        ///    set => _userinfo.Id = value;
        ///  }
        ///  public SomeViewModel() {
        ///    _userinfo = new UserInfo();
        ///    _userinfo.PropertyChanged += base.PropagatePropertyChanged;
        ///  }
        /// }
        /// UserInfoServiceのプロパティが書き換えられて発生したPropertyChangedを、
        /// SomeViewModelが受け取り、そこからさらにPropertyChangedを発生させる。
        /// そのため、UserInfoServiceとSomeViewModelのプロパティ名は一致させる必要がある。
        /// </summary>
        protected void PropagatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
        /// <summary>
        /// 引数のviewmodelのPropertyChangedイベントをこのクラスのPropertyChangedイベントに伝播させる。
        /// </summary>
        protected void PropagatePropertyChangedEventFrom(INotifyPropertyChanged viewmodel)
        {
            viewmodel.PropertyChanged += PropagatePropertyChanged;
        }
    }
}
