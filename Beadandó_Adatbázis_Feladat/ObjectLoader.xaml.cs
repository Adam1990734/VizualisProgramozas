using Beadandó_Adatbázis_Feladat.Models;
using System.Dynamic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class ObjectLoader : UserControl
    {
        private bool Selectable;
        private List<string> CurrentColumns;
        public List<string> Columns { get => CurrentColumns; }
        public ObjectLoader()
        {
            InitializeComponent();
            this.Selectable = false;
        }
        public void LoadData(List<PropertyDbBase> Loadable, bool IsSelectable = true)
        {
            if (Loadable == null || Loadable.Count == 0)
                return;

            Selectable = IsSelectable;
            DataPanel.Columns.Clear();

            Type RuntimeType = Loadable.First().GetType();

            //Ha kell kijelölési funkció:
            if (IsSelectable)
                DataPanel.Columns.Add(new DataGridCheckBoxColumn
                {
                    Header = "Kijelölés",
                    Binding = new Binding("IsChecked")
                    {
                        Mode = BindingMode.TwoWay
                    },
                    IsReadOnly = false
                });

            if (RuntimeType == typeof(Property) || RuntimeType == typeof(Agent) || RuntimeType == typeof(PropertyType))
            {
                var ObjectProperties = RuntimeType.GetProperties().Where(prop => !prop.Name.ToUpper().Contains("ID"));
                this.CurrentColumns = ObjectProperties.Select(prop => prop.Name).ToList();
                foreach (var col in ObjectProperties)
                    DataPanel.Columns.Add(new DataGridTextColumn
                    {
                        Header = col.Name,
                        Binding = new Binding(col.Name),
                        IsReadOnly = true
                    });
                DataPanel.ItemsSource = Loadable.Select(x =>
                {
                    IDictionary<string, object> Expando = new ExpandoObject();

                    if (Selectable)
                        Expando["IsChecked"] = false;

                    foreach (var prop in ObjectProperties)
                        Expando[prop.Name] = prop.GetValue(x);

                    Expando["Source"] = x;

                    return Expando;
                }).ToList();
            }
            else
            {
                this.CurrentColumns = GetFlattenedColumns(RuntimeType);
                foreach (var Col in this.CurrentColumns)
                {
                    
                    DataPanel.Columns.Add(new DataGridTextColumn
                    {
                        Header = Col.Split('_')[1],
                        Binding = new Binding(Col),
                        IsReadOnly = true
                    });
                }
                DataPanel.ItemsSource = Loadable
                    .Select(x => FlattenObject(x, this.CurrentColumns))
                    .ToList();
            }
        }
        private List<string> GetFlattenedColumns(Type GivenType)
        {
            var Result = new List<string>();
            foreach (var Prop in GivenType.GetProperties()) {
                foreach (var SubProp in Prop.PropertyType.GetProperties())
                {
                    if (!SubProp.Name.ToUpper().Contains("ID"))
                        Result.Add(Prop.Name + "_" + SubProp.Name);
                }
            }

            return Result;
        }
        private object FlattenObject( PropertyDbBase Source, List<string> Columns)
        {
            IDictionary<string, object?> Row = new ExpandoObject();

            if(Selectable)
                Row["IsChecked"] = false;

            foreach (var Col in Columns)
            {
                object? Value = Source;
                string[] Path = Col.Split('_');

                foreach (var Part in Path)
                {
                    if (Value == null)
                        break;
                    PropertyInfo? Pi = Value.GetType().GetProperty(Part);
                    Value = Pi?.GetValue(Value);
                }

                Row[Col] = Value;
            }
            Row["Source"] = Source;

            return Row;
        }
        public List<PropertyDbBase>? GetSelectedObjects()
        {
            if (!Selectable)
                return null;
            DataPanel.CommitEdit(DataGridEditingUnit.Cell, true);
            DataPanel.CommitEdit(DataGridEditingUnit.Row, true);
            return DataPanel.ItemsSource
                .Cast<IDictionary<string, object?>>()
                .Where(r => r.TryGetValue("IsChecked", out var v) && (bool)v)
                .Select(r => r["Source"])
                .Cast<PropertyDbBase>()
                .ToList();
        }
    }
}
