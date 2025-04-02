using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppLibrary.Models
{
    public class DayTime
    {
        //may need to provide logic
        private long minutes;

        //constructor
        public DayTime(long minutes)
        {
            this.minutes = minutes;
        }

        //methods
        public static DayTime operator +(DayTime lhs, int minutes)
        {
            return new DayTime(lhs.minutes + minutes);
        }

        public override string ToString()
        {
            long minutes = this.minutes;

            const int HOUR = 60;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            const int YEAR = 12 * MONTH;

            int year = 2023 + (int)(minutes / YEAR);
            int month = 1 + (int)((minutes % YEAR) / MONTH);
            int day = 1 + (int)((minutes % MONTH) / DAY);
            int hour = (int)((minutes % DAY) / HOUR);
            int minute = (int)(minutes % HOUR);
            return $"{year:D4} - {month:D2} - {day:D2} {hour:D2}:{minutes:D2}";
        }
    }
}
