using System;

namespace Project_Engine
{
    /// <summary>
    /// Represents a generic user account in the system.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the username of the account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the type of account (e.g., "Donor" or "Student").
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccount"/> class.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="address">The address of the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="username">The username for the account.</param>
        /// <param name="password">The password for the account.</param>
        /// <param name="accountType">The type of the account.</param>
        public UserAccount(string name, string address, string email, string username, string password, string accountType)
        {
            Name = name;
            Address = address;
            Email = email;
            Username = username;
            Password = password;
            AccountType = accountType;
        }
    }
}
