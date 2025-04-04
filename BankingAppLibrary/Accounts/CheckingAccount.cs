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
    public class CheckingAccount : Account
    {
        // Static fields shared across all CheckingAccount instances
        private const decimal COST_PER_TRANSACTION = 0.05m;
        private const decimal INTEREST_RATE = 0.005m;

        // Instance field
        private bool hasOverdraft;

        // Constructor
        public CheckingAccount(decimal balance = 0, bool hasOverdraft = false)
            : base("CK", balance)
        {
            this.hasOverdraft = hasOverdraft;
        }

        // Deposit method (new — not overriding)
        public new void Deposit(decimal amount, Person person)
        {
            base.Deposit(amount, person);

            TransactionEventArgs args = new TransactionEventArgs(
                person.Name,
                amount,
                true
            );
            base.OnTransactionOccur(this, args);
        }

        // Withdraw method
        public void Withdraw(decimal amount, Person person)
        {
            if (!IsUser(person))
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            if (!person.IsAuthenticated)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.USER_NOT_LOGGED_IN);
            }

            if (amount > Balance && !hasOverdraft)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.NO_OVERDRAFT_FOR_THIS_ACCOUNT);
            }

            // Perform the withdrawal
            base.Deposit(-amount, person);

            base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }

        // Monthly report logic
        public override void PrepareMonthlyStatement()
        {
            decimal serviceCharge = transactions.Count * COST_PER_TRANSACTION;
            decimal interest = (LowestBalance * INTEREST_RATE) / 12;

            Balance += interest - serviceCharge;

            transactions.Clear(); // Reset for next cycle
        }
    }
}
