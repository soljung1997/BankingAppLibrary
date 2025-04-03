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
    public class VisaAccount : Account, ITransaction
    {
        private decimal creditLimit;
        private static readonly decimal INTEREST_RATE = 0.1995m;
    
        public VisaAccount(decimal balance = 0, decimal creditLimit = 1200)
        {
            this.creditLimit = creditLimit;
        }

        public void DoPayment(decimal amount, Person person)
        {
            //calls Deposit() from the base class with appropriate arguments
            base.Deposit(amount, person);

            //calls OnTransactionOccur method of the base class with the appropriate arguments
            //the second argument TransactionEventArgs needs the name of the person, the amount and true (success of the operation)
            var args = new TransactionEventArgs(person.Name, amount, true);
            OnTransactionOccur(this, args);
        }

        public void DoPurchase(decimal amount, Person person)
        {
            if (!IsUser(person))
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));

                throw new AccountException("User not authorized for this account", AccountExceptionType.UserNotAuthorized);
            }

            if (!person.IsAuthenticated())
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException("User not authenticated", AccountExceptionType.UserNotAuthenticated);
            }

            if (Balance + creditLimit < amount)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException("Insufficient funds", AccountExceptionType.InsufficientFunds);
            }

            base.Deposit(-amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }

        public override void PrepareMonthlyReport()
        {
            Balance -= (LowestBalance * INTEREST_RATE) / 12;
            transactions.Clear();
        }
    }
}
