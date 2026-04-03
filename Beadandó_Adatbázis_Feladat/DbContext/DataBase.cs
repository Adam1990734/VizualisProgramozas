using System;
using System.Collections.Generic;
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
        public List<MAgentProperty> JoinProperiesAndAgents()
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
            ).ToList();
        }
        public List<MTypeProperty> JoinProperiesAndTypes()
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
            ).ToList();
        }
        public List<PropertyDbBase> JoinAllTable()
        {
            return JoinProperiesAndTypes().Join(
                Agents,
                propAndType => propAndType.MProperty.AgentId,
                agent => agent.Id,
                (propAndType, agent) => new MAgentPropertyType
                {
                    MAgent = agent,
                    MProperty = propAndType.MProperty,
                    MPropertyType = propAndType.MPropertyType
                }
            ).ToList().Cast<PropertyDbBase>().ToList();
        }
        //Úgy van elkészítve hogy a getter minden adatot visszaad az Id-k nélkül
        public List<PropertyDbBase> getAllData
        {
            get
            {
                return JoinAllTable().Cast<MAgentPropertyType>().Select(
                    record => new MAgentPropertyType()
                    {
                        MAgent = new Agent(record.MAgent),
                        MProperty = new Property(record.MProperty),
                        MPropertyType = new PropertyType(record.MPropertyType)
                    }
                ).Cast<PropertyDbBase>().ToList();
            }
        }
    }
}
