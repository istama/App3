using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    readonly struct ToibanCheckedListItem
    {
        public static Boolean TryParse(String csv, out ToibanCheckedListItem item)
        {
            item = default;

            var e = csv.Split(',');
            if (e.Length < 0)
                return false;

            if (e.Length == 1)
            {
                if (!Toiban.TryCreate(e[0], out var toiban))
                    return false;

                item = new ToibanCheckedListItem(toiban, true);
                return true;
            }
            else
            {
                if (!Boolean.TryParse(e[0], out var check))
                    check = true;

                if (!Toiban.TryCreate(e[1], out var toiban))
                    return false;

                item = new ToibanCheckedListItem(toiban, check);
                return true;
            }

        }

        public ToibanCheckedListItem(Toiban toiban, Boolean checked_)
        {
            Toiban = toiban;
            Checked = checked_;
        }

        public ToibanCheckedListItem(Boolean checked_, Toiban toiban)
        {
            Checked = checked_;
            Toiban = toiban;
        }

        public Toiban  Toiban  { get; }
        public Boolean Checked { get; }
        public String ToCsv()
            => $"{Checked.ToString()},{Toiban}";
    }
}
