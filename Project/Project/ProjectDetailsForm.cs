using System;
using System.Windows.Forms;
using Project_Engine;

namespace Project
{
    /// <summary>
    /// This class represents the form that displays the details of a project.
    /// </summary>
    public partial class ProjectDetailsForm : Form
    {
        /// <summary>
        /// Label to display the projectview.
        /// </summary>
        private Project_View project;

        /// <summary>
        /// Label to display the project name.
        /// </summary>
        private Label lblProjectName;

        /// <summary>
        /// Label to display the club name associated with the project.
        /// </summary>
        private Label lblClubName;

        /// <summary>
        /// Label to display the target amount of the project.
        /// </summary>
        private Label lblTargetAmount;

        /// <summary>
        /// Label to display the current amount raised for the project.
        /// </summary>
        private Label lblCurrentAmount;

        /// <summary>
        /// Label to display the percentage of the project funded.
        /// </summary>
        private Label lblPercentageFunded;

        /// <summary>
        /// Label to display the number of donations received for the project.
        /// </summary>
        private Label lblNumberOfDonations;

        /// <summary>
        /// Label to display the number of days remaining for the project.
        /// </summary>
        private Label lblDaysLeft;

        /// <summary>
        /// Text box to display the project description (read-only).
        /// </summary>
        private TextBox txtDescription;

        /// <summary>
        /// List view to display the list of donations made to the project.
        /// </summary>
        private ListView listViewDonations;


        /// <summary>
        /// Constructor for the ProjectDetailsForm class.
        /// Takes a Project_View object as a parameter.
        /// </summary>
        /// <param name="project">The project object to display details for.</param>
        public ProjectDetailsForm(Project_View project)
        {
            this.InitializeComponent();
            this.InitializeControls();
            this.project = project;
            this.LoadProjectDetails();
        }

        /// <summary>
        /// Initializes the controls on the form (labels, text box, list view).
        /// </summary>
        private void InitializeControls()
        {
            // Initialize controls (labels, textboxes, listviews)
            this.lblProjectName = new Label { Location = new Point(20, 20), Size = new Size(300, 20) };
            this.lblClubName = new Label { Location = new Point(20, 50), Size = new Size(300, 20) };
            this.lblTargetAmount = new Label { Location = new Point(20, 80), Size = new Size(300, 20) };
            this.lblCurrentAmount = new Label { Location = new Point(20, 110), Size = new Size(300, 20) };
            this.lblPercentageFunded = new Label { Location = new Point(20, 140), Size = new Size(300, 20) };
            this.lblNumberOfDonations = new Label { Location = new Point(20, 170), Size = new Size(300, 20) };
            this.lblDaysLeft = new Label { Location = new Point(20, 200), Size = new Size(300, 20) };
            this.txtDescription = new TextBox { Location = new Point(20, 230), Multiline = true, Size = new Size(400, 60), ReadOnly = true };
            this.listViewDonations = new ListView { Location = new Point(20, 300), Size = new Size(400, 150) };

            // Set up list view columns
            this.listViewDonations.View = View.Details;
            this.listViewDonations.Columns.Add("Date", 100);
            this.listViewDonations.Columns.Add("Amount", 100);
            this.listViewDonations.Columns.Add("Donor", 150);

            // Add controls to form
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.lblClubName);
            this.Controls.Add(this.lblTargetAmount);
            this.Controls.Add(this.lblCurrentAmount);
            this.Controls.Add(this.lblPercentageFunded);
            this.Controls.Add(this.lblNumberOfDonations);
            this.Controls.Add(this.lblDaysLeft);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.listViewDonations);

            // Set form properties
            this.Text = "Project Details";
            this.ClientSize = new Size(450, 500);
        }

        /// <summary>
        /// Load Project Details.
        /// </summary>
        private void LoadProjectDetails()
        {
            this.lblProjectName.Text = this.project.Name;
            this.lblClubName.Text = this.project.ClubName;
            this.lblTargetAmount.Text = $"${this.project.TargetAmount:N2}";
            this.lblCurrentAmount.Text = $"${this.project.CurrentAmount:N2}";
            this.lblPercentageFunded.Text = $"{this.project.PercentageFunded:N1}%";
            this.lblNumberOfDonations.Text = this.project.NumberOfDonations.ToString();
            this.lblDaysLeft.Text = this.project.DaysLeft.ToString();
            this.txtDescription.Text = this.project.Description;

            this.LoadDonations();
        }

        /// <summary>
        /// Load Donations.
        /// </summary>
        private void LoadDonations()
        {
            foreach (var donation in this.project.Donations)
            {
                ListViewItem item = new ListViewItem(donation.Date.ToShortDateString());
                item.SubItems.Add(donation.DisplayAmount);
                item.SubItems.Add(donation.DisplayDonor);
                this.listViewDonations.Items.Add(item);
            }
        }
    }
}