namespace Project_Engine
{
    /// <summary>
    /// Represents a donor account with specific donor-related details.
    /// </summary>
    public class DonorAccount : UserAccount
    {
        /// <summary>
        /// Gets or sets the affiliation of the donor.
        /// </summary>
        public string Affiliation { get; set; }

        /// <summary>
        /// Gets or sets the credit card information of the donor.
        /// </summary>
        public string CreditCardInfo { get; set; }

        /// <summary>
        /// Gets or sets whether the donor's donations are anonymous.
        /// </summary>
        public bool AnonymousDonations { get; set; }

        /// <summary>
        /// Gets or sets whether the donor's donation amounts are posted.
        /// </summary>
        public bool PostAmounts { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorAccount"/> class.
        /// </summary>
        /// <param name="name">The name of the donor.</param>
        /// <param name="address">The address of the donor.</param>
        /// <param name="email">The email of the donor.</param>
        /// <param name="username">The username for the account.</param>
        /// <param name="password">The password for the account.</param>
        /// <param name="affiliation">The donor's affiliation.</param>
        /// <param name="creditCardInfo">The donor's credit card information.</param>
        /// <param name="anonymousDonations">Whether the donations are anonymous.</param>
        /// <param name="postAmounts">Whether the donation amounts are posted.</param>
        public DonorAccount(string name, string address, string email, string username, string password,
            string affiliation, string creditCardInfo, bool anonymousDonations, bool postAmounts)
            : base(name, address, email, username, password, "Donor")
        {
            Affiliation = affiliation;
            CreditCardInfo = creditCardInfo;
            AnonymousDonations = anonymousDonations;
            PostAmounts = postAmounts;
        }
    }
}
