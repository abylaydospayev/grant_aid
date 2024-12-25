using System.Collections.Generic;

namespace Project_Engine
{
    /// <summary>
    /// Manages a collection of projects and provides methods for adding, retrieving, and potentially searching projects.
    /// </summary>
    public class ProjectManager
    {
        private readonly List<Project_View> projects = new List<Project_View>();

        /// <summary>
        /// Adds a new project to the collection.
        /// </summary>
        /// <param name="project">The project to add. The project should be a valid instance of the Project_View class.</param>
        public void AddProject(Project_View project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            projects.Add(project);
        }

        /// <summary>
        /// Gets all the projects in the collection.
        /// </summary>
        /// <returns>A read-only list of all projects.</returns>
        public IReadOnlyList<Project_View> GetAllProjects()
        {
            return projects;
        }

        /// <summary>
        /// Gets a project from the collection by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project to retrieve.</param>
        /// <returns>The project with the specified ID, or null if no project is found.</returns>
        public Project_View GetProjectById(int id)
        {
            return projects.FirstOrDefault(p => p.Id == id);
        }
    }
}
