using System.Windows;
using System.Windows.Controls;
using Beadandó_Adatbázis_Feladat.DbContext;
using Beadandó_Adatbázis_Feladat.Models;
using Microsoft.IdentityModel.Tokens;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class SearchBar : UserControl
    {
        internal class CompItem
        {
            public string ColumnWrapper { get; set; }
            public string Column { get; set; }
        }

        private List<string> Columns;
        private List<PropertyDbBase> UsingObjects;
        public List<PropertyDbBase>? ResultList { get; private set; }
        public SearchBar()
        {
            InitializeComponent();
            SearchBtn.Click += OnSearch;
        }
        public void SetContext(List<string> CurrantColumns, List<PropertyDbBase> CurrantObjects)
        {
            if (CurrantColumns.IsNullOrEmpty())
                throw new Exception("The given Column list is empty!");
            if (CurrantObjects.IsNullOrEmpty())
                throw new Exception("The given Object list is empty!");

            this.Columns = CurrantColumns;
            this.UsingObjects = CurrantObjects;
            LoadColumns();
        }
        private void LoadColumns(List<string> ToLoad)
        {
            Type RuntimeType = UsingObjects.GetType();
            if (RuntimeType == typeof(Property) || RuntimeType == typeof(Agent) || RuntimeType == typeof(PropertyType))
                ColumnChooser.ItemsSource = ToLoad;
            else
            {
                var ComplexList = new List<CompItem>();
                foreach (var Col in ToLoad)
                    ComplexList.Add(new CompItem
                    {
                        ColumnWrapper = Col.Replace("_", "->"),
                        Column = Col.Split('_')[1]
                    });
                ColumnChooser.ItemsSource = ComplexList;
                ColumnChooser.DisplayMemberPath = "ColumnWrapper";
                ColumnChooser.SelectedValuePath = "Column";
            }
            ColumnChooser.SelectedIndex = 0;
        }
        private void LoadColumns()
        {
            Type RuntimeType = UsingObjects.First().GetType();
            if (RuntimeType == typeof(Property) || RuntimeType == typeof(Agent) || RuntimeType == typeof(PropertyType))
                ColumnChooser.ItemsSource = Columns;
            else
            {
                var ComplexList = new List<CompItem>();
                foreach (var Col in Columns)
                    ComplexList.Add(new CompItem
                    {
                        ColumnWrapper = Col.Substring(1, Col.Length-1).Replace("_", "->"),
                        Column = Col.Split('_')[1]
                    });
                ColumnChooser.ItemsSource = ComplexList;
                ColumnChooser.DisplayMemberPath = "ColumnWrapper";
                ColumnChooser.SelectedValuePath = "Column";
            }
            ColumnChooser.SelectedIndex = 0;
        }
        private string GetSelectedColumn() => ColumnChooser.SelectedValue as string;
        private int GetSelectedColumnIndex() => ColumnChooser.SelectedIndex;
        private string GetInput() => SearchInput.Text.Trim();
        public List<PropertyDbBase> GetSelectedObjects()
        {
            Type RuntimeType = UsingObjects.First().GetType();
            if (RuntimeType == typeof(Property) || RuntimeType == typeof(Agent) || RuntimeType == typeof(PropertyType))
                return UsingObjects.Where(Obj => {
                    var Prop = Obj.GetType()
                        .GetProperty(GetSelectedColumn())?
                        .GetValue(Obj);
                    if (Prop == null)
                        return false;
                    Prop = Prop.GetType() == typeof(double) ? Prop.ToString().Replace(',', '.') : Prop.ToString();
                    return Prop.Equals(GetInput());
                }).Cast<PropertyDbBase>().ToList();
            else
                return UsingObjects.Where(Obj =>
                {
                    var Inner = Obj.GetType()
                        .GetProperty(Columns[GetSelectedColumnIndex()].Split('_')[0])?
                        .GetValue(Obj);

                    if (Inner == null)
                        return false;

                    var InnerProp = Inner.GetType().GetProperty(GetSelectedColumn());
                    if (InnerProp == null)
                        return false;

                    var Value = InnerProp.GetValue(Inner);

                    if (Value == null)
                        return false;

                    Value = Value.GetType() == typeof(double) ? Value.ToString().Replace(',', '.') : Value.ToString();
                    return Value.Equals(GetInput());
                }).Cast<PropertyDbBase>().ToList();

        }
        private void OnSearch(object sender, EventArgs e)
        {
            if (GetInput().IsNullOrEmpty())
                ResultList = null;
            try
            {
                ResultList = GetSelectedObjects();
                this.SearchInput.Text = "";
            } catch(Exception ex)
            {
                ResultList = null;
            }
        }
    }
}
