using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    public sealed class MTypeProperty : PropertyDbBase
    {
        //Ingatlan adatai:
        public Property MProperty { get; set; }
        //Típus adatai:
        public PropertyType MPropertyType { get; set; }

        public MTypeProperty() { }
        public MTypeProperty(MTypeProperty propertyType)
        {
            this.MPropertyType = new PropertyType(propertyType.MPropertyType);
            this.MProperty = new Property(propertyType.MProperty);
        }

        public void Clone(out PropertyDbBase Clone) => Clone = new MTypeProperty(this);

        public void Copy(PropertyDbBase ToCopy)
        {
            var Copy = (MTypeProperty)ToCopy;
            this.MProperty.Copy(Copy.MProperty);
            this.MProperty.Copy(Copy.MProperty);
        }
    }
}
