using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAppLibrary.Models;

namespace BankingAppLibrary.Utils
{
    public static class Util
    {
        private static Random random = new Random();

        // Initialized to represent 2025-03-22 10:15
        private static DayTime currentTime = new DayTime(1_153_055);

        public static DayTime Now
        {
            get
            {
                int minutesToAdd = random.Next(1, 91); // 1 to 90 minutes
                currentTime = currentTime.AddMinutes(minutesToAdd);
                return currentTime;
            }
        }
    }
}
