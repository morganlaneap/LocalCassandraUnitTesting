using Bogus;
using System;
namespace CassandraLogic.Models
{
    [Cassandra.Mapping.Attributes.Table("products")]
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ProductModel Generate()
        {
            Faker<ProductModel> productFaker = new Faker<ProductModel>();
            productFaker.RuleFor(o => o.ProductId, f => Guid.NewGuid());
            productFaker.RuleFor(o => o.ProductName, f => f.Commerce.ProductName());
            productFaker.RuleFor(o => o.ProductDescription, f => f.Commerce.ProductAdjective());
            productFaker.RuleFor(o => o.CreatedAt, f => f.Date.Past());
            return productFaker.Generate();
        }
    }
}