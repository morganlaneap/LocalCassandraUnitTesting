using System;
using Microsoft.Extensions.Configuration;
using CassandraLogic.Helpers;
using CassandraLogic.Models;
namespace CassandraLogic
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This is an example console app to insert data into Cassandra!");

            IConfigurationSection configuration = GetConfigSection(Environment.CurrentDirectory, "EnvironmentVariables");
            string cassandraContactPoint = configuration["CassandraContactPoint"];
            int cassandraPort = int.Parse(configuration["CassandraPort"]);

            CassandraHelper cassandraHelper = new CassandraHelper(cassandraContactPoint, cassandraPort);

            // Insert a record

            // Get a record
        }

        public static IConfigurationSection GetConfigSection(string directory, string section)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(directory)
                                .AddJsonFile($"appsettings.json", true, reloadOnChange: true)
                                .Build();
            return configuration.GetSection(section);
        }
    }
}
