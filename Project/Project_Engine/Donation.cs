namespace Project_Engine
{
    /// <summary>
    /// Represents a donation made to a project or scholarship.
    /// </summary>
    public class Donation
    {
        private DateTime date;
        private decimal amount;

        /// <summary>
        /// Gets or sets the unique identifier for the donation.
        /// </summary>
        public int DonationId { get; set; }

        /// <summary>
        /// Gets or sets the date of the donation.
        /// </summary>
        public DateTime Date
        {
            get => this.date;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Date cannot be in the future.");
                }

                this.date = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of the donation.
        /// </summary>
        public decimal Amount
        {
            get => this.amount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Amount must be positive.");
                }

                this.amount = value;
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier for the donor.
        /// </summary>
        public int DonorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the donor.
        /// </summary>
        public string DonorName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the donation amount is disclosed.
        /// </summary>
        public bool IsAmountDisclosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the donor wishes to remain anonymous.
        /// </summary>
        public bool IsDonorAnonymous { get; set; }

        /// <summary>
        /// Gets or sets the purpose of the donation.
        /// </summary>
        public string DonationPurpose { get; set; }

        /// <summary>
        /// Gets or sets the donor's contact information.
        /// </summary>
        public string DonorContactInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the donation is tax-deductible.
        /// </summary>
        public bool IsTaxDeductible { get; set; }

        /// <summary>
        /// Gets or sets the status of the donation (e.g., "Pending", "Processed", "Refunded").
        /// </summary>
        public string DonationStatus { get; set; }

        /// <summary>
        /// Gets the display value for the donation amount based on disclosure preferences.
        /// </summary>
        public string DisplayAmount => this.IsAmountDisclosed ? $"${this.Amount:N2}" : "Undisclosed";

        /// <summary>
        /// Gets the display value for the donor's name based on anonymity preferences.
        /// </summary>
        public string DisplayDonor => this.IsDonorAnonymous ? "Anonymous" : this.DonorName;
    }
}