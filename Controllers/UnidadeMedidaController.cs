using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;
using PetApi.ViewModels.UnidadeMedidaVM;

namespace PetApi.Controllers;

[ApiController]
[Route("v1/unidademedida")]
public class UnidadeMedidaController : ControllerBase
{
    private readonly PetDbContext _context;

    public UnidadeMedidaController(PetDbContext context)
    {
        _context = context;
    }

    // GET ALL:
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadeMedida>>> GetUnidadeMedidas()
    {
        try
        {
            if (_context.UnidadeMedidas == null)
                return NotFound();

            return await _context.UnidadeMedidas.ToListAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadeMedida>> GetUnidadeMedida(int id)
    {
        try
        {
            var UnidadeMedida = await _context.UnidadeMedidas.FindAsync(id);
            if (UnidadeMedida == null)
                return NotFound();

            return UnidadeMedida;
        }
        catch
        {
            return StatusCode(500);
        }
    }
    // POST
    [HttpPost]
    public async Task<ActionResult<UnidadeMedida>> PostUnidadeMedida(CreateUnidadeMedidaVM unidadeMedidaVM)
    {
        if (_context.UnidadeMedidas == null)
            return Problem("Entidade adiciona 'ApplicationDbContext.Categoria' é nula.");

        var unidadeMedida = new UnidadeMedida{
            UnidadeMedidaId = 0,
            Ativo = 0,
            Nome = unidadeMedidaVM.Nome,
            Sigla = unidadeMedidaVM.Sigla      
        };

        _context.UnidadeMedidas.Add(unidadeMedida);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUnidadeMedida", new { id = unidadeMedida.UnidadeMedidaId }, unidadeMedida);
    }
    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UnidadeMedida unidadeMedida)
    {
        if (id != unidadeMedida.UnidadeMedidaId)
            return BadRequest("01xE4 - Id diferente do Id da Unidade Medida");

        _context.Entry(unidadeMedida).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UnidadeMedidaExists(id))
                return NotFound("");

            return BadRequest("01xE5 - Falha ao alterar categoria");
        }

        return NoContent();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnidadeMedida(int id)
    {
        if (_context.UnidadeMedidas == null)
            return NotFound();
        
        var unidadeMedida = await _context.UnidadeMedidas.FindAsync(id);
        if (unidadeMedida == null)
            return NotFound();

        _context.UnidadeMedidas.Remove(unidadeMedida);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool UnidadeMedidaExists(int id)
    {
        return (_context.UnidadeMedidas?.Any(e => e.UnidadeMedidaId == id)).GetValueOrDefault();
    }




}