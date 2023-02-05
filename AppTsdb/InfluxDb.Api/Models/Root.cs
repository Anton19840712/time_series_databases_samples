using Newtonsoft.Json;

namespace InfluxDb.Api.Models;

public class Root
{
	[JsonProperty("results")]
	public List<Result>? Results { get; set; }
}