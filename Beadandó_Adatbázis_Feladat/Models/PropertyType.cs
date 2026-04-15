using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    [Table("tipusok")]
    public class PropertyType : PropertyDbBase, IComparable<PropertyType>
    {
        [Column("tipus_ID")]
        public int? Id { get; protected set; }
        [Column("tipus_nev")]
        public string Name { get; set; }

        public PropertyType(PropertyType other)
        {
            this.Id = null;
            this.Name = other.Name;
        }
        public PropertyType() { }
        public void Copy(PropertyDbBase ToCopy)
        {
            var other = (PropertyType)ToCopy;
            this.Id = null;
            this.Name = other.Name;
        }
        public void Clone(out PropertyDbBase Clone) => Clone = new PropertyType(this);

        public int CompareTo(PropertyType other) => this.Name.CompareTo(other.Name);
        public bool hasId() { return Id == null ? false : true; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as PropertyType;
            if (other == null) return false;
            if(other.Name != this.Name) return false;
            return true;
        }
    }
}
