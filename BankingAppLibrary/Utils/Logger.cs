using BankingAppLibrary.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppLibrary.Utils
{
    public static class Logger
    {
        private static List<string> loginEvents = new List<string>();
        private static List<string> transactionEvents = new List<string>();

        public static void LoginHandler(object sender, LoginEventArgs args)
        {
            string entry = $"{args.PersonName} {(args.Success ? "successfully" : "unsuccessfully")} {args.EventType} on {args.Time}";
            loginEvents.Add(entry);
        }

        public static void TransactionHandler(object sender, EventArgs e)
        {
            TransactionEventArgs args = e as TransactionEventArgs;
            if (args == null) return;

            string result = $"{Math.Abs(args.Amount):C2} ";

            result += args.Amount >= 0 ? "deposited" : "withdrawn";

            result += $" by {args.PersonName} on {Util.Now}";

            

            transactionEvents.Add(result);
        }

        public static void DisplayLoginEvents()
        {
            int i = 1;
            foreach (string e in loginEvents)
            {
                Console.WriteLine($"{i++}. {e}");
            }
        }

        public static void DisplayTransactionEvents()
        {
            int i = 1;
            foreach (string e in transactionEvents)
            {
                Console.WriteLine($"{i++}. {e}");
            }
        }
    }
}
