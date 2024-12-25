using System;
using System.Collections.Generic;
using System.Linq;
using Project_Engine;

/// <summary>
/// Represents a scholarship with its properties and associated data.
/// </summary>
public class Scholarship
{
    private decimal totalAmount;
    private decimal amountPerStudentFunded;

    /// <summary>
    /// Gets or sets the name of the scholarship.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the scholarship.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the scholarship has been awarded.
    /// </summary>
    public bool IsAwarded { get; set; }

    /// <summary>
    /// Gets or sets a list of students who have been awarded the scholarship.
    /// </summary>
    public List<string> AwardedStudents { get; set; } = new List<string>();

    /// <summary>
    /// Gets the current status of the scholarship ("Awarded" or "Not Awarded").
    /// </summary>
    public string Status => this.IsAwarded ? "Awarded" : "Not Awarded";

    /// <summary>
    /// Gets a formatted string of awarded students, or "None" if no students are awarded.
    /// </summary>
    public string AwardedStudentsDisplay => this.AwardedStudents.Any() ? string.Join(", ", this.AwardedStudents) : "None";

    /// <summary>
    /// Gets or sets a list of donations made to the scholarship.
    /// </summary>
    public List<Donation> Donations { get; set; } = new List<Donation>();

    /// <summary>
    /// Gets or sets the total amount of funding available for the scholarship.
    /// </summary>
    public decimal TotalAmount
    {
        get => this.totalAmount;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Total amount must be positive.");
            }

            this.totalAmount = value;
        }
    }

    /// <summary>
    /// Gets or sets the amount of funding awarded to each student.
    /// </summary>
    public decimal AmountPerStudentFunded
    {
        get => this.amountPerStudentFunded;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Amount per student must be positive.");
            }

            this.amountPerStudentFunded = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of students who will be awarded the scholarship.
    /// </summary>
    public int NumberOfStudentsFunded { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a specific major is required for eligibility.
    /// </summary>
    public bool SpecificMajorRequired { get; set; }

    /// <summary>
    /// Gets or sets the minimum GPA required for eligibility.
    /// </summary>
    public decimal GPAMinimumRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether membership in a community group is required for eligibility.
    /// </summary>
    public bool CommunityGroupRequired { get; set; }

    /// <summary>
    /// Gets or sets additional criteria for eligibility, if any.
    /// </summary>
    public string AdditionalCriteria { get; set; }

    /// <summary>
    /// Gets or sets the name of the donor who established the scholarship.
    /// </summary>
    public string DonorName { get; set; }

    /// <summary>
    /// Gets or sets the date when the scholarship was established.
    /// </summary>
    public DateTime SetupDate { get; set; }

    /// <summary>
    /// Gets or sets the username of the donor who established the scholarship.
    /// </summary>
    public string DonorUsername { get; set; }

    /// <summary>
    /// Gets or sets a list of applicants who have applied for the scholarship.
    /// </summary>
    public List<Applicant> Applicants { get; set; } = new List<Applicant>();

    /// <summary>
    /// Gets or sets the deadline for applications.
    /// </summary>
    public DateTime ScholarshipDeadline { get; set; }

    /// <summary>
    /// Gets or sets the source of funding for the scholarship (e.g., endowment, annual donation).
    /// </summary>
    public string FundingSource { get; set; }

    /// <summary>
    /// Gets or sets the type of scholarship (e.g., academic, athletic, need-based).
    /// </summary>
    public string ScholarshipType { get; set; }
}