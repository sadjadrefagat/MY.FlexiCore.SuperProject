using Microsoft.Extensions.DependencyInjection;

namespace MY.FlexiCore.Infrastructure
{
	public interface IDatabaseEngineRegistrar
	{
		void Configure(IServiceCollection services, string connStr);
	}
}
