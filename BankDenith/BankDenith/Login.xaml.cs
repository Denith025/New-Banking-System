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
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            // this.WindowState = WindowState.Minimized;
        }

        private void BtnRegister_Click(object sender, object e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void BtnLogin_Click(object sender, object e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            string connectionString = "server=127.0.0.1;port=3307;database=bank;uid=root;pwd=;";

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM signup WHERE Username = @Username AND Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {

                            Menu dashboard = new Menu();
                            dashboard.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void BtnClear_Click(object sender, object e)
        {
            Application.Current.Shutdown();
        }
        private void BtnLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnLogin.Background = Brushes.White;
        }
        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
    }

