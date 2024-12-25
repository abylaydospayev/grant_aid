using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;
using Project;
using Project_Engine;

namespace Project_Test
{
    /// <summary>
    /// Test fixture for validating the functionality of the ScholarshipListForm.
    /// </summary>
    [TestFixture]
    public class ScholarshipListFormTests
    {
        private List<Scholarship> sampleScholarships;

        /// <summary>
        /// Sets up the test environment by initializing sample scholarships before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.sampleScholarships = new List<Scholarship>
            {
                new Scholarship { Name = "Academic Excellence", IsAwarded = true, AwardedStudents = new List<string> { "Alice Smith", "Bob Johnson" } },
                new Scholarship { Name = "Community Service", IsAwarded = false },
                new Scholarship { Name = "Leadership Award", IsAwarded = true, AwardedStudents = new List<string> { "Charlie Brown" } },
            };
        }

        /// <summary>
        /// Verifies that the ScholarshipListForm displays the correct number of scholarships.
        /// </summary>
        [Test]
        public void ScholarshipListForm_DisplaysCorrectNumberOfScholarships()
        {
            // Arrange
            var form = new ScholarshipListForm(this.sampleScholarships);

            // Act
            form.Show(); // Ensure controls are initialized

            // Assert
            var listView = (ListView)form.Controls[0];
            Assert.AreEqual(this.sampleScholarships.Count, listView.Items.Count); // Verify the number of items

            form.Close(); // Close the form after test
        }
    }
}