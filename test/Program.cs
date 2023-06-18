using NUnit.Framework;
using System.Security.Principal;

namespace Bank.Tests
{
    [TestFixture]
    public class BankTests
    {
        private Bank bank;

        [SetUp]
        public void Setup()
        {
            bank = new Bank();
        }

        // Test valid deposit
        [Test]
        public void Deposit_ValidAmount_Success()
        {
            // Arrange
            Account account = new Account("John", "Doe", 1234567890, "Savings", 1000.00m, "Gift");
            bank.AddAccount(account);

            // Act
            bank.Deposit(1234567890, 500.00m);

            // Assert
            Assert.AreEqual(1500.00m, account.Balance);
        }

        // Test valid withdrawal
        [Test]
        public void Withdraw_ValidAmount_Success()
        {
            // Arrange
            Account account = new Account("John", "Doe", 1234567890, "Savings", 1000.00m, "Gift");
            bank.AddAccount(account);

            // Act
            bank.Withdraw(1234567890, 200.00m);

            // Assert
            Assert.AreEqual(800.00m, account.Balance);
        }

        // Test valid transfer
        [Test]
        public void Transfer_ValidAmount_Success()
        {
            // Arrange
            Account fromAccount = new Account("John", "Doe", 1234567890, "Savings", 1000.00m, "Gift");
            Account toAccount = new Account("Jane", "Smith", 0987654321, "Current", 500.00m, "Food");
            bank.AddAccount(fromAccount);
            bank.AddAccount(toAccount);

            // Act
            bank.Transfer(1234567890, 0987654321, 100.00m);

            // Assert
            Assert.AreEqual(900.00m, fromAccount.Balance);
            Assert.AreEqual(600.00m, toAccount.Balance);
        }

        // Test invalid withdrawal with insufficient funds
        [Test]
        public void Withdraw_InsufficientFunds_ThrowsException()
        {
            // Arrange
            Account account = new Account("John", "Doe", 1234567890, "Savings", 1000.00m, "Gift");
            bank.AddAccount(account);

            // Act and Assert
            Assert.Throws<Exception>(() => bank.Withdraw(1234567890, 1200.00m));
        }

        // Test invalid duplicate account number
        [Test]
        public void AddAccount_DuplicateAccountNumber_ThrowsException()
        {
            // Arrange
            Account account1 = new Account("John", "Doe", 1234567890, "Savings", 1000.00m, "Gift");
            Account account2 = new Account("Jane", "Smith", 1234567890, "Current", 500.00m, "Food");
            bank.AddAccount(account1);

            // Act and Assert
            Assert.Throws<Exception>(() => bank.AddAccount(account2));
        }

        // Test invalid duplicate account type for the same user
        [Test]
        public void AddAccount_DuplicateAccountTypeForSameUser_ThrowsException()
        {
            // Arrange
            Account account1 = new Account("John", "Doe", 1234567890, "Savings", 1000.00m, "Gift");
            Account account2 = new Account("John", "Doe", 0987654321, "Savings", 500.00m, "Food");
            bank.AddAccount(account1);

            // Act and Assert
            Assert.Throws<Exception>(() => bank.AddAccount(account2));
        }
    }
}
