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
                //name and surname required
                if (Name.Text == "")
                    throw new Exception("Imię nie może być puste!");
                if (Surname.Text == "")
                    throw new Exception("Nazwisko nie może być puste!");

                //only letters allowed in name and surname
                if(!(Regex.IsMatch(Name.Text, @"^[a-zA-Z]+$")))
                    throw new Exception("Tylko litery!");
                if (!(Regex.IsMatch(Surname.Text, @"^[a-zA-Z]+$")))
                    throw new Exception("Tylko litery!");

                //get customers collection
                IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");

                //get Customer with maximal Id
                Customer maxId = collection.Find(_ => true).Sort("{_id: -1}").ToList().ElementAt(0);

                //new customer to be add to collection
                Customer c = new Customer
                {
                    Id = ++(maxId.Id),
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

                //check if name and surname are string types
                if(c.Name.GetType() != typeof(string))
                    throw new Exception("Błędny typ wejścia!");
                if (c.Surname.GetType() != typeof(string))
                    throw new Exception("Błędny typ wejścia!");

                //add customer to collection
                collection.InsertOne(c);

                //show list of customers
                MainWindow.MainFrame.Content = new Customers();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
