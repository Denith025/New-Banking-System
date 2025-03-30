using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class AccountDetails : Window
    {
        public AccountDetails()
        {
            InitializeComponent();
        }
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtType_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtBalance_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string enteredUsername = TxtUserName.Text.Trim();

            if (string.IsNullOrEmpty(enteredUsername))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            string connectionString = "server=127.0.0.1;port=3307;database=bank;uid=root;pwd=;";

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT First_Name, AccountNo, Account_Type, Balance FROM signup WHERE Username = @Username";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", enteredUsername);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LblName.Content = reader["First_Name"].ToString();
                                LblNumber.Content = reader["AccountNo"].ToString();
                                LblAccType.Content = reader["Account_Type"].ToString();
                                LblBalance.Content = reader["Balance"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No account details found for this username.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving account details: " + ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
    }

        
    

