using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdontoPrev.Controllers;
using OdontoPrev.Data;
using OdontoPrev.Models;
using OdontoPrev.Dtos;
using System.Threading.Tasks;

public class UsersControllerTests
{
    private DbContextOptions<AppDbContext> GetInMemoryDbOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // DB único por teste
            .Options;
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var options = GetInMemoryDbOptions();
        using var context = new AppDbContext(options);

        var user = new User { Id = 1, Name = "João", Points = 100, Cep = "01001000" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var controller = new UsersController(context, httpClientFactoryMock.Object);

        // Act
        var result = await controller.GetUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal("João", dto.Name);
        Assert.Equal(100, dto.Points);
    }
}
