using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    [Table("tipusok")]
    internal class PropertyType : IComparable<PropertyType>
    {
        [Column("tipus_ID")]
        public int? Id { get; protected set; }
        [Column("tipus_nev")]
        public string? Name { get; set; }

        public PropertyType(PropertyType other)
        {
            this.Id = null;
            this.Name = other.Name;
        }
        public PropertyType() { }

        public int CompareTo(PropertyType other)
        {
            if (this.Name != null && other.Name != null)
                return this.Name.CompareTo(other.Name);
            else throw new Exception("Unable to decide which is graiter, one of the elements is null");
        }
    }
}
