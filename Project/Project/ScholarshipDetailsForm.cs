using Project_Engine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    /// <summary>
    /// This form displays detailed information about a scholarship.
    /// </summary>
    public partial class ScholarshipDetailsForm : Form
    {
        private Scholarship scholarship;

        private Label lblScholarshipName;
        private Label lblStatus;
        private Label lblAwardedStudents;
        private TextBox txtDescription;
        private Label lblSetupDate;
        private Label lblTotalAmount;
        private Label lblAmountPerStudent;
        private Label lblSpecificMajor;
        private Label lblGPAThreshold;
        private Label lblCommunityGroup;
        private Label lblAdditionalCriteria;
        private Label lblDonorName;

        /// <summary>
        /// Constructor that takes a Scholarship object as input.
        /// </summary>
        /// <param name="scholarship">The scholarship to display details for.</param>
        public ScholarshipDetailsForm(Scholarship scholarship)
        {
            this.scholarship = scholarship ?? throw new ArgumentNullException(nameof(scholarship));
            InitializeComponent();
            InitializeDetails();
            LoadScholarshipDetails();
        }

        /// <summary>
        /// Initializes the basic properties and layout of the form.
        /// </summary>
        private void InitializeDetails()
        {
            this.ClientSize = new Size(600, 500);
            this.Text = "Scholarship Details";

            lblScholarshipName = new Label { Location = new Point(20, 20), Size = new Size(300, 20) };
            lblStatus = new Label { Location = new Point(20, 50), Size = new Size(300, 20) };
            lblAwardedStudents = new Label { Location = new Point(20, 80), Size = new Size(300, 20) };
            txtDescription = new TextBox { Location = new Point(20, 110), Multiline = true, Size = new Size(550, 100), ReadOnly = true };

            lblSetupDate = new Label { Location = new Point(20, 220), Size = new Size(300, 20) };
            lblTotalAmount = new Label { Location = new Point(20, 250), Size = new Size(300, 20) };
            lblAmountPerStudent = new Label { Location = new Point(20, 280), Size = new Size(300, 20) };
            lblSpecificMajor = new Label { Location = new Point(20, 310), Size = new Size(300, 20) };
            lblGPAThreshold = new Label { Location = new Point(20, 340), Size = new Size(300, 20) };
            lblCommunityGroup = new Label { Location = new Point(20, 370), Size = new Size(300, 20) };
            lblAdditionalCriteria = new Label { Location = new Point(20, 400), Size = new Size(300, 40) };

            Controls.Add(lblScholarshipName);
            Controls.Add(lblStatus);
            Controls.Add(lblAwardedStudents);
            Controls.Add(txtDescription);
            Controls.Add(lblSetupDate);
            Controls.Add(lblTotalAmount);
            Controls.Add(lblAmountPerStudent);
            Controls.Add(lblSpecificMajor);
            Controls.Add(lblGPAThreshold);
            Controls.Add(lblCommunityGroup);
            Controls.Add(lblAdditionalCriteria);
        }

        /// <summary>
        /// Populates the form controls with scholarship details.
        /// </summary>
        private void LoadScholarshipDetails()
        {
            lblScholarshipName.Text = $"Name: {scholarship.Name}";
            lblStatus.Text = $"Status: {(scholarship.IsAwarded ? "Awarded" : "Not Awarded")}";
            lblAwardedStudents.Text = $"Awarded Students: {(scholarship.AwardedStudents.Count > 0 ? string.Join(", ", scholarship.AwardedStudents) : "None")}";

            // Format and assign description text
            txtDescription.Text =
                $"Setup Date: {scholarship.SetupDate.ToShortDateString()}\n" +
                $"Total Amount: ${scholarship.TotalAmount:N2}\n" +
                $"Amount per student: ${scholarship.AmountPerStudentFunded:N2}\n" +
                $"Criteria:\n" +
                $"- Specific Major: {(scholarship.SpecificMajorRequired ? "Yes" : "No")}\n" +
                $"- GPA Threshold: {scholarship.GPAMinimumRequired}\n" +
                $"- Community Group: {(scholarship.CommunityGroupRequired ? "Yes" : "No")}\n" +
                $"- Additional Criteria: {scholarship.AdditionalCriteria}";
        }
    }
}