using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// UserConfigの読み書きを行うインスタンスを生成するファクトリのインタフェース。
    /// </summary>
    interface IUserConfigIOFactory
    {
        /// <summary>
        /// UserConfigReaderのインスタンスを生成する。
        /// 同じファイルパスで生成済みなら同じインスタンスを返す。
        /// </summary>
        IUserConfigDTOReader GetOrCreateUserConfigDTOReader(string filepath);
        
        /// <summary>
        /// UserConfigWriterのインスタンスを生成する。
        /// 同じファイルパスで生成済みなら同じインスタンスを返す。
        /// </summary>
        IUserConfigDTOWriter GetOrCreateUserConfigDTOWriter(string filepath);
    }
}
