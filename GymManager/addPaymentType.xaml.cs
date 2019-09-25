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
    /// Logika interakcji dla klasy addPaymentType.xaml
    /// </summary>
    public partial class addPaymentType : Page
    {
        public addPaymentType()
        {
            InitializeComponent();
        }

        private void newPaymentType(object sender, RoutedEventArgs e)
        {
            try
            {
                //new payment type to be added to collection
                PaymentType c = new PaymentType
                {
                    Name = Name.Text
                };

                c.add();

                //show list of payment types
                MainWindow.MainFrame.Content = new showPaymentTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
