using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace tmp.src.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserRole
    {
        Common,
        Organization,
        Fund,
        Admin,
    }
}