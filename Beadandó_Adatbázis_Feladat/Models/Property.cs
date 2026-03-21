using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    [Table("ingatlanok")]
    internal class Property : IComparable<Property>
    {
        [Column("ingatlan_ID")]
        public int Id { get; set; }
        [Column("fk_tipusID")]
        public int TypeId { get; set; }
        [Column("fk_ugynokID")]
        public int? AgentId { get; set; }
        private string _Location;
        [Column("helyseg")]
        public string Location
        {
            get => _Location;
            set
            {
                if (value == "" || value == null)
                    throw new Exception("There should be a Location given!");
            }
        }
        [Column("kerulet")]
        public int? District { get; set; }
        //Épület specifikációs adatok:
        private int _Area;
        [Column("terulet")]
        public int Area
        {
            get => _Area;
            set
            {
                if (value <= 0)
                    throw new Exception("Area is out of range.");
                _Area = value;
            }
        }
        private int _CountOfRoums;
        [Column("szobaszam")]
        public int CountOfRoums
        {
            get => _CountOfRoums;
            set
            {
                if(value <= 0)
                    throw new Exception("Room count is out of range.");
                _CountOfRoums = value;
            }
        }
        private double _Price;
        [Column("ar")]
        public double Price
        {
            get => _Price;
            set
            {
                if (value <= 0)
                    throw new Exception("Property price out of range.");
                _Price = value;
            }
        }
        [Column("garazs")]
        public bool Garazs { get; set; }
        [Column("zoldovezet")]
        public bool GreenArea { get; set; }
        public Property(Property other)
        {
            this.TypeId = other.TypeId;
            this.AgentId = other.AgentId;

            this.Location = other.Location;
            this.District = other.District;
            this.Area = other.Area;
            this.CountOfRoums = other.CountOfRoums;
            this.Price = other.Price;
            this.Garazs = other.Garazs;
            this.GreenArea = other.GreenArea;
        }
        public Property() { }

        public int CompareTo(Property other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }
}
