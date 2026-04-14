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
            Loaded += LoadAgents;
            //Esemény kezelők:
            AllElem.Click += LoadAll;
            //Egy táblás:
            PropElem.Click += LoadProperties;
            PropAgent.Click += LoadAgents;
            PropType.Click += LoadTypes;
            //Specifikus:
            SpecPropAndAgent.Click += LoadPropAndAgents;
            SpecPropAndType.Click += LoadPropAndType;
        }
        private void LoadAll(object sender, EventArgs e)
        {
            using var db = new DataBase();
            PropertyObjects = db.AllData;
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadAgents(object sender, EventArgs e)
        {
            using var db = new DataBase();
            PropertyObjects = db.Agents.Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadProperties(object sender, EventArgs e)
        {
            using var db = new DataBase();
            PropertyObjects = db.Properties.Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadTypes(object sender, EventArgs e)
        {
            using var db = new DataBase();
            PropertyObjects = db.PropertyTypes.Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadPropAndType(object sender, EventArgs e)
        {
            using var db = new DataBase();
            PropertyObjects = db.JoinProperiesAndTypes().Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadPropAndAgents(object sender, EventArgs e)
        {
            using var db = new DataBase();
            PropertyObjects = db.JoinProperiesAndAgents().Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
    }
}