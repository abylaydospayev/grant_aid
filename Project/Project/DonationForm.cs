using Project_Engine;
using System;

namespace Project
{
    /// <summary>
    /// This form allows a donor to make a donation to one of the eligible projects or view their donation history.
    /// </summary>
    public partial class DonationForm : Form
    {
        private readonly List<Project_View> eligibleProjects;
        private readonly DonorAccount donor;
        private readonly ProjectManager projectManager;

        private ComboBox cmbProjects;
        private TextBox txtAmount;
        private CheckBox chkAnonymous;
        private CheckBox chkDiscloseAmount;
        private Button btnDonate;
        private Button btnViewHistory;

        /// <summary>
        /// Initializes a new instance of the DonationForm class.
        /// </summary>
        /// <param name="eligibleProjects">A list of projects the donor can donate to.</param>
        /// <param name="donor">The donor making the donation.</param>
        public DonationForm(List<Project_View> eligibleProjects, DonorAccount donor, ProjectManager projectManager)
        {
            this.eligibleProjects = eligibleProjects ?? throw new ArgumentNullException(nameof(eligibleProjects));
            this.donor = donor ?? throw new ArgumentNullException(nameof(donor));
            this.InitializeComponent();
            this.InitializeDetails();
        }

        /// <summary>
        /// Initializes the form controls and populates the project combo box.
        /// </summary>
        private void InitializeDetails()
        {
            this.ClientSize = new Size(400, 350);
            this.Text = "Make a Donation";

            this.cmbProjects = new ComboBox { Location = new Point(120, 20), Width = 200 };
            foreach (var project in this.eligibleProjects)
            {
                this.cmbProjects.Items.Add(project.Name);
            }
            this.Controls.Add(new Label { Text = "Select Project:", Location = new Point(20, 20), AutoSize = true });
            this.Controls.Add(this.cmbProjects);

            this.txtAmount = new TextBox { Location = new Point(120, 60), Width = 200 };
            this.Controls.Add(new Label { Text = "Amount:", Location = new Point(20, 60), AutoSize = true });
            this.Controls.Add(this.txtAmount);

            this.chkAnonymous = new CheckBox { Text = "Donate Anonymously", Location = new Point(20, 100), AutoSize = true };
            this.chkDiscloseAmount = new CheckBox { Text = "Disclose Amount", Location = new Point(20, 130), AutoSize = true };
            this.Controls.Add(this.chkAnonymous);
            this.Controls.Add(this.chkDiscloseAmount);

            this.btnDonate = new Button { Text = "Donate", Location = new Point(120, 170), Width = 100, Height = 30 };
            this.btnDonate.Click += this.BtnDonate_Click;
            this.Controls.Add(this.btnDonate);

            this.btnViewHistory = new Button { Text = "View History", Location = new Point(120, 210), Width = 100, Height = 30 };
            this.btnViewHistory.Click += this.BtnViewHistory_Click;
            this.Controls.Add(this.btnViewHistory);
        }

        /// <summary>
        /// Handles the click event of the donate button.
        /// Validates user input and creates a donation object.
        /// </summary>
        private void BtnDonate_Click(object sender, EventArgs e)
        {
            if (cmbProjects.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(this.txtAmount.Text, out var amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid donation amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedProject = this.eligibleProjects[this.cmbProjects.SelectedIndex];
            var donation = new Donation
            {
                Date = DateTime.Now,
                Amount = amount,
                DonorName = this.donor.Name,
                IsDonorAnonymous = this.chkAnonymous.Checked,
                IsAmountDisclosed = this.chkDiscloseAmount.Checked,
            };

            selectedProject.AddDonation(donation);

            MessageBox.Show($"Thank you for your donation to \"{selectedProject.Name}\"!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        /// <summary>
        /// Handles the click event of the view history button.
        /// Displays the donor's past donations.
        /// </summary>
        private void BtnViewHistory_Click(object sender, EventArgs e)
        {
            this.ShowDonationHistory(this.donor);
        }

        /// <summary>
        /// Shows the donation history for the given donor.
        /// </summary>
        private void ShowDonationHistory(DonorAccount donor)
        {
            var allProjects = projectManager.GetAllProjects();
            var donorDonations = new List<Donation>();

            foreach (var project in allProjects)
            {
                foreach (var donation in project.Donations)
                {
                    if (donation.DonorName == donor.Name || (donation.IsDonorAnonymous && donor.AnonymousDonations))
                    {
                        donorDonations.Add(donation);
                    }
                }
            }

            if (!donorDonations.Any())
            {
                MessageBox.Show("No donation history available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new DonationHistoryForm(donorDonations);
            form.ShowDialog();
        }
    }
}