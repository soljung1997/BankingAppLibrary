using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAppLibrary.Events;
using BankingAppLibrary.Exceptions;
using BankingAppLibrary.Models;

namespace BankingAppLibrary.Accounts
{
    public class VisaAccount : Account
    {
        // Fields
        private decimal creditLimit;
        private const decimal INTEREST_RATE = 0.1995m;

        // Constructor
        public VisaAccount(decimal balance = 0, decimal creditLimit = 1200)
            : base("VS", balance)
        {
            this.creditLimit = creditLimit;
        }

        // DoPayment adds positive amount
        public void DoPayment(decimal amount, Person person)
        {
            base.Deposit(amount, person);

            TransactionEventArgs args = new TransactionEventArgs(
                person.Name,
                amount,
                true
            );

            base.OnTransactionOccur(this, args);
        }

        // DoPurchase subtracts amount if allowed
        public void DoPurchase(decimal amount, Person person)
        {
            bool isUser = base.IsUser(person);

            if (!isUser)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            if (!person.IsAuthenticated)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.USER_NOT_LOGGED_IN);
            }

            if (amount > Balance + creditLimit)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
            }

            base.Deposit(-amount, person); // negative amount = purchase

            base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }

        // Monthly report — apply interest and reset transaction list
        public override void PrepareMonthlyReport()
        {
            decimal interest = (LowestBalance * INTEREST_RATE) / 12;
            Balance -= interest;

            transactions.Clear(); // Re-initialize transaction list
        }
        public void Pay(decimal amount, Person person)
        {
            DoPayment(amount, person);
        }

        public void Purchase(decimal amount, Person person)
        {
            DoPurchase(amount, person);
        }
    }
}
