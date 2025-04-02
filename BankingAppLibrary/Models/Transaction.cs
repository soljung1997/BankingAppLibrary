using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAppLibrary.Utils;
using BankingAppLibrary.Models;

namespace BankingAppLibrary.Models
{
    public struct Transaction
    {
        public string AccountNumber { get; }
        public decimal Amount { get; }
        public Person Originator { get; }
        public DayTime Time { get; }

        public Transaction(string accountNumber, decimal amount, Person person)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            Originator = person;
            Time = Util.Now;
        }

        public override string ToString()
        {
            return $"{AccountNumber} {Amount:C} {Originator?.Name} {Time}";
        }
    }

}
