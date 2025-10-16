using System;
using System.Collections.Generic;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// DIコンテナにサービスを登録するための <see cref="IServiceCollection"/> の実装です。
    /// </summary>
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();

        /// <summary>
        /// <see cref="ServiceCollection"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ServiceCollection()
        {
        }

        /// <inheritdoc />
        public void AddSingleton<T>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(T), typeof(T)));
        }

        /// <inheritdoc />
        public void AddSingleton<I, T>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(I), typeof(T)));
        }

        /// <inheritdoc />
        public void AddSingleton<I>(I instance)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(I), instance));
        }

        /// <inheritdoc />
        public void AddSingleton<T>(Func<IServiceProvider, T> implementationFactory)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(T), provider => implementationFactory(provider)));
        }

        /// <inheritdoc />
        public void AddKeyedSingleton<T>(Key key)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(T), typeof(T), key));
        }

        /// <inheritdoc />
        public void AddKeyedSingleton<I, T>(Key key)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(I), typeof(T), key));
        }

        /// <inheritdoc />
        public void AddKeyedSingleton<I>(Key key, I instance)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(I), instance, key));
        }

        /// <inheritdoc />
        public void AddKeyedSingleton<T>(Key key, Func<IServiceProvider, T> implementationFactory)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(T), provider => implementationFactory(provider), key));
        }

        /// <inheritdoc />
        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_serviceDescriptors);
        }
    }
}
