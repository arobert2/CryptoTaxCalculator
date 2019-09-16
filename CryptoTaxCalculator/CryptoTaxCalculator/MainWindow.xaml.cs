using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace CryptoTaxCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string DbFilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeLastDb();
            Application.Current.Exit += (o, args) => OnCloseEvent();
        }

        private void MenuItem_New(object sender, MouseButtonEventArgs e)
        {
            var newDb = new SaveFileDialog();
            if(newDb.ShowDialog() == true)
            {
                SQLiteConnection.CreateFile(newDb.FileName);
                if (!File.Exists(newDb.FileName))
                    throw new Exception("File not found after creation!");
                DbFilePath = newDb.FileName;
            }
        }
        /// <summary>
        /// Save last opened database location
        /// </summary>
        private void OnCloseEvent()
        {
            if (DbFilePath == null)
                return;
            var savePath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("AppData"), @"CryptoTaxCalculator\PreviousDb.xml");
            var xmlSerializer = new XmlSerializer(typeof(string));
            using (var fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write))
                xmlSerializer.Serialize(fs, DbFilePath);
        }
        /// <summary>
        /// Read last opened database location
        /// </summary>
        private void InitializeLastDb()
        {
            var openPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("AppData"), @"CryptoTaxCalculator\PreviousDb.xml");
            if (!File.Exists(openPath))
                return;
            var xmlDeserializer = new XmlSerializer(typeof(string));
            using (var fs = new FileStream(openPath, FileMode.Open, FileAccess.Read))
                DbFilePath = xmlDeserializer.Deserialize(fs) as string;          
        }
    }
}
