using Microsoft.AspNetCore.Mvc;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MaestroTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policy = "AdminPolicy")]
    public class CultosController : ControllerBase
    {
        private readonly ICultoRepository _cultoRepository;

        public CultosController(ICultoRepository cultoRepository)
        {
            _cultoRepository = cultoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cultos = await _cultoRepository.GetAllAsync();
            return Ok(cultos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var culto = await _cultoRepository.GetByIdAsync(id);
            if (culto == null) return NotFound();
            return Ok(culto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Culto culto)
        {
            await _cultoRepository.AddAsync(culto);
            return CreatedAtAction(nameof(GetById), new { id = culto.Id }, culto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Culto culto)
        {
            if (id != culto.Id) return BadRequest();
            await _cultoRepository.UpdateAsync(culto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cultoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
