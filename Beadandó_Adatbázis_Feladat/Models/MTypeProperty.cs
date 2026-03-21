using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    internal class MTypeProperty
    {
        //Ingatlan adatai:
        public Property MProperty { get; set; }
        //Típus adatai:
        public PropertyType MPropertyType { get; set; }
    }
}
