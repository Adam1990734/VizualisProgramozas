using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    [Table("tipusok")]
    public class PropertyType : IComparable<PropertyType>
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
        public virtual void Copy(PropertyType other)
        {
            this.Id = null;
            this.Name = other.Name;
        }
        public virtual void Clone(out PropertyType CopyPropertyType) => CopyPropertyType = new PropertyType(this);

        public int CompareTo(PropertyType other) => this.Name.CompareTo(other.Name);
        public bool hasId() { return Id == null ? false : true; }
    }
}
