using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy showPaymentTypes.xaml
    /// </summary>
    public partial class showPaymentTypes : Page
    {
        public showPaymentTypes()
        {
            InitializeComponent();
            //display
            paymentTypesList.ItemsSource = PaymentType.getPaymentTypes();
        }

        private void newPaymentType(object sender, RoutedEventArgs e)
        {
            //show buy package page
            MainWindow.MainFrame.Content = new addPaymentType();
        }

        //method for automatic searching 
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(paymentTypesList.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    PaymentType p = o as PaymentType;
                    return (p.Name == filter);
                };
            }
        }

        //method for automatic searching in customers list - different way
        private void txtNameTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(paymentTypesList.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    PaymentType p = o as PaymentType;
                    return (p.Name == filter);
                };
            }
        }
    }
}

