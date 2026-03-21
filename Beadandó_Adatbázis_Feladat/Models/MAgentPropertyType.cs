using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    internal class MAgentPropertyType
    {
        //Ügynök adatai:
        public Agent MAgent { get; set; }

        //Ingatlan adatai:
        public Property MProperty { get; set; }
        
        //Típusok:
        public PropertyType MPropertyType { get; set; }
    }
}
