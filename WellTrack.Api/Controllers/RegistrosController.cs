using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellTrack.Api.Data;
using WellTrack.Api.Dtos;
using WellTrack.Api.Models;


namespace WellTrack.Api.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class RegistrosController : ControllerBase
{
private readonly AppDbContext _context;


public RegistrosController(AppDbContext context)
{
_context = context;
}


[HttpGet]
public async Task<ActionResult<IEnumerable<RegistroBemEstar>>> GetAll()
{
return await _context.Registros.Include(r => r.Colaborador).ToListAsync();
}


[HttpGet("{id}")]
public async Task<ActionResult<RegistroBemEstar>> Get(int id)
{
var registro = await _context.Registros.Include(r => r.Colaborador)
.FirstOrDefaultAsync(r => r.Id == id);
if (registro == null) return NotFound();
return registro;
}

[HttpPost]
public async Task<IActionResult> Post([FromBody] RegistroBemEstarCreateDto dto)
{
    var registro = new RegistroBemEstar
    {
        Data = dto.Data,
        Nota = dto.Nota,
        Comentario = dto.Comentario,
        ColaboradorId = dto.ColaboradorId
    };

    _context.Registros.Add(registro);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = registro.Id }, registro);
}


[HttpPut("{id}")]
public async Task<IActionResult> Put(int id, [FromBody] RegistroBemEstarUpdateDto dto)
{
    var registro = await _context.Registros.FindAsync(id);
    if (registro == null) return NotFound();

    registro.Data = dto.Data;
    registro.Nota = dto.Nota;
    registro.Comentario = dto.Comentario;
    registro.ColaboradorId = dto.ColaboradorId;

    await _context.SaveChangesAsync();
    return NoContent();
}



[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
var registro = await _context.Registros.FindAsync(id);
if (registro == null) return NotFound();
_context.Registros.Remove(registro);
await _context.SaveChangesAsync();
return NoContent();
}
}
}
