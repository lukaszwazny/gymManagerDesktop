using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymManager
{
    /// <summary>
    /// Logika interakcji dla klasy AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {
        public AddCustomer()
        {
            InitializeComponent();
            //make default join date - now
            JoinDate.SelectedDate = DateTime.Now;
        }

        private void newCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                //new customer to be added to collection
                Customer c = new Customer
                {
                    Name = Name.Text,
                    Surname = Surname.Text,
                    CardNumber = CardNumber.Text,
                    Gender = Gender.Text,
                    Phone = Phone.Text,
                    Street = Street.Text,
                    StreetNumber = StreetNumber.Text,
                    ZipCode = ZipCode.Text,
                    City = City.Text,
                    Birthday = Birthday.SelectedDate.Value,
                    JoinDate = JoinDate.SelectedDate.Value
                };

                c.add();

                //show list of customers
                MainWindow.MainFrame.Content = new Customers();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
