using Newtonsoft.Json;

namespace Catty.Api.Extensions
{
    public static class UtilityExtensions
    {
        public static string Serialize(this object payload) => JsonConvert.SerializeObject(payload);
    }
}
