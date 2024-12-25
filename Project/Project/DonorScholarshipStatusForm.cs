using System;

namespace Project
{
    public partial class DonorScholarshipStatusForm : Form
    {
        private readonly List<Scholarship> scholarships;

        public DonorScholarshipStatusForm(List<Scholarship> scholarships)
        {
            this.scholarships = scholarships ?? throw new ArgumentNullException(nameof(scholarships));
            this.InitializeComponent();
            this.InitializeDetails();
            this.LoadScholarships();
        }

        private void InitializeDetails()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Donor Scholarship Status";

            ListView listViewScholarships = new ()
            {
                Dock = DockStyle.Fill,
                View = View.Details,
            };

            listViewScholarships.Columns.Add("Name", 150);
            listViewScholarships.Columns.Add("Total Amount", 100);
            listViewScholarships.Columns.Add("Amount Per Student", 120);
            listViewScholarships.Columns.Add("Is Awarded", 80);
            listViewScholarships.Columns.Add("Criteria", 200);

            this.Controls.Add(listViewScholarships);
        }

        private void LoadScholarships()
        {
            var listView = (ListView)this.Controls[0];

            foreach (var scholarship in this.scholarships)
            {
                var item = new ListViewItem(scholarship.Name)
                {
                    SubItems =
                    {
                        $"${scholarship.TotalAmount:N2}",
                        $"${scholarship.AmountPerStudentFunded:N2}",
                        scholarship.IsAwarded ? "Yes" : "No",
                        this.FormatCriteria(scholarship),
                    },
                };
                listView.Items.Add(item);
            }
        }

        private string FormatCriteria(Scholarship scholarship)
        {
            return $"Major: {(scholarship.SpecificMajorRequired ? "Yes" : "No")}, " +
                   $"GPA: {scholarship.GPAMinimumRequired}, " +
                   $"Community: {(scholarship.CommunityGroupRequired ? "Yes" : "No")}, " +
                   $"Additional: {scholarship.AdditionalCriteria}";
        }
    }
}