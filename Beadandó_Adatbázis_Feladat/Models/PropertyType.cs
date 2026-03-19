using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    internal class PropertyType : IComparable<PropertyType>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public PropertyType(PropertyType other)
        {
            this.Name = other.Name;
        }

        public int CompareTo(PropertyType other)
        {
            if (this.Name != null && other.Name != null)
                return this.Name.CompareTo(other.Name);
            else throw new Exception("Unable to decide which is graiter, one of the elements is null");
        }
    }
}
