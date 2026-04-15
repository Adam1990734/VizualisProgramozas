using Beadandó_Adatbázis_Feladat.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class ObjectLoader : UserControl
    {
        public ObjectLoader()
        {
            InitializeComponent();
        }
        public void LoadData(List<PropertyDbBase> data)
        {
            if (data == null || data.Count == 0)
                return;

            DataPanel.Columns.Clear();

            Type runtimeType = data.First().GetType();

            var columns = GetFlattenedColumns(runtimeType);

            //Ha kell kijelölési funkció:
            DataPanel.Columns.Add(new DataGridCheckBoxColumn
            {
                Header = "Kijelölés",
                Binding = new Binding("IsChecked")
                {
                    Mode = BindingMode.TwoWay
                }
            });

            //Amikor összetett:
            if (runtimeType == typeof(Property) || runtimeType == typeof(Agent) || runtimeType == typeof(PropertyType))
                foreach (var col in columns)
                {
                    if (!col.Header.Contains("."))
                        DataPanel.Columns.Add(new DataGridTextColumn
                        {
                            Header = col.Header,
                            Binding = new Binding(col.Binding)
                        });
                }
            else 
                foreach (var col in columns)
                    {
                        DataPanel.Columns.Add(new DataGridTextColumn
                        {
                           Header = col.Header.Split('.')[1],
                           Binding = new Binding(col.Binding)
                        });
                    }
            DataPanel.ItemsSource = data
                .Select(x => FlattenObject(x, columns))
                .ToList();
            
        }
        private List<(string Header, string Binding)> GetFlattenedColumns(Type type)
        {
            var result = new List<(string, string)>();

            foreach (var prop in type.GetProperties())
            {
                if (IsSimple(prop.PropertyType))
                    result.Add((prop.Name, prop.Name));
                else
                {
                    foreach (var subProp in prop.PropertyType.GetProperties())
                    {
                        if (IsSimple(subProp.PropertyType))
                        {
                            string binding = $"{prop.Name}_{subProp.Name}";
                            string header = $"{prop.Name}.{subProp.Name}";
                            result.Add((header, binding));
                        }
                    }
                }
            }

            return result;
        }
        private object FlattenObject( PropertyDbBase source, List<(string Header, string Binding)> columns)
        {
            IDictionary<string, object?> row = new ExpandoObject();
            row["IsChecked"] = false;

            foreach (var col in columns)
            {
                object? value = source;
                string[] path = col.Binding.Split('_');

                foreach (var part in path)
                {
                    if (value == null)
                        break;
                    PropertyInfo? pi = value.GetType().GetProperty(part);
                    value = pi?.GetValue(value);
                }

                row[col.Binding] = value;
            }
            row["Source"] = source;
            return row;
        }
        private bool IsSimple(Type type)
        {
            return
                type.IsPrimitive ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(double) ||
                type == typeof(DateTime);
        }
        public List<PropertyDbBase> GetSelectedObjects()
        {

            return DataPanel.ItemsSource
                .Cast<IDictionary<string, object?>>()
                .Where(r => r.TryGetValue("IsChecked", out var v) && (bool)v)
                .Select(r => r["__Source"])
                .Cast<PropertyDbBase>()
                .ToList();
        }
    }
}
