using Microsoft.AspNetCore.Mvc;
using MY.FlexiCore.Core.Entities;

namespace MY.FlexiCore.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EntitiesController : ControllerBase
	{
		private static List<DynamicEntity> _entities = new();

		[HttpGet]
		public ActionResult<IEnumerable<DynamicEntity>> GetAll() => Ok(_entities);

		[HttpGet("{id}")]
		public ActionResult<DynamicEntity> GetById(int id)
		{
			var entity = _entities.FirstOrDefault(e => e.Id == id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpPost]
		public ActionResult<DynamicEntity> Create(DynamicEntity entity)
		{
			entity.Id = _entities.Count > 0 ? _entities.Max(e => e.Id) + 1 : 1;
			_entities.Add(entity);
			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
		}

		[HttpPut("{id}")]
		public ActionResult Update(int id, DynamicEntity updated)
		{
			var entity = _entities.FirstOrDefault(e => e.Id == id);
			if (entity == null) return NotFound();
			entity.Title = updated.Title;
			entity.Name = updated.Name;
			entity.Fields = updated.Fields;
			return NoContent();
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var entity = _entities.FirstOrDefault(e => e.Id == id);
			if (entity == null) return NotFound();
			_entities.Remove(entity);
			return NoContent();
		}
	}
}
