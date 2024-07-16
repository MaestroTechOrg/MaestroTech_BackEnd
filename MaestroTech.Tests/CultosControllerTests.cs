using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MaestroTech.API.Controllers;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

public class CultosControllerTests
{
    private readonly Mock<ICultoRepository> _mockCultoRepository;
    private readonly CultosController _cultosController;

    public CultosControllerTests()
    {
        _mockCultoRepository = new Mock<ICultoRepository>();
        _cultosController = new CultosController(_mockCultoRepository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfCultos()
    {
        // Arrange
        var cultos = new List<Culto>
        {
            new Culto { Id = 1, DiaDaSemana = "Domingo" },
            new Culto { Id = 2, DiaDaSemana = "Quarta-feira" }
        };
        _mockCultoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(cultos);

        // Act
        var result = await _cultosController.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Culto>>(okResult.Value);
        Assert.Equal(cultos.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithCulto()
    {
        // Arrange
        var culto = new Culto { Id = 1, DiaDaSemana = "Domingo" };
        _mockCultoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(culto);

        // Act
        var result = await _cultosController.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Culto>(okResult.Value);
        Assert.Equal(culto.Id, returnValue?.Id);
        Assert.Equal(culto.DiaDaSemana, returnValue?.DiaDaSemana);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenCultoNotFound()
    {
        // Arrange
        _mockCultoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Culto)null);

        // Act
        var result = await _cultosController.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult_WithCulto()
    {
        // Arrange
        var culto = new Culto { Id = 1, DiaDaSemana = "Domingo" };
        _mockCultoRepository.Setup(repo => repo.AddAsync(culto)).Returns(Task.CompletedTask);

        // Act
        var result = await _cultosController.Create(culto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Culto>(createdAtActionResult.Value);
        Assert.Equal(culto.Id, returnValue.Id);
        Assert.Equal(culto.DiaDaSemana, returnValue.DiaDaSemana);
    }

    [Fact]
    public async Task Update_ReturnsNoContentResult_WhenCultoIsUpdated()
    {
        // Arrange
        var culto = new Culto { Id = 1, DiaDaSemana = "Domingo" };
        _mockCultoRepository.Setup(repo => repo.UpdateAsync(culto)).Returns(Task.CompletedTask);

        // Act
        var result = await _cultosController.Update(1, culto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequestResult_WhenIdMismatch()
    {
        // Arrange
        var culto = new Culto { Id = 1, DiaDaSemana = "Domingo" };

        // Act
        var result = await _cultosController.Update(2, culto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult_WhenCultoIsDeleted()
    {
        // Arrange
        _mockCultoRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _cultosController.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
