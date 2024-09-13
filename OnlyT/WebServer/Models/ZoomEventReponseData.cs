using Newtonsoft.Json;

namespace OnlyT.WebServer.Models;

internal sealed class ZoomEventResponseData
{
    [JsonProperty(PropertyName = "success")]
    public bool Success { get; set; }
}