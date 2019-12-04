using Bogus;
using System;
namespace CassandraLogic.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductModel()
        {
            Faker<ProductModel> productFaker = new Faker<ProductModel>();
            productFaker.RuleFor(o => o.ProductId, f => Guid.NewGuid());
            productFaker.RuleFor(o => o.ProductName, f => f.Commerce.ProductName());
            productFaker.RuleFor(o => o.ProductDescription, f => f.Commerce.ProductAdjective());
            productFaker.RuleFor(o => o.CreatedAt, f => f.Date.Past());
            ProductModel fakeModel = productFaker.Generate();
            this.ProductId = fakeModel.ProductId;
            this.ProductName = fakeModel.ProductName;
            this.ProductDescription = fakeModel.ProductDescription;
            this.CreatedAt = fakeModel.CreatedAt;
        }
    }
}