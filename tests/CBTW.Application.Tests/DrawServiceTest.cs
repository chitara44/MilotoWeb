using CBTW.Application.Interfaces.Repositories;
using CBTW.Application.Services.Implementations;
using CBTW.Domain.Entities;
using NSubstitute;

namespace CBTW.Application.Tests;

[TestFixture]
public class DrawServiceTests
{
    [Test]
    public async Task ValidateAsync_ReturnsFalse_WhenDuplicateExists()
    {
        // Arrange
        var repo = Substitute.For<IDrawRepository>();
        repo.GetAllAsync(Arg.Any<CancellationToken>()).Returns(new List<Draw>
        {
            new Draw { DrawId = 1, N1 = 1, N2 = 2, N3 = 3, N4 = 4, N5 = 5 }
        });

        var service = new DrawsService(repo);

        var candidate = new Draw { DrawId = 2, N1 = 1, N2 = 2, N3 = 3, N4 = 4, N5 = 5 };

        // Act
        var result = await service.ValidateAsync(candidate, CancellationToken.None);

        // Assert
        Assert.That (result, Is.False);
    }

    [Test]
    public async Task ValidateAsync_ReturnsTrue_WhenNumbersAreUnique()
    {
        // Arrange
        var repo = Substitute.For<IDrawRepository>();
        repo.GetAllAsync(Arg.Any<CancellationToken>()).Returns(new List<Draw>
        {
            new Draw { DrawId = 1, N1 = 5, N2 = 6, N3 = 7, N4 = 8, N5 = 9 }
        });

        var service = new DrawsService(repo);

        var candidate = new Draw { DrawId = 2, N1 = 1, N2 = 2, N3 = 3, N4 = 4, N5 = 5 };

        // Act
        var result = await service.ValidateAsync(candidate, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
    }
}





