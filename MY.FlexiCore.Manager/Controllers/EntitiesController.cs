using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Infrastructure;
using MY.FlexiCore.Infrastructure.Logging;
using MY.FlexiCore.Manager.Core.Hooks;
using MY.FlexiCore.Manager.Infrastructure.Services;

namespace MY.FlexiCore.Manager.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EntitiesController : ControllerBase
	{
		private readonly MyDbContext _db;
		private readonly ILogWriter _logWriter;

		public EntitiesController(MyDbContext db, ILogWriter logWriter)
		{
			_db = db;
			_logWriter = logWriter;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DynamicEntity>>> GetAll()
		{
			return Ok(await _db.DynamicEntities.Include(e => e.Fields).ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DynamicEntity>> GetById(int id)
		{
			var entity = await _db.DynamicEntities.Include(e => e.Fields)
												  .FirstOrDefaultAsync(e => e.Id == id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpPost]
		public async Task<ActionResult<DynamicEntity>> Create(DynamicEntity entity)
		{
			var ctx = new MYExecutionContext(_db, _logWriter, entity);
			var hooks = new MyEntityHooks();

			await hooks.BeforeSaveAsync(ctx);

			_db.DynamicEntities.Add(entity);
			await _db.SaveChangesAsync();

			await hooks.AfterSaveAsync(ctx);

			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, DynamicEntity updated)
		{
			var entity = await _db.DynamicEntities.Include(e => e.Fields)
												  .FirstOrDefaultAsync(e => e.Id == id);
			if (entity == null) return NotFound();

			entity.Title = updated.Title;
			entity.Name = updated.Name;
			entity.Fields = updated.Fields;

			var ctx = new MYExecutionContext(_db, _logWriter, entity);
			var hooks = new MyEntityHooks();

			await hooks.BeforeSaveAsync(ctx);

			await _db.SaveChangesAsync();

			await hooks.AfterSaveAsync(ctx);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var entity = await _db.DynamicEntities.FirstOrDefaultAsync(e => e.Id == id);
			if (entity == null) return NotFound();

			var ctx = new MYExecutionContext(_db, _logWriter, entity);
			var hooks = new MyEntityHooks();

			await hooks.BeforeDeleteAsync(ctx);

			_db.DynamicEntities.Remove(entity);
			await _db.SaveChangesAsync();

			await hooks.AfterDeleteAsync(ctx);

			return NoContent();
		}
	}
}
