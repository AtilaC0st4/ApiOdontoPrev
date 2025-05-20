using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using OdontoPrev.Dtos;
using System.Net.Http.Json;

public class UsersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        var newUser = new UserDto
        {
            Name = "João Teste",
            Points = 100,
            Cep = "01001000"
        };

        var postResponse = await _client.PostAsJsonAsync("/api/users", newUser);
        postResponse.EnsureSuccessStatusCode();

        var createdJson = await postResponse.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<UserDto>(createdJson);

        var getResponse = await _client.GetAsync($"/api/users/{createdUser.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var jsonString = await getResponse.Content.ReadAsStringAsync();
        var userDto = JsonConvert.DeserializeObject<UserDto>(jsonString);

        Assert.NotNull(userDto);
        Assert.Equal(createdUser.Id, userDto.Id);
        Assert.Equal(newUser.Name, userDto.Name);
    }
}
