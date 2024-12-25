using System;
using Project_Engine;

namespace Project
{
    /// <summary>
    /// This class represents the donation history form.
    /// </summary>
    public partial class DonationHistoryForm : Form
    {
        /// <summary>
        /// List of donations to be displayed.
        /// </summary>
        private readonly List<Donation> donations;

        /// <summary>
        /// Initializes a new instance of the DonationHistoryForm class.
        /// </summary>
        /// <param name="donations">The list of donations to display.</param>
        /// <exception cref="ArgumentNullException">Thrown if donations is null.</exception>
        public DonationHistoryForm(List<Donation> donations)
        {
            this.donations = donations ?? throw new ArgumentNullException(nameof(donations));
            this.InitializeComponent();
            this.InitializeDetails();
            this.LoadDonationHistory();
        }

        /// <summary>
        /// Initializes the basic form details like size and title.
        /// </summary>
        private void InitializeDetails()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Donation History";

            ListView listViewDonations = new ()
            {
                Dock = DockStyle.Fill,
                View = View.Details,
            };

            listViewDonations.Columns.Add("Date", 100);
            listViewDonations.Columns.Add("Amount", 100);
            listViewDonations.Columns.Add("Donor", 150);

            this.Controls.Add(listViewDonations);
        }

        /// <summary>
        /// Loads the donation details from the donation list into the ListView.
        /// </summary>
        private void LoadDonationHistory()
        {
            var listViewDonations = (ListView)this.Controls[0];

            foreach (var donation in donations)
            {
                var item = new ListViewItem(donation.Date.ToShortDateString())
                {
                    SubItems =
                    {
                        donation.DisplayAmount,
                        donation.DisplayDonor,
                    },
                };
                listViewDonations.Items.Add(item);
            }
        }
    }
}
