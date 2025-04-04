using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppLibrary.Models
{
    public struct DayTime
    {
        // Private field to store minutes since 2023-01-01 00:00
        private long minutes;

        // Constructor
        public DayTime(long minutes)
        {
            this.minutes = minutes;
        }

        // Overload the + operator
        public static DayTime operator +(DayTime lhs, int additionalMinutes)
        {
            return new DayTime(lhs.minutes + additionalMinutes);
        }

        // AddMinutes method for use in Util class
        public DayTime AddMinutes(int additionalMinutes)
        {
            return this + additionalMinutes;
        }

        // ToString override to convert minutes to Y-M-D H:M format
        public override string ToString()
        {
            // Time breakdown constants
            const int MinutesPerHour = 60;
            const int MinutesPerDay = 1440;
            const int MinutesPerMonth = 43200;
            const int MinutesPerYear = 518400;

            long remaining = minutes;

            long year = 2023 + (remaining / MinutesPerYear);
            remaining %= MinutesPerYear;

            long month = 1 + (remaining / MinutesPerMonth);
            remaining %= MinutesPerMonth;

            long day = 1 + (remaining / MinutesPerDay);
            remaining %= MinutesPerDay;

            long hour = remaining / MinutesPerHour;
            long minute = remaining % MinutesPerHour;

            return $"{year:D4}-{month:D2}-{day:D2} {hour:D2}:{minute:D2}";
        }
    }
}
