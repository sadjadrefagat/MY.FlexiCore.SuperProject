using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MY.FlexiCore.Infrastructure.SystemEntities.DtatabaseEngines
{
	sealed public class SqlServerEngineRegister : IDatabaseEngineRegistrar
	{
		public void Configure(IServiceCollection services, string connStr)
		{
			services.AddDbContext<MyDbContext>(options =>
						   options.UseSqlServer(connStr));
		}
	}
}
