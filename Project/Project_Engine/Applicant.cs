namespace Project_Engine
{
    /// <summary>
    /// Represents an applicant for a scholarship or grant.
    /// </summary>
    public class Applicant
    {
        /// <summary>
        /// The applicant's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The applicant's Grade Point Average (GPA).
        /// </summary>
        public decimal GPA { get; set; }

        /// <summary>
        /// Indicates whether the applicant meets the specific criteria for the scholarship or grant.
        /// </summary>
        public bool MeetsCriteria { get; set; }

        /// <summary>
        /// Indicates whether the applicant has been awarded the scholarship or grant.
        /// </summary>
        public bool IsAwarded { get; set; }
    }
}