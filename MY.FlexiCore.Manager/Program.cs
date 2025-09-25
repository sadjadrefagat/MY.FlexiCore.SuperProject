using MY.FlexiCore.Infrastructure.Logging;
using MY.FlexiCore.Utilities;

var builder = WebApplication.CreateBuilder(args);


var (dbEngine, register) = DatabaseEngineFactory.Create(builder.Configuration);
builder.Services.AddSingleton(dbEngine);
register.Configure(builder.Services, dbEngine.ConnectionString);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Log Writer
builder.Services.AddScoped<ILogWriter, DbLogWriter>();

// CORS برای React
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp",
		policy => policy.WithOrigins("http://localhost:3000")
						.AllowAnyHeader()
						.AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowReactApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
