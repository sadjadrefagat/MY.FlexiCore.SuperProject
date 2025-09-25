using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Interfaces;

namespace MY.FlexiCore.Infrastructure.Services
{
	public class DynamicTableCreator<T>
		where T : DynamicMasterEntity
	{
		readonly private MyDbContext _dbContext;
		readonly private IDatabaseEngine _engine;

		public DynamicTableCreator(MyDbContext dbContext, IDatabaseEngine engine)
		{
			_dbContext = dbContext;
			_engine = engine;
		}

		async public Task CreateTable(T entity)
		{
			var sql = _engine.GetCreateTableQuery<T>(entity);
			await _dbContext.Database.ExecuteSqlRawAsync(sql);
		}
	}
}
