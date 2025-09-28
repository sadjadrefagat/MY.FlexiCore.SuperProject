using Microsoft.Extensions.Configuration;
using MY.FlexiCore.Core.Interfaces;

namespace MY.FlexiCore.Infrastructure
{
	static public class DatabaseEngineFactory
	{
		public static (IDatabaseEngine, IDatabaseEngineRegistrar) Create(IConfiguration config)
		{
			var provider = config.GetValue<string>("Database:Provider") ?? "";
			var connStr = config.GetValue<string>("Database:ConnectionString") ?? "";

			var engineType = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.FirstOrDefault(t =>
					typeof(IDatabaseEngine).IsAssignableFrom(t) &&
					!t.IsInterface && !t.IsAbstract &&
					string.Equals(
						Activator.CreateInstance(t, connStr) is IDatabaseEngine e ? e.Name : null,
						provider, StringComparison.OrdinalIgnoreCase));

			if (engineType == null)
				throw new Exception($"Unsupported provider: {provider}");

			var dbEngine = (IDatabaseEngine)Activator.CreateInstance(engineType, connStr)!;

			var registrarType = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.FirstOrDefault(t =>
					typeof(IDatabaseEngineRegistrar).IsAssignableFrom(t) &&
					!t.IsInterface && !t.IsAbstract &&
					t.Name.StartsWith(dbEngine.Name, StringComparison.OrdinalIgnoreCase));

			if (registrarType == null)
				throw new Exception($"No registrar found for {dbEngine.Name}");

			var registrar = (IDatabaseEngineRegistrar)Activator.CreateInstance(registrarType)!;

			return (dbEngine, registrar);
		}
	}
}

