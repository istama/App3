using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsTama.Utils.DependencyInjectionSimple
{
    /// <summary>
    /// DIコンテナを利用してインスタンスを生成するためのユーティリティクラス。
    /// </summary>
    public class ActivatorUtilities
    {
        /// <summary>
        /// ビルド失敗時のエラーメッセージを細かく表示するならtrueをセットする。
        /// </summary>
        public static bool DebugMode { get; set; } = false;

        /// <summary>
        /// 指定された型のインスタンスを生成します。
        /// </summary>
        /// <typeparam name="T">生成するインスタンスの型。</typeparam>
        /// <param name="provider">依存関係の解決に使用するサービスプロバイダー。</param>
        /// <returns>生成されたインスタンス。</returns>
        public static T CreateInstance<T>(IServiceProvider provider)
        {
            return CreateInstance<T>(provider, Array.Empty<object>());
        }

        /// <summary>
        /// 指定された型のインスタンスを生成します。コンストラクタの引数として使用する追加のオブジェクトを指定できます。
        /// </summary>
        /// <typeparam name="T">生成するインスタンスの型。</typeparam>
        /// <param name="provider">依存関係の解決に使用するサービスプロバイダー。</param>
        /// <param name="args">コンストラクタの引数として優先的に使用されるオブジェクトの配列。</param>
        /// <returns>生成されたインスタンス。</returns>
        /// <exception cref="InvalidOperationException">インスタンスの生成に失敗した場合にスローされます。</exception>
        public static T CreateInstance<T>(IServiceProvider provider, params object[] args)
        {
            var type = typeof(T);
            var constructors = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length);

            if (!constructors.Any())
            {
                throw new InvalidOperationException($"{type.Name}にpublicなコンストラクタがありません。");
            }

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var parameterValues = new object[parameters.Length];
                var canCreate = true;

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var value = args.FirstOrDefault(arg => parameter.ParameterType.IsInstanceOfType(arg));

                    if (value != null)
                    {
                        parameterValues[i] = value;
                        continue;
                    }

                    try
                    {
                        parameterValues[i] = provider.GetRequiredService(parameter.ParameterType);
                    }
                    catch (InvalidOperationException ex)
                    {
                        if (DebugMode)
                        {
                            System.Windows.Forms.MessageBox.Show("ActivatorUtilities.CreateInstance()" + 
                                Environment.NewLine + $"type:{type.ToString()} paramType:{parameter.ParameterType.ToString()} paramIndex:{i}" +
                                Environment.NewLine + ex.ToString());
                        }
                        canCreate = false;
                        break;
                    }
                }

                if (canCreate)
                {
                    return (T)constructor.Invoke(parameterValues);
                }
            }

            throw new InvalidOperationException($"{type.Name}のインスタンス化に失敗しました。どのコンストラクタの引数も解決できませんでした。");
        }
    }
}
