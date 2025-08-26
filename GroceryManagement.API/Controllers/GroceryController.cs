using GroceryManagement.API.Models;
using GroceryManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroceryController : ControllerBase
    {
        private readonly IGroceryService _service;

        public GroceryController(IGroceryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GroceryItem>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<GroceryItem> GetById(int id)
        {
            var item = _service.GetById(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<GroceryItem> Create(GroceryItem item)
        {
            var created = _service.Create(item);
            return CreatedAtRoute("GetById", new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GroceryItem item)
        {
            if (!_service.Update(id, item))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
}
