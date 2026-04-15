using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Beadandó_Adatbázis_Feladat.Models;
using Microsoft.EntityFrameworkCore;

namespace Beadandó_Adatbázis_Feladat.DbContext
{
    class DataBase : Microsoft.EntityFrameworkCore.DbContext
    {
        //Adatbázis elemek:
        public DbSet<Agent> Agents { get; protected set; }
        public DbSet<Property> Properties { get; protected set; }
        public DbSet<PropertyType> PropertyTypes { get; protected set; }
        //Konfigurációs függvény --> ez tölti be az adatbázist:
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
                options.UseSqlServer(DbContext.Config.ConnectionString);
        }
        public List<PropertyDbBase> JoinProperiesAndAgents()
        {
            return Properties.Join(
                Agents,
                prop => prop.AgentId,
                agent => agent.Id,
                (prop, agent) => new MAgentProperty
                {
                    MAgent = agent,
                    MProperty = prop
                }
            ).Cast<PropertyDbBase>().ToList();
        }
        public List<PropertyDbBase> JoinProperiesAndTypes()
        {
            return Properties.Join(
                PropertyTypes,
                prop => prop.TypeId,
                type => type.Id,
                (prop, type) => new MTypeProperty
                {
                    MProperty = prop,
                    MPropertyType = type
                }
            ).Cast<PropertyDbBase>().ToList();
        }
        public List<PropertyDbBase> JoinAllTable()
        {
            return JoinProperiesAndTypes().Cast<MTypeProperty>().Join(
                Agents,
                propAndType => propAndType.MProperty.AgentId,
                agent => agent.Id,
                (propAndType, agent) => new MAgentPropertyType
                {
                    MAgent = agent,
                    MProperty = propAndType.MProperty,
                    MPropertyType = propAndType.MPropertyType
                }
            ).Cast<PropertyDbBase>().ToList();
        }
        //Úgy van elkészítve hogy a getter minden adatot visszaad az Id-k nélkül
        public List<PropertyDbBase> AllData
        {
            get
            {
                return JoinAllTable().Cast<MAgentPropertyType>().Select(
                    record => new MAgentPropertyType()
                    {
                        MAgent = record.MAgent,
                        MProperty = record.MProperty,
                        MPropertyType = record.MPropertyType
                    }
                ).Cast<PropertyDbBase>().ToList();
            }
        }
        //Törlés:
        public void DeleteObject<T>(T Object)//Ezt le lehet egyszerűsíteni az Equals függvénnyel
        {
            if (Object.GetType() == null)
                throw new Exception("The specified ojbect type does not exsists!");
            if (Object.GetType() == typeof(Property))
                DeleteProperty(Object as Property);
            else if (Object.GetType() == typeof(PropertyType))
                throw new NotImplementedException();
            else if (Object.GetType() == typeof(Agent))
                throw new NotImplementedException();
        }
        private void DeleteProperty(Property? Prop)
        {
            if (Prop == null)
                throw new Exception("The given object is not valid!");
            if (this.Database.CanConnect())
                throw new Exception("The Database is not connected!");
            this.Remove(this.Properties.Where(Record => Record.Id == 0));
            this.SaveChanges();
        }
        private void DeletePropertyType(int Id)
        {
            if (Id < 0)
                throw new Exception("The given Id is out of Id range");
            if (this.Database.CanConnect())
                throw new Exception("The Database is not connected!");
            this.Remove(this.PropertyTypes.Where(Record => Record.Id == Id));
            this.SaveChanges();
        }
        private void DeleteAgent(int Id)
        {
            if (Id < 0)
                throw new Exception("The given Id is out of Id range");
            if (this.Database.CanConnect())
                throw new Exception("The Database is not connected!");
            this.Remove(this.Agents.Where(Record => Record.Id == Id));
            this.SaveChanges();
        }
    }
}
