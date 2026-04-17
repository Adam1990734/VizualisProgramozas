using Beadandó_Adatbázis_Feladat.DbContext;
using Beadandó_Adatbázis_Feladat.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class UpdateOld : Window
    {
        internal enum OptionsToCreate { PROP, TYPE, AGENT }
        private OptionsToCreate CurrentlyShowing;
        private PropertyDbBase GivenObject;
        public UpdateOld(PropertyDbBase DatabaseObject)
        {
            InitializeComponent();

            //===================== Esemény kezelők =====================
            this.SaveBtn.Click += OnSave;
            this.CancelBtn.Click += OnCancel;
            //===========================================================

            if (DatabaseObject == null)
                throw new Exception("Unable to change on a null element!");
            //===================== Cím beállítás =====================
            if (DatabaseObject.GetType() == typeof(Agent))
            {
                this.MainLabel.Content = "Ügynök módosítás";
                LoadInToTextBoxes(DatabaseObject as Agent);
                CurrentlyShowing = OptionsToCreate.AGENT;
                this.AgentPanel.Visibility = Visibility.Visible;
            }
            else if (DatabaseObject.GetType() == typeof(Property))
            {
                this.MainLabel.Content = "Ingatlan módosítás";
                LoadInToTextBoxes(DatabaseObject as Property);
                CurrentlyShowing = OptionsToCreate.PROP;
                this.PropertyPanel.Visibility = Visibility.Visible;
            }
            else if (DatabaseObject.GetType() == typeof(PropertyType))
            {
                this.MainLabel.Content = "Ingatlan típus módosítás";
                LoadInToTextBoxes(DatabaseObject as PropertyType);
                CurrentlyShowing = OptionsToCreate.TYPE;
                this.PropertyTypePanel.Visibility = Visibility.Visible;
            }
            else throw new Exception("Not supported type has given!");
            //=========================================================
            this.GivenObject = DatabaseObject;
        }
        //==================================== Betöltő függvények ====================================
        private void LoadInToTextBoxes(Agent ObjectToLoad)
        {
            this.AgentNameInput.Text = ObjectToLoad.Name;
            this.AgentPhoneInput.Text = ObjectToLoad.PhoneNumber;
            if (ObjectToLoad.Status)
                this.AgentStatusInput.SelectedIndex = 0;
            else this.AgentStatusInput.SelectedIndex = 1;
        }
        private void LoadInToTextBoxes(PropertyType ObjectToLoad)
        {
            this.PropTypeNameInput.Text = ObjectToLoad.Name;
        }
        private void LoadInToTextBoxes(Property ObjectToLoad)
        {
            this.LoacationInput.Text = ObjectToLoad.Location;
            this.DistrictInput.Text = ObjectToLoad.District.ToString();
            this.AreaInput.Text = ObjectToLoad.Area.ToString();
            this.CountOfRoomsInput.Text = ObjectToLoad.CountOfRooms.ToString();
            this.PriceInput.Text = Math.Round(ObjectToLoad.Price, 2).ToString();
            if (ObjectToLoad.Garage)
                this.GarageInput.SelectedIndex = 0;
            else this.GarageInput.SelectedIndex = 1;
            if (ObjectToLoad.GreenArea)
                this.GreenAreaInput.SelectedIndex = 0;
            else this.GreenAreaInput.SelectedIndex = 1;
            using (var db = new DataBase())
            {
                if(!db.Database.CanConnect())
                    throw new Exception("Cannot connect to the database!");
                this.PropTypesInput.ItemsSource = db.PropertyTypes.ToList();
                this.PropTypesInput.DisplayMemberPath = "Name";
                this.PropTypesInput.SelectedValuePath = "Id";
                this.PropTypesInput.SelectedValue = ObjectToLoad.TypeId;

                this.AgentsInput.ItemsSource = db.Agents.ToList();
                this.AgentsInput.DisplayMemberPath = "Name";
                this.AgentsInput.SelectedValuePath = "Id";
                this.AgentsInput.SelectedValue = ObjectToLoad.AgentId;
            }
        }
        //==================================== Kiolvasó függvény ====================================
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
        private void OnSave(object sender, EventArgs e)
        {
            try
            {
                var InputData = ReadInput(CurrentlyShowing);
                using (var db = new DataBase())
                {
                    switch (CurrentlyShowing)
                    {
                        case OptionsToCreate.PROP:
                            GivenObject.Copy(new Property
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
                            db.Update(GivenObject);
                            break;
                        case OptionsToCreate.TYPE:
                            GivenObject.Copy(new PropertyType
                            {
                                Name = (string)InputData["Name"]
                            });
                            db.Update(GivenObject);
                            break;
                        case OptionsToCreate.AGENT:
                            GivenObject.Copy(new Agent
                            {
                                Name = (string)InputData["Name"],
                                PhoneNumber = InputData["Phone"] == null ? null : (string)InputData["Phone"],
                                Status = (bool)InputData["Status"]
                            });
                            db.Update(GivenObject);
                            break;
                    }
                    db.SaveChanges();
                }
                this.DialogResult = true;
            } catch(Exception ex)
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
