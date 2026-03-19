using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    internal class Property : IComparable<Property>
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int? AgentId { get; set; }
        private string _Location;
        public string Location
        {
            get => _Location;
            set
            {
                if (value == "" || value == null)
                    throw new Exception("There should be a Location given!");
            }
        }
        public short? District { get; set; }
        //Épület specifikációs adatok:
        private double _Area;
        public double Area
        {
            get => _Area;
            set
            {
                if (value <= 0)
                    throw new Exception("Area is out of range.");
                _Area = value;
            }
        }
        private short _CountOfRoums;
        public short CountOfRoums
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
        public bool Garazs { get; set; }
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

        public int CompareTo(Property other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }
}
