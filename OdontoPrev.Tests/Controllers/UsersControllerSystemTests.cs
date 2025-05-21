using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using Xunit;
using System.Net.Http.Json;


namespace OdontoPrev.OdontoPrev.Tests.Controllers
{
    public class UsersControllerSystemTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsersControllerSystemTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Full_User_System_Test()
        {
            // 1. Criar usuário
            var newUser = new UserDto
            {
                Name = "Teste Sistema",
                Points = 100,
                Cep = "01001000" // CEP válido para o teste (Sé - SP)
            };

            var postResponse = await _client.PostAsJsonAsync("/api/users", newUser);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            var createdUser = JsonConvert.DeserializeObject<UserDto>(await postResponse.Content.ReadAsStringAsync());
            Assert.NotNull(createdUser);
            Assert.Equal("Teste Sistema", createdUser.Name);
            var userId = createdUser.Id;

            // 2. Buscar usuário por ID
            var getResponse = await _client.GetAsync($"/api/users/{userId}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var userFromGet = JsonConvert.DeserializeObject<UserDto>(await getResponse.Content.ReadAsStringAsync());
            Assert.NotNull(userFromGet);
            Assert.Equal(userId, userFromGet.Id);

            // 3. Atualizar usuário
            var updatedUser = new
            {
                Id = userId,
                Name = "Usuário Atualizado",
                Points = 200,
                Cep = "01001000",
                Logradouro = createdUser.Logradouro,
                Bairro = createdUser.Bairro,
                Cidade = createdUser.Cidade,
                Estado = createdUser.Estado
            };

            var putResponse = await _client.PutAsJsonAsync($"/api/users/{userId}", updatedUser);
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);

            // 4. Buscar nível do usuário
            var levelResponse = await _client.GetAsync($"/api/users/{userId}/level");
            Assert.Equal(HttpStatusCode.OK, levelResponse.StatusCode);
            var level = int.Parse(await levelResponse.Content.ReadAsStringAsync());
            Assert.InRange(level, 0, 100); // Só como validação básica

            // 5. Deletar usuário
            var deleteResponse = await _client.DeleteAsync($"/api/users/{userId}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // 6. Confirmar exclusão
            var getAfterDelete = await _client.GetAsync($"/api/users/{userId}");
            Assert.Equal(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
        }
    }
}
