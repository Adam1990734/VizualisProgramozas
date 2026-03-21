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
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
                options.UseSqlServer(DbContext.Config.ConnectionString);
        }
    }
}
