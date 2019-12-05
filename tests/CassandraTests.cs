using NUnit.Framework;
using CassandraLogic.Models;
using CassandraLogic.Helpers;
using System.Linq;
using System.Net.Sockets;
using System;
using System.Diagnostics;

namespace Tests
{
    public class Tests
    {
        // TODO: fix this
        // [OneTimeSetUp]
        // public void WaitForCassandra()
        // {
        //     try
        //     {
        //         Console.WriteLine("Checking for DB port to be open...");

        //         bool connected = false;
        //         string command = "lsof -Pi commerce-test-db:9042 -sTCP:LISTEN | grep 9042";

        //         while (!connected)
        //         {
        //             var netstat = new Process
        //             {
        //                 StartInfo = new ProcessStartInfo
        //                 {
        //                     FileName = "/bin/bash",
        //                     Arguments = "-c \"" + command + "\"",
        //                     UseShellExecute = false,
        //                     RedirectStandardOutput = true,
        //                     CreateNoWindow = true
        //                 }
        //             };
        //             netstat.Start();
        //             netstat.WaitForExit();
        //             if (netstat.ExitCode == 0)
        //             {
        //                 connected = true;
        //             }
        //         }

        //         Console.WriteLine("Starting tests...");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         Environment.Exit(-1);
        //     }
        // }

        [Test]
        public void InsertTest()
        {
            System.Threading.Thread.Sleep(60000); // Sleep for 60s to giv the DB time to wake up
            ProductModel productModel = ProductModel.Generate();
            CassandraHelper cassandraHelper = new CassandraHelper("commerce-test-db", 9042);
            cassandraHelper.CreateCommerceKeyspace();
            cassandraHelper.CreateProductsTable();
            cassandraHelper.InsertData(productModel);

            ProductModel foundProductModel = cassandraHelper.GetData<ProductModel>("SELECT * FROM commerce.products WHERE ProductId = " + productModel.ProductId.ToString()).FirstOrDefault();

            Assert.That(foundProductModel, Is.Not.Null);
            Assert.That(foundProductModel.ProductName, Is.EqualTo(productModel.ProductName));
        }
    }
}