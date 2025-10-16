using System;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// DIコンテナからサービスのインスタンスを取得するためのメソッドを定義します。
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// ビルド失敗時のエラーメッセージを細かく表示するならtrueをセットする。
        /// </summary>
        bool DebugMode { get; set; }

        /// <summary>
        /// 指定された型のサービスインスタンスを取得します。
        /// </summary>
        /// <typeparam name="T">取得するサービスの型。</typeparam>
        /// <returns>サービスのインスタンス。</returns>
        /// <exception cref="InvalidOperationException">指定された型のサービスが登録されていない場合にスローされます。</exception>
        T GetRequiredService<T>();

        /// <summary>
        /// 指定された型のサービスインスタンスを取得します。
        /// </summary>
        /// <param name="serviceType">取得するサービスの型。</param>
        /// <returns>サービスのインスタンス。</returns>
        /// <exception cref="InvalidOperationException">指定された型のサービスが登録されていない場合にスローされます。</exception>
        object GetRequiredService(Type serviceType);

        /// <summary>
        /// 指定されたキーと型のキー付きサービスインスタンスを取得します。
        /// </summary>
        /// <typeparam name="T">取得するサービスの型。</typeparam>
        /// <param name="key">サービスを識別するためのキー。</param>
        /// <returns>サービスのインスタンス。</returns>
        /// <exception cref="InvalidOperationException">指定されたキーと型のサービスが登録されていない場合にスローされます。</exception>
        T GetRequiredKeyedService<T>(Key key);
    }
}
