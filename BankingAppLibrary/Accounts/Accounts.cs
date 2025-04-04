using System;
using System.Collections.Generic;
using BankingAppLibrary.Events;
using BankingAppLibrary.Models;
using BankingAppLibrary.Utils;

namespace BankingAppLibrary.Accounts
{
    public abstract class Account
    {
        private static int LAST_NUMBER = 100000;

        protected readonly List<Person> users = new();
        public readonly List<Transaction> transactions = new();

        public event TransactionEventHandler OnTransaction;

        public string Number { get; }
        public decimal Balance { get; protected set; }
        public decimal LowestBalance { get; protected set; }

        public Account(string type, decimal balance)
        {
            Number = $"{type}-{LAST_NUMBER++}";
            Balance = balance;
            LowestBalance = balance;
        }

        protected void Deposit(decimal amount, Person person)
        {
            Balance += amount;
            if (Balance < LowestBalance)
                LowestBalance = Balance;

            Transaction t = new(Number, amount, person);
            transactions.Add(t);
        }

        public void AddUser(Person user)
        {
            users.Add(user);
        }

        public bool IsUser(Person person)
        {
            foreach (var u in users)
            {
                if (u.Name == person.Name && u.Sin == person.Sin)
                    return true;
            }
            return false;
        }

        public virtual void OnTransactionOccur(object sender, TransactionEventArgs args)
        {
            OnTransaction?.Invoke(sender, args);
        }

        public abstract void PrepareMonthlyReport();

        public override string ToString()
        {
            string result = $"\n{Number}\n";
            foreach (var u in users)
            {
                result += $" {u}\n";
            }
            result += $"${Balance:0.00}\n";
            foreach (var t in transactions)
            {
                result += $"  {t}\n";
            }
            return result;
        }
    }
}
