using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Models;
using System.Collections.Generic;
using System.Linq;
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