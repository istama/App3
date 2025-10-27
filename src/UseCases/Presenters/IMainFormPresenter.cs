using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.NengaApps;

namespace IsTama.NengaBooster.UseCases.Presenters
{
    interface IMainFormPresenter
    {
        /// <summary>
        /// 問番を出力リストに追加する。
        /// </summary>
        void AddToibanToCheckedList(Toiban toiban);

        /// <summary>
        /// 出力リストから指定の問番のチェックを外す。
        /// </summary>
        void UncheckToibanFromCheckedListAt(Toiban index);

        //void CheckToNaireMode(NaireOpenMode mode);
        //void CheckToHensyuOpenMode(HensyuOpenMode mode);
        //void CheckToShouldAddToibanToOutputList(Boolean should);
        //void CheckToInformationOpenMode(InformationOpenMode mode);
        //void CheckToPostToibanCheckedListOutputMode(PostToibanCheckedListOutputMode mode);
    }
}
