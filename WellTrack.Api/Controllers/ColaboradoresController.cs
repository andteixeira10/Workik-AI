using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellTrack.Api.Data;
using WellTrack.Api.Models;


namespace WellTrack.Api.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class ColaboradoresController : ControllerBase
{
private readonly AppDbContext _context;


public ColaboradoresController(AppDbContext context)
{
_context = context;
}


[HttpGet]
public async Task<ActionResult<IEnumerable<Colaborador>>> GetAll()
{
return await _context.Colaboradores.ToListAsync();
}


[HttpGet("{id}")]
public async Task<ActionResult<Colaborador>> Get(int id)
{
var colaborador = await _context.Colaboradores.FindAsync(id);
if (colaborador == null) return NotFound();
return colaborador;
}


[HttpPost]
public async Task<ActionResult<Colaborador>> Post(Colaborador colaborador)
{
_context.Colaboradores.Add(colaborador);
await _context.SaveChangesAsync();
return CreatedAtAction(nameof(Get), new { id = colaborador.Id }, colaborador);
}


[HttpPut("{id}")]
public async Task<IActionResult> Put(int id, Colaborador colaborador)
{
if (id != colaborador.Id) return BadRequest();
_context.Entry(colaborador).State = EntityState.Modified;
await _context.SaveChangesAsync();
return NoContent();
}


[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
var colaborador = await _context.Colaboradores.FindAsync(id);
if (colaborador == null) return NotFound();
_context.Colaboradores.Remove(colaborador);
await _context.SaveChangesAsync();
return NoContent();
}
}
}