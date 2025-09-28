using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Entities.FieldTypes;
using MY.FlexiCore.Core.Enums;
using MY.FlexiCore.Core.Interfaces;
using MY.FlexiCore.Infrastructure;
using MY.FlexiCore.Infrastructure.Logging;
using MY.FlexiCore.Manager.Controllers;
using System.Reflection;

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


// رجیستر تمامی انواع داده‌ای تعریف شده در برنامه
#region DataTypes Registration
var DataTypes = new Dictionary<FieldTypes, BaseDataType>();

var dbDataTypes = db1.Set<BaseDataType>().ToList();
var typesAssembly = Assembly.GetAssembly(typeof(BaseDataType));
var sysTypes = typesAssembly?.GetTypes()
	.Where(t => t.BaseType == typeof(BaseDataType))
	.ToArray() ?? Array.Empty<Type>();

foreach (var systemDataType in sysTypes)
	if (Activator.CreateInstance(systemDataType) is BaseDataType ii)
	{
		var x = dbDataTypes.FirstOrDefault(d => d.FieldType == ii.FieldType);

		if (x == null)
		{
			db1.Set<BaseDataType>().Add(ii);
			db1.SaveChanges();
			x = ii;
		}

		DataTypes.Add(ii.FieldType, x);
	}
#endregion


#region Test
//---------------------------تست------------------------------
// تست قبل از اجرای اپ
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var db1 = services.GetRequiredService<MyDbContext>();
	var logWriter1 = services.GetRequiredService<ILogWriter>();
	var dbEngine1 = services.GetRequiredService<IDatabaseEngine>();

	var controller = new EntitiesController(db1, logWriter1, dbEngine1);


	var testEntity = new DynamicMasterEntity
	{
		Title = "دوره زراعی",
		Name = "AgriculturePeriod",
		HeaderFields = new List<DynamicField>
		{
			new DynamicField
			{
				Name= "Name",
				Title = "نام دوره",
				DataType = DataTypes[FieldTypes.Integer],
			},
			new DynamicField
			{
				Name="Code",
				Title = "کد دوره",
				DataType = DataTypes[FieldTypes.String],
			}
		},
		FooterFields = new List<DynamicField>
		{
			new DynamicField
			{
				Name = "Description",
				Title = "توضیحات",
				DataType = DataTypes[FieldTypes.DateTime],
			}
		},
		Details = new List<DynamicDetailEntity>
		{
			new DynamicDetailEntity
			{
				Name = "AgriculturePeriodInputs",
				Title="نهاده ها",
				Fields = new List<DynamicField>
				{
					new DynamicField
					{
						Name= "Name",
						Title = "نام نهاده",
						DataType = DataTypes[FieldTypes.String],
					},
				},
				Items = new List<DynamicDetailItemEntity>(),
			}
		}
	};

	var result = await controller.Create(testEntity);

	Console.WriteLine("Create: " + result.ToString());
}
#endregion

app.Run();
