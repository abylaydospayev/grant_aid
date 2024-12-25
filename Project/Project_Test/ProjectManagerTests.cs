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
    public class ProjectManagerTests
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
        /// Verifies that GetAllProjects returns an empty list when no projects are added.
        /// </summary>
        [Test]
        public void GetAllProjects_ShouldReturnEmptyList_WhenNoProjectsAdded()
        {
            var allProjects = this.projectManager.GetAllProjects();

            Assert.IsEmpty(allProjects);
        }
    }
}
