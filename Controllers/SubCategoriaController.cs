using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;
using PetApi.ViewModels.SubcategoriaVM;

namespace PetApi.Controllers;

[ApiController]
[Route("v1/subcategoria")]
public class SubCategoriaController : ControllerBase    
{
    private readonly PetDbContext _context;

    public SubCategoriaController(PetDbContext context)
    {
        _context = context;
    }

    // GET ALL:
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubCategoria>>> GetSubCategorias()
    {
        try
        {
            if (_context.SubCategorias == null)
                return NotFound();
            
            return await _context.SubCategorias.ToListAsync();
        }
        catch(Exception e)
        {
            return StatusCode(500, e);
        }
       
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategoria>> GetSubCategoria(int id)
    {
        try
        {
            var subCategoria = await _context.SubCategorias.FindAsync(id);
            if (subCategoria == null)
                return Problem("Subcategoria nula");

            var categoria = await _context.Categorias.FindAsync(subCategoria.CategoriaId);
            subCategoria.Categoria = categoria;
            
            return subCategoria;
        }
        catch
        {
            return StatusCode(500);
        }
       
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<SubCategoria>> PostSubCategoria(CreateSubcategoriaVM subcategoriaVM)
    {
        if (_context.SubCategorias == null)
            return Problem("Entidade adiciona 'ApplicationDbContext.SubCategoria' é nula.");

        var subcategoria = new SubCategoria{
            SubCategoriaId = 0,
            Ativo = 0,
            Nome = subcategoriaVM.Nome,
            CategoriaId = subcategoriaVM.CategoriaId,
            Categoria = await _context.Categorias.FindAsync(subcategoriaVM.CategoriaId)
        };    
        
        if(subcategoria.Categoria == null)
            return Problem("Categoria não existe");

        _context.SubCategorias.Add(subcategoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubCategoria", new { id = subcategoria.SubCategoriaId }, subcategoria);
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateSubcategoriaVM subcategoriaVM)
    {
        var subcategoria = new SubCategoria
        {
            SubCategoriaId = id,
            Ativo = subcategoriaVM.Ativo,
            Nome = subcategoriaVM.Nome,
            CategoriaId = subcategoriaVM.CategoriaId
        };

        // se o CategoriaId for 0, ele pega o CategoriaId atual e coloca no lugar do CategoriaId = 0
        if (subcategoria.CategoriaId == 0)
            subcategoria.CategoriaId = await _context.SubCategorias.Where(x => x.SubCategoriaId == id).Select(x => x.CategoriaId).FirstOrDefaultAsync();

        _context.Entry(subcategoria).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubCategoriaExists(id))
                return NotFound("");
            
            return BadRequest("01xE5 - Falha ao alterar subcategoria");
            
        }

        return NoContent();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubCategoria(int id)
    {
        if (_context.SubCategorias == null)
            return NotFound();
        
        var subcategoria = await _context.SubCategorias.FindAsync(id);
        if (subcategoria == null)
            return NotFound();

        _context.SubCategorias.Remove(subcategoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool SubCategoriaExists(int id)
    {
        return (_context.SubCategorias?.Any(e => e.SubCategoriaId == id)).GetValueOrDefault();
    }




}