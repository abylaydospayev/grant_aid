using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project_Engine;

namespace Project
{
    /// <summary>
    /// Represents a registration form for Donor and Student accounts.
    /// </summary>
    public partial class RegistrationForm : Form
    {
        private readonly AccountManager accountManager;

        // UI Controls
        private ComboBox cmbAccountType;
        private TextBox txtName;
        private TextBox txtAddress;
        private TextBox txtEmail;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtWSUID;
        private CheckBox chkAnonymous;
        private CheckBox chkPostAmounts;
        private TextBox txtAffiliation;
        private TextBox txtCreditCard;
        private ListBox lstClubs;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationForm"/> class.
        /// </summary>
        /// <param name="manager">An instance of <see cref="AccountManager"/> for managing account registrations.</param>
        public RegistrationForm(AccountManager manager)
        {
            this.InitializeComponent();
            this.SetupRegistrationControls();
            this.Size = new Size(400, 500);
            this.AutoScroll = true;
            this.accountManager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>
        /// Placeholder method for initializing the form. 
        /// Actual control initialization occurs in <see cref="SetupRegistrationControls"/>.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Sets up the registration form's controls.
        /// </summary>
        private void SetupRegistrationControls()
        {
            // Basic information controls
            Label lblName = CreateLabel("Name:", new Point(20, 20));
            this.txtName = CreateTextBox(new Point(120, 20));

            Label lblAddress = CreateLabel("Address:", new Point(20, 50));
            this.txtAddress = CreateTextBox(new Point(120, 50));

            Label lblEmail = CreateLabel("Email:", new Point(20, 80));
            this.txtEmail = CreateTextBox(new Point(120, 80));

            Label lblUsername = CreateLabel("Username:", new Point(20, 110));
            this.txtUsername = CreateTextBox(new Point(120, 110));

            Label lblPassword = CreateLabel("Password:", new Point(20, 140));
            this.txtPassword = CreateTextBox(new Point(120, 140), true);

            Label lblAccountType = CreateLabel("Account Type:", new Point(20, 170));
            this.cmbAccountType = new ComboBox
            {
                Location = new Point(120, 170),
                Width = 200
            };
            this.cmbAccountType.Items.AddRange(new object[] { "Donor", "Student" });
            this.cmbAccountType.SelectedIndexChanged += this.CmbAccountType_SelectedIndexChanged;

            // Donor-specific controls
            this.txtAffiliation = CreateTextBox(new Point(120, 200), visible: false);
            this.txtCreditCard = CreateTextBox(new Point(120, 230), visible: false);
            this.chkAnonymous = CreateCheckBox("Anonymous Donations", new Point(120, 260), visible: false);
            this.chkPostAmounts = CreateCheckBox("Post Amounts", new Point(120, 290), visible: false);

            // Student-specific controls
            this.txtWSUID = CreateTextBox(new Point(120, 200), visible: false);
            this.lstClubs = new ListBox
            {
                Location = new Point(120, 230),
                Width = 200,
                Height = 100,
                Visible = false
            };

            // Register button
            Button btnRegister = new Button
            {
                Text = "Register",
                Location = new Point(120, 340),
                Width = 100,
                Height = 30
            };
            btnRegister.Click += this.BtnRegister_Click;

            // Add controls to the form
            this.Controls.AddRange(new Control[] { lblName, this.txtName, lblAddress, this.txtAddress, lblEmail, this.txtEmail,
                lblUsername, this.txtUsername, lblPassword, this.txtPassword, lblAccountType, this.cmbAccountType,
                this.txtAffiliation, this.txtCreditCard, this.chkAnonymous, this.chkPostAmounts, this.txtWSUID, this.lstClubs, btnRegister });
        }

        /// <summary>
        /// Updates the visibility of controls based on the selected account type.
        /// </summary>
        private void CmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isDonor = this.cmbAccountType.SelectedItem.ToString() == "Donor";

            // Toggle visibility based on account type
            this.txtAffiliation.Visible = this.txtCreditCard.Visible = this.chkAnonymous.Visible = this.chkPostAmounts.Visible = isDonor;
            this.txtWSUID.Visible = this.lstClubs.Visible = !isDonor;
        }

        /// <summary>
        /// Handles the click event for the Register button.
        /// </summary>
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string accountType = this.cmbAccountType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(accountType))
            {
                MessageBox.Show("Please select an account type.");
                return;
            }

            try
            {
                if (accountType == "Donor")
                {
                    this.accountManager.RegisterDonor(this.txtName.Text, this.txtAddress.Text, this.txtEmail.Text, this.txtUsername.Text, this.txtPassword.Text,
                        this.txtAffiliation.Text, this.txtCreditCard.Text, this.chkAnonymous.Checked, this.chkPostAmounts.Checked);
                }
                else if (accountType == "Student")
                {
                    this.accountManager.RegisterStudent(this.txtName.Text, this.txtAddress.Text, this.txtEmail.Text, this.txtUsername.Text, this.txtPassword.Text,
                        this.txtWSUID.Text, this.lstClubs.SelectedItems.Cast<string>().ToList());
                }

                MessageBox.Show($"Registration successful for {accountType} account: {this.txtUsername.Text}");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during registration: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a label with specified text and location.
        /// </summary>
        private static Label CreateLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Location = location,
                AutoSize = true
            };
        }

        /// <summary>
        /// Creates a textbox with specified location and optional settings.
        /// </summary>
        private static TextBox CreateTextBox(Point location, bool isPassword = false, bool visible = true)
        {
            return new TextBox
            {
                Location = location,
                Width = 200,
                PasswordChar = isPassword ? '*' : '\0',
                Visible = visible
            };
        }

        /// <summary>
        /// Creates a checkbox with specified text, location, and visibility.
        /// </summary>
        private static CheckBox CreateCheckBox(string text, Point location, bool visible = true)
        {
            return new CheckBox
            {
                Text = text,
                Location = location,
                AutoSize = true,
                Visible = visible
            };
        }
    }
}
