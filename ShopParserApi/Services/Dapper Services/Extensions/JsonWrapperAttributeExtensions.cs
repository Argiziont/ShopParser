using System;
using System.Linq;

namespace ShopParserApi.Services.Dapper_Services.Extensions
{
    public static class JsonWrapperAttributeExtensions
    {
        public static JsonWrapperAttribute GetAttributeCustom<T>() where T : class
        {
            try
            {
                return (JsonWrapperAttribute) typeof(T).GetCustomAttributes(typeof(JsonWrapperAttribute), false)
                    .FirstOrDefault();
            }
            catch (SystemException)
            {
                return null;
            }
        }

        public static bool ContainsAttribute<T>() where T : class
        {
            return typeof(T).IsDefined(typeof(JsonWrapperAttribute), true);
        }
    }
}