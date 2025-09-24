using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Infrastructure;
using MY.FlexiCore.Infrastructure.Logging;
using MY.FlexiCore.Manager.Core.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseMySql(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
	)
);

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


//------------ تست ---------------
var partEntity = new DynamicMasterEntity()
{
	Title = "کالا",
	Name = "Part",
	HasLogicalDelete = false,
	HasStateMachine = true,

	HeaderFields = new List<DynamicField>()
	{
		new DynamicField(){Title="کد کالا", Name="Code", DataType = FieldTypes.Text, IsRequired=true},
		new DynamicField(){Title="نام کالا", Name="Name", DataType = FieldTypes.Text, IsRequired=true},
		new DynamicField(){Title="موجودی اولیه", Name="InitialValue", DataType = FieldTypes.Integer, IsRequired=true},
	},

	FooterFields = new List<DynamicField>()
	{
		new DynamicField(){Title="توضیحات", Name="Description", DataType = FieldTypes.Text, IsRequired=false},
	},

	Details = new List<DynamicDetailEntity>()
	{
		new DynamicDetailEntity()
		{
			Title="مشخصات فنی کالا",
			Name="TechnicalSpecifications",
			HasLogicalDelete=false,
			HasStateMachine=false,
			Fields = new List<DynamicField>()
			{
				new DynamicField()
				{
					Title="عنوان",
					Name="Title",
					DataType = FieldTypes.Text,
					IsRequired=true,
				},
				new DynamicField()
				{
					Title="مقدار",
					Name="Value",
					DataType = FieldTypes.Text,
					IsRequired=true,
				},
			},

			Items = new List<DynamicDetailItemEntity>()
			{
				new DynamicDetailItemEntity()
				{
					Title="شرح فنی",
					Name="TechnicalDescription",
					HasStateMachine = false,
					HasLogicalDelete = false,
					Fields = new List<DynamicField>()
					{
						new DynamicField()
						{
							Title="توضیحات",
							Name="Description",
							DataType = FieldTypes.Text,
						},
					},
				},
			},

		},
	}
};








app.Run();
