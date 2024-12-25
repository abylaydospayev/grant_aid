using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project_Engine;

namespace Project
{
    /// <summary>
    /// Represents a registration form for Donor and Student accounts.
    /// </summary>
    public partial class SetupScholarshipForm : Form
    {
        private readonly List<Scholarship> scholarships;
        private readonly DonorAccount currentDonor;
        private TextBox txtScholarshipName;
        private TextBox txtTotalAmount;
        private TextBox txtNumberOfStudents;
        private CheckBox chkSpecificMajor;
        private TextBox txtGPAThreshold;
        private CheckBox chkCommunityGroup;
        private TextBox txtCriteriaDescription;
        private Button btnSubmit;
        private ListView listViewApplicants;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupScholarshipForm"/> class.
        /// </summary>
        /// <param name="scholarships">The list of scholarships.</param>
        /// <param name="donor">The current donor account.</param>
        public SetupScholarshipForm(List<Scholarship> scholarships, DonorAccount donor)
        {
            this.scholarships = scholarships ?? throw new ArgumentNullException(nameof(scholarships));
            this.currentDonor = donor ?? throw new ArgumentNullException(nameof(donor));
            this.InitializeComponent();
            this.InitializeDetails();
        }

        private void InitializeDetails()
        {
            this.ClientSize = new Size(600, 500);
            this.Text = "Setup Scholarship";

            Label lblScholarshipName = new Label { Text = "Scholarship Name:", Location = new Point(20, 20), AutoSize = true };
            this.txtScholarshipName = new TextBox { Location = new Point(180, 20), Width = 250 };

            Label lblTotalAmount = new Label { Text = "Total Amount:", Location = new Point(20, 60), AutoSize = true };
            this.txtTotalAmount = new TextBox { Location = new Point(180, 60), Width = 250 };

            Label lblNumberOfStudents = new Label { Text = "Number of Students:", Location = new Point(20, 100), AutoSize = true };
            this.txtNumberOfStudents = new TextBox { Location = new Point(180, 100), Width = 250 };

            this.chkSpecificMajor = new CheckBox { Text = "Enrolled in Specific Major", Location = new Point(20, 140), AutoSize = true };

            Label lblGPAThreshold = new Label { Text = "GPA Threshold:", Location = new Point(20, 180), AutoSize = true };
            this.txtGPAThreshold = new TextBox { Location = new Point(180, 180), Width = 250 };

            this.chkCommunityGroup = new CheckBox { Text = "Part of a Community Group", Location = new Point(20, 220), AutoSize = true };

            Label lblCriteriaDescription = new Label { Text = "Additional Criteria:", Location = new Point(20, 260), AutoSize = true };
            this.txtCriteriaDescription = new TextBox { Location = new Point(180, 260), Width = 250, Multiline = true, Height = 60 };

            this.listViewApplicants = new ListView
            {
                Location = new Point(20, 330),
                Size = new Size(550, 100),
                View = View.Details
            };
            this.listViewApplicants.Columns.Add("Name", 150);
            this.listViewApplicants.Columns.Add("GPA", 50);
            this.listViewApplicants.Columns.Add("Meets Criteria", 100);

            Button btnAddApplicant = new Button { Text = "Add Applicant", Location = new Point(450, 300), Width = 100 };
            btnAddApplicant.Click += this.BtnAddApplicant_Click;

            this.btnSubmit = new Button { Text = "Submit", Location = new Point(250, 440), Width = 100 };
            this.btnSubmit.Click += this.BtnSubmit_Click;

            this.Controls.AddRange(new Control[]
            {
                lblScholarshipName, this.txtScholarshipName, lblTotalAmount, this.txtTotalAmount,
                lblNumberOfStudents, this.txtNumberOfStudents, this.chkSpecificMajor, lblGPAThreshold,
                this.txtGPAThreshold, this.chkCommunityGroup, lblCriteriaDescription, this.txtCriteriaDescription,
                this.listViewApplicants, btnAddApplicant, this.btnSubmit
            });
        }

        private void BtnAddApplicant_Click(object sender, EventArgs e)
        {
            // Logic to add an applicant
            string applicantName = Prompt.ShowDialog("Enter Applicant Name:", "Applicant Name");
            if (decimal.TryParse(Prompt.ShowDialog("Enter GPA:", "Applicant GPA"), out decimal gpa))
            {
                bool meetsCriteria = MessageBox.Show("Does the applicant meet the criteria?", "Criteria Check", MessageBoxButtons.YesNo) == DialogResult.Yes;

                var item = new ListViewItem(applicantName)
                {
                    SubItems =
                    {
                        gpa.ToString("F2"),
                        meetsCriteria ? "Yes" : "No"
                    }
                };
                this.listViewApplicants.Items.Add(item);
            }
            else
            {
                MessageBox.Show("Invalid GPA entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtScholarshipName.Text))
            {
                MessageBox.Show("Please enter a scholarship name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(this.txtTotalAmount.Text, out var totalAmount) || totalAmount <= 0)
            {
                MessageBox.Show("Please enter a valid total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(this.txtNumberOfStudents.Text, out var numberOfStudents) || numberOfStudents <= 0)
            {
                MessageBox.Show("Please enter a valid number of students.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(this.txtGPAThreshold.Text, out var gpaThreshold) || gpaThreshold < 0 || gpaThreshold > 4)
            {
                MessageBox.Show("Please enter a valid GPA threshold (0-4).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var criteriaDescription = this.txtCriteriaDescription.Text.Trim();

            decimal amountPerStudent = totalAmount / numberOfStudents;

            // Create a list of applicants from ListView items
            var applicants = this.listViewApplicants.Items.Cast<ListViewItem>()
                            .Select(item => new Applicant
                            {
                                Name = item.SubItems[0].Text,
                                GPA = decimal.Parse(item.SubItems[1].Text),
                                MeetsCriteria = item.SubItems[2].Text == "Yes"
                            }).ToList();

            // Create a new Scholarship object and add it to the list
            var scholarship = new Scholarship
            {
                Name = this.txtScholarshipName.Text,
                TotalAmount = totalAmount,
                NumberOfStudentsFunded = numberOfStudents,
                AmountPerStudentFunded = amountPerStudent,
                SpecificMajorRequired = this.chkSpecificMajor.Checked,
                GPAMinimumRequired = gpaThreshold,
                CommunityGroupRequired = this.chkCommunityGroup.Checked,
                AdditionalCriteria = criteriaDescription,
                DonorUsername = this.currentDonor.Username,
                SetupDate = DateTime.Now,
                Applicants = applicants // Add applicants to the scholarship
            };

            this.scholarships.Add(scholarship);

            // Confirmation message
            MessageBox.Show($"Scholarship '{scholarship.Name}' created successfully with {applicants.Count} applicants.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}