using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GymManager
{
    /// <summary>
    /// Logika interakcji dla klasy DatabaseLoading.xaml
    /// </summary>
    public partial class DatabaseLoading : Window
    {
        public DatabaseLoading()
        {
            InitializeComponent();

            //reading the data necessary to connect to database from text file
            textBlock.Text = "Odczytywanie danych konfiguracyjnych..........";
            string connectionString = null;
            string databaseName = null;
            //here to enter name of file with config data
            string textFileName = "config.txt";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(textFileName))
                {
                    // Read the stream to a string..
                    connectionString = sr.ReadLine();
                    databaseName = sr.ReadLine();
                }
                try
                {
                    //client
                    textBlock.Text = "Tworzenie klienta..........";
                    MongoDatabaseSingleton.Instance.client = new MongoClient(connectionString);
                    textBlock.Text = "Pomyślnie stworzono klienta........\n";
                    textBlock.Text += "Klient: " + MongoDatabaseSingleton.Instance.client.ToString();
                    //database
                    textBlock.Text = "Łączenie z bazą danych..........";
                    MongoDatabaseSingleton.Instance.database = MongoDatabaseSingleton.Instance.client.GetDatabase(databaseName);
                    textBlock.Text = "Pomyślnie połączono z bazą danych........\n";
                    textBlock.Text += "Baza danych: " + databaseName;
                    //opening new window
                    try
                    {
                        Window win = new MainWindow();
                        win.Show();
                        this.Close();
                        
                    }
                    catch (Exception ex)
                    {
                        textBlock.Text = ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    textBlock.Text = "Nie udało się stworzyć klienta.......\n";
                    textBlock.Text += ex.Message;
                }
            }
            catch (IOException e)
            {
                textBlock.Text = "Nie odczytano pliku" + textFileName + ":\n";
                textBlock.Text += e.Message;
            }
            
        }
    }
}
