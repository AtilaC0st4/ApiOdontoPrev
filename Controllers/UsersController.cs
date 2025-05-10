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
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.Users.Include(u => u.BrushingRecords).ToList();
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
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Include(u => u.BrushingRecords).FirstOrDefault(u => u.Id == id);
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
        public ActionResult<User> PostUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Os dados do usuário são inválidos.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

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
        public ActionResult<User> PutUser(int id, User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do usuário.");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Atualiza as propriedades do usuário existente com os novos valores
            existingUser.Name = updatedUser.Name;
            existingUser.Points = updatedUser.Points;

            _context.SaveChanges();

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
        public ActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

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
        public ActionResult<int> GetUserLevel(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user.Level);
        }
    }
}
