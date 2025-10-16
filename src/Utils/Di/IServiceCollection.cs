using System;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// DIコンテナにサービスを登録するためのメソッドを定義します。
    /// </summary>
    public interface IServiceCollection
    {
        /// <summary>
        /// シングルトンサービスを自身の型で登録します。
        /// </summary>
        /// <typeparam name="T">登録するサービスの型。</typeparam>
        void AddSingleton<T>();

        /// <summary>
        /// 指定されたインターフェースと実装クラスでシングルトンサービスを登録します。
        /// </summary>
        /// <typeparam name="I">サービスのインターフェース型。</typeparam>
        /// <typeparam name="T">サービスの実装型。</typeparam>
        void AddSingleton<I, T>();

        /// <summary>
        /// 既存のインスタンスをシングルトンサービスとして登録します。
        /// </summary>
        /// <typeparam name="I">サービスのインターフェース型。</typeparam>
        /// <param name="instance">登録するインスタンス。</param>
        void AddSingleton<I>(I instance);

        /// <summary>
        /// ファクトリメソッドを使用してシングルトンサービスを登録します。
        /// </summary>
        /// <typeparam name="T">登録するサービスの型。</typeparam>
        /// <param name="implementationFactory">サービスのインスタンスを生成するファクトリメソッド。</param>
        void AddSingleton<T>(Func<IServiceProvider, T> implementationFactory);

        /// <summary>
        /// キー付きシングルトンサービスを自身の型で登録します。
        /// </summary>
        /// <typeparam name="T">登録するサービスの型。</typeparam>
        /// <param name="key">サービスを識別するためのキー。</param>
        void AddKeyedSingleton<T>(Key key);

        /// <summary>
        /// 指定されたインターフェースと実装クラスでキー付きシングルトンサービスを登録します。
        /// </summary>
        /// <typeparam name="I">サービスのインターフェース型。</typeparam>
        /// <typeparam name="T">サービスの実装型。</typeparam>
        /// <param name="key">サービスを識別するためのキー。</param>
        void AddKeyedSingleton<I, T>(Key key);

        /// <summary>
        /// 既存のインスタンスをキー付きシングルトンサービスとして登録します。
        /// </summary>
        /// <typeparam name="I">サービスのインターフェース型。</typeparam>
        /// <param name="key">サービスを識別するためのキー。</param>
        /// <param name="instance">登録するインスタンス。</param>
        void AddKeyedSingleton<I>(Key key, I instance);

        /// <summary>
        /// ファクトリメソッドを使用してキー付きシングルトンサービスを登録します。
        /// </summary>
        /// <typeparam name="T">登録するサービスの型。</typeparam>
        /// <param name="key">サービスを識別するためのキー。</param>
        /// <param name="implementationFactory">サービスのインスタンスを生成するファクトリメソッド。</param>
        void AddKeyedSingleton<T>(Key key, Func<IServiceProvider, T> implementationFactory);

        /// <summary>
        /// 登録されたサービスから <see cref="IServiceProvider"/> を構築します。
        /// </summary>
        /// <returns>構築された <see cref="IServiceProvider"/>。</returns>
        IServiceProvider BuildServiceProvider();
    }
}
