using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// ダブルクリックを判定するクラス。
    /// </summary>
    sealed class MouseHookDoubleClickDetector
    {
        public static readonly MouseHookDoubleClickDetector None = new MouseHookDoubleClickDetector(-1);

        // ダブルクリックになるクリックの間隔（ミリ秒単位）
        private readonly int _doubleClickInterval_MS;
        // 時間を取得するインタフェース
        private readonly IDateTimeGetter _dateTimeGetter;
        // ダブルクリック判定を行うかどうか
        private readonly bool _shouldDetectDoubleClick;

        // 最後にクリックした時間
        private DateTime _lastLClickTime = default;
        private DateTime _lastMClickTime = default;
        private DateTime _lastRClickTime = default;


        /// <summary>
        /// doubleClickInterval_MSが0未満の場合はダブルクリック判定を行わない。
        /// </summary>
        public MouseHookDoubleClickDetector(int doubleClickInterval_MS) : this(doubleClickInterval_MS, new DateTimeGetter())
        {
        }
        public MouseHookDoubleClickDetector(int doubleClickInterval_MS, IDateTimeGetter dateTimeGetter)
        {
            Assert.IsNull(dateTimeGetter, nameof(dateTimeGetter));

            _doubleClickInterval_MS = doubleClickInterval_MS;
            _dateTimeGetter = dateTimeGetter;

            _shouldDetectDoubleClick = _doubleClickInterval_MS >= 0;
        }


        /// <summary>
        /// 左ダブルクリックならtrueを返す。
        /// </summary>
        public bool IsLDoubleClick()
            => IsDoubleClick(ref _lastLClickTime);

        /// <summary>
        /// 中ダブルクリックならtrueを返す。
        /// </summary>
        public bool IsMDoubleClick()
            => IsDoubleClick(ref _lastMClickTime);

        /// <summary>
        /// 右ダブルクリックならtrueを返す。
        /// </summary>
        public bool IsRDoubleClick()
            => IsDoubleClick(ref _lastRClickTime);

        private bool IsDoubleClick(ref DateTime lastClickDateTime)
        {
            if (!_shouldDetectDoubleClick)
                return false;

            var now = _dateTimeGetter.GetNow();

            if (lastClickDateTime == default)
            {
                lastClickDateTime = now;
                return false;
            }

            var interval = now - lastClickDateTime;
            var is_dobule_click = interval.TotalMilliseconds <= _doubleClickInterval_MS;

            lastClickDateTime = now;
            return is_dobule_click;
        }

        /// <summary>
        /// 内部で保持しているクリック情報をクリアする。
        /// </summary>
        public void Clear()
        {
            _lastLClickTime = default;
            _lastMClickTime = default;
            _lastRClickTime = default;
        }
    }

    sealed class DateTimeGetter : IDateTimeGetter
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
