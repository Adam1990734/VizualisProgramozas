using System.Windows;
using Beadandó_Adatbázis_Feladat.Models;
using Beadandó_Adatbázis_Feladat.DbContext;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //========================= Ablak API Elemek =========================
        //Egyszerű egyedek:
        public List<Agent>? AgentsAPI { get; set; }
        public List<Property>? PropertiesAPI { get; set; }
        public List<PropertyType>? PropertyTypesAPI { get; set; }
        //Összetet egyedek:
        public List<MAgentPropertyType>? AllInOneAPI { get; set; }
        public List<MAgentProperty>? AgentAndPropertyAPI { get; set; }
        public List<MTypeProperty>? TypeAndPropertyAPI { get; set; }
        //====================================================================
        //Inicializálás:
        public MainWindow()
        {
            InitializeComponent();
            //Csak teszt hozzáadás a DataGrid-hez:
            using var db = new DataBase();
            AllInOneAPI = db.getAllData.Take(10).ToList();

            this.DataContext = this;
        }
    }
}