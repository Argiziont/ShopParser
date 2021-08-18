using HotChocolate;
using HotChocolate.Types;
using ShopParserApi.Services.GraphQlServices.GeneratedService;

namespace ShopParserApi.Services.GraphQlServices
{
    public class SubscriptionObjectType
    {

        [Topic]
        [Subscribe]
        public ProductJson SubscribeProductGetDate([EventMessage] ProductJson product)
        {
            return product;
        }
    }
}