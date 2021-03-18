namespace PrjModule25_Parser.Models.Helpers
{
    public class UrlEntry
    {
        public int Id { get; set; }
        public int? ShopId { get; set; }
        public ShopData Shop { get; set; }
        public string Url { get; set; }
        
    }
}