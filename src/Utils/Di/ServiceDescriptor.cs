using System;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// 登録されたサービスに関する情報を保持します。
    /// </summary>
    internal class ServiceDescriptor
    {
        /// <summary>
        /// サービスの型を取得します。
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// サービスの実装型を取得します。
        /// </summary>
        public Type ImplementationType { get; private set; }

        /// <summary>
        /// サービスのシングルトンインスタンスを取得または設定します。
        /// </summary>
        public object ImplementationInstance { get; internal set; }

        /// <summary>
        /// サービスのインスタンスを生成するファクトリメソッドを取得します。
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory { get; private set; }

        /// <summary>
        /// サービスのキーを取得します。
        /// </summary>
        public Key Key { get; }

        /// <summary>
        /// キー付きサービスとして、サービス型と実装型を使用して <see cref="ServiceDescriptor"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceType">サービスの型。</param>
        /// <param name="implementationType">サービスの実装型。</param>
        /// <param name="key">サービスを識別するためのキー。</param>
        public ServiceDescriptor(Type serviceType, Type implementationType, Key key)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Key = key;
        }

        /// <summary>
        /// サービス型と実装型を使用して <see cref="ServiceDescriptor"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceType">サービスの型。</param>
        /// <param name="implementationType">サービスの実装型。</param>
        public ServiceDescriptor(Type serviceType, Type implementationType)
            : this(serviceType, implementationType, Key.Empty)
        {
        }

        /// <summary>
        /// キー付きサービスとして、ファクトリメソッドを使用して <see cref="ServiceDescriptor"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceType">サービスの型。</param>
        /// <param name="factory">サービスのインスタンスを生成するファクトリメソッド。</param>
        /// <param name="key">サービスを識別するためのキー。</param>
        public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> factory, Key key)
        {
            ServiceType = serviceType;
            ImplementationFactory = factory;
            Key = key;
        }

        /// <summary>
        /// ファクトリメソッドを使用して <see cref="ServiceDescriptor"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceType">サービスの型。</param>
        /// <param name="factory">サービスのインスタンスを生成するファクトリメソッド。</param>
        public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> factory)
            : this(serviceType, factory, Key.Empty)
        {
        }

        /// <summary>
        /// キー付きサービスとして、既存のインスタンスを使用して <see cref="ServiceDescriptor"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceType">サービスの型。</param>
        /// <param name="instance">登録するインスタンス。</param>
        /// <param name="key">サービスを識別するためのキー。</param>
        public ServiceDescriptor(Type serviceType, object instance, Key key)
        {
            ServiceType = serviceType;
            ImplementationInstance = instance;
            Key = key;
        }

        /// <summary>
        /// 既存のインスタンスを使用して <see cref="ServiceDescriptor"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceType">サービスの型。</param>
        /// <param name="instance">登録するインスタンス。</param>
        public ServiceDescriptor(Type serviceType, object instance)
            : this(serviceType, instance, Key.Empty)
        {
        }
    }
}