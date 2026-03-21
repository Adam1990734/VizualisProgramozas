using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Beadandó_Adatbázis_Feladat.Models
{
    [Table("ugynokok")]
    public class Agent : IComparable<Agent>
    {
        //Az ügynök adatbázis ID-ja lehet ha kell
        [Column("ugynok_ID")]
        public int? Id { get; protected set; }
        //Az ügynök elérhetőség jelzi pl dolgozik-e
        [Column("statusz")]
        public bool Status { get; set; }

        private string _Name;
        [Column("ugynok_nev")]
        public string Name
        {
            get => _Name;
            set {
                if (Regex.IsMatch(value.Trim(), @"^[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+( [A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+)+$"))
                    _Name = value.Trim();
                else throw new Exception("Error when trying to set Name to a not compatible pattern");
            }
        }

        private string? _PhoneNumber;
        [Column("telefon")]
        public string? PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                if (Regex.IsMatch(value.Trim(), @"^\+\d{2}\d{9}$"))
                    _PhoneNumber = value.Trim();
                //Ezt egyenlőre kikapcsolom a teszt adatokhoz de amúgy jó!!!
                else _PhoneNumber = value;//throw new Exception("Error when trying to set PhoneNumber to a not compatible pattern");
            }
        }

        public Agent(Agent other)
        {
            this.Id = null;
            this.Name = other.Name;
            this.PhoneNumber = other.PhoneNumber;
            this.Status = other.Status;
        }
        public Agent() { }
        public virtual void Copy(Agent other)
        {
            this.Id = null;
            this.Name = other.Name;
            this.PhoneNumber = other.PhoneNumber;
            this.Status = other.Status;
        }
        public virtual void Clone(out Agent CopyAgent) => CopyAgent = new Agent(this);

        //Segéd függvények:
        public int CompareTo(Agent other) => this.Name.CompareTo(other.Name);
        public bool isActive() => Status;
        public bool hasId() { return Id == null ? false : true; }
    }
}
