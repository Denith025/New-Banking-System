using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankDenith
{
    
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string First_Name = TxtFirstName.Text;
            string Last_Name = TxtLastName.Text;
            string Address = TxtAddress.Text;
            string TelePhone_No = TxtTelephone.Text;
            string Gender = RbtnMale.IsChecked == true ? "Male" : (RbtnFemale.IsChecked == true ? "Female" : "");

            if (string.IsNullOrEmpty(Gender))
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            string NIC = TxtNIC.Text;
            string AccountNo = TxtAccoNo.Text; 
            string acctype = ((ComboBoxItem)cmbAccType.SelectedItem)?.Content.ToString();

            if (string.IsNullOrEmpty(acctype))
            {
                MessageBox.Show("Please select an account type.");
                return;
            }

            string Branch = ((ComboBoxItem)TxtBranch.SelectedItem)?.Content.ToString(); 

            if (string.IsNullOrEmpty(Branch))
            {
                MessageBox.Show("Please select a branch.");
                return;
            }

            string Username = TxtUsername.Text;
            string Password = TxtPassword.Text;

            string connectionString = "server=127.0.0.1;port=3307;database=bank;uid=root;pwd=;";

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open(); 

                    string query = "INSERT INTO signup (First_Name, Last_Name, Address, Telephone_No, Gender, NIC, AccountNo, Account_Type, Branch, Username, Password) " +
                                   "VALUES (@First_Name, @Last_Name, @Address, @Telephone_No, @Gender, @NIC, @Account_No, @Account_Type, @Branch, @Username, @Password)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@First_Name", First_Name);
                        cmd.Parameters.AddWithValue("@Last_Name", Last_Name);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Telephone_No", TelePhone_No);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@NIC", NIC);
                        cmd.Parameters.AddWithValue("@Account_No", AccountNo); 
                        cmd.Parameters.AddWithValue("@Account_Type", acctype);
                        cmd.Parameters.AddWithValue("@Branch", Branch);
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registration Successful");

                    // Clear form fields
                    TxtFirstName.Clear();
                    TxtLastName.Clear();
                    TxtAddress.Clear();
                    TxtTelephone.Clear();
                    RbtnMale.IsChecked = false;
                    RbtnFemale.IsChecked = false;
                    TxtNIC.Clear();
                    TxtAccoNo.Clear();
                    cmbAccType.SelectedIndex = -1;
                    TxtBranch.SelectedIndex = -1; 
                    TxtUsername.Clear();
                    TxtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); 
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Hide();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtAccoNo.Clear();
            TxtUsername.Clear();
            TxtPassword.Clear();
            TxtTelephone.Clear();
            TxtAddress.Clear();
            TxtBranch.SelectedItem = null; 
            TxtFirstName.Clear();
            TxtLastName.Clear();
            TxtNIC.Clear() ;
            RbtnMale.IsChecked = false;
            RbtnFemale.IsChecked = false;
            cmbAccType.SelectedItem = null;

        }
    }
}
