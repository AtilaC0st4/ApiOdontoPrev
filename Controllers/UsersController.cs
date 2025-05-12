using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Data;
using OdontoPrev.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;

namespace OdontoPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                                      .Include(u => u.BrushingRecords)
                                      .ToListAsync();

            if (!users.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(users);
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <returns>O usuário correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter um usuário pelo ID", Description = "Retorna um usuário específico com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                                     .Include(u => u.BrushingRecords)
                                     .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">Os dados do usuário a ser criado.</param>
        /// <returns>O usuário criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Criar um novo usuário", Description = "Adiciona um novo usuário à lista.")]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Os dados do usuário são inválidos.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// Atualiza um usuário existente pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser atualizado.</param>
        /// <param name="updatedUser">Os dados atualizados do usuário.</param>
        /// <returns>O usuário atualizado.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um usuário pelo ID", Description = "Atualiza os dados de um usuário existente com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<User>> PutUser(int id, User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do usuário.");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Points = updatedUser.Points;

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }

        /// <summary>
        /// Exclui um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser excluído.</param>
        /// <returns>Nenhum conteúdo.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um usuário pelo ID", Description = "Remove um usuário com base no ID fornecido.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Obtém o nível de um usuário com base nos pontos.
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <returns>O nível do usuário calculado com base nos pontos.</returns>
        [HttpGet("{id}/level")]
        [SwaggerOperation(Summary = "Obter o nível do usuário", Description = "Retorna o nível do usuário com base na pontuação.")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<int>> GetUserLevel(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user.Level);
        }
    }
}
