using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    public interface PropertyDbBase
    {
        void Copy(PropertyDbBase ToCopy);
        void Clone(out PropertyDbBase Clone);
    }
}
