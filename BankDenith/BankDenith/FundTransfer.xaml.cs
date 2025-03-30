using System;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;

namespace BankDenith
{

    public partial class FundTransfer : Window
    {
        public FundTransfer()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtAccNo.Clear();
            txtBankName.Clear();
            txtBranch.Clear();
            txtSenderAcc.Clear();
            txtSenderName.Clear();
            txtAmount.Clear();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string receiverName = txtName.Text;
            string receiverAccNo = txtAccNo.Text;
            string receiverBankName = txtBankName.Text;
            string branch = txtBranch.Text;
            string senderName = txtSenderName.Text;
            string senderAccNo = txtSenderAcc.Text;
            string amount = txtAmount.Text;

            string connectionString = "server=127.0.0.1;port=3306;database=bank;uid=root;pwd=;";

            try
            {
                if (string.IsNullOrWhiteSpace(receiverName) || string.IsNullOrWhiteSpace(receiverAccNo) ||
                    string.IsNullOrWhiteSpace(receiverBankName) || string.IsNullOrWhiteSpace(branch) ||
                    string.IsNullOrWhiteSpace(senderName) || string.IsNullOrWhiteSpace(senderAccNo) ||
                    string.IsNullOrWhiteSpace(amount))
                {
                    MessageBox.Show("Please fill in all fields before transferring.", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!receiverName.All(char.IsLetter))
                {
                    MessageBox.Show("Receiver name must contain only letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!int.TryParse(receiverAccNo, out int rAccNo))
                {
                    MessageBox.Show("Reciever Account Number must contain only numbers", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (receiverAccNo.Length <= 8 || receiverAccNo.Length >= 12)
                {
                    MessageBox.Show("Invalid reciever account number", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!receiverBankName.All(char.IsLetter))
                {
                    MessageBox.Show("Receiver bank name must contain only letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!branch.All(char.IsLetter))
                {
                    MessageBox.Show("Branch must contain only letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!senderName.All(char.IsLetter))
                {
                    MessageBox.Show("Sender name must contain only letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!int.TryParse(senderAccNo, out int sAccNo))
                {
                    MessageBox.Show("Sender Account Number must contain only numbers", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (senderAccNo.Length <= 8 || senderAccNo.Length >= 12)
                {
                    MessageBox.Show("Invalid sender account number", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!int.TryParse(amount, out int am))
                {
                    MessageBox.Show("Invalid amount,please recheck", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (am <= 0)
                {
                    MessageBox.Show("Invalid amount,please recheck", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MessageBoxResult confirmationResult = MessageBox.Show("Are you sure you to make this transfer?", "Confirm Transfer", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmationResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Transfer cancelled", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }


                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    DateTime transactionDateTime = DateTime.Now;
                    string query = "INSERT INTO transfer (Sender_Account_No, Sender_Name, Reciever_Name, Receiver_Account_No, Receiver_Bank_Name, Branch, Transfer_Amount,transactionDateTime) " +
                                   "VALUES (@SenderAccNo, @SenderName, @ReceiverName, @ReceiverAccNo, @ReceiverBankName, @Branch, @Amount, @transactionDateTime)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SenderAccNo", senderAccNo);
                        cmd.Parameters.AddWithValue("@SenderName", senderName);
                        cmd.Parameters.AddWithValue("@ReceiverName", receiverName);
                        cmd.Parameters.AddWithValue("@ReceiverAccNo", receiverAccNo);
                        cmd.Parameters.AddWithValue("@ReceiverBankName", receiverBankName);
                        cmd.Parameters.AddWithValue("@Branch", branch);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@transactionDateTime", transactionDateTime);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBoxResult result = MessageBox.Show("Transfer SuccessFul"+Environment.NewLine+ "Do you want to make another payment?", "Payment successful", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (result == MessageBoxResult.Yes)
                    {
                        FundTransfer fundtransfer = new FundTransfer();
                        fundtransfer.Show();
                        this.Close();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Menu menu = new Menu();
                        menu.Show();
                        this.Close();
                    }
                    btnClear_Click(sender, e);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
