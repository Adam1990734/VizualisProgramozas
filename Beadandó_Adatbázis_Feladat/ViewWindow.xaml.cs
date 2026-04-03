using System.Windows;
using System.Windows.Controls;
using Beadandó_Adatbázis_Feladat.Models;

namespace Beadandó_Adatbázis_Feladat
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : UserControl
    {
        public static readonly DependencyProperty Show = DependencyProperty.Register(
            "Show",
            typeof(List<PropertyDbBase>),
            typeof(ViewWindow)
        );
        private List<PropertyDbBase> ToShow {
            get => (List<PropertyDbBase>)GetValue(Show);
            set => SetValue(Show, value);
        }
        public ViewWindow()
        {
            InitializeComponent();
            
        }
    }
}
