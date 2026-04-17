using System.Windows;
using Beadandó_Adatbázis_Feladat.Models;
using Beadandó_Adatbázis_Feladat.DbContext;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class AddNew : Window
    {
        internal enum OptionsToCreate { PROP, TYPE, AGENT }

        private OptionsToCreate CurrentlyShowing;

        private PropertyDbBase ObjectToUpdate;
        public AddNew()
        {
            InitializeComponent();
            ShowPanel(OptionsToCreate.PROP);
            //========================== Esemény kezelők ==========================
            this.SaveBtn.Click += OnSave;
            this.CancelBtn.Click += OnCancel;

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
            switch (ToShow)
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
            switch (ChoosenType)
            {
                case OptionsToCreate.PROP:
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
                        { "Name", this.PropTypeNameInput.Text }
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
        //========================== Mód váltások és változás kezelés ==========================
        public List<TextBox> GetAllTexBoxes(Grid Root)
        {
            var Result = new List<TextBox>();

            if (Root == null)
                return Result;

            FindTextBoxes(Root, Result);
            return Result;
        }

        private void FindTextBoxes(DependencyObject Parent, List<TextBox> Result)
        {
            int ChildCount = VisualTreeHelper.GetChildrenCount(Parent);

            for (int i = 0; i < ChildCount; i++)
            {
                DependencyObject Child = VisualTreeHelper.GetChild(Parent, i);

                if (Child is TextBox tb)
                    Result.Add(tb);

                FindTextBoxes(Child, Result);
            }
        }
        private void OnReset()
        {
            switch (CurrentlyShowing)
            {
                case OptionsToCreate.PROP:
                    foreach (var Input in GetAllTexBoxes(this.PropertyPanel))
                        Input.Text = "";
                    return;
                case OptionsToCreate.TYPE:
                    foreach (var Input in GetAllTexBoxes(this.PropertyTypePanel))
                        Input.Text = "";
                    return;
                case OptionsToCreate.AGENT:
                    foreach (var Input in GetAllTexBoxes(this.AgentPanel))
                        Input.Text = "";
                    return;
                default:
                    throw new Exception("Unknown type has given!");
            }
        }
        //========================== Mentés és Elutasítás ==========================
        private void OnSave(object sender, EventArgs e)
        {
            try
            {
                var InputData = ReadInput(CurrentlyShowing);
                using (var db = new DataBase())
                {
                    if (!db.Database.CanConnect())
                        throw new Exception("Cannot connect to the database!");
                    switch (CurrentlyShowing)
                    {
                        case OptionsToCreate.PROP:
                            db.Add(new Property
                            {
                                TypeId = (int)InputData["Type"],
                                AgentId = (int)InputData["Agent"],
                                Location = (string)InputData["Location"],
                                District = InputData["District"] == null ? null : (int)InputData["District"],
                                Area = (int)InputData["Area"],
                                CountOfRooms = (int)InputData["CountOfRooms"],
                                Price = (double)InputData["Price"],
                                Garage = (bool)InputData["Garage"],
                                GreenArea = (bool)InputData["GreenArea"]
                            });
                            break;
                        case OptionsToCreate.TYPE:
                            db.Add(new PropertyType
                            {
                                Name = (string)InputData["Name"]
                            });
                            break;
                        case OptionsToCreate.AGENT:
                            db.Add(new Agent
                            {
                                Name = (string)InputData["Name"],
                                PhoneNumber = InputData["Phone"] == null ? null : (string)InputData["Phone"],
                                Status = (bool)InputData["Status"]
                            });
                            break;
                        default:
                            throw new Exception("Unknown type has given!");
                    }
                    db.SaveChanges();
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sikeretelen mentés, hibás adat megadás!");
            }
        }
        private void OnCancel(object sender, EventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
