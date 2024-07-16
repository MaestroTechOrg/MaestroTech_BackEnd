using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MaestroTech.API.Controllers;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using System.Collections.Generic;

public class MusicasControllerTests
{
    private readonly Mock<IMusicaRepository> _mockMusicaRepository;
    private readonly MusicasController _musicasController;

    public MusicasControllerTests()
    {
        _mockMusicaRepository = new Mock<IMusicaRepository>();
        _musicasController = new MusicasController(_mockMusicaRepository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfMusicas()
    {
        // Arrange
        var musicas = new List<Musica>
        {
            new Musica { Id = 1, Nome = "Musica 1" },
            new Musica { Id = 2, Nome = "Musica 2" }
        };
        _mockMusicaRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(musicas);

        // Act
        var result = await _musicasController.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Musica>>(okResult.Value);
        Assert.Equal(musicas.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithMusica()
    {
        // Arrange
        var musica = new Musica { Id = 1, Nome = "Musica 1" };
        _mockMusicaRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(musica);

        // Act
        var result = await _musicasController.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Musica>(okResult.Value);
        Assert.Equal(musica.Id, returnValue?.Id);
        Assert.Equal(musica.Nome, returnValue?.Nome);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenMusicaNotFound()
    {
        // Arrange
        _mockMusicaRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Musica)null);

        // Act
        var result = await _musicasController.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult_WithMusica()
    {
        // Arrange
        var musica = new Musica { Id = 1, Nome = "Musica 1" };
        _mockMusicaRepository.Setup(repo => repo.AddAsync(musica)).Returns(Task.CompletedTask);

        // Act
        var result = await _musicasController.Create(musica);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Musica>(createdAtActionResult.Value);
        Assert.Equal(musica.Id, returnValue.Id);
        Assert.Equal(musica.Nome, returnValue.Nome);
    }

    [Fact]
    public async Task Update_ReturnsNoContentResult_WhenMusicaIsUpdated()
    {
        // Arrange
        var musica = new Musica { Id = 1, Nome = "Musica 1" };
        _mockMusicaRepository.Setup(repo => repo.UpdateAsync(musica)).Returns(Task.CompletedTask);

        // Act
        var result = await _musicasController.Update(1, musica);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequestResult_WhenIdMismatch()
    {
        // Arrange
        var musica = new Musica { Id = 1, Nome = "Musica 1" };

        // Act
        var result = await _musicasController.Update(2, musica);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult_WhenMusicaIsDeleted()
    {
        // Arrange
        _mockMusicaRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _musicasController.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
