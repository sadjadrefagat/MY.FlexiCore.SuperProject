using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Infrastructure;
using MY.FlexiCore.Infrastructure.Logging;
using MY.FlexiCore.Infrastructure.Services;
using MY.FlexiCore.Manager.Core.Hooks;

namespace MY.FlexiCore.Manager.Controllers
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
		public async Task<ActionResult<IEnumerable<DynamicMasterEntity>>> GetAll()
		{
			return Ok(await _db.DynamicMasterEntity.Include(e => e.FooterFields).ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DynamicMasterEntity>> GetById(int id)
		{
			var entity = await _db.DynamicMasterEntity.Include(e => e.FooterFields)
												  .FirstOrDefaultAsync(e => e.Id == id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpPost]
		public async Task<ActionResult<DynamicMasterEntity>> Create(DynamicMasterEntity entity)
		{
			var ctx = new MYExecutionContext(_db, entity, _logWriter); // ✅ نسخه جدید
			var hooks = new MyEntityHooks();

			await hooks.BeforeSaveAsync(ctx);

			_db.DynamicMasterEntity.Add(entity);
			await _db.SaveChangesAsync();

			ctx.Log($"موجودیت '{entity.Title}' ایجاد شد.", "Info");
			await hooks.AfterSaveAsync(ctx);

			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, DynamicMasterEntity updated)
		{
			var entity = await _db.DynamicMasterEntity.Include(e => e.FooterFields)
												  .FirstOrDefaultAsync(e => e.Id == id);
			if (entity == null) return NotFound();

			entity.Title = updated.Title;
			entity.Name = updated.Name;
			entity.FooterFields = updated.FooterFields;

			var ctx = new MYExecutionContext(_db, entity, _logWriter); // ✅ نسخه جدید
			var hooks = new MyEntityHooks();

			await hooks.BeforeSaveAsync(ctx);

			await _db.SaveChangesAsync();

			ctx.Log($"موجودیت '{entity.Title}' ویرایش شد.", "Info");
			await hooks.AfterSaveAsync(ctx);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var entity = await _db.DynamicMasterEntity.FirstOrDefaultAsync(e => e.Id == id);
			if (entity == null) return NotFound();

			var ctx = new MYExecutionContext(_db, entity, _logWriter); // ✅ نسخه جدید
			var hooks = new MyEntityHooks();

			await hooks.BeforeDeleteAsync(ctx);

			_db.DynamicMasterEntity.Remove(entity);
			await _db.SaveChangesAsync();

			ctx.Log($"موجودیت '{entity.Title}' حذف شد.", "Warning");
			await hooks.AfterDeleteAsync(ctx);

			return NoContent();
		}

		[HttpPost("{id}/publish")]
		public async Task<ActionResult> Publish(int id)
		{
			var entity = await _db.DynamicMasterEntity
				.Include(e => e.FooterFields)
				.FirstOrDefaultAsync(e => e.Id == id);

			if (entity == null) return NotFound();

			var ctx = new MYExecutionContext(_db, entity, _logWriter); // ✅ نسخه جدید

			ctx.Log($"موجودیت '{entity.Title}' منتشر شد.", "Info");


			entity.IsPublished = true;

			//TODO:Create schema in database

			await _db.SaveChangesAsync();

			return NoContent();
		}
	}
}
