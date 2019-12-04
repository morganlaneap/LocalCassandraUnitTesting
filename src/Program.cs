using System;
using Microsoft.Extensions.Configuration;
using CassandraLogic.Helpers;
using CassandraLogic.Models;
using System.Collections.Generic;
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
            ProductModel newProduct = ProductModel.Generate();
            cassandraHelper.InsertData<ProductModel>(newProduct);

            // Get a record
            IEnumerable<ProductModel> allProducts = cassandraHelper.GetData<ProductModel>($"SELECT * FROM {Constants.KEYSPACE_NAME}.{Constants.PRODUCT_TABLE_NAME}");
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
