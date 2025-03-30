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
    public partial class BillPayment : Window
    {
        public BillPayment()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, object e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
        private void MyComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem selectedItem = (ComboBoxItem)MyComboBox1.SelectedItem;
            if (selectedItem != null)
            {
                string selectedContent = selectedItem.Content.ToString();
                if (selectedContent == "Mobile")
                {
                    Mobile mobile = new Mobile();
                    mobile.Show();
                    this.Close();
                }
                else if (selectedContent == "Broadband")
                {
                    Broadband broadband = new Broadband();
                    broadband.Show();
                    this.Close();
                }
                else
                {
                    Television television = new Television();
                    television.Show();
                    this.Close();
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)MyComboBox2.SelectedItem;
            if (selectedItem != null)
            {
                string selectedContent = selectedItem.Content.ToString();
                if (selectedContent == "Water Bill")
                {
                    WaterBill waterbill = new WaterBill();
                    waterbill.Show();
                    this.Close();
                }
                else if (selectedContent == "Electricity Bill")
                {
                    ElectricityBill electricityBill = new ElectricityBill();
                    electricityBill.Show();
                    this.Close();
                }
                else
                {
                    OtherBills otherbills = new OtherBills();
                    otherbills.Show();
                    this.Hide();
                }
            }
        }
    }
}