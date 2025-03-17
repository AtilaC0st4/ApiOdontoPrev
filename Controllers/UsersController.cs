using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] 
    public class UsersController : ControllerBase
    {
        private static List<User> Users = new List<User>();

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os usuários", Description = "Retorna uma lista de todos os usuários cadastrados.")]
        [ProducesResponseType(200, Type = typeof(List<User>))] // Resposta de sucesso
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(Users);
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <returns>O usuário correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter um usuário pelo ID", Description = "Retorna um usuário específico com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(User))] // Resposta de sucesso
        [ProducesResponseType(404)] // Resposta de não encontrado
        public ActionResult<User> GetUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
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
        [ProducesResponseType(201, Type = typeof(User))] // Resposta de criação bem-sucedida
        [ProducesResponseType(400)] // Resposta de requisição inválida
        public ActionResult<User> PostUser(User user)
        {
            user.Id = Users.Count + 1;
            Users.Add(user);
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
        [ProducesResponseType(200, Type = typeof(User))] // Resposta de sucesso
        [ProducesResponseType(400)] // Requisição inválida (IDs não correspondem)
        [ProducesResponseType(404)] // Usuário não encontrado
        public ActionResult<User> PutUser(int id, User updatedUser)
        {
            // Verifica se o ID da rota corresponde ao ID do usuário enviado no corpo da requisição
            if (id != updatedUser.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do usuário.");
            }

            // Procura o usuário existente na lista
            var existingUser = Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Atualiza todas as propriedades do usuário existente com os novos valores
            existingUser.Name = updatedUser.Name;
            existingUser.Points = updatedUser.Points;
            // Adicione aqui outras propriedades que precisam ser atualizadas

            return Ok(existingUser);
        }

        /// <summary>
        /// Exclui um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser excluído.</param>
        /// <returns>Nenhum conteúdo.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um usuário pelo ID", Description = "Remove um usuário com base no ID fornecido.")]
        [ProducesResponseType(204)] // Resposta de sucesso sem conteúdo
        [ProducesResponseType(404)] // Resposta de não encontrado
        public ActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            Users.Remove(user);
            return NoContent();
        }
    }
}