using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Infrastructure;
using MY.FlexiCore.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
