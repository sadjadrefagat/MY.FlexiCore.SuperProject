using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MY.FlexiCore.Infrastructure.SystemEntities.DtatabaseEngines
{
	sealed public class MariaDBEngineRegistrar : IDatabaseEngineRegistrar
	{
		public void Configure(IServiceCollection services, string connStr)
		{
			services.AddDbContext<MyDbContext>(options =>
						   options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
		}
	}
}
