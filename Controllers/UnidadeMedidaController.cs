using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;
using PetApi.ViewModels;
using PetApi.ViewModels.SubcategoriaVM;
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
                return NotFound(new ResultViewModel<UnidadeMedida>("Não existe nenhuma unidade medida"));

            var unidadeMedida = await _context.UnidadeMedidas.ToListAsync();

            return Ok(new ResultViewModel<List<UnidadeMedida>>(unidadeMedida));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<UnidadeMedida>("Falha interna do servidor"));
        }
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadeMedida>> GetUnidadeMedida(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<UnidadeMedida>("Falha interna no servidor"));
        try
        {
            var unidadeMedida = await _context.UnidadeMedidas.FindAsync(id);
            if (unidadeMedida == null)
                return NotFound(new ResultViewModel<SubCategoria>("Não existe essa unidade de medida"));

            return Ok(new ResultViewModel<UnidadeMedida>(unidadeMedida));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<UnidadeMedida>("Falha interna do servidor"));
        }
    }
    // POST
    [HttpPost]
    public async Task<ActionResult<UnidadeMedida>> PostUnidadeMedida(CreateUnidadeMedidaVM unidadeMedidaVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<UnidadeMedida>("Falha interna no servidor"));

        if (_context.UnidadeMedidas == null)
            return NotFound((new ResultViewModel<UnidadeMedida>("Unidade de medida é nula")));
        try
        {
            var unidadeMedida = new UnidadeMedida
            {
                UnidadeMedidaId = 0,
                Ativo = 0,
                Nome = unidadeMedidaVM.Nome,
                Sigla = unidadeMedidaVM.Sigla
            };

            _context.UnidadeMedidas.Add(unidadeMedida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnidadeMedida", new { id = unidadeMedida.UnidadeMedidaId }, unidadeMedida);
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<UnidadeMedida>("Falha interna do servidor"));
        }
        
    }
    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateSubcategoriaVM unidadeMedidaVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<UnidadeMedida>("Falha interna no servidor"));

        var unidadeMedida = new UnidadeMedida
        {
            UnidadeMedidaId = id,
            Ativo = unidadeMedidaVM.Ativo,
            Nome = unidadeMedidaVM.Nome
        };

        _context.Entry(unidadeMedida).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UnidadeMedidaExists(id))
                return NotFound(new ResultViewModel<UnidadeMedida>("Unidade de medida não existe"));

            return BadRequest(new ResultViewModel<UnidadeMedida>("Falha ao alterar a unidade de medida"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<UnidadeMedida>("Falha interna do servidor"));
        }

        return Ok(new ResultViewModel<UnidadeMedida>("Unidade de medida alterada"));
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnidadeMedida(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<UnidadeMedida>("Falha interna no servidor"));
            
        if (_context.UnidadeMedidas == null)
            return NotFound(new ResultViewModel<UnidadeMedida>("Unidade de medida é nula"));

        try
        {
            var unidadeMedida = await _context.UnidadeMedidas.FindAsync(id);
            if (unidadeMedida == null)
                return NotFound(new ResultViewModel<UnidadeMedida>("Unidade de medida é não encotrada"));
            _context.UnidadeMedidas.Remove(unidadeMedida);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<UnidadeMedida>("Falha interna do servidor"));
        }
        return Ok(new ResultViewModel<UnidadeMedida>("Unidade de medida excluída"));
    }
    private bool UnidadeMedidaExists(int id)
    {
        return (_context.UnidadeMedidas?.Any(e => e.UnidadeMedidaId == id)).GetValueOrDefault();
    }




}