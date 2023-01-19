using Common;
using Models;
using Microsoft.AspNetCore.Mvc;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.repository = categoryRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repository.GetAll());
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var current = await repository.GetById(id);

            if(string.IsNullOrEmpty(current.Name))
                return NotFound();

            return Ok(current);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO objDto)
        {
            var current = await repository.GetById(id);

            if (string.IsNullOrEmpty(current.Name))
                return NotFound();

            current.Name = objDto.Name;
            current.Symbol = objDto.Symbol;

            return Ok(await repository.Update(current));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO objDto)
        {
            return Ok(await repository.Create(objDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var current = await repository.GetById(id);

            if (string.IsNullOrEmpty(current.Name))
                return NotFound();

            return Ok(await repository.Delete(id) > 0);
        }
    }
}
