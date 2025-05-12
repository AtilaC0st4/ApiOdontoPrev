using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [SwaggerOperation(Summary = "Listar todos os registros de escovação")]
        public async Task<ActionResult<IEnumerable<BrushingRecord>>> GetBrushingRecords()
        {
            return await _context.BrushingRecords.Include(b => b.User).ToListAsync();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Buscar um registro de escovação por ID")]
        public async Task<ActionResult<BrushingRecord>> GetBrushingRecord(int id)
        {
            var record = await _context.BrushingRecords.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
            if (record == null)
            {
                return NotFound("Registro não encontrado.");
            }

            return Ok(record);
        }

      [HttpPost]
[SwaggerOperation(Summary = "Criar um novo registro de escovação")]
public async Task<ActionResult<BrushingRecord>> PostBrushingRecord(BrushingRecord record)
{
    if (record == null)
    {
        return BadRequest("Dados inválidos.");
    }

    // Encontra o usuário
    var user = await _context.Users.FindAsync(record.UserId);

    if (user == null)
    {
        return NotFound("Usuário não encontrado.");
    }

    // Adiciona o novo registro de escovação
    _context.BrushingRecords.Add(record);

    // Atualiza os pontos do usuário
    user.AddPoints(10); // Adiciona 10 pontos pela escovação

    // Atualiza o usuário
    _context.Users.Update(user);

    // Salva as alterações no banco de dados
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetBrushingRecord), new { id = record.Id }, record);
}



        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar um registro de escovação")]
        public async Task<ActionResult<BrushingRecord>> PutBrushingRecord(int id, BrushingRecord updatedRecord)
        {
            if (id != updatedRecord.Id)
            {
                return BadRequest("ID inconsistente.");
            }

            var existingRecord = await _context.BrushingRecords.FindAsync(id);
            if (existingRecord == null)
            {
                return NotFound("Registro não encontrado.");
            }

            existingRecord.BrushingTime = updatedRecord.BrushingTime;
            existingRecord.UserId = updatedRecord.UserId;

            await _context.SaveChangesAsync();

            return Ok(existingRecord);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletar um registro de escovação")]
        public async Task<IActionResult> DeleteBrushingRecord(int id)
        {
            var record = await _context.BrushingRecords.FindAsync(id);
            if (record == null)
            {
                return NotFound("Registro não encontrado.");
            }

            // Encontrar o usuário relacionado
            var user = await _context.Users.FindAsync(record.UserId);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Subtrair os pontos do usuário
            user.Points -= 10; // Remova 10 pontos (ou o valor correto de pontos que você atribui por escovação)

            // Remover o registro de escovação
            _context.BrushingRecords.Remove(record);

            // Forçar a atualização do usuário
            _context.Users.Update(user);

            // Salvar as mudanças no banco de dados
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
