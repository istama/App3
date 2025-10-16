using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.SSS
{
    sealed class ScreenSaverStopperPeriods
    {
        private readonly IEnumerable<(DateTime, DateTime)> _periods;


        public ScreenSaverStopperPeriods(IEnumerable<(DateTime, DateTime)> periods)
        {
            Assert.IsNull(periods, nameof(periods));

            _periods = periods;
        }


        public bool IsWithinTimeRage(DateTime time)
        {
            return _periods.Any(period =>
            {
                var (start, end) = period;
                return time >= start && time <= end;
            });
        }
    }
}
