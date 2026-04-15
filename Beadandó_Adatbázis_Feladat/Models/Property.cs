using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    [Table("ingatlanok")]
    public class Property : PropertyDbBase, IComparable<Property>
    {
        [Column("ingatlan_ID")]
        public int? Id { get; protected set; }
        [Column("fk_tipusID")]
        public int? TypeId { get; set; }
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
                _Location = value;
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
        private int _CountOfRooms;
        [Column("szobaszam")]
        public int CountOfRooms
        {
            get => _CountOfRooms;
            set
            {
                if(value <= 0)
                    throw new Exception("Room count is out of range.");
                _CountOfRooms = value;
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
        public bool Garage { get; set; }
        [Column("zoldovezet")]
        public bool GreenArea { get; set; }
        public Property(Property other)
        {
            //Biztonságosan csak hogy ne lehesse "adatlopás"
            this.Id = this.TypeId = this.AgentId = null;

            this.Location = other.Location;
            this.District = other.District;
            this.Area = other.Area;
            this.CountOfRooms = other.CountOfRooms;
            this.Price = other.Price;
            this.Garage = other.Garage;
            this.GreenArea = other.GreenArea;
        }
        public Property() { }

        //Az összehasonlítási faktor itt alapértelmezetten az ár alapján megy szóval minden másra majd LINQ kell.
        public int CompareTo(Property other) => this.Price.CompareTo(other.Price);
        //Másolat készítés objektumok szerint (ha elkészült egy adatbázi egyed az "másolhatatlan")
        public void Copy(PropertyDbBase ToCopy) {
            var other = (Property)ToCopy;
            //Biztonságosan csak hogy ne lehesse "adatlopás"
            this.Id = this.TypeId = this.AgentId = null;

            this.Location = other.Location;
            this.District = other.District;
            this.Area = other.Area;
            this.CountOfRooms = other.CountOfRooms;
            this.Price = other.Price;
            this.Garage = other.Garage;
            this.GreenArea = other.GreenArea;
        }
        public void Clone(out PropertyDbBase Clone) => Clone = new Property(this);
        //Operátorok:
        public static bool operator<(Property a, Property b) => a.Price < b.Price;
        public static bool operator >(Property a, Property b) => a.Price > b.Price;
        public static bool operator ==(Property a, Property b) => a.Price == b.Price;
        public static bool operator !=(Property a, Property b) => a.Price != b.Price;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Property;
            if (other == null) return false;
            if (other.District != this.District) return false;
            if(other.Area != this.Area) return false;
            if(other.GreenArea != this.GreenArea) return false;
            if(other.CountOfRooms != this.CountOfRooms) return false;
            if(other.Garage != this.Garage) return false;
            if(other.Price != this.Price) return false;
            if(other.Location  != this.Location) return false;
            return true;
        }
    }
}
