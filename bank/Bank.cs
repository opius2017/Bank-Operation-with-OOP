using System;
using System.Collections.Generic;

namespace Bank
{
    // Create an Account class to represent a bank account
    public class Account
    {
        // Define properties for an account
        //[Required(ErrorMessage = "First name is required")]
        //[StringLength(20, ErrorMessage = "First name must not exceed 20 characters")]
        //[RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First name must start with an uppercase letter and contain only letters")]
        //public string FirstName { get; set; }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("First name is required");
                }

                if (value.Length > 20)
                {
                    throw new ArgumentException("First name must not exceed 20 characters");
                }

                if (char.IsDigit(value[0]))
                {
                    throw new ArgumentException("First name must not start with a number");
                }

                firstName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Last name is required");
                }

                if (value.Length > 20)
                {
                    throw new ArgumentException("Last name must not exceed 20 characters");
                }

                if (char.IsDigit(value[0]))
                {
                    throw new ArgumentException("Last name must not start with a number");
                }

                lastName = value;
            }
        }

        public long AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Note { get; set; }


        // Constructor to create an account
        public Account(string firstName, string lastName, long accountNumber, string accountType, decimal balance, string note)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Balance = balance;
            Note = note;
        }

        // Method to deposit money into the account
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        // Method to withdraw money from the account
        public void Withdraw(decimal amount)
        {
            // Check if the account is a savings account and if the withdrawal will result in a balance less than 1000
            if (AccountType == "Savings" && Balance - amount < 1000m)
            {
                // Throw an exception with a message if there are insufficient funds
                throw new Exception("Insufficient funds");
            }

            // Subtract the amount from the balance
            Balance -= amount;
        }

        // Method to transfer money from the current account to another account
        public void Transfer(decimal amount, Account otherAccount)
        {
            // Withdraw the amount from the current account
            Withdraw(amount);

            // Deposit the amount into the other account
            otherAccount.Deposit(amount);
        }

        // Override ToString() method to provide a string representation of the account
        public override string ToString()
        {
            return $"Account {AccountNumber}: {FirstName} {LastName} ({AccountType}) - Balance: {Balance}";
        }
    }

    // Represents a bank with multiple accounts
    public class Bank
    {
        private List<Account> accounts = new List<Account>();

        // Method to add a new account to the bank
        public void AddAccount(Account account)
        {
            // Check if the account number already exists
            foreach (Account existingAccount in accounts)
            {
                if (existingAccount.AccountNumber == account.AccountNumber)
                {
                    throw new Exception("Account number already exists");
                }
            }

            // Check if the user already has an account of the same type
            foreach (Account existingAccount in accounts)
            {
                if (existingAccount.AccountType == account.AccountType && existingAccount.FirstName == account.FirstName && existingAccount.LastName == account.LastName)
                {
                    throw new Exception("User cannot have the same account type");
                }
            }

            // Add the account to the list of accounts
            accounts.Add(account);
        }


        //// Method to remove an account from the bank
        //public void RemoveAccount(long accountNumber) // Change the parameter type to long
        //{
        //    // Remove the account with the specified account number from the list of accounts
        //    accounts.RemoveAll(a => a.AccountNumber == accountNumber);
        //}

        //// Method to get an account by account number
        //public Account GetAccount(long accountNumber) // Change the return type and parameter type to long
        //{
        //    // Find and return the account with the specified account number
        //    return accounts.Find(a => a.AccountNumber == accountNumber);
        //}

        public void RemoveAccount(long accountNumber)
        {
            // Iterate over the accounts and remove the one with the specified account number
            foreach (var account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    accounts.Remove(account);
                    break; // Exit the loop once the account is found and removed
                }
            }
        }

        public Account GetAccount(long accountNumber)
        {
            // Iterate over the accounts and find the one with the specified account number
            foreach (var account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }

            // Account not found, return null or throw an exception based on your requirements
            return null;
        }

        // Method to get all accounts in the bank
        public List<Account> GetAccounts()
        {
            // Return the list of accounts
            return accounts;
        }

        // Method to deposit money into an account
        public void Deposit(long accountNumber, decimal amount) // Change the parameter type to long
        {
            // Get the account with the specified account number
            Account account = GetAccount(accountNumber);

            // Deposit the amount into the account
            account.Deposit(amount);
        }

        // Method to withdraw money from an account
        public void Withdraw(long accountNumber, decimal amount) // Change the parameter type to long
        {
            // Get the account with the specified account number
            Account account = GetAccount(accountNumber);

            // Check if the account is a savings account and if the withdrawal will result in a balance less than 1000
            if (account.AccountType == "Savings" && account.Balance - amount < 1000m)
            {
                throw new Exception("Insufficient funds");
            }

            // Withdraw the amount from the account
            account.Withdraw(amount);
        }

        // Method to transfer money between accounts
        public void Transfer(long fromAccountNumber, long toAccountNumber, decimal amount) // Change the parameter types to long
        {
            // Get the accounts involved in the transfer
            Account fromAccount = GetAccount(fromAccountNumber);
            Account toAccount = GetAccount(toAccountNumber);

            // Withdraw the amount from the sender's account
            fromAccount.Withdraw(amount);

            // Deposit the amount into the recipient's account
            toAccount.Deposit(amount);
        }

        // Method to print all accounts in the bank
        public void PrintAccounts()
        {
            Console.WriteLine("|---------------|------------------|--------------|-------------|--------|");
            Console.WriteLine("| FULL NAME     | ACCOUNT NUMBER   | ACCOUNT TYPE | ACCOUNT BAL | NOTE   |");
            Console.WriteLine("|---------------|------------------|--------------|-------------|--------|");

            foreach (var account in accounts)
            {
                Console.WriteLine($"| {account.FirstName} {account.LastName,-9} | {account.AccountNumber,-16} | {account.AccountType,-13} | {account.Balance,-11} | {account.Note,-6} |");
            }

            Console.WriteLine("|---------------|------------------|--------------|-------------|--------|");
        }
    }

    public class Program
    {
        public static void Main()
        {
            Bank bank = new Bank();

            // Create some accounts
            Account account1 = new Account("John", "Doe", 1234567890, "Savings", 10000.00m, "Gift");
            Account account2 = new Account("Jane", "Smith", 9876543210, "Current", 100000.00m, "Food");

            // Add the accounts to the bank
            bank.AddAccount(account1);
            bank.AddAccount(account2);

            // Deposit some money into account1
            bank.Deposit(1234567890, 5000.00m);

            // Withdraw some money from account1
            bank.Withdraw(1234567890, 2000.00m);

            // Transfer some money from account2 to account1
            bank.Transfer(9876543210, 1234567890, 1000.00m);

            // Print all accounts and balances
            bank.PrintAccounts();
        }
    }
}
