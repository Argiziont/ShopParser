namespace ShopParserApi.Models.Helpers
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

        public ProductData Product { get; set; }
    }
}