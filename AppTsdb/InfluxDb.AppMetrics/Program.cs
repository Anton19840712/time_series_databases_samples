using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.InfluxDB;
using App.Metrics.Scheduling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var filter = new MetricsFilter().WhereType(MetricType.Timer);

var metrics = new MetricsBuilder().Report.ToInfluxDb(
	options =>
	{
		options.InfluxDb.BaseUri = new Uri("http://127.0.0.1:8086");
		options.InfluxDb.Database = "tick_test_database";
		options.InfluxDb.Consistenency = "any";
		options.InfluxDb.UserName = "admin";
		options.InfluxDb.Password = "password";
		options.InfluxDb.RetentionPolicy = "rp";
		options.InfluxDb.CreateDataBaseIfNotExists = true;
		options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
		options.HttpPolicy.FailuresBeforeBackoff = 5;
		options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
		options.MetricsOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter();
		options.Filter = filter;
		options.FlushInterval = TimeSpan.FromSeconds(20);
	}).Build();

var sheduler = new AppMetricsTaskScheduler(TimeSpan.FromSeconds(5), () =>
{
	var result = Task.WhenAll(metrics.ReportRunner.RunAllAsync());
	return result;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
