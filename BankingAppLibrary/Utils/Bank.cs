using BankingAppLibrary.Accounts;
using BankingAppLibrary.Models;
using BankingAppLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;

namespace BankingAppLibrary.Utils
{
    public static class Bank
    {
        // Fields
        public static readonly Dictionary<string, Account> ACCOUNTS = new();
        public static readonly Dictionary<string, Person> USERS = new();

        // Static Constructor
        static Bank()
        {
            // Initialize USERS
            AddUser("Narendra", "1234-5678");
            AddUser("Ilia", "2345-6789");
            AddUser("Mehrdad", "3456-7890");
            AddUser("Vinay", "4567-8901");
            AddUser("Arben", "5678-9012");
            AddUser("Patrick", "6789-0123");
            AddUser("Yin", "7890-1234");
            AddUser("Hao", "8901-2345");
            AddUser("Jake", "9012-3456");
            AddUser("Mayy", "1224-5678");
            AddUser("Nicoletta", "2344-6789");

            // Initialize ACCOUNTS
            AddAccount(new VisaAccount());
            AddAccount(new VisaAccount(150, -500));
            AddAccount(new SavingAccount(5000));
            AddAccount(new SavingAccount());
            AddAccount(new CheckingAccount(2000));
            AddAccount(new CheckingAccount(1500, true));
            AddAccount(new VisaAccount(50, -550));
            AddAccount(new SavingAccount(1000));
        }

        // Methods

        // PrintAccounts method
        public static void PrintAccounts()
        {
            foreach (var account in ACCOUNTS.Values)
            {
                Console.WriteLine(account);
            }
        }

        // PrintUsers method
        public static void PrintUsers()
        {
            foreach (var user in USERS.Values)
            {
                Console.WriteLine(user);
            }
        }

        // SaveAccounts method
        public static void SaveAccounts(string filename)
        {
            string json = JsonSerializer.Serialize(ACCOUNTS.Values);
            File.WriteAllText(filename, json);
        }

        // SaveUsers method
        public static void SaveUsers(string filename)
        {
            string json = JsonSerializer.Serialize(USERS.Values);
            File.WriteAllText(filename, json);
        }

        // GetUser method
        public static Person GetUser(string name)
        {
            if (USERS.TryGetValue(name, out var user))
            {
                return user;
            }

            // Use correct exception type: USER_DOES_NOT_EXIST
            throw new AccountException(AccountExceptionType.USER_DOES_NOT_EXIST);
        }

        // GetAccount method
        public static Account GetAccount(string number)
        {
            if (ACCOUNTS.TryGetValue(number, out var account))
            {
                return account;
            }

            // Use correct exception type: ACCOUNT_DOES_NOT_EXIST
            throw new AccountException(AccountExceptionType.ACCOUNT_DOES_NOT_EXIST);
        }

        // AddUser method
        public static void AddUser(string name, string sin)
        {
            if (USERS.ContainsKey(sin))
            {
                // Use correct exception type: USER_ALREADY_EXIST
                throw new AccountException(AccountExceptionType.USER_ALREADY_EXIST);
            }

            var person = new Person(name, sin);
            person.OnLogin += Logger.LoginHandler;
            USERS.Add(sin, person);
        }

        // AddAccount method
        public static void AddAccount(Account account)
        {
            account.OnTransaction += Logger.TransactionHandler;
            ACCOUNTS.Add(account.Number, account);
        }

        // AddUserToAccount method
        public static void AddUserToAccount(string number, string name)
        {
            var account = GetAccount(number);
            var user = GetUser(name);
            account.AddUser(user);
        }
    }
}
