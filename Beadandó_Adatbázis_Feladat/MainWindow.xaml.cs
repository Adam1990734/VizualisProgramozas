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
        public MainWindow()
        {
            InitializeComponent();
            using var db = new DataBase();
            //foreach (var a in db.Agents.ToList())
            //{
            //    MessageBox.Show(a.Name);
            //}
            //Példa a join-ra:
        }
    }
}