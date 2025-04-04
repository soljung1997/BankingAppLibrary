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

        // Constructor
        public Transaction(string accountNumber, decimal amount, Person person)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            Originator = person;
            Time = Util.Now; // Assuming Util has a static property Now of type DayTime
        }

        // ToString method override
        public override string ToString()
        {
            return $"Transaction for Account: {AccountNumber}, " +
                   $"By: {Originator}, " +
                   $"Amount: {Amount:C}, " +
                   $"Time: {Time}";
        }
    }

}
