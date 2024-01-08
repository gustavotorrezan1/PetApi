using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;

namespace PetApi.Controllers;

[ApiController]
[Route("v1/categoria")]
public class CategoriaController : ControllerBase    
{
    private readonly PetDbContext _context;

    public CategoriaController(PetDbContext context)
    {
        _context = context;
    }

    // GET ALL:
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        try
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            return await _context.Categorias.ToListAsync();
        }
        catch(Exception e)
        {
            return StatusCode(500, e);
        }
       
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        try
        {
            var Categoria = await _context.Categorias.FindAsync(id);

            if (Categoria == null)
                return NotFound();
            
            return Categoria;
        }
        catch 
        {
            return StatusCode(500);
        }
       
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
    {
        if (_context.Categorias == null)
            return Problem("Entidade adiciona 'ApplicationDbContext.Categoria' é nula.");
        
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest("01xE4 - Id diferente do Id da categoria");
        }

        _context.Entry(categoria).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoriaExists(id))
                return NotFound("");
            

            return BadRequest("01xE5 - Falha ao alterar categoria");
            
        }

        return NoContent();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        if (_context.Categorias == null)
        {
            return NotFound();
        }
        var todo = await _context.Categorias.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        _context.Categorias.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool CategoriaExists(int id)
    {
        return (_context.Categorias?.Any(e => e.CategoriaId == id)).GetValueOrDefault();
    }




}