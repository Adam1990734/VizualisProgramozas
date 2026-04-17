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

            //Amikor összetett:
            if (RuntimeType == typeof(Property) || RuntimeType == typeof(Agent) || RuntimeType == typeof(PropertyType))
            {
                var ObjectProperties = RuntimeType.GetProperties().Where(prop => !prop.Name.ToUpper().Contains("ID"));
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
                var columns = GetFlattenedColumns(RuntimeType);
                foreach (var col in columns)
                {
                    DataPanel.Columns.Add(new DataGridTextColumn
                    {
                        Header = col.Header.Split('.')[1],
                        Binding = new Binding(col.Binding),
                        IsReadOnly = true
                    });
                }
                DataPanel.ItemsSource = Loadable
                    .Select(x => FlattenObject(x, columns))
                    .ToList();
            }
        }
        private List<(string Header, string Binding)> GetFlattenedColumns(Type GivenType)
        {
            var Result = new List<(string, string)>();

            foreach (var Prop in GivenType.GetProperties())
            {
                if (IsSimple(Prop.PropertyType))
                    Result.Add((Prop.Name, Prop.Name));
                else
                {
                    foreach (var SubProp in Prop.PropertyType.GetProperties())
                    {
                        if (IsSimple(SubProp.PropertyType))
                        {
                            string Binding = $"{Prop.Name}_{SubProp.Name}";
                            string Header = $"{Prop.Name}.{SubProp.Name}";
                            Result.Add((Header, Binding));
                        }
                    }
                }
            }

            return Result;
        }
        private object FlattenObject( PropertyDbBase Source, List<(string Header, string Binding)> Columns)
        {
            IDictionary<string, object?> Row = new ExpandoObject();

            if(Selectable)
                Row["IsChecked"] = false;

            foreach (var Col in Columns)
            {
                object? Value = Source;
                string[] Path = Col.Binding.Split('_');

                foreach (var Part in Path)
                {
                    if (Value == null)
                        break;
                    PropertyInfo? pi = Value.GetType().GetProperty(Part);
                    Value = pi?.GetValue(Value);
                }

                Row[Col.Binding] = Value;
            }
            Row["Source"] = Source;

            return Row;
        }
        private bool IsSimple(Type Type)
        {
            return
                Type.IsPrimitive ||
                Type == typeof(string) ||
                Type == typeof(decimal) ||
                Type == typeof(double) ||
                Type == typeof(DateTime);
        }
        public List<PropertyDbBase> GetSelectedObjects()
        {
            if (!Selectable)
                throw new Exception("The current table has no selction column!");
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
