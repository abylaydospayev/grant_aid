using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project_Engine;

namespace Project
{
    /// <summary>
    /// The main form of the application.
    /// Provides functionality for user login, registration, and project viewing.
    /// </summary>
    public partial class Form1 : Form
    {
        private static readonly AccountManager AccountManager = new AccountManager();
        private static readonly ProjectManager ProjectManager = new ProjectManager();
        private List<Scholarship> scholarships;
        private DonorAccount currentDonor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.SetupLoginControls();
            this.SetupGuestControls();
            this.InitializeSampleProjects();
            this.SetupScholarshipButton();
            this.InitializeSampleScholarships();
        }

        /// <summary>
        /// Sets up controls for guest users to view projects.
        /// </summary>
        private void SetupGuestControls()
        {
            Button btnViewProjects = new Button
            {
                Text = "View Projects",
                Location = new Point(100, 120),
                Width = 100,
                Height = 30,
            };
            btnViewProjects.Click += (sender, e) => this.OpenProjectListForm();
            this.Controls.Add(btnViewProjects);
        }

        /// <summary>
        /// Opens the project list form.
        /// </summary>
        private void OpenProjectListForm()
        {
            ProjectListForm projectListForm = new ProjectListForm(ProjectManager);
            projectListForm.Show();
        }

        /// <summary>
        /// Sets up controls for the login interface.
        /// </summary>
        private void SetupLoginControls()
        {
            // Create login controls
            Label lblUsername = new Label
            {
                Text = "Username:",
                Location = new Point(20, 20),
                AutoSize = true,
            };
            TextBox txtUsername = new TextBox
            {
                Location = new Point(100, 20),
                Width = 150,
            };
            Label lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(20, 50),
                AutoSize = true,
            };
            TextBox txtPassword = new TextBox
            {
                Location = new Point(100, 50),
                Width = 150,
                PasswordChar = '*',
            };
            Button btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(100, 80),
                Width = 75,
                Height = 30,
            };
            Button btnRegister = new Button
            {
                Text = "Register",
                Location = new Point(180, 80),
                Width = 75,
                Height = 30,
            };

            // Add controls to the form
            this.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPassword, txtPassword, btnLogin, btnRegister });

            // Add event handlers
            btnLogin.Click += (sender, e) => this.Login(txtUsername.Text, txtPassword.Text);
            btnRegister.Click += (sender, e) => this.OpenRegistrationForm();
        }

        /// <summary>
        /// Handles the login functionality by authenticating user credentials.
        /// </summary>
        /// <param name="username">The username entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        private void Login(string username, string password)
        {
            UserAccount account = AccountManager.Login(username, password);
            if (account != null)
            {
                MessageBox.Show($"Login successful! Welcome, {account.Name}");

                // Check if the logged-in user is a DonorAccount
                if (account is DonorAccount donor)
                {
                    this.currentDonor = donor;
                    this.SetupDonorControls(donor);
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.");
            }
        }

        /// <summary>
        /// Opens the registration form for new users.
        /// </summary>
        private void OpenRegistrationForm()
        {
            RegistrationForm registrationForm = new RegistrationForm(AccountManager);
            registrationForm.ShowDialog();
        }

        /// <summary>
        /// Initializes sample projects for demonstration purposes.
        /// </summary>
        private void InitializeSampleProjects()
        {
            var project1 = new Project_View
            {
                Id = 1,
                Name = "New Computer Lab",
                ClubName = "Computer Science Club",
                TargetAmount = 10000,
                CurrentAmount = 5000,
                NumberOfDonations = 25,
                EndDate = DateTime.Now.AddDays(30),
                Description = "A project to raise funds for a new computer lab.",
                Donations = new List<Donation>
                {
                    new Donation { Date = DateTime.Now.AddDays(-5), Amount = 100, DonorName = "Alice", IsAmountDisclosed = true, IsDonorAnonymous = false },
                    new Donation { Date = DateTime.Now.AddDays(-3), Amount = 200, DonorName = "Bob", IsAmountDisclosed = true, IsDonorAnonymous = false },
                    new Donation { Date = DateTime.Now.AddDays(-1), Amount = 50, DonorName = null, IsAmountDisclosed = false, IsDonorAnonymous = true },
                },
            };
            ProjectManager.AddProject(project1);

            var project2 = new Project_View
            {
                Id = 2,
                Name = "Art Exhibition",
                ClubName = "Art Club",
                TargetAmount = 5000,
                CurrentAmount = 2000,
                NumberOfDonations = 15,
                EndDate = DateTime.Now.AddDays(45),
                Description = "An exhibition to showcase student artworks.",
                Donations = new List<Donation>
                {
                    new Donation { Date = DateTime.Now.AddDays(-10), Amount = 150, DonorName = "Charlie", IsAmountDisclosed = true, IsDonorAnonymous = false },
                    new Donation { Date = DateTime.Now.AddDays(-8), Amount = 300, DonorName = null, IsAmountDisclosed = false, IsDonorAnonymous = true },
                },
            };
            ProjectManager.AddProject(project2);
        }

        /// <summary>
        /// Sets up the button to view scholarships.
        /// </summary>
        private void SetupScholarshipButton()
        {
            Button btnViewScholarships = new Button
            {
                Text = "View Scholarships",
                Location = new Point(100, 160),
                Width = 150,
                Height = 30,
            };
            btnViewScholarships.Click += this.BtnViewScholarships_Click;
            this.Controls.Add(btnViewScholarships);
        }

        /// <summary>
        /// Initializes sample scholarships for demonstration purposes.
        /// </summary>
        private void InitializeSampleScholarships()
        {
            this.scholarships = new List<Scholarship>
            {
                new Scholarship
                {
                    Name = "Academic Excellence",
                    Description = "This scholarship is awarded to students who have demonstrated outstanding academic performance.",
                    IsAwarded = true,
                    AwardedStudents = new List<string> { "Alice Smith", "Bob Johnson" },
                    Donations = new List<Donation>
                    {
                        new Donation { Date = DateTime.Now.AddDays(-10), Amount = 500, DonorName = "John Doe", IsAmountDisclosed = true, IsDonorAnonymous = false },
                        new Donation { Date = DateTime.Now.AddDays(-5), Amount = 250, DonorName = null, IsAmountDisclosed = false, IsDonorAnonymous = true },
                    },
                },
                new Scholarship
                {
                    Name = "Community Service",
                    Description = "Awarded to students who have made significant contributions to community service.",
                    IsAwarded = false,
                    Donations = new List<Donation>
                    {
                        new Donation { Date = DateTime.Now.AddDays(-15), Amount = 300, DonorName = "Jane Doe", IsAmountDisclosed = true, IsDonorAnonymous = false },
                    },
                },
                new Scholarship
                {
                    Name = "Leadership Award",
                    Description = "Recognizes students who have shown exceptional leadership skills.",
                    IsAwarded = true,
                    AwardedStudents = new List<string> { "Charlie Brown" },
                    Donations = new List<Donation>
                    {
                        new Donation { Date = DateTime.Now.AddDays(-20), Amount = 400, DonorName = null, IsAmountDisclosed = false, IsDonorAnonymous = true },
                    },
                },
            };
        }

        /// <summary>
        /// Handles the click event for viewing scholarships.
        /// </summary>
        private void BtnViewScholarships_Click(object sender, EventArgs e)
        {
            this.ShowScholarshipList();
        }

        /// <summary>
        /// Opens the scholarship list form.
        /// </summary>
        private void ShowScholarshipList()
        {
            var form = new ScholarshipListForm(this.scholarships);
            form.ShowDialog();
        }

        private void OpenMakeDonationForm(DonorAccount donor)
        {
            var eligibleProjects = ProjectManager.GetAllProjects()
                .Where(p => p.EndDate > DateTime.Now)
                .ToList();
            if (!eligibleProjects.Any())
            {
                MessageBox.Show("No projects are currently accepting donations.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new DonationForm(eligibleProjects, donor, ProjectManager); // Pass projectManager here
            form.ShowDialog();
        }

        private void ShowDonationHistory(DonorAccount donor)
        {
            // Collect all donations made by this donor across all projects
            var allProjects = ProjectManager.GetAllProjects();
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

        private void SetupDonorControls(DonorAccount donor)
        {
            // Button to make a donation
            Button btnAddDonation = new Button
            {
                Text = "Add Donation",
                Location = new Point(100, 200), // Adjust location as needed
                Width = 150,
                Height = 30,
            };
            btnAddDonation.Click += (sender, e) => this.OpenMakeDonationForm(donor);
            this.Controls.Add(btnAddDonation);

            // Button to view donation history
            Button btnViewHistory = new Button
            {
                Text = "View Donation History",
                Location = new Point(100, 240), // Adjust location as needed
                Width = 150,
                Height = 30,
            };
            btnViewHistory.Click += (sender, e) => this.ShowDonationHistory(donor);
            this.Controls.Add(btnViewHistory);

            // Button to setup scholarship
            Button btnSetupScholarship = new Button
            {
                Text = "Setup Scholarship",
                Location = new Point(100, 280), // Adjust location as needed
                Width = 150,
                Height = 30,
            };
            btnSetupScholarship.Click += (sender, e) => this.OpenSetupScholarshipForm();
            this.Controls.Add(btnSetupScholarship);

            // Button to view scholarship status
            Button btnViewScholarshipStatus = new Button
            {
                Text = "View My Scholarships",
                Location = new Point(100, 320),
                Width = 300,
                Height = 30,
            };
            btnViewScholarshipStatus.Click += (sender, e) => this.ShowDonorScholarshipList(donor);
            this.Controls.Add(btnViewScholarshipStatus);

            // Button to review applications
            Button btnReviewApplications = new Button
            {
                Text = "Review Applications",
                Location = new Point(100, 360), // Adjust location as needed
                Width = 150,
                Height = 30,
            };
            btnReviewApplications.Click += (sender, e) => this.OpenApplicationReviewForm(donor);
            this.Controls.Add(btnReviewApplications);
        }

        private void OpenApplicationReviewForm(DonorAccount donor)
        {
            var donorScholarships = this.scholarships.Where(s => s.DonorUsername == donor.Username).ToList();
            if (!donorScholarships.Any())
            {
                MessageBox.Show("No scholarships found for this account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // For simplicity, open the first scholarship's application review form
            var form = new ApplicationReviewForm(donorScholarships.First());
            form.ShowDialog();
        }

        private void OpenSetupScholarshipForm()
        {
            if (this.currentDonor == null)
            {
                MessageBox.Show("No donor is currently logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new SetupScholarshipForm(this.scholarships, this.currentDonor); // Pass both scholarships and currentDonor
            form.ShowDialog();
        }

        private void OpenScholarshipStatusForm()
        {
            var form = new DonorScholarshipStatusForm(this.scholarships); // Pass the existing scholarships list
            form.ShowDialog();
        }

        private void ShowDonorScholarshipList(DonorAccount currentDonor)
        {
            var donorScholarships = this.scholarships.Where(s => s.DonorUsername == currentDonor.Username).ToList();
            if (!donorScholarships.Any())
            {
                MessageBox.Show("No scholarships found for this account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new ScholarshipListForm(donorScholarships); // Assuming you have a form to display these
            form.ShowDialog();
        }

        private void ShowApplicationReview(Scholarship scholarship)
        {
            var form = new ApplicationReviewForm(scholarship);
            form.ShowDialog();
        }
    }
}