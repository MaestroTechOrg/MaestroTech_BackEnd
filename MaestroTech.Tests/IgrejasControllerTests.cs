using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MaestroTech.API.Controllers;
using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

public class IgrejasControllerTests
{
    private readonly Mock<IIgrejaRepository> _mockIgrejaRepository;
    private readonly IgrejasController _igrejasController;

    public IgrejasControllerTests()
    {
        _mockIgrejaRepository = new Mock<IIgrejaRepository>();
        _igrejasController = new IgrejasController(_mockIgrejaRepository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfIgrejas()
    {
        // Arrange
        var igrejas = new List<Igreja>
        {
            new Igreja { Id = 1, Nome = "Igreja1", Cidade = "Cidade1", UF = "UF1" },
            new Igreja { Id = 2, Nome = "Igreja2", Cidade = "Cidade2", UF = "UF2" }
        };
        _mockIgrejaRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(igrejas);

        // Act
        var result = await _igrejasController.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Igreja>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithIgreja()
    {
        // Arrange
        var igreja = new Igreja { Id = 1, Nome = "Igreja1", Cidade = "Cidade1", UF = "UF1" };
        _mockIgrejaRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(igreja);

        // Act
        var result = await _igrejasController.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Igreja>(okResult.Value);
        Assert.Equal(igreja.Id, returnValue.Id);
        Assert.Equal(igreja.Nome, returnValue.Nome);
        Assert.Equal(igreja.Cidade, returnValue.Cidade);
        Assert.Equal(igreja.UF, returnValue.UF);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenIgrejaNotFound()
    {
        // Arrange
        _mockIgrejaRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Igreja)null);

        // Act
        var result = await _igrejasController.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult_WithCreatedIgreja()
    {
        // Arrange
        var igreja = new Igreja { Id = 1, Nome = "Igreja1", Cidade = "Cidade1", UF = "UF1" };
        _mockIgrejaRepository.Setup(repo => repo.AddAsync(igreja)).Returns(Task.CompletedTask);

        // Act
        var result = await _igrejasController.Create(igreja);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Igreja>(createdAtActionResult.Value);
        Assert.Equal(igreja.Id, returnValue.Id);
        Assert.Equal(igreja.Nome, returnValue.Nome);
        Assert.Equal(igreja.Cidade, returnValue.Cidade);
        Assert.Equal(igreja.UF, returnValue.UF);
    }

    [Fact]
    public async Task Update_ReturnsNoContentResult()
    {
        // Arrange
        var igreja = new Igreja { Id = 1, Nome = "Igreja1", Cidade = "Cidade1", UF = "UF1" };
        _mockIgrejaRepository.Setup(repo => repo.UpdateAsync(igreja)).Returns(Task.CompletedTask);

        // Act
        var result = await _igrejasController.Update(1, igreja);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var igreja = new Igreja { Id = 1, Nome = "Igreja1", Cidade = "Cidade1", UF = "UF1" };

        // Act
        var result = await _igrejasController.Update(2, igreja);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        // Arrange
        _mockIgrejaRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _igrejasController.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
