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
        public void DeleteObject(List<PropertyDbBase> ToDelete)
        {
            if (!this.Database.CanConnect())
                throw new Exception("Delete: The databes is unable to conncet to!");
            if (ToDelete == null || ToDelete.Count == 0)
                throw new Exception("Delete: The specified ojbect type does not exsists!");
            //============================== Speciális esetek ==============================
            var RuntimeType = ToDelete[0].GetType();
            if (RuntimeType == typeof(Agent) || RuntimeType == typeof(Property) || RuntimeType == typeof(PropertyType))
                this.RemoveRange(ToDelete);
            else
            {
                //Ömlesztett lista mely tartalmaz miden property elemet az összetett objektumokból
                this.RemoveRange(
                ToDelete.SelectMany(record =>
                    record.GetType().GetProperties()
                    .Select(prop => prop.GetValue(record))
                )
                .Where(rv => rv != null)
                .Cast<Object>()
                .ToList()
                );
                this.SaveChanges();
            }
        }
    }
}
