using System.Windows;

namespace Beadandó_Adatbázis_Feladat
{
    public partial class AddNew : Window
    {
        internal enum OptionsToCreate { PROP, TYPE, AGENT }

        OptionsToCreate ToCreate;
        public AddNew()
        {
            InitializeComponent();
            ToCreate = OptionsToCreate.PROP;
        }
        //Ide kellnek még a létrehozó függvények és a beolvasó függvények!
    }
}
