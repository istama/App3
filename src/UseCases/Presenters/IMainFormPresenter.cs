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
        /// すでに問番がある場合は何もしない。
        /// </summary>
        void AddToibanToCheckedList(Toiban toiban);

        /// <summary>
        /// 問番を出力リストに指定したチェック状態で追加する。
        /// すでに問番がある場合は、チェックのみ指定した状態にする。
        /// </summary>
        void AddToibanToCheckedList(Toiban toiban, bool check);

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
