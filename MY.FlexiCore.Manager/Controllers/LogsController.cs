using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Infrastructure;

namespace MY.FlexiCore.Manager.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LogsController : ControllerBase
	{
		private readonly MyDbContext _db;

		public LogsController(MyDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ExecutionLog>>> GetAll(
			[FromQuery] string? entityName,
			[FromQuery] int? entityId,
			[FromQuery] string? level
			)
		{
			try
			{
				var query = _db.ExecutionLogs.AsQueryable();

				if (!string.IsNullOrWhiteSpace(entityName))
					query = query.Where(l => l.EntityName == entityName);

				if (entityId.HasValue)
					query = query.Where(l => l.EntityId == entityId);

				if (!string.IsNullOrWhiteSpace(level))
					query = query.Where(l => l.Level == level);

				var logs = await query
					.OrderByDescending(l => l.CreatedAt)
					.Take(200)
					.ToListAsync();

				return Ok(logs);
			}
			catch (Exception ex)
			{
				// نمایش دقیق خطا در خروجی
				return StatusCode(500, ex.ToString());
			}
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<ExecutionLog>> GetById(int id)
		{
			var log = await _db.ExecutionLogs.FirstOrDefaultAsync(l => l.Id == id);
			if (log == null) return NotFound();
			return Ok(log);
		}
	}
}
