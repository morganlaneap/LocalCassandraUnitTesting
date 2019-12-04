using NUnit.Framework;
using CassandraLogic.Models;
using CassandraLogic.Helpers;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void InsertTest()
        {
            System.Threading.Thread.Sleep(60000);
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