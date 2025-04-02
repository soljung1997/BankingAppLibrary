using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAppLibrary.Models;
using BankingAppLibrary.Utils;

namespace BankingAppLibrary.Accounts
{
    public interface ITransaction
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
    }
    public abstract class Account : ITransaction
    {
        private static int LASt_NUMBER 100_000;

        protected readonly List<Person> users = new List<Person>();

        public readonly List<Transaction> transactions = new List<Transaction>();

        public event TransactionEventHandler OnTransaction;

        public string Number { get; }

        public decimal Balance { get; protected set; }
        public decimal LowestBalance { get; protected set; }

        public Account(string type, decimal balance = 0)
        {
            //add code here
        }

        protected void Deposit(decimal amount, Person person)
        {
            //add code here
        }

        public void AddUser(Person user)
        {

        }

        public bool IsUser(Person user)
        {
        }

        public abstract void PrepareMonthlyReport();

        public virtual void OnTransactionOccur(object sender, TransactionEventArgs e)
        {

        }

        public override string ToString()
        {
            //add string override here
        }

    }


}
