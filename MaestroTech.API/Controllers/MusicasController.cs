using Microsoft.AspNetCore.Mvc;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MaestroTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MusicasController : ControllerBase
    {
        private readonly IMusicaRepository _musicaRepository;

        public MusicasController(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var musicas = await _musicaRepository.GetAllAsync();
            return Ok(musicas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var musica = await _musicaRepository.GetByIdAsync(id);
            if (musica == null) return NotFound();
            return Ok(musica);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Musica musica)
        {
            await _musicaRepository.AddAsync(musica);
            return CreatedAtAction(nameof(GetById), new { id = musica.Id }, musica);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Musica musica)
        {
            if (id != musica.Id) return BadRequest();
            await _musicaRepository.UpdateAsync(musica);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _musicaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
