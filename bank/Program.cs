using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bank
{
    // Create a Account class
    public class Account
    {
        // Define the Account properties
        [Required] // Specify that a datafield (FirstName) is required
        [StringLength(20)] // Specify the length of FirstName
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "First name must not start with a digit or a small letter")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last name must not start with a digit or a small letter")]
        public string LastName { get; set; }

        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Note { get; set; }

        // Constructor to initialize the account
        public Account(string firstName, string lastName, int accountNumber, string accountType, decimal balance, string note)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Balance = balance;
            Note = note;
        }

        // Create a method to deposit funds into the account
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        // Create a method to withdraw funds from the account
        public void Withdraw(decimal amount)
        {
            // Check if the balance in account is less than 1000
            if (AccountType == "Savings" && Balance - amount < 1000m)
            {
                // Tell the user that there is insufficient funds in his account
                throw new Exception("Insufficient funds");
            }
            // if there's sufficient balance he can withdraw and new balance is calculated
            Balance -= amount;
        }

        // Method to transfer funds from the account to another account (from savings => current)
        // Or from one account holder to another
        public void Transfer(decimal amount, Account otherAccount)
        {
            Withdraw(amount);
            otherAccount.Deposit(amount);
        }

        // Override of ToString() method to provide a string representation of the account
        public override string ToString()
        {
            return $"Account {AccountNumber}: {FirstName} {LastName} ({AccountType}) - Balance: {Balance}";
        }
    }

    // Represents a bank with multiple accounts
    public class Bank
    {
        private List<Account> Accounts = new List<Account>();

        // Method to add a new account to the bank
        public void AddAccount(Account account)
        {
            // Check if an account number exist to avoid duplicate account numbers
            if (Accounts.Any(a => a.AccountNumber == account.AccountNumber))
            {
                throw new Exception("Account number already exists");
            }
            if (Accounts.Any(a => a.AccountType == account.AccountType && a.FirstName == account.FirstName && a.LastName == account.LastName))
            {
                throw new Exception("User cannot have the same account type");
            }
            Accounts.Add(account);
        }

        // Method to remove an account from the bank
        public void RemoveAccount(int accountNumber)
        {
            Accounts.RemoveAll(a => a.AccountNumber == accountNumber);
        }

        // Method to get an account by account number
        public Account GetAccount(int accountNumber)
        {
            return Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);
        }

        // Method to get all accounts in the bank
        public List<Account> GetAccounts()
        {
            return Accounts;
        }

        // Method to deposit funds into an account
        public void Deposit(int accountNumber, decimal amount)
        {
            Account account = GetAccount(accountNumber);
            account.Deposit(amount);
        }

        // Method to withdraw funds from an account
        public void Withdraw(int accountNumber, decimal amount)
        {
            Account account = GetAccount(accountNumber);
            if (account.AccountType == "Savings" && account.Balance - amount < 1000m)
            {
                throw new Exception("Insufficient funds");
            }
            account.Withdraw(amount);
        }

        // Method to transfer funds between accounts
        public void Transfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            Account fromAccount = GetAccount(fromAccountNumber);
            Account toAccount = GetAccount(toAccountNumber);
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        }

        // Method to print all accounts in the bank
        public void PrintAccounts()
        {
            Console.WriteLine("|---------------|------------------|--------------|-------------|--------|");
            Console.WriteLine("| FULL NAME     | ACCOUNT NUMBER   | ACCOUNT TYPE | ACCOUNT BAL | NOTE   |");
            Console.WriteLine("|---------------|------------------|--------------|-------------|--------|");
            foreach (var account in Accounts)
            {
                Console.WriteLine($"| {account.FirstName + " " + account.LastName}      | {account.AccountNumber}        | {account.AccountType}      | {account.Balance}    | {account.Note}   |");
            }
            Console.WriteLine("|---------------|------------------|---------------|-------------|--------|");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Bank bank = new Bank();

            // Add some accounts 
            bank.AddAccount(new Account("John", "Doe", 0987654321, "Savings", 10000.00m, "Gift"));
            bank.AddAccount(new Account("Jane", "Smith", 0987654311, "Current", 100000.00m, "Food"));

            // Deposit some money
            bank.Deposit(0987654321, 5000.00m);

            // Withdraw some money
            bank.Withdraw(0987654321, 2000.00m);

            // Transfer some money
            bank.Transfer(0987654311, 0987654321, 1000.00m);

            // Print all accounts and balances to the console
            bank.PrintAccounts();
        }
    }
}
