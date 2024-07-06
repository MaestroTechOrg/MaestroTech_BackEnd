using Microsoft.AspNetCore.Mvc;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MaestroTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policy = "AdminPolicy")]
    public class IgrejasController : ControllerBase
    {
        private readonly IIgrejaRepository _igrejaRepository;

        public IgrejasController(IIgrejaRepository igrejaRepository)
        {
            _igrejaRepository = igrejaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var igrejas = await _igrejaRepository.GetAllAsync();
            return Ok(igrejas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var igreja = await _igrejaRepository.GetByIdAsync(id);
            if (igreja == null) return NotFound();
            return Ok(igreja);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Igreja igreja)
        {
            await _igrejaRepository.AddAsync(igreja);
            return CreatedAtAction(nameof(GetById), new { id = igreja.Id }, igreja);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Igreja igreja)
        {
            if (id != igreja.Id) return BadRequest();
            await _igrejaRepository.UpdateAsync(igreja);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _igrejaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
