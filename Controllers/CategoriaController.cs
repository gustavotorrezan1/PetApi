using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Extensions;
using PetApi.Models;
using PetApi.ViewModels;
using PetApi.ViewModels.CategoriaVM;

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
                return NotFound(new ResultViewModel<Categoria>("Categoria-E02 = falha interna no servidor"));

            var categorias = await _context.Categorias.ToListAsync();

            return Ok(new ResultViewModel<List<Categoria>>(categorias));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Categoria>>("Categoria-E03 = falha interna no servidor"));
        }
       
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Categoria>("Categoria-E04 = falha interna no servidor"));

        try
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound(new ResultViewModel<Categoria>("Categoria-E05 = categoria é nula"));
            
            return Ok(new ResultViewModel<Categoria>(categoria));
        }
        catch 
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Categoria-E06 = falha interna no servidor"));
        }

    }

    // POST
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(CreateCategoriaVM categoriaVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Categoria>("Falha interna no servidor"));

        if (_context.Categorias == null)
            return BadRequest(new ResultViewModel<Categoria>("Categoria-E08 = Categoria é nula"));
        
        var categoria = new Categoria{
            CategoriaId = 0,
            Ativo = 0,
            Nome = categoriaVM.Nome
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCategoria", new { id = categoria.CategoriaId }, new ResultViewModel<Categoria>(categoria));
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateCategoriaVM categoriaVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Categoria>(ModelState.GetErrors()));

        var categoria = new Categoria
        {
            CategoriaId = id,
            Nome = categoriaVM.Nome,
            Ativo = categoriaVM.Ativo
        };

        if (id != categoria.CategoriaId)
            return BadRequest(new ResultViewModel<Categoria>(ModelState.GetErrors()));

        _context.Entry(categoria).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoriaExists(id))
                return NotFound(new ResultViewModel<Categoria>("Categoria não existe"));
    
            return BadRequest(new ResultViewModel<Categoria>("Falha ao alterar categoria"));
            
        }
        return Ok(new ResultViewModel<Categoria>(categoria));
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Categoria>("Erro"));

        try
        {
            var categoria = await _context.Categorias.FindAsync(id);
            
            if(categoria == null)
                return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Erro ao excluir categoria"));
        }

        return Ok(new ResultViewModel<Categoria>("Categoria excluída"));
    }
    private bool CategoriaExists(int id)
    {
        return (_context.Categorias?.Any(e => e.CategoriaId == id)).GetValueOrDefault();
    }




}