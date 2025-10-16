using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// DIコンテナからサービスのインスタンスを取得するための <see cref="IServiceProvider"/> の実装です。
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        public bool DebugMode { get; set; } = false;

        private readonly List<ServiceDescriptor> _serviceDescriptors;
        private readonly Dictionary<Type, object> _singletonInstances = new Dictionary<Type, object>();
        private readonly Dictionary<(Type, Key), object> _keyedSingletonInstances = new Dictionary<(Type, Key), object>();

        /// <summary>
        /// 指定されたサービス記述子のリストを使用して <see cref="ServiceProvider"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serviceDescriptors">サービス記述子のリスト。</param>
        internal ServiceProvider(List<ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }

        /// <inheritdoc />
        public T GetRequiredService<T>()
        {
            return (T)GetRequiredService(typeof(T));
        }

        /// <inheritdoc />
        public object GetRequiredService(Type serviceType)
        {
            if (_singletonInstances.TryGetValue(serviceType, out var instance))
            {
                return instance;
            }

            var descriptors = _serviceDescriptors.Where(d => d.ServiceType == serviceType && d.Key == Key.Empty).ToList();
            if (descriptors.Count == 0)
            {
                throw new InvalidOperationException($"Service of type {serviceType.Name} is not registered.");
            }
            if (descriptors.Count > 1)
            {
                throw new InvalidOperationException($"Multiple services of type {serviceType.Name} are registered.");
            }
            var descriptor = descriptors.Single();

            if (descriptor.ImplementationInstance != null)
            {
                _singletonInstances[serviceType] = descriptor.ImplementationInstance;
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationFactory != null)
            {
                var instanceFromFactory = descriptor.ImplementationFactory(this);
                _singletonInstances[serviceType] = instanceFromFactory;
                return instanceFromFactory;
            }

            var implementation = CreateInstance(descriptor.ImplementationType, Key.Empty);
            _singletonInstances[serviceType] = implementation;
            return implementation;
        }

        /// <inheritdoc />
        public T GetRequiredKeyedService<T>(Key key)
        {
            return (T)GetRequiredKeyedService(typeof(T), key);
        }

        /// <summary>
        /// 指定されたキーと型のキー付きサービスインスタンスを取得します。
        /// </summary>
        /// <param name="serviceType">取得するサービスの型。</param>
        /// <param name="key">サービスを識別するためのキー。</param>
        /// <returns>サービスのインスタンス。</returns>
        private object GetRequiredKeyedService(Type serviceType, Key key)
        {
            var cacheKey = (serviceType, key);
            if (_keyedSingletonInstances.TryGetValue(cacheKey, out var instance))
            {
                return instance;
            }

            var descriptors = _serviceDescriptors.Where(d => d.Key == key && d.ServiceType == serviceType).ToList();
            if (descriptors.Count == 0)
            {
                throw new InvalidOperationException($"Service with key {key} and type {serviceType.Name} is not registered.");
            }
            if (descriptors.Count > 1)
            {
                throw new InvalidOperationException($"Multiple services with key {key} and type {serviceType.Name} are registered.");
            }
            var descriptor = descriptors.Single();

            if (descriptor.ImplementationInstance != null)
            {
                _keyedSingletonInstances[cacheKey] = descriptor.ImplementationInstance;
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationFactory != null)
            {
                var instanceFromFactory = descriptor.ImplementationFactory(this);
                _keyedSingletonInstances[cacheKey] = instanceFromFactory;
                return instanceFromFactory;
            }

            var implementation = CreateInstance(descriptor.ImplementationType, key);
            _keyedSingletonInstances[cacheKey] = implementation;
            return implementation;
        }

        /// <summary>
        /// 指定された実装型のインスタンスを生成します。
        /// </summary>
        /// <param name="implementationType">生成するインスタンスの実装型。</param>
        /// <param name="resolutionKey">依存関係の解決に使用する可能性のあるキー。</param>
        /// <returns>生成されたインスタンス。</returns>
        /// <exception cref="InvalidOperationException">インスタンスの生成に失敗した場合にスローされます。</exception>
        private object CreateInstance(Type implementationType, Key resolutionKey)
        {
            var constructors = implementationType.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);

            if (!constructors.Any())
            {
                throw new InvalidOperationException($"No public constructor found for {implementationType.Name}.");
            }

            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters()
                        .Select(p =>
                        {
                            if (resolutionKey != Key.Empty)
                            {
                                var keyedDescriptor = _serviceDescriptors.FirstOrDefault(d => d.ServiceType == p.ParameterType && d.Key == resolutionKey);
                                if (keyedDescriptor != null)
                                {
                                    return GetRequiredKeyedService(p.ParameterType, resolutionKey);
                                }
                            }
                            return GetRequiredService(p.ParameterType);
                        })
                        .ToArray();

                    return constructor.Invoke(parameters);
                }
                catch (InvalidOperationException ex)
                {
                    // This constructor failed, try the next one.
                    if (DebugMode)
                        System.Windows.Forms.MessageBox.Show("ServiceProvider.CreateInstance()" + Environment.NewLine + Environment.NewLine + ex.ToString());
                }
            }

            throw new InvalidOperationException($"Failed to instantiate {implementationType.Name}. Could not resolve dependencies for any constructor.");
        }
    }
}
