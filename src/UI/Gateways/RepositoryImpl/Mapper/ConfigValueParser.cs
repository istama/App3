using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// 文字列とオブジェクトを相互変換するクラス。
    /// </summary>
    sealed class ConfigValueParser : DefaultParser
    {
        public ConfigValueParser()
        {
            Register(ToStringFromDateTimePair, ToDateTimePairFromString);
        }

        private static String ToStringFromDateTimePair((DateTime, DateTime) timeRange)
        {
            var startTime = timeRange.Item1.ToString("HHmm");
            var endTime = timeRange.Item2.ToString("HHmm");

            // 変換した2つの文字列をハイフンで結合します。
            return $"{startTime}-{endTime}";
        }
        private static (DateTime, DateTime) ToDateTimePairFromString(String text)
        {
            // 文字列をハイフンで分割
            var parts = text.Split('-');

            // 書式が正しいか
            if (!Regex.IsMatch(text, @"^\d{4}-\d{4}$"))
            {
                throw new ArgumentException($"(DateTime, DateTime)型の文字列の書式が不正です。/ {text}");
            }

            // 今日の日付を取得
            var today = DateTime.Today;

            // 開始時刻の文字列を解析
            var startTime = DateTime.ParseExact(parts[0], "HHmm", CultureInfo.InvariantCulture);
            startTime = today.AddHours(startTime.Hour).AddMinutes(startTime.Minute);

            // 終了時刻の文字列を解析
            var endTime = DateTime.ParseExact(parts[1], "HHmm", CultureInfo.InvariantCulture);
            endTime = today.AddHours(endTime.Hour).AddMinutes(endTime.Minute);

            return (startTime, endTime);
        }
    }
}
