using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    public sealed class MAgentProperty
    {
        //Ingatlan adatai:
        public Property MProperty { get; set; }
        //Típus adatai:
        public Agent MAgent { get; set; }
    }
}
