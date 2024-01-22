using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;
using PetApi.ViewModels;
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
                return NotFound(new ResultViewModel<SubCategoria>("Não existe nenhuma subcategoria"));
                
            var subcategorias = await _context.SubCategorias.Include(x => x.Categoria).ToListAsync();
            return Ok(new ResultViewModel<List<SubCategoria>>(subcategorias));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<SubCategoria>("Falha interna do servidor"));
        }
       
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategoria>> GetSubCategoria(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<SubCategoria>("Falha interna no servidor"));
        try
        {
            var subcategoria = await _context.SubCategorias.Include(x => x.Categoria)
            .FirstOrDefaultAsync(x => x.SubCategoriaId == id);
            if (subcategoria == null)
                return NotFound(new ResultViewModel<SubCategoria>("Não existe essa subcategoria"));

            return Ok(new ResultViewModel<SubCategoria>(subcategoria));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<SubCategoria>("Falha interna do servidor"));
        } 
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<SubCategoria>> PostSubCategoria(CreateSubcategoriaVM subcategoriaVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<SubCategoria>("Falha interna no servidor"));

        if (_context.SubCategorias == null)
            return NotFound(new ResultViewModel<SubCategoria>("Subcategoria nula"));

        try
        {
            var subcategoria = new SubCategoria
            {
                SubCategoriaId = 0,
                Ativo = 0,
                Nome = subcategoriaVM.Nome,
                CategoriaId = subcategoriaVM.CategoriaId,
                Categoria = await _context.Categorias.FindAsync(subcategoriaVM.CategoriaId)
            };

            _context.SubCategorias.Add(subcategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoria", new { id = subcategoria.SubCategoriaId }, new ResultViewModel<SubCategoria>(subcategoria));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<SubCategoria>("Falha interna do servidor"));
        }
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateSubcategoriaVM subcategoriaVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<SubCategoria>("Falha interna no servidor"));

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
                return NotFound(new ResultViewModel<SubCategoria>("Subcategoria não existe"));

            return BadRequest(new ResultViewModel<SubCategoria>("Falha ao alterar subcategoria"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<SubCategoria>("Falha interna do servidor"));
        }

        return Ok(new ResultViewModel<SubCategoria>("Subcategoria alterada"));
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubCategoria(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<SubCategoria>("Falha interna no servidor"));

        if (_context.SubCategorias == null)
            return NotFound(new ResultViewModel<SubCategoria>("Subcategoria nula"));
        try
        {
            var subcategoria = await _context.SubCategorias.FindAsync(id);
            if (subcategoria == null)
                return NotFound(new ResultViewModel<SubCategoria>("Subcategoria não encontrada"));

            _context.SubCategorias.Remove(subcategoria);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<SubCategoria>("Falha interna do servidor"));
        }
        return Ok(new ResultViewModel<SubCategoria>("Subcategoria excluída"));
    }
    private bool SubCategoriaExists(int id)
    {
        return (_context.SubCategorias?.Any(e => e.SubCategoriaId == id)).GetValueOrDefault();
    }




}