using Beadandó_Adatbázis_Feladat.Models;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnAddEmber(object sender, EventArgs e) {
            using(var db = new AppDbContext())
            {
                db.Add(new Ember { Nev = "Valaki", Kor = 25 });
                db.SaveChanges();

                var lista = db.Emberek.ToList();
                foreach (var ember in lista)
                    MessageBox.Show($"{ember.Id} - {ember.Nev} - {ember.Kor}");
            }
        }
    }
}