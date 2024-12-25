using System.Collections.Generic;
using NUnit.Framework;
using Project;
using Project_Engine;

namespace Project_Test
{
    /// <summary>
    /// Test fixture for validating the functionality of the AccountManager class.
    /// </summary>
    [TestFixture]
    public class AccountManagerTests
    {
        private AccountManager accountManager;

        /// <summary>
        /// Sets up the test environment by initializing a new instance of AccountManager before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.accountManager = new AccountManager();
        }

        /// <summary>
        /// Verifies that a donor can be successfully registered with valid input.
        /// </summary>
        [Test]
        public void RegisterDonor_ValidInput_SuccessfulRegistration()
        {
            // Arrange
            string name = "John Doe";
            string address = "123 Main St";
            string email = "john@example.com";
            string username = "johndoe";
            string password = "password123";
            string affiliation = "ABC Corp";
            string creditCardInfo = "1234-5678-9012-3456";
            bool anonymousDonations = true;
            bool postAmounts = false;

            // Act
            this.accountManager.RegisterDonor(name, address, email, username, password, affiliation, creditCardInfo, anonymousDonations, postAmounts);

            // Assert
            var account = this.accountManager.Login(username, password);
            Assert.IsNotNull(account);
            Assert.IsInstanceOf<DonorAccount>(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual("Donor", account.AccountType);
        }

        /// <summary>
        /// Verifies that a student can be successfully registered with valid input.
        /// </summary>
        [Test]
        public void RegisterStudent_ValidInput_SuccessfulRegistration()
        {
            // Arrange
            string name = "Jane Smith";
            string address = "456 Elm St";
            string email = "jane@example.com";
            string username = "janesmith";
            string password = "password456";
            string wsuid = "12345678";
            List<string> clubs = new List<string> { "Chess Club", "Debate Team" };

            // Act
            this.accountManager.RegisterStudent(name, address, email, username, password, wsuid, clubs);

            // Assert
            var account = this.accountManager.Login(username, password);
            Assert.IsNotNull(account);
            Assert.IsInstanceOf<StudentAccount>(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual("Student", account.AccountType);
        }

        /// <summary>
        /// Verifies that a user can log in with valid credentials and receive the correct account object.
        /// </summary>
        [Test]
        public void Login_ValidCredentials_ReturnsUserAccount()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            this.accountManager.RegisterDonor("Test User", "Test Address", "test@example.com", username, password, null, null, false, false);

            // Act
            var account = this.accountManager.Login(username, password);

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(username, account.Username);
        }

        /// <summary>
        /// Verifies that logging in with invalid credentials returns null.
        /// </summary>
        [Test]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string username = "nonexistent";
            string password = "wrongpass";

            // Act
            var account = this.accountManager.Login(username, password);

            // Assert
            Assert.IsNull(account);
        }
    }
}
