using InfluxDB.Client.Core;

namespace InfluxDb.Api.Models;

[Measurement("mem")]
internal class Mem
{
	[Column("host", IsTag = true)] public string? Host { get; set; }
	[Column("used_percent")] public double? UsedPercent { get; set; }
	[Column(IsTimestamp = true)] public DateTime Time { get; set; }
	[Column("sequence_number")] public int SequenceNumber { get; set; }
}