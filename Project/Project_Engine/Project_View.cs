using System;
using System.Collections.Generic;

namespace Project_Engine
{
    /// <summary>
    /// Represents the details of a project, including funding and associated donations.
    /// </summary>
    public class Project_View
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the club associated with the project.
        /// </summary>
        public string ClubName { get; set; }

        /// <summary>
        /// Gets or sets the target amount for funding the project.
        /// </summary>
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// Gets or sets the total amount of funding received so far.
        /// </summary>
        public decimal CurrentAmount { get; set; }

        /// <summary>
        /// Gets or sets the total number of donations received for the project.
        /// </summary>
        public int NumberOfDonations { get; set; }

        /// <summary>
        /// Gets or sets the end date of the project.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the percentage of the target amount that has been funded.
        /// </summary>
        public double PercentageFunded => TargetAmount > 0 ? (double)CurrentAmount / (double)TargetAmount * 100 : 0;

        /// <summary>
        /// Gets the list of donations made to the project.
        /// </summary>
        public List<Donation> Donations { get; set; } = new List<Donation>();

        /// <summary>
        /// Gets the number of days left until the project reaches its end date.
        /// Returns 0 if the project has already ended.
        /// </summary>
        public int DaysLeft => Math.Max(0, (EndDate.Date - DateTime.Now.Date).Days);

        /// <summary>
        /// Adds a new donation to the project and updates funding metrics.
        /// </summary>
        /// <param name="donation">The donation to be added.</param>
        public void AddDonation(Donation donation)
        {
            Donations.Add(donation);
            CurrentAmount += donation.Amount;
            NumberOfDonations++;
        }

    }


}
