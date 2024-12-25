using Project_Engine;
using System;

namespace Project
{
    /// <summary>
    /// Represents a form that displays a list of projects in a ListView control with options to view project details.
    /// </summary>
    public partial class ProjectListForm : Form
    {
        private readonly ProjectManager projectManager;
        private ListView listViewProjects;
        private Button btnViewDetails;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectListForm"/> class.
        /// </summary>
        /// <param name="projectManager">An instance of <see cref="ProjectManager"/> to manage and retrieve project data.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="projectManager"/> is null.</exception>
        public ProjectListForm(ProjectManager projectManager)
        {
            this.InitializeComponent();
            this.projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            this.InitializeControls();
        }

        /// <summary>
        /// Sets up and initializes the form controls.
        /// </summary>
        private void InitializeControls()
        {
            this.LoadProjects();
            this.SetupViewDetailsButton();
        }

        /// <summary>
        /// Loads projects into the ListView control for display.
        /// </summary>
        private void LoadProjects()
        {
            // Initialize and configure the ListView
            this.listViewProjects = CreateListView();

            // Populate the ListView with project data
            foreach (var project in this.projectManager.GetAllProjects())
            {
                ListViewItem item = CreateProjectListViewItem(project);
                this.listViewProjects.Items.Add(item);
            }

            // Adjust column widths for content
            this.listViewProjects.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Add the ListView to the form
            this.Controls.Add(this.listViewProjects);
        }

        /// <summary>
        /// Creates and configures a <see cref="ListView"/> control for displaying project details.
        /// </summary>
        /// <returns>A configured <see cref="ListView"/> instance.</returns>
        private static ListView CreateListView()
        {
            return new ListView
            {
                Dock = DockStyle.Top,
                View = View.Details,
                Height = 300,
                Columns =
                {
                    new ColumnHeader { Text = "Project Name", Width = -2 },
                    new ColumnHeader { Text = "Club", Width = -2 },
                    new ColumnHeader { Text = "Target Amount", Width = -2 },
                    new ColumnHeader { Text = "Funded (%)", Width = -2 },
                    new ColumnHeader { Text = "Donations", Width = -2 },
                    new ColumnHeader { Text = "Days Left", Width = -2 },
                },
            };
        }

        /// <summary>
        /// Sets up the "View Details" button and adds it to the form.
        /// </summary>
        private void SetupViewDetailsButton()
        {
            this.btnViewDetails = new Button
            {
                Text = "View Details",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnViewDetails.Click += BtnViewDetails_Click;
            Controls.Add(btnViewDetails);
        }

        /// <summary>
        /// Handles the click event for the "View Details" button, opening the details of the selected project.
        /// </summary>
        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            OpenSelectedProjectDetails();
        }

        /// <summary>
        /// Opens a details form for the currently selected project in the ListView.
        /// </summary>
        private void OpenSelectedProjectDetails()
        {
            if (listViewProjects.SelectedItems.Count > 0)
            {
                int selectedIndex = listViewProjects.SelectedIndices[0];
                Project_View selectedProject = projectManager.GetAllProjects()[selectedIndex];
                using (ProjectDetailsForm detailsForm = new ProjectDetailsForm(selectedProject))
                {
                    detailsForm.ShowDialog(); // Opens as a modal dialog
                }
            }
            else
            {
                MessageBox.Show("Please select a project to view details.", "No Project Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Creates a ListViewItem for a given project.
        /// </summary>
        /// <param name="project">The <see cref="Project_View"/> instance representing the project.</param>
        /// <returns>A configured <see cref="ListViewItem"/> with project details.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="project"/> is null.</exception>
        private static ListViewItem CreateProjectListViewItem(Project_View project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            var item = new ListViewItem(project.Name)
            {
                SubItems =
                {
                    project.ClubName,
                    $"${project.TargetAmount:N2}",
                    $"{project.PercentageFunded:N1}%",
                    project.NumberOfDonations.ToString(),
                    project.DaysLeft.ToString()
                },
            };

            return item;
        }
    }
}
