using Newtonsoft.Json;

namespace InfluxDb.Api.Models;

public class Result
{
	[JsonProperty("StatementId")]
	public int StatementId { get; set; }

	[JsonProperty("Series")]
	public List<Series>? Series { get; set; }
}