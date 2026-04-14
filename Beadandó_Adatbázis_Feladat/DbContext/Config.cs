using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.DbContext
{
    class Config
    {
        private static IConfigurationRoot _Config;
        
        static Config()
        {
            _Config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("DbContext/appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
        public static string ConnectionString => _Config.GetConnectionString("DefaultConnection");
    }
}
