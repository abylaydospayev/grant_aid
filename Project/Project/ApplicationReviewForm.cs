using System;
using System.Linq;

namespace Project
{
    /// <summary>
    /// This form is used to review applications for a scholarship.
    /// </summary>
    public partial class ApplicationReviewForm : Form
    {
        private readonly Scholarship scholarship;

        private ListView listViewApplicants;
        private Button btnAward;

        /// <summary>
        /// Initializes a new instance of the ApplicationReviewForm class.
        /// </summary>
        /// <param name="scholarship">The scholarship to review applications for.</param>
        /// <exception cref="ArgumentNullException">Thrown if scholarship is null.</exception>
        public ApplicationReviewForm(Scholarship scholarship)
        {
            this.scholarship = scholarship ?? throw new ArgumentNullException(nameof(scholarship));
            this.InitializeComponent();
            this.InitializeDetails();
            this.LoadApplicants();
        }

        /// <summary>
        /// Initializes the form properties and layout.
        /// </summary>
        private void InitializeDetails()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Application Review";

            this.listViewApplicants = new ListView
            {
                Dock = DockStyle.Top,
                View = View.Details,
                Height = 300
            };

            this.listViewApplicants.Columns.Add("Name", 150);
            this.listViewApplicants.Columns.Add("GPA", 50);
            this.listViewApplicants.Columns.Add("Meets Criteria", 100);
            this.listViewApplicants.Columns.Add("Awarded", 100);

            this.btnAward = new Button
            {
                Text = "Award/Unaward",
                Location = new Point(250, 320),
                Width = 150,
                Height = 30,
            };
            this.btnAward.Click += this.BtnAward_Click;

            this.Controls.Add(this.listViewApplicants);
            this.Controls.Add(this.btnAward);
        }

        /// <summary>
        /// Loads the list of applicants for the scholarship into the ListView.
        /// </summary>
        private void LoadApplicants()
        {
            this.listViewApplicants.Items.Clear();

            foreach (var applicant in this.scholarship.Applicants)
            {
                var item = new ListViewItem(applicant.Name)
                {
                    SubItems =
                    {
                        applicant.GPA.ToString("F2"),
                        applicant.MeetsCriteria ? "Yes" : "No",
                        applicant.IsAwarded ? "Yes" : "No",
                    },
                };
                this.listViewApplicants.Items.Add(item);
            }

            if (this.listViewApplicants.Items.Count == 0)
            {
                this.listViewApplicants.Items.Add(new ListViewItem("No applicants available."));
            }
        }

        /// <summary>
        /// Handles the click event of the award button.
        /// Toggles the award status of the selected applicant.
        /// </summary>
        /// <param name="sender">The object that raised the event (the button).</param>
        /// <param name="e">The event arguments.</param>
        private void BtnAward_Click(object sender, EventArgs e)
        {
            if (this.listViewApplicants.SelectedItems.Count > 0)
            {
                var selectedIndex = this.listViewApplicants.SelectedIndices[0];
                var selectedApplicant = this.scholarship.Applicants[selectedIndex];

                // Check if awarding this student would exceed the maximum allowed
                if (!selectedApplicant.IsAwarded && this.scholarship.AwardedStudents.Count >= this.scholarship.NumberOfStudentsFunded)
                {
                    MessageBox.Show(
                        $"Cannot award more than {this.scholarship.NumberOfStudentsFunded} students for this scholarship.",
                        "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Toggle award status
                selectedApplicant.IsAwarded = !selectedApplicant.IsAwarded;

                // Update ListView display
                this.listViewApplicants.SelectedItems[0].SubItems[3].Text = selectedApplicant.IsAwarded ? "Yes" : "No";

                // Add or remove from AwardedStudents list
                if (selectedApplicant.IsAwarded)
                {
                    if (!this.scholarship.AwardedStudents.Contains(selectedApplicant.Name))
                    {
                        this.scholarship.AwardedStudents.Add(selectedApplicant.Name);
                    }
                }
                else
                {
                    this.scholarship.AwardedStudents.Remove(selectedApplicant.Name);
                }

                MessageBox.Show($"Updated award status for {selectedApplicant.Name}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an applicant to award/unaward.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}