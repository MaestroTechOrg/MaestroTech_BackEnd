using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MaestroTech.API.Controllers;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

public class UsuariosControllerTests
{
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly UsuariosController _usuariosController;

    public UsuariosControllerTests()
    {
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _usuariosController = new UsuariosController(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfUsuarios()
    {
        // Arrange
        var usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "Usuario1", Email = "usuario1@example.com" },
            new Usuario { Id = 2, Nome = "Usuario2", Email = "usuario2@example.com" }
        };
        _mockUsuarioRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usuarios);

        // Act
        var result = await _usuariosController.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Usuario>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithUsuario()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, Nome = "Usuario1", Email = "usuario1@example.com" };
        _mockUsuarioRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(usuario);

        // Act
        var result = await _usuariosController.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Usuario>(okResult.Value);
        Assert.Equal(usuario.Id, returnValue.Id);
        Assert.Equal(usuario.Nome, returnValue.Nome);
        Assert.Equal(usuario.Email, returnValue.Email);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenUsuarioNotFound()
    {
        // Arrange
        _mockUsuarioRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Usuario)null);

        // Act
        var result = await _usuariosController.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult_WithCreatedUsuario()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, Nome = "Usuario1", Email = "usuario1@example.com" };
        _mockUsuarioRepository.Setup(repo => repo.AddAsync(usuario)).Returns(Task.CompletedTask);

        // Act
        var result = await _usuariosController.Create(usuario);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Usuario>(createdAtActionResult.Value);
        Assert.Equal(usuario.Id, returnValue.Id);
        Assert.Equal(usuario.Nome, returnValue.Nome);
        Assert.Equal(usuario.Email, returnValue.Email);
    }

    [Fact]
    public async Task Update_ReturnsNoContentResult()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, Nome = "Usuario1", Email = "usuario1@example.com" };
        _mockUsuarioRepository.Setup(repo => repo.UpdateAsync(usuario)).Returns(Task.CompletedTask);

        // Act
        var result = await _usuariosController.Update(1, usuario);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, Nome = "Usuario1", Email = "usuario1@example.com" };

        // Act
        var result = await _usuariosController.Update(2, usuario);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        // Arrange
        _mockUsuarioRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _usuariosController.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
