using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project_Engine;

namespace Project_Test
{
    /// <summary>
    /// Test fixture for validating the functionality of the ProjectManager and Project_View classes.
    /// </summary>
    [TestFixture]
    public class ProjectView_Test
    {
        private ProjectManager projectManager;

        /// <summary>
        /// Sets up the test environment by initializing a new instance of ProjectManager before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.projectManager = new ProjectManager();
        }

        /// <summary>
        /// Verifies that a project can be successfully added to the project list.
        /// </summary>
        [Test]
        public void AddProject_ShouldAddProjectToList()
        {
            var project = new Project_View
            {
                Name = "Library Renovation",
                ClubName = "Book Club",
                TargetAmount = 5000,
                CurrentAmount = 2000,
                NumberOfDonations = 10,
                EndDate = DateTime.Now.AddDays(30)
            };

            this.projectManager.AddProject(project);

            var allProjects = this.projectManager.GetAllProjects();
            Assert.AreEqual(1, allProjects.Count);
            Assert.AreEqual("Library Renovation", allProjects[0].Name);
        }

        /// <summary>
        /// Ensures that all added projects are retrieved correctly.
        /// </summary>
        [Test]
        public void GetAllProjects_ShouldReturnAllAddedProjects()
        {
            var project1 = new Project_View
            {
                Name = "Library Renovation",
                ClubName = "Book Club",
                TargetAmount = 5000,
                CurrentAmount = 2000,
                NumberOfDonations = 10,
                EndDate = DateTime.Now.AddDays(30)
            };

            var project2 = new Project_View
            {
                Name = "Sports Equipment",
                ClubName = "Sports Club",
                TargetAmount = 10000,
                CurrentAmount = 7000,
                NumberOfDonations = 20,
                EndDate = DateTime.Now.AddDays(15)
            };

            this.projectManager.AddProject(project1);
            this.projectManager.AddProject(project2);

            var allProjects = this.projectManager.GetAllProjects();

            Assert.AreEqual(2, allProjects.Count);
            Assert.AreEqual("Library Renovation", allProjects[0].Name);
            Assert.AreEqual("Sports Equipment", allProjects[1].Name);
        }

        /// <summary>
        /// Validates the calculation of the percentage of funds raised for a project.
        /// </summary>
        [Test]
        public void PercentageFunded_ShouldCalculateCorrectly()
        {
            var project = new Project_View
            {
                Name = "Community Garden",
                ClubName = "Environment Club",
                TargetAmount = 10000,
                CurrentAmount = 2500,
                NumberOfDonations = 5,
                EndDate = DateTime.Now.AddDays(20)
            };

            Assert.AreEqual(25.0, project.PercentageFunded);
        }

        /// <summary>
        /// Ensures that the days remaining until the project's end date are calculated accurately.
        /// </summary>
        [Test]
        public void DaysLeft_ShouldCalculateCorrectly()
        {
            var projectEndingIn10Days = new Project_View
            {
                Name = "Art Supplies",
                ClubName = "Art Club",
                TargetAmount = 3000,
                CurrentAmount = 1500,
                NumberOfDonations = 8,
                EndDate = DateTime.Now.AddDays(10)
            };

            var projectEndingToday = new Project_View
            {
                Name = "Music Instruments",
                ClubName = "Music Club",
                TargetAmount = 5000,
                CurrentAmount = 2500,
                NumberOfDonations = 12,
                EndDate = DateTime.Now.Date
            };

            Assert.AreEqual(10, projectEndingIn10Days.DaysLeft);
            Assert.AreEqual(0, projectEndingToday.DaysLeft);
        }
    }
}