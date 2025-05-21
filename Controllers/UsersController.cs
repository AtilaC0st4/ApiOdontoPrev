using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdontoPrev.Data;
using OdontoPrev.Dtos;
using OdontoPrev.Models;
using OdontoPrev.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http.Json;

namespace OdontoPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly MlModelService _mlService;
        private readonly MotivationService _motivationService;

        public UsersController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _mlService = new MlModelService(); // ou usar DI se preferir
            _motivationService = new MotivationService();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.BrushingRecords).ToListAsync();

            if (!users.Any())
                return NotFound("Nenhum usuário encontrado.");

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Points = user.Points,
                Cep = user.Cep,
                Logradouro = user.Logradouro,
                Bairro = user.Bairro,
                Cidade = user.Cidade,
                Estado = user.Estado,
                BrushingRecords = user.BrushingRecords?.Select(b => new BrushingRecordDto
                {
                    Id = b.Id,
                    BrushingTime = b.BrushingTime,
                    UserId = b.UserId
                }).ToList()
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter um usuário pelo ID", Description = "Retorna um usuário específico com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.BrushingRecords).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Points = user.Points,
                Cep = user.Cep,
                Logradouro = user.Logradouro,
                Bairro = user.Bairro,
                Cidade = user.Cidade,
                Estado = user.Estado,
                BrushingRecords = user.BrushingRecords?.Select(b => new BrushingRecordDto
                {
                    Id = b.Id,
                    BrushingTime = b.BrushingTime,
                    UserId = b.UserId
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criar um novo usuário", Description = "Adiciona um novo usuário à lista.")]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<UserDto>> PostUser([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Cep))
                return BadRequest("Os dados do usuário ou o CEP são inválidos.");

            ViaCepResponse endereco;
            try
            {
                var viaCepUrl = $"https://viacep.com.br/ws/{userDto.Cep}/json/";
                endereco = await _httpClient.GetFromJsonAsync<ViaCepResponse>(viaCepUrl);

                if (endereco == null || string.IsNullOrEmpty(endereco.Cep))
                    return BadRequest("CEP inválido ou não encontrado.");
            }
            catch
            {
                return BadRequest("Erro ao consultar o CEP.");
            }

            var user = new User
            {
                Name = userDto.Name,
                Points = userDto.Points,
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Bairro = endereco.Bairro,
                Cidade = endereco.Localidade,
                Estado = endereco.Uf
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdUserDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Points = user.Points,
                Cep = user.Cep,
                Logradouro = user.Logradouro,
                Bairro = user.Bairro,
                Cidade = user.Cidade,
                Estado = user.Estado,
                BrushingRecords = new List<BrushingRecordDto>()
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUserDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um usuário pelo ID", Description = "Atualiza os dados de um usuário existente.")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<User>> PutUser(int id, User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest("O ID da rota não corresponde ao ID do usuário.");

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
                return NotFound("Usuário não encontrado.");

            existingUser.Name = updatedUser.Name;
            existingUser.Points = updatedUser.Points;
            existingUser.Cep = updatedUser.Cep;
            existingUser.Logradouro = updatedUser.Logradouro;
            existingUser.Bairro = updatedUser.Bairro;
            existingUser.Cidade = updatedUser.Cidade;
            existingUser.Estado = updatedUser.Estado;

            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um usuário pelo ID", Description = "Remove um usuário com base no ID fornecido.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/level")]
        [SwaggerOperation(Summary = "Obter o nível do usuário", Description = "Retorna o nível do usuário com base na pontuação.")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<int>> GetUserLevel(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user.Level);
        }

        [HttpGet("{id}/predict-behavior")]
        [SwaggerOperation(Summary = "Prever o comportamento do usuário", Description = "Utiliza IA para prever se o usuário continuará ativo com base nos registros de escovação.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult PredictUserBehavior(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            var last7Days = DateTime.Now.AddDays(-7);
            var brushingCount = _context.BrushingRecords
                .Count(r => r.UserId == id && r.BrushingTime >= last7Days);

            var prediction = _mlService.Predict(user.Points, brushingCount);
            var message = _motivationService.GenerateMessage(user.Level, brushingCount, user.Points);

            return Ok(new
            {
                user.Id,
                user.Name,
                Level = user.Level,
                Points = user.Points,
                WeeklyBrushingCount = brushingCount,
                WillStayActive = prediction.Prediction,
                Probability = prediction.Probability,
                Message = message
            });
        }
    }
}
