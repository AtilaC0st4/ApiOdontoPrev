using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdontoPrev.Data;
using OdontoPrev.Models;

namespace OdontoPrev.Controllers
{
    /// <summary>
    /// Controller para gerenciar registros de escovação.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BrushingRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrushingRecordsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os registros de escovação.
        /// </summary>
        /// <returns>Uma lista de registros de escovação.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrushingRecord>>> GetBrushingRecords()
        {
            return await _context.BrushingRecords.ToListAsync();
        }

        /// <summary>
        /// Retorna um registro de escovação específico pelo ID.
        /// </summary>
        /// <param name="id">ID do registro de escovação.</param>
        /// <returns>O registro de escovação correspondente ao ID.</returns>
        /// <response code="200">Registro encontrado.</response>
        /// <response code="404">Registro não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<BrushingRecord>> GetBrushingRecord(int id)
        {
            var brushingRecord = await _context.BrushingRecords.FindAsync(id);

            if (brushingRecord == null)
            {
                return NotFound();
            }

            return brushingRecord;
        }

        /// <summary>
        /// Cria um novo registro de escovação.
        /// </summary>
        /// <param name="brushingRecord">Dados do registro de escovação a ser criado.</param>
        /// <returns>O registro de escovação criado.</returns>
        /// <response code="201">Registro criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPost]
        public async Task<ActionResult<BrushingRecord>> PostBrushingRecord(BrushingRecord brushingRecord)
        {
            var user = await _context.Users.FindAsync(brushingRecord.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Points += 10;

            _context.BrushingRecords.Add(brushingRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrushingRecord), new { id = brushingRecord.Id }, brushingRecord);
        }

        /// <summary>
        /// Atualiza um registro de escovação existente.
        /// </summary>
        /// <param name="id">ID do registro de escovação a ser atualizado.</param>
        /// <param name="brushingRecord">Novos dados do registro de escovação.</param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">Registro atualizado com sucesso.</response>
        /// <response code="400">ID inválido.</response>
        /// <response code="404">Registro não encontrado.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrushingRecord(int id, BrushingRecord brushingRecord)
        {
            if (id != brushingRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(brushingRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BrushingRecords.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um registro de escovação específico.
        /// </summary>
        /// <param name="id">ID do registro de escovação a ser excluído.</param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">Registro excluído com sucesso.</response>
        /// <response code="404">Registro não encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrushingRecord(int id)
        {
            var brushingRecord = await _context.BrushingRecords.FindAsync(id);
            if (brushingRecord == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(brushingRecord.UserId);
            if (user != null)
            {
                user.Points -= 10;
            }

            _context.BrushingRecords.Remove(brushingRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}