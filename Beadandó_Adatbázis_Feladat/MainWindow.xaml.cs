using System.Windows;
using Beadandó_Adatbázis_Feladat.Models;
using Beadandó_Adatbázis_Feladat.DbContext;

namespace Beadandó_Adatbázis_Feladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //========================= Ablak API Elemek =========================
        //Lekéredéz container (minden objectet elbír):
        public List<PropertyDbBase>? PropertyObjects { get; set; }
        //====================================================================
        //Inicializálás:
        public MainWindow()
        {
            InitializeComponent();
            //Csak teszt hozzáadás a DataGrid-hez:
            using var db = new DataBase();
            PropertyObjects = db.getAllData.Take(10).ToList();

            this.DataContext = this;
        }
    }
}