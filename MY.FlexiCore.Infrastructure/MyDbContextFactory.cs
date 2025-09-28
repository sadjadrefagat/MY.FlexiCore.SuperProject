using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace MY.FlexiCore.Infrastructure
{
	public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
	{
		public MyDbContext CreateDbContext(string[] args)
		{
			// مسیر پروژه MY.FlexiCore.Manager
			var basePath = Path.GetFullPath(
				Path.Combine(Directory.GetCurrentDirectory(), "..", "MY.FlexiCore.Manager"));

			var configFilePath = Path.Combine(basePath, "appsettings.json");

			if (!File.Exists(configFilePath))
				throw new FileNotFoundException($"Cannot find appsettings.json at path: {configFilePath}");

			var configuration = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			var (dbEngine, register) = DatabaseEngineFactory.Create(configuration);

			var services = new ServiceCollection();
			register.Configure(services, dbEngine.ConnectionString);

			var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
			optionsBuilder.UseMySql(dbEngine.ConnectionString, ServerVersion.AutoDetect(dbEngine.ConnectionString));

			return new MyDbContext(optionsBuilder.Options);
		}
	}
}
