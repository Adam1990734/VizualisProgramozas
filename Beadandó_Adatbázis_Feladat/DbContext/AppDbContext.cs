using System;
using System.Collections.Generic;
using System.Text;
using Beadandó_Adatbázis_Feladat.Models;
using Microsoft.EntityFrameworkCore;

namespace Beadandó_Adatbázis_Feladat
{
    class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //Adatbázis elemek:
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(DbContext.Config.ConnectionString);
        }
    }
}
