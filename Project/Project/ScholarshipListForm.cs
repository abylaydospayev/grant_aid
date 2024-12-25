using Project_Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    /// <summary>
    /// This form displays a list of scholarships with details like name, status, and awarded students.
    /// </summary>
    public partial class ScholarshipListForm : Form
    {
        /// <summary>
        /// List of scholarships to be displayed.
        /// </summary>
        private List<Scholarship> scholarships;

        /// <summary>
        /// Constructor that takes a list of scholarships as input.
        /// Throws an ArgumentNullException if the scholarships list is null.
        /// </summary>
        /// <param name="scholarships">List of scholarships to be displayed.</param>
        public ScholarshipListForm(List<Scholarship> scholarships)
        {
            InitializeComponent();
            InitializeView();
            this.scholarships = scholarships ?? throw new ArgumentNullException(nameof(scholarships));
            LoadScholarships();
        }

        /// <summary>
        /// Initializes the basic properties of the form.
        /// </summary>
        private void InitializeView()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Scholarship List";
            this.Controls.Add(CreateListView());

            // Add a button to view details of a selected scholarship
            Button btnViewDetails = new Button
            {
                Text = "View Details",
                Location = new Point(450, 350),
                Width = 100,
                Height = 30
            };
            btnViewDetails.Click += BtnViewDetails_Click;
            Controls.Add(btnViewDetails);
        }

        /// <summary>
        /// Creates a ListView control to display scholarship details.
        /// </summary>
        /// <returns>A ListView control with configured columns.</returns>
        private ListView CreateListView()
        {
            ListView listView = new ListView
            {
                Dock = DockStyle.Top,
                View = View.Details,
                Height = 300 // Adjust height as needed
            };

            listView.Columns.Add("Scholarship Name", -2);
            listView.Columns.Add("Status", -2);
            listView.Columns.Add("Awarded Students", -2);

            return listView;
        }

        /// <summary>
        /// Populates the ListView with scholarship data from the provided list.
        /// </summary>
        private void LoadScholarships()
        {
            var listView = (ListView)this.Controls[0];

            foreach (var scholarship in scholarships)
            {
                var item = new ListViewItem(scholarship.Name)
                {
                    SubItems =
                {
                    scholarship.Status,
                    scholarship.AwardedStudentsDisplay
                }
                };
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Handles the click event of the "View Details" button.
        /// Opens the ScholarshipDetailsForm for the selected scholarship.
        /// </summary>
        /// <param name="sender">The button object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            ShowSelectedScholarshipDetails();
        }

        /// <summary>
        /// Shows the details of the selected scholarship in a separate form.
        /// Displays a message if no scholarship is selected.
        /// </summary>
        private void ShowSelectedScholarshipDetails()
        {
            var listView = (ListView)this.Controls[0];

            if (listView.SelectedItems.Count > 0)
            {
                int selectedIndex = listView.SelectedIndices[0];
                Scholarship selectedScholarship = scholarships[selectedIndex];
                var form = new ScholarshipDetailsForm(selectedScholarship);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a scholarship to view details.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowScholarshipList()
        {
            var form = new ScholarshipListForm(scholarships); // Pass updated scholarships list
            form.ShowDialog();
        }
    }
}