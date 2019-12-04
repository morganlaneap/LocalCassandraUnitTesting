namespace CassandraLogic
{
    public class Constants
    {
        public const string KEYSPACE_NAME = "commerce";
        public const string PRODUCT_TABLE_NAME = "products";
        public const string CREATE_PRODUCTS_TABLE_CQL = "CREATE TABLE products (ProductId uuid, ProductName text, ProductDescription text, CreatedAt timestamp)";
    }
}