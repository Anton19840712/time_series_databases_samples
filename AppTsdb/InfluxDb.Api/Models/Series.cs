using Newtonsoft.Json;

namespace InfluxDb.Api.Models;

public class Series
{
	[JsonProperty("name")]
	public string? Name { get; set; }

	[JsonProperty("columns")]
	public List<string>? Columns { get; set; }

	[JsonProperty("values")]
	public List<List<object>>? Values { get; set; }
}