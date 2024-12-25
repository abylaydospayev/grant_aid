using System.Collections.Generic;
using System.Linq;

namespace Project_Engine
{
    /// <summary>
    /// Manages user accounts, including donors and students.
    /// Provides functionality for registering and authenticating users.
    /// </summary>
    public class AccountManager
    {
        /// <summary>
        /// List of registered user accounts.
        /// </summary>
        private static List<UserAccount> accounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager"/> class.
        /// </summary>
        public AccountManager()
        {
            accounts = new List<UserAccount>();
        }

        /// <summary>
        /// Registers a new donor account.
        /// </summary>
        /// <param name="name">The name of the donor.</param>
        /// <param name="address">The address of the donor.</param>
        /// <param name="email">The email of the donor.</param>
        /// <param name="username">The username for the donor's account.</param>
        /// <param name="password">The password for the donor's account.</param>
        /// <param name="affiliation">The donor's affiliation.</param>
        /// <param name="creditCardInfo">The donor's credit card information.</param>
        /// <param name="anonymousDonations">Indicates whether the donor's donations are anonymous.</param>
        /// <param name="postAmounts">Indicates whether the donation amounts are posted publicly.</param>
        public void RegisterDonor(
            string name,
            string address,
            string email,
            string username,
            string password,
            string affiliation,
            string creditCardInfo,
            bool anonymousDonations,
            bool postAmounts)
        {
            var donorAccount = new DonorAccount(
                name,
                address,
                email,
                username,
                password,
                affiliation,
                creditCardInfo,
                anonymousDonations,
                postAmounts);
            accounts.Add(donorAccount);
        }

        /// <summary>
        /// Registers a new student account.
        /// </summary>
        /// <param name="name">The name of the student.</param>
        /// <param name="address">The address of the student.</param>
        /// <param name="email">The email of the student.</param>
        /// <param name="username">The username for the student's account.</param>
        /// <param name="password">The password for the student's account.</param>
        /// <param name="wsuid">The WSU ID of the student.</param>
        /// <param name="clubs">A list of clubs the student belongs to.</param>
        public void RegisterStudent(
            string name,
            string address,
            string email,
            string username,
            string password,
            string wsuid,
            List<string> clubs)
        {
            var studentAccount = new StudentAccount(
                name,
                address,
                email,
                username,
                password,
                wsuid,
                clubs);
            accounts.Add(studentAccount);
        }

        /// <summary>
        /// Authenticates a user by their username and password.
        /// </summary>
        /// <param name="username">The username of the account.</param>
        /// <param name="password">The password of the account.</param>
        /// <returns>
        /// The authenticated <see cref="UserAccount"/> object if the credentials match; otherwise, null.
        /// </returns>
        public UserAccount Login(string username, string password)
        {
            return accounts.FirstOrDefault(account =>
                account.Username == username && account.Password == password);
        }
    }
}
