using MongoDB.Driver;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymManager
{
    /// <summary>
    /// Logika interakcji dla klasy manageCustomer.xaml
    /// </summary>
    public partial class manageCustomer : Page
    {
        //customer that is being edited
        private Customer customer;
        public manageCustomer(Customer c)
        {
            InitializeComponent();
            //fill all fields with customer data
            Name.Text = c.Name;
            Surname.Text = c.Surname;
            CardNumber.Text = c.CardNumber;
            Gender.Text = c.Gender;
            Phone.Text = c.Phone;
            Street.Text = c.Street;
            StreetNumber.Text = c.StreetNumber;
            ZipCode.Text = c.ZipCode;
            City.Text = c.City;
            Birthday.SelectedDate = c.Birthday;
            JoinDate.SelectedDate = c.JoinDate;
            customer = c;
        }

        private void updateCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                //required name and surname
                if (Name.Text == "")
                    throw new Exception("Imię nie może być puste!");
                if (Surname.Text == "")
                    throw new Exception("Nazwisko nie może być puste!");

                Customer c = new Customer
                {
                    Id = customer.Id,
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

                c.update();

                //show customers list
                MainWindow.MainFrame.Content = new Customers();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    
        }

        private void deleteCustomer(object sender, RoutedEventArgs e)
        {
            //message box for safety reasons
            MessageBoxResult areYouSure = MessageBox.Show("Czy na pewno chcesz usunąć tego klienta?", "Uwaga!", MessageBoxButton.YesNo);

            if(areYouSure == MessageBoxResult.Yes)
            {
                customer.delete();
                MainWindow.MainFrame.Content = new Customers();
            }
                 
        }

        //handling these three buttons (just showing appropriate pages)
        private void buyPackage(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new BuyPackage(customer);
        }

        private void packageHistory(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new packageHistory(customer);
        }

        private void entrance(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new addEntrance(customer);
        }

        private void family(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new showFamily(customer);
        }
    }
}
