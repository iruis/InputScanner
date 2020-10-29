using Newtonsoft.Json;

namespace InputScanner.JsonObject
{
    public interface BaseObject
    {
        [JsonProperty("kind")]
        string Kind { get; }
    }
}
