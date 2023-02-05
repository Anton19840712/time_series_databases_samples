// You can generate a Token from the "Tokens Tab" in the UI
namespace InfluxDb.Business;

class Program
{
	public static void Main(string[] args)
	{
		const string token = "f_haxRGmM8cEC5tV3_dYxu3oVVPc2LIFqm7aHGgsfLn-q5ecGe7G7e3lWlzn8o1EfG1U7H_WMmqzDZ12ejj7aw==";
		const string bucket = "test_bucket";
		const string org = "test_org";

		var client = InfluxDBClientFactory.Create("http://localhost:8099", token.ToCharArray());
	}
}