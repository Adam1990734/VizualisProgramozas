using System.Windows;
using Beadandó_Adatbázis_Feladat.Models;
using Beadandó_Adatbázis_Feladat.DbContext;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class AddNew : Window
    {
        internal enum WindowMode { CREATE, UPDATE }
        internal enum OptionsToCreate { PROP, TYPE, AGENT }

        private OptionsToCreate CurrentlyShowing;
        private WindowMode WorkingMode;
        public AddNew()
        {
            InitializeComponent();
            ShowPanel(OptionsToCreate.PROP);
            //========================== Esemény kezelők ==========================
            this.SaveBtn.Click += OnSave;
            this.CancelBtn.Click += OnCancel;

            this.CreateBtn.Selected += (object sender, RoutedEventArgs e) => { WorkingMode = WindowMode.CREATE; };
            this.UpdateBtn.Selected += (object sender, RoutedEventArgs e) => { WorkingMode = WindowMode.UPDATE; };

            this.ChosseProp.Selected += (object sender, RoutedEventArgs e) => ShowPanel(OptionsToCreate.PROP);
            this.ChooseType.Selected += (object sender, RoutedEventArgs e) => ShowPanel(OptionsToCreate.TYPE);
            this.ChooseAgent.Selected += (object sender, RoutedEventArgs e) => ShowPanel(OptionsToCreate.AGENT);
            //=====================================================================
        }
        //========================== Betöltő események ==========================
        private void HideCurrent()
        {
            switch (CurrentlyShowing)
            {
                case OptionsToCreate.PROP:
                    this.PropertyPanel.Visibility = Visibility.Collapsed;
                    return;
                case OptionsToCreate.TYPE:
                    this.PropertyTypePanel.Visibility = Visibility.Collapsed;
                    return;
                case OptionsToCreate.AGENT:
                    this.AgentPanel.Visibility = Visibility.Collapsed;
                    return;
                default:
                    throw new Exception("Unknow show parameter has given!");
            }
        }
        private void ShowPanel(OptionsToCreate ToShow)
        {
            HideCurrent();
            switch(ToShow)
            {
                case OptionsToCreate.PROP:
                    this.PropertyPanel.Visibility = Visibility.Visible;
                    //Betöltendő objektumok:
                    using (var db = new DataBase())
                    {
                        if (!db.Database.CanConnect())
                            throw new Exception("Cannont connect to the Database!");
                        this.PropTypesInput.ItemsSource = db.PropertyTypes.ToList();
                        this.PropTypesInput.DisplayMemberPath = "Name";
                        this.PropTypesInput.SelectedValuePath = "Id";

                        this.AgentsInput.ItemsSource = db.Agents.ToList();
                        this.AgentsInput.DisplayMemberPath = "Name";
                        this.AgentsInput.SelectedValuePath = "Id";
                    }
                    break;
                case OptionsToCreate.TYPE:
                    this.PropertyTypePanel.Visibility = Visibility.Visible;
                    break;
                case OptionsToCreate.AGENT:
                    this.AgentPanel.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new Exception("Unknow show parameter has given!");
            }
            CurrentlyShowing = ToShow;
        }

        //========================== Input olvasás függvények ==========================
        private Dictionary<string, object?> ReadInput(OptionsToCreate ChoosenType)
        {
            switch(ChoosenType)
            {
                case OptionsToCreate.PROP:
                    var valami = (this.GarageInput.SelectedItem as ComboBoxItem).Tag.ToString();
                    return new Dictionary<string, object?>
                    {
                        { "Location", this.LoacationInput.Text.Equals("") ? null : this.LoacationInput.Text },
                        { "District", int.TryParse(this.DistrictInput.Text, out int District) ? District : null },
                        { "Area", int.TryParse(this.AreaInput.Text, out int Area) ? Area : null },
                        { "CountOfRooms", int.TryParse(this.CountOfRoomsInput.Text, out int CountOfRooms ) ? CountOfRooms : null },
                        { "Price", double.TryParse(this.PriceInput.Text, out double Price) ? Price : null },
                        { "Garage", bool.TryParse((this.GarageInput.SelectedItem as ComboBoxItem).Tag.ToString(), out bool Garage) ? Garage : false },
                        { "GreenArea", bool.TryParse((this.GreenAreaInput.SelectedItem as ComboBoxItem).Tag.ToString(), out bool GreenArea) ? GreenArea : false },
                        { "Type", this.PropTypesInput.SelectedValue },
                        { "Agent", this.AgentsInput.SelectedValue }
                    };
                case OptionsToCreate.TYPE:
                    return new Dictionary<string, object?>
                    {
                        { "TypeName", this.PropTypeNameInput.Text }
                    };
                case OptionsToCreate.AGENT:
                    return new Dictionary<string, object?>
                    {
                        { "Name", this.AgentNameInput.Text },
                        { "Phone", this.AgentPhoneInput.Text },
                        { "Status", bool.TryParse((this.AgentStatusInput.SelectedItem as ComboBoxItem).Tag.ToString(), out bool Status) ? Status : false }
                    };
                default:
                    throw new Exception("Unknow type has given!");
            }
        }
        //========================== Mentés és Elutasítás ==========================
        private void OnSave(object sender, EventArgs e)
        {
            var valami = ReadInput(CurrentlyShowing);
            return;
        }
        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
