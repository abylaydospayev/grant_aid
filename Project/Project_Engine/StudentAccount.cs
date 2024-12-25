using System.Collections.Generic;

namespace Project_Engine
{
    /// <summary>
    /// Represents a student account with specific student-related details.
    /// </summary>
    public class StudentAccount : UserAccount
    {
        /// <summary>
        /// Gets or sets the WSU ID of the student.
        /// </summary>
        public string WSUID { get; set; }

        /// <summary>
        /// Gets or sets the list of clubs the student is affiliated with.
        /// </summary>
        public List<string> Clubs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentAccount"/> class.
        /// </summary>
        /// <param name="name">The name of the student.</param>
        /// <param name="address">The address of the student.</param>
        /// <param name="email">The email of the student.</param>
        /// <param name="username">The username for the account.</param>
        /// <param name="password">The password for the account.</param>
        /// <param name="wsuid">The WSU ID of the student.</param>
        /// <param name="clubs">The list of clubs the student belongs to.</param>
        public StudentAccount(string name, string address, string email, string username, string password,
            string wsuid, List<string> clubs)
            : base(name, address, email, username, password, "Student")
        {
            WSUID = wsuid;
            Clubs = clubs;
        }
    }
}
