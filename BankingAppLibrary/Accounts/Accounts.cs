using System;
using System.Collections.Generic;
using BankingAppLibrary.Events;
using BankingAppLibrary.Models;
using BankingAppLibrary.Utils;

namespace BankingAppLibrary.Accounts
{
    public abstract class Account
    {
        // Fields
        private static int LAST_NUMBER = 100_000;
        protected readonly List<Person> users = new List<Person>();
        public readonly List<Transaction> transactions = new List<Transaction>();

        // Event
        public event TransactionEventHandler OnTransaction;

        // Properties
        public string Number { get; }
        public decimal Balance { get; protected set; }
        public decimal LowestBalance { get; protected set; }

        // Constructor
        public Account(string type, decimal balance)
        {
            Number = type + "-" + LAST_NUMBER++;
            Balance = balance;
            LowestBalance = balance;
        }

        // Protected method: Deposit
        protected void Deposit(decimal amount, Person person)
        {
            Balance += amount;
            if (Balance < LowestBalance)
            {
                LowestBalance = Balance;
            }

            Transaction t = new Transaction(Number, amount, person);
            transactions.Add(t);
        }

        // Add a user to the account
        public void AddUser(Person user)
        {
            users.Add(user);
        }

        // Check if the given person is a user of this account
        public bool IsUser(Person person)
        {
            foreach (Person u in users)
            {
                if (u.Name == person.Name && u.SIN == person.SIN)
                {
                    return true;
                }
            }
            return false;
        }

        // Raise transaction event
        public virtual void OnTransactionOccur(object sender, TransactionEventArgs args)
        {
            if (OnTransaction != null)
            {
                OnTransaction(sender, args);
            }
        }

        // Abstract method for monthly statements
        public abstract void PrepareMonthlyStatement();

        // Display string for account
        public override string ToString()
        {
            string result = "\n" + Number + "\n";

            foreach (Person u in users)
            {
                result += " " + u + "\n";
            }

            result += "$" + Balance.ToString("0.00") + "\n";

            foreach (Transaction t in transactions)
            {
                result += "  " + t + "\n";
            }

            return result;
        }
    }
}
