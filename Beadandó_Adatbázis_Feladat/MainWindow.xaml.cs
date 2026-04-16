using System.Windows;
using Beadandó_Adatbázis_Feladat.Models;
using Beadandó_Adatbázis_Feladat.DbContext;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class MainWindow : Window
    {
        //========================= Ablak API Elemek =========================
        //Lekéredéz container (minden objectet elbír):
        internal enum ModeOfLoad { ALL, AGENT, PROP, TYPE, AandP, PTYandP }
        ModeOfLoad LastLoadMode;
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
            //Törlés:
            DeleteBtn.Click += DeleteSeleceted;

            //=======================================================================

            Info.Click += (object sender, RoutedEventArgs e) => MessageBox.Show("Készítették: Pálmai Ádám (YB5SIV) és Kiss Ádám (P8D48P)");
        }
        //==================================== Betöltő Események ====================================
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
            LastLoadMode = ModeOfLoad.ALL;
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
            LastLoadMode = ModeOfLoad.AGENT;
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
            LastLoadMode = ModeOfLoad.PROP;
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
            LastLoadMode = ModeOfLoad.TYPE;
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
            LastLoadMode = ModeOfLoad.PTYandP;
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
            LastLoadMode = ModeOfLoad.AandP;
            this.DataContext = this;
        }
        private void NoDatabaseConnect()
        {
            MainPanel.Children.Remove(MainPanel.Children[1]);
            Label label = new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 22
            };
            label.Content = new TextBlock()
            {
                Text = "AZ ADATBÁZIS NEM ELÉRHETŐ VAGY HIBÁSAN VAN MEGADVA!",
                TextWrapping = TextWrapping.Wrap,
            };
            MainPanel.Children.Add(label);
        }
        //Utolsó hívás meghívása úrja töltés céljából
        private void LoadLastChoice()
        {
            switch(LastLoadMode)
            {
                case ModeOfLoad.ALL:
                    LoadAll(this, EventArgs.Empty);
                    break;
                case ModeOfLoad.PTYandP:
                    LoadPropAndType(this, EventArgs.Empty);
                    break;
                case ModeOfLoad.AandP:
                    LoadPropAndAgents(this, EventArgs.Empty);
                    break;
                case ModeOfLoad.PROP:
                    LoadProperties(this, EventArgs.Empty);
                    break;
                case ModeOfLoad.AGENT:
                    LoadAgents(this, EventArgs.Empty);
                    break;
                case ModeOfLoad.TYPE:
                    LoadTypes(this, EventArgs.Empty);
                    break;
            }
        }
        //==================================== Törlés esemény ====================================
        private void DeleteSeleceted(object sender, EventArgs e)
        {
            using var db = new DataBase();
            try
            {
                if(!db.Database.CanConnect())
                {
                    NoDatabaseConnect();
                    return;
                }
                db.DeleteObjects(Loader.GetSelectedObjects());
                LoadLastChoice();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //==================================== Hozzáadás esemény ====================================
        private void CreateNew()
        {
            //Ide kell kód
        }
        //==================================== Frissítés esemény ====================================
        private void UpdateOld()
        {
            //Ide kell kód
        }
    }
}