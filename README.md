# Project: Scholarship and Donation Management System

## Overview

This project implements a comprehensive Scholarship and Donation Management System designed to facilitate the creation, management, and awarding of scholarships, as well as handling donations for various projects. The system caters to different user types, including donors, students, and administrators.

## Key Features

- **User Account Management**: Supports registration and authentication for donor and student accounts.
- **Project Management**: Allows creation and tracking of fundraising projects.
- **Scholarship Management**: Enables setup and management of scholarships with customizable criteria.
- **Donation Handling**: Facilitates donation processes for both projects and scholarships.
- **Application Review**: Provides functionality for reviewing scholarship applications.

## Core Components

### User Management
- `AccountManager`: Handles user registration and authentication.
- `UserAccount`: Base class for user accounts.
- `DonorAccount`: Specialized account for donors with donation preferences.
- `StudentAccount`: Specialized account for students with academic information.

### Project Management
- `ProjectManager`: Manages the collection of fundraising projects.
- `Project_View`: Represents individual projects with funding details.

### Scholarship Management
- `Scholarship`: Represents individual scholarships with criteria and applicant information.
- `Applicant`: Represents scholarship applicants.

### Donation Handling
- `Donation`: Represents individual donations with amount and donor information.

## User Interface

The system includes several forms for user interaction:

- `Form1`: Main form for user login and navigation.
- `RegistrationForm`: Handles user registration for both donors and students.
- `ProjectListForm`: Displays a list of available projects.
- `ScholarshipListForm`: Shows available scholarships.
- `DonationForm`: Facilitates the donation process.
- `ApplicationReviewForm`: Allows review of scholarship applications.

## Getting Started

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Build the solution to restore NuGet packages.
4. Run the application.

## Dependencies

- .NET Framework
- Windows Forms for the user interface

## Contributing

Contributions to improve the system are welcome. Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature.
3. Commit your changes.
4. Push to the branch.
5. Create a new Pull Request.

## License

This project is licensed under the MIT License.

