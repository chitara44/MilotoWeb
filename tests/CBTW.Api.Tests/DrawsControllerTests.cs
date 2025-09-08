using CBTW.Api.Controllers;
using CBTW.Application.Interfaces.Repositories;
using CBTW.Application.Interfaces.Services;
using CBTW.Application.Services.Implementations;
using CBTW.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CBTW.Api.Tests;


[TestFixture]
public class DrawsControllerTests
{
    private DrawsController _controller;
    private DrawsService _service;

    [SetUp]
    public void SetUp()
    {
        _service = Substitute.For<DrawsService>(Substitute.For<IDrawRepository>());
        _controller = new DrawsController(_service);
    }

    [Test]
    public async Task Get_ReturnsOkWithDraws()
    {
        // Arrange
        var draws = new List<Draw>
            {
                new Draw { DrawId = 1, N1 = 1, N2 = 2, N3 = 3, N4 = 4, N5 = 5 },
                new Draw { DrawId = 2, N1 = 6, N2 = 7, N3 = 8, N4 = 9, N5 = 10 }
            };
        _service.GetAllAsync(Arg.Any<CancellationToken>()).Returns(draws);

        // Act
        var result = await _controller.Get(CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(draws));
    }
}

