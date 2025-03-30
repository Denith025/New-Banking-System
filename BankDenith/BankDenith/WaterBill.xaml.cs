﻿using MySql.Data.MySqlClient;
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
   
    public partial class WaterBill : Window
    {
        public WaterBill()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BillPayment billPayment = new BillPayment();
            billPayment.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string waterbillNo = txtWaterBill.Text;
            string amount = txtAmount.Text;

            string connectionString = "server=127.0.0.1;port=3307;database=bank;uid=root;pwd=;";

            try
            {
                if (string.IsNullOrWhiteSpace(waterbillNo) || string.IsNullOrWhiteSpace(amount))
                {
                    MessageBox.Show("Please fill all the required fields", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!int.TryParse(waterbillNo, out int bNo))
                {
                    MessageBox.Show("Invalid WaterBill Account number or amount. Please enter a numeric value", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (waterbillNo.Length <6||waterbillNo.Length>12)
                {
                    MessageBox.Show("Invalid WaterBill Account number", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!decimal.TryParse(amount, out decimal am))
                {
                    MessageBox.Show("Invalid WaterBill Account number or amount. Please enter a numeric value.", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (am <= 0)
                {
                    MessageBox.Show("Invalid amount", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    DateTime transactionDateTime = DateTime.Now;
                    string query = "INSERT INTO waterbill (WaterBill_No, Amount,transactionDateTime) VALUES (@WaterBillNo, @Amount,@transactionDateTime)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@WaterBillNo", waterbillNo);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@TransactionDateTime", transactionDateTime);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBoxResult result = MessageBox.Show("Do you want to make another payment?", "Payment successful", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (result == MessageBoxResult.Yes)
                    {
                        BillPayment billPayment = new BillPayment();
                        billPayment.Show();
                        this.Close();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Menu menu = new Menu();
                        menu.Show();
                        this.Close();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values for Mobile Number and Amount.", "error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
