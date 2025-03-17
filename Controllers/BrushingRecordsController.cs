using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BrushingRecordsController : ControllerBase
    {
        private static List<BrushingRecord> BrushingRecords = new List<BrushingRecord>();

        /// <summary>
        /// Obtém todos os registros de escovação.
        /// </summary>
        /// <returns>Uma lista de registros de escovação.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os registros de escovação", Description = "Retorna uma lista de todos os registros de escovação cadastrados.")]
        [ProducesResponseType(200, Type = typeof(List<BrushingRecord>))] // Resposta de sucesso
        public ActionResult<IEnumerable<BrushingRecord>> GetBrushingRecords()
        {
            return Ok(BrushingRecords);
        }

        /// <summary>
        /// Obtém um registro de escovação pelo ID.
        /// </summary>
        /// <param name="id">O ID do registro de escovação.</param>
        /// <returns>O registro de escovação correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter um registro de escovação pelo ID", Description = "Retorna um registro de escovação específico com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(BrushingRecord))] // Resposta de sucesso
        [ProducesResponseType(404)] // Resposta de não encontrado
        public ActionResult<BrushingRecord> GetBrushingRecord(int id)
        {
            var record = BrushingRecords.FirstOrDefault(b => b.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        /// <summary>
        /// Cria um novo registro de escovação.
        /// </summary>
        /// <param name="record">Os dados do registro de escovação a ser criado.</param>
        /// <returns>O registro de escovação criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Criar um novo registro de escovação", Description = "Adiciona um novo registro de escovação à lista.")]
        [ProducesResponseType(201, Type = typeof(BrushingRecord))] // Resposta de criação bem-sucedida
        [ProducesResponseType(400)] // Resposta de requisição inválida
        public ActionResult<BrushingRecord> PostBrushingRecord(BrushingRecord record)
        {
            record.Id = BrushingRecords.Count + 1;
            BrushingRecords.Add(record);
            return CreatedAtAction(nameof(GetBrushingRecord), new { id = record.Id }, record);
        }

        /// <summary>
        /// Exclui um registro de escovação pelo ID.
        /// </summary>
        /// <param name="id">O ID do registro de escovação a ser excluído.</param>
        /// <returns>Nenhum conteúdo.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um registro de escovação pelo ID", Description = "Remove um registro de escovação com base no ID fornecido.")]
        [ProducesResponseType(204)] // Resposta de sucesso sem conteúdo
        [ProducesResponseType(404)] // Resposta de não encontrado
        public ActionResult DeleteBrushingRecord(int id)
        {
            var record = BrushingRecords.FirstOrDefault(b => b.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            BrushingRecords.Remove(record);
            return NoContent();
        }
    }
}