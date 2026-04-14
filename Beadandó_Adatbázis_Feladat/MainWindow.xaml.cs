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
        //Lekéredéz container (minden objectet elbír):
        public List<PropertyDbBase>? PropertyObjects { get; set; }
        //====================================================================
        //Inicializálás:
        public MainWindow()
        {
            InitializeComponent();
            Loaded += LoadAll;
            //=========================== Esemény kezelők ===========================
            AllElem.Click += LoadAll;
            //Egy táblás:
            PropElem.Click += LoadProperties;
            PropAgent.Click += LoadAgents;
            PropType.Click += LoadTypes;
            //Specifikus:
            SpecPropAndAgent.Click += LoadPropAndAgents;
            SpecPropAndType.Click += LoadPropAndType;
            //=======================================================================
        }
        private void LoadAll(object sender, EventArgs e)
        {
            using var db = new DataBase();
            if (!db.Database.CanConnect())
            {
                NoDatabaseConnect();
                return;
            }
            PropertyObjects = db.AllData;
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadAgents(object sender, EventArgs e)
        {
            using var db = new DataBase();
            if (!db.Database.CanConnect())
            {
                NoDatabaseConnect();
                return;
            }
            PropertyObjects = db.Agents.Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadProperties(object sender, EventArgs e)
        {
            using var db = new DataBase();
            if (!db.Database.CanConnect())
            {
                NoDatabaseConnect();
                return;
            }
            PropertyObjects = db.Properties.Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadTypes(object sender, EventArgs e)
        {
            using var db = new DataBase();
            if (!db.Database.CanConnect())
            {
                NoDatabaseConnect();
                return;
            }
            PropertyObjects = db.PropertyTypes.Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadPropAndType(object sender, EventArgs e)
        {
            using var db = new DataBase();
            if (!db.Database.CanConnect())
            {
                NoDatabaseConnect();
                return;
            }
            PropertyObjects = db.JoinProperiesAndTypes().Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void LoadPropAndAgents(object sender, EventArgs e)
        {
            using var db = new DataBase();
            if (!db.Database.CanConnect())
            {
                NoDatabaseConnect();
                return;
            }
            PropertyObjects = db.JoinProperiesAndAgents().Cast<PropertyDbBase>().ToList();
            Loader.LoadData(PropertyObjects);
            this.DataContext = this;
        }
        private void NoDatabaseConnect()
        {
            MainPanel.Children.Remove(MainPanel.Children[1]);
            Label label = new Label();
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 22;
            label.Content = new TextBlock()
            {
                Text = "AZ ADATBÁZIS NEM ELÉRHETŐ VAGY HIBÁSAN VAN MEGADVA!",
                TextWrapping = TextWrapping.Wrap
            };
            MainPanel.Children.Add(label);
        }
    }
}