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

                //defining what to update and with what data
                UpdateDefinition<Customer> update = Builders<Customer>.Update
                .Set(c => c.Name, Name.Text)
                .Set(c => c.Surname, Surname.Text)
                .Set(c => c.CardNumber, CardNumber.Text)
                .Set(c => c.Gender, Gender.Text)
                .Set(c => c.Phone, Phone.Text)
                .Set(c => c.Street, Street.Text)
                .Set(c => c.StreetNumber, StreetNumber.Text)
                .Set(c => c.ZipCode, ZipCode.Text)
                .Set(c => c.City, City.Text)
                .Set(c => c.Birthday, Birthday.SelectedDate.Value)
                .Set(c => c.JoinDate, JoinDate.SelectedDate.Value);

                //get customers collection
                IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");

                //update customer
                collection.FindOneAndUpdate(c => c.Id == customer.Id, update);

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
            //get customers collection
            IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");

            //message box for safety reasons
            MessageBoxResult areYouSure = MessageBox.Show("Czy na pewno chcesz usunąć tego klienta?", "Uwaga!", MessageBoxButton.YesNo);

            if(areYouSure == MessageBoxResult.Yes)
            {
                //deleting
                collection.FindOneAndDelete(c => c.Id == customer.Id);
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
    }
}
