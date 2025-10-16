using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace IsTama.Utils
{
	/// <summary>
	/// Stringクラスの拡張メソッド。
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// 文字列がパスとして正しい書式ならtrueを返す。
		/// </summary>
		public static Boolean IsValidAsPath(this String text)
		{
			try {
				Path.GetFullPath(text);
				return true;
			}
			catch {
				return false;
			}
		}

        /// <summary>
        /// 文字列がパスならそのパスのファイルを消す。
        /// </summary>
        public static void DeleteIfPath(this String text)
        {
            if (text.IsValidAsPath() && File.Exists(text))
                File.Delete(text);
        }

        /// <summary>
        /// 半角数字を全角数字に変換する。
        /// </summary>
        public static String ToFullWidthNumericFromHalf(this String text)
        {
            var builder = new StringBuilder(text.Length);
            foreach (var c in text)
            {
                if ('0' <= c && c <= '9')
                {
                    var converted = Convert.ToChar('０' + (c - '0'));
                    builder.Append(converted);
                }
                else
                    builder.Append(c);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 文字数ではなく文字幅（半角文字１つを１とする）でインデックスと長さを指定し、文字列を切り取る。
        /// </summary>
        public static String SubStringByCharacterWidth(this String text, Int32 start, Int32 length)
        {
            var bytes = Encoding.GetEncoding(932).GetBytes(text);
            var selected_bytes = bytes.Skip(start).Take(length).ToArray();
            return Encoding.GetEncoding(932).GetString(selected_bytes);
        }

        public static String SubStringByCharacterWidth(this String text, Int32 start)
        {
            var length = text.LengthByCharaterWidth();
            return text.SubStringByCharacterWidth(start, length - start);
        }

        /// <summary>
        /// 文字数ではなく、文字幅で（半角文字１つを１として）文字列の長さを取得する。
        /// </summary>
        public static Int32 LengthByCharaterWidth(this String text)
        {
            return Encoding.GetEncoding(932).GetBytes(text).Length;
        }

        /// <summary>
        /// 文字数ではなく文字幅（半角文字１つを１とする）でインデックスと長さを指定し、元の文字列から指定範囲の文字列を削除した文字列を変えす。
        /// </summary>
        public static String RemoveByCharacterWidth(this String text, Int32 start, Int32 count)
        {
            var front_text = text.SubStringByCharacterWidth(0, start);
            var back_text = text.SubStringByCharacterWidth(start + count);
            return front_text + back_text;
        }

        /// <summary>
        /// 引数の文字列で文字列を分割する。
        /// なお""で囲った文字列内にある分割文字列は無視する。
        /// </summary>
        public static IEnumerable<String> SeparateElement(this String text, char separator)
        {
            if (separator == '"')
                throw new ArgumentException("セパレーターに \" を使うことはできません。");

            var builder     = new StringBuilder(text.Length);

            var quote       = false; // ダブルクオーテーション文字が出ているか
            var escaped     = false; // エスケープ文字が出ているか
            var is_building = false; // 文字列を構築中か

            foreach (var c in text)
            {
                if (!quote)
                {
                    if (c == separator)
                    {
                        yield return builder.ToString();
                        builder.Clear();
                        is_building = false;
                        continue;
                    }

                    if (c == '"' && !is_building && !escaped)
                    {
                        quote = true;
                        continue;
                    }
                }
                else
                {
                    if (c == '"' && !escaped)
                    {
                        quote = false;
                        continue;
                    }
                }

                if (!escaped)
                {
                    if (c == '\\')
                    {
                        escaped = true;
                        continue;
                    }
                }

                builder.Append(c);
                is_building = true;
                escaped     = false;
            }

            yield return builder.ToString();
        }
	}
}
