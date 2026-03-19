using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Beadandó_Adatbázis_Feladat.Models
{
    internal class Agent : IComparable<Agent>
    {
        //Az ügynök adatbázis ID-ja lehet ha kell
        public int? Id { get; set; }
        //Az ügynök elérhetőség jelzi pl dolgozik-e
        public bool Status { get; set; }

        private string _Name;
        public string Name
        {
            get => _Name;
            set {
                if (Regex.IsMatch(value.Trim(), @"^[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+( [A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+)+$"))
                    _Name = value.Trim();
                else throw new Exception("Error when trying to set Name to a not compatible pattern");
            }
        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                if (Regex.IsMatch(value.Trim(), @"^\+36\d{9}$"))
                    PhoneNumber = value.Trim();
                else throw new Exception("Error when trying to set PhoneNumber to a not compatible pattern");
            }
        }
        
        //Segéd függvények:
        public bool isActive() { return Status; }
        public bool hasId() { return Id == null ? false : true; }

        public Agent(Agent other)
        {
            this.Name = other.Name;
            this.PhoneNumber = other.PhoneNumber;
            this.Status = other.Status;
        }
        public int CompareTo(Agent other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
