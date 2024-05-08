using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    // Helper class for managing database-related operations
    public class DatabaseHelper
    {
        // Method to retrieve the connection string from the appSettings.json file
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            string? connectionString = builder.Build().GetConnectionString("MyConfig");

            return connectionString;
        }
    }

}
