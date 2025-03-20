using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Data;
using OdontoPrev.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BrushingRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrushingRecordsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os registros de escovação.
        /// </summary>
        /// <returns>Uma lista de registros de escovação.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os registros de escovação", Description = "Retorna uma lista de todos os registros de escovação cadastrados.")]
        [ProducesResponseType(200, Type = typeof(List<BrushingRecord>))]
        public ActionResult<IEnumerable<BrushingRecord>> GetBrushingRecords()
        {
            return Ok(_context.BrushingRecords.ToList());
        }

        /// <summary>
        /// Obtém um registro de escovação pelo ID.
        /// </summary>
        /// <param name="id">O ID do registro de escovação.</param>
        /// <returns>O registro de escovação correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter um registro de escovação pelo ID", Description = "Retorna um registro de escovação específico com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(BrushingRecord))]
        [ProducesResponseType(404)]
        public ActionResult<BrushingRecord> GetBrushingRecord(int id)
        {
            var record = _context.BrushingRecords.FirstOrDefault(b => b.Id == id);
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
        [ProducesResponseType(201, Type = typeof(BrushingRecord))]
        [ProducesResponseType(400)]
        public ActionResult<BrushingRecord> PostBrushingRecord(BrushingRecord record)
        {
            _context.BrushingRecords.Add(record);
            _context.SaveChanges(); // Salva as alterações no banco de dados
            return CreatedAtAction(nameof(GetBrushingRecord), new { id = record.Id }, record);
        }

        /// <summary>
        /// Atualiza um registro de escovação existente pelo ID.
        /// </summary>
        /// <param name="id">O ID do registro de escovação a ser atualizado.</param>
        /// <param name="updatedRecord">Os dados atualizados do registro de escovação.</param>
        /// <returns>O registro de escovação atualizado.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um registro de escovação pelo ID", Description = "Atualiza os dados de um registro de escovação existente com base no ID fornecido.")]
        [ProducesResponseType(200, Type = typeof(BrushingRecord))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<BrushingRecord> PutBrushingRecord(int id, BrushingRecord updatedRecord)
        {
            if (id != updatedRecord.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do registro de escovação.");
            }

            var existingRecord = _context.BrushingRecords.FirstOrDefault(b => b.Id == id);
            if (existingRecord == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades do registro existente com os novos valores
            existingRecord.BrushingTime = updatedRecord.BrushingTime;
            existingRecord.Period = updatedRecord.Period;

            _context.SaveChanges(); // Salva as alterações no banco de dados

            return Ok(existingRecord);
        }

        /// <summary>
        /// Exclui um registro de escovação pelo ID.
        /// </summary>
        /// <param name="id">O ID do registro de escovação a ser excluído.</param>
        /// <returns>Nenhum conteúdo.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir um registro de escovação pelo ID", Description = "Remove um registro de escovação com base no ID fornecido.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteBrushingRecord(int id)
        {
            var record = _context.BrushingRecords.FirstOrDefault(b => b.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            _context.BrushingRecords.Remove(record);
            _context.SaveChanges(); // Salva as alterações no banco de dados
            return NoContent();
        }
    }
}