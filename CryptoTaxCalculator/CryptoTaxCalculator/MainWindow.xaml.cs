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
        private readonly SQLiteConnection _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            InitializeLastDb();
            Application.Current.Exit += (o, args) => OnCloseEvent();
            if (!(DbFilePath == null))
                _dbContext = new SQLiteConnection(string.Format("Data Source={0};Version=3", DbFilePath));
            _dbContext.Open();
            System.Diagnostics.Debug.WriteLine(new StatementBuilder().CreateTable("testTable").AddTableField("testfield", "INT").AddTableField("testfield2", "VARCHAR(20)").Query);
        }
        #region UI Event Handlers
        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            var newDb = new SaveFileDialog();
            newDb.Filter = "SQLite 3 DB (*.SQLITE3)|*.SQLITE3";
            if (newDb.ShowDialog() == true)
            {
                SQLiteConnection.CreateFile(newDb.FileName);
                if (!File.Exists(newDb.FileName))
                    throw new Exception("File not found after creation!");
                DbFilePath = newDb.FileName;
            }
        }
        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            var openDialogResult = new OpenFileDialog();
            openDialogResult.Filter = "SQLite 3 DB (*.SQLITE3)|*.SQLITE3";
            if (openDialogResult.ShowDialog() == true)
            {
                DbFilePath = openDialogResult.FileName;
            }
        }
        private void MenuItem_GenerateReport(object sender, RoutedEventArgs e) { }
        private void MenuItem_Exit(object sender, RoutedEventArgs e) { }
        private void MenuItem_Close(object sender, RoutedEventArgs e) { }

        #endregion
        #region Support Methods
        /// <summary>
        /// Save last opened database location
        /// </summary>
        private void OnCloseEvent()
        {
            if (DbFilePath == null)
                return;

            var savePath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("AppData"), @"CryptoTaxCalculator\PreviousDb.xml");
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(savePath)))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(savePath));

            var xmlSerializer = new XmlSerializer(typeof(string));
            using (var fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write))
                xmlSerializer.Serialize(fs, DbFilePath);
            _dbContext.Close();
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
        #endregion
    }
}
