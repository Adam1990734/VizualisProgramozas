using System.Windows;
using Beadandó_Adatbázis_Feladat.Models;
using Beadandó_Adatbázis_Feladat.DbContext;
using System.Windows.Controls;
using System.Net.NetworkInformation;
using Microsoft.IdentityModel.Tokens;

namespace Beadandó_Adatbázis_Feladat
{
    public enum ModeOfLoad { ALL, AGENT, PROP, TYPE, AandP, PTYandP }
    public partial class MainWindow : Window
    {
        //========================= Ablak API Elemek =========================
        //Lekéredéz container (minden objectet elbír):
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
            DeleteBtn.Click += OnDelete;
            //Felvétel:
            NewBtn.Click += OnCreate;
            //Frissítés:
            UpdateBtn.Click += OnUpdate;
            //Kersés:
            Searcher.SearchBtn.Click += OnOuterSearch;

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
            Loader.LoadData(PropertyObjects, false);
            LastLoadMode = ModeOfLoad.ALL;
            Searcher.SetContext(Loader.Columns, PropertyObjects);
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
            Searcher.SetContext(Loader.Columns, PropertyObjects);
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
            Searcher.SetContext(Loader.Columns, PropertyObjects);
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
            Searcher.SetContext(Loader.Columns, PropertyObjects);
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
            Loader.LoadData(PropertyObjects, false);
            LastLoadMode = ModeOfLoad.PTYandP;
            Searcher.SetContext(Loader.Columns, PropertyObjects);
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
            Loader.LoadData(PropertyObjects, false);
            LastLoadMode = ModeOfLoad.AandP;
            Searcher.SetContext(Loader.Columns, PropertyObjects);
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
        private void ShowResponse(string Message)
        {
            this.ResponseForUser.Content = Message;
            this.ResponseForUser.Visibility = Visibility.Visible;
        }
        private void HideResponse() => this.ResponseForUser.Visibility = Visibility.Collapsed;
        //==================================== Törlés esemény ====================================
        private void OnDelete(object sender, EventArgs e)
        {
            var SelectedList = Loader.GetSelectedObjects();
            if(SelectedList == null || SelectedList.Count < 1)
            {
                MessageBox.Show("Válasszon ki minimum egy sort!");
                return;
            }
            using var db = new DataBase();
            try
            {
                if(!db.Database.CanConnect())
                {
                    NoDatabaseConnect();
                    return;
                }
                db.DeleteObjects(SelectedList);
                LoadLastChoice();
                ShowResponse("Sikeres adat törlés!");
            } catch(Exception ex)
            {
                ShowResponse("Sikeretelen törlés!");
            }
        }
        //==================================== Hozzáadás esemény ====================================
        private void OnCreate(object sender, EventArgs e)
        {
            var NewElementWindow = new AddNew();
            if (NewElementWindow.ShowDialog() == true)
                ShowResponse("Sikeres adat rögzítés!");
            else
                ShowResponse("Sikeretelen adat felvétel!");
        }
        //==================================== Frissítés esemény ====================================
        private void OnUpdate(object sender, EventArgs e)
        {
            var SelectedList = Loader.GetSelectedObjects();
            if (SelectedList == null || SelectedList.Count < 1)
            {
                MessageBox.Show("Válasszon ki egy sort!");
                return;
            }
            else if(SelectedList.Count > 1)
            {
                MessageBox.Show("Csak egy elemet lehet egyszerre módosítani!");
                return;
            }
            var UpdateElementWindow = new UpdateOld(SelectedList[0]);
            if (UpdateElementWindow.ShowDialog() == true)
            {
                ShowResponse("Sikeres adat rögzítés!");
                LoadLastChoice();
            }
            else
                ShowResponse("Sikeretelen adat felvétel!");
        }
        //==================================== Keresési esemény ====================================
        private void OnOuterSearch(object sender, EventArgs e)
        {
            if (Searcher.ResultList.IsNullOrEmpty())
                ShowResponse("Sikeretelen keresés!");
            else
            {
                if (LastLoadMode == ModeOfLoad.ALL || LastLoadMode == ModeOfLoad.AandP || LastLoadMode == ModeOfLoad.PTYandP)
                    Loader.LoadData(Searcher.ResultList, false);
                else Loader.LoadData(Searcher.ResultList);
                ShowResponse("Sikeres keresés!");
            }
        }
    }
}