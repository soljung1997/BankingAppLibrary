using BankingAppLibrary.Accounts;
using BankingAppLibrary.Models;
using BankingAppLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BankingAppLibrary.Utils
{
    public static class Bank
    {
        // Fields
        public static readonly Dictionary<string, Account> ACCOUNTS = new Dictionary<string, Account>();
        public static readonly Dictionary<string, Person> USERS = new Dictionary<string, Person>();

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
            AddAccount(new VisaAccount());                     // VS-100000
            AddAccount(new VisaAccount(150, -500));            // VS-100001
            AddAccount(new SavingAccount(5000));               // SV-100002
            AddAccount(new SavingAccount());                   // SV-100003
            AddAccount(new CheckingAccount(2000));             // CK-100004
            AddAccount(new CheckingAccount(1500, true));       // CK-100005
            AddAccount(new VisaAccount(50, -550));             // VS-100006
            AddAccount(new SavingAccount(1000));               // SV-100007

            // Associate users with accounts
            string number = "VS-100000";
            AddUserToAccount(number, "Narendra");
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Mehrdad");

            number = "VS-100001";
            AddUserToAccount(number, "Vinay");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Patrick");

            number = "SV-100002";
            AddUserToAccount(number, "Yin");
            AddUserToAccount(number, "Hao");
            AddUserToAccount(number, "Jake");

            number = "SV-100003";
            AddUserToAccount(number, "Mayy");
            AddUserToAccount(number, "Nicoletta");

            number = "CK-100004";
            AddUserToAccount(number, "Mehrdad");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Yin");

            number = "CK-100005";
            AddUserToAccount(number, "Jake");
            AddUserToAccount(number, "Nicoletta");

            number = "VS-100006";
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Vinay");

            number = "SV-100007";
            AddUserToAccount(number, "Patrick");
            AddUserToAccount(number, "Hao");
        }

        // Methods

        public static void PrintAccounts()
        {
            foreach (Account account in ACCOUNTS.Values)
            {
                Console.WriteLine(account);
            }
        }

        public static void PrintUsers()
        {
            foreach (Person person in USERS.Values)
            {
                Console.WriteLine(person);
            }
        }

        public static void SaveAccounts(string filename)
        {
            string json = JsonConvert.SerializeObject(ACCOUNTS.Values, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static void SaveUsers(string filename)
        {
            string json = JsonConvert.SerializeObject(USERS.Values, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static Person GetUser(string name)
        {
            foreach (Person person in USERS.Values)
            {
                if (person.Name == name)
                {
                    return person;
                }
            }

            throw new AccountException(AccountExceptionType.USER_DOES_NOT_EXIST);
        }

        public static Account GetAccount(string number)
        {
            Account account;
            if (ACCOUNTS.TryGetValue(number, out account))
            {
                return account;
            }

            throw new AccountException(AccountExceptionType.ACCOUNT_DOES_NOT_EXIST);
        }

        public static void AddUser(string name, string sin)
        {
            if (USERS.ContainsKey(sin))
            {
                throw new AccountException(AccountExceptionType.USER_ALREADY_EXIST);
            }

            Person person = new Person(name, sin);
            person.OnLogin += Logger.LoginHandler;
            USERS.Add(sin, person);
        }

        public static void AddAccount(Account account)
        {
            account.OnTransaction += Logger.TransactionHandler;
            ACCOUNTS.Add(account.Number, account);
        }

        public static void AddUserToAccount(string number, string name)
        {
            Account account = GetAccount(number);
            Person user = GetUser(name);
            account.AddUser(user);
        }
        public static List<Transaction> GetAllTransactions()
        {
            List<Transaction> allTransactions = new List<Transaction>();

            foreach (Account account in ACCOUNTS.Values)
            {
                allTransactions.AddRange(account.transactions);
            }

            return allTransactions;
        }
    }
    
}
