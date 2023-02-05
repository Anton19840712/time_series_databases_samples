using InfluxDb.Api.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace InfluxDb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InfluxDbController : ControllerBase
{
	private readonly string _token;

	public InfluxDbController(IConfiguration configuration)
	{
		_token = configuration.GetValue<string>("InfluxDB:Token");
	}

	[HttpPost]
	[Obsolete("Obsolete")]
	public void Write()
	{
		const string bucket = "test_bucket";
		const string org = "test_org";

		//create
		var client = InfluxDBClientFactory.Create("http://localhost:8099", _token.ToCharArray());

		//const string data = "mem,host=host1 used_percent=23.43234543";

		//then write using line protocol
		//Модель InfluxDB отличается от других решений для временных рядов, таких как Graphite, RRD.InfluxDB имеет линейный протокол для отправки данных временных рядов, который принимает следующую форму.

		//	имя - измерения — строка
		//tag - set — набор пар ключ / значение(где значения могут быть строкой)
		//field - set — набор пар ключ / значение(где значения могут быть int64, float64, bool или string)
		//отметка времени
		using var writeApi = client.GetWriteApi();

		////execute writing...:
		//writeApi.WriteRecord(data, WritePrecision.Ns, bucket, org);

		////then write using data point
		//var point = PointData
		//	.Measurement("mem")
		//	.Tag("host", "host1")
		//	.Field("used_percent", 23.43234543)
		//	.Timestamp(DateTime.UtcNow, WritePrecision.Ns);

		//writeApi.WritePoint(point, bucket, org);
		
		//execute writing...:
		for (var i = 0; i < 300; i++)
		{
			var mem = new Mem { Host = "host1", UsedPercent = 23.43234543, Time = DateTime.UtcNow, SequenceNumber = i};

			writeApi.WriteMeasurement(mem, WritePrecision.Ns, bucket, org);
		}
		
		//var query = "from(bucket:\"{bucket}\") |> range(start: -1h)";

		//var tables = await client.GetQueryApi().QueryAsync(query, org);

		//var elements = tables.SelectMany(x => x.Records);
	}
}