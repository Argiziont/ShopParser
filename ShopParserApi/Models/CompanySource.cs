using System.Collections.Generic;

namespace ShopParserApi.Models
{
    public class CompanySource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Data base connections
        public ICollection<CompanyData> Companies { get; set; }
    }
}