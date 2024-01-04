using PetApi.Data;
using PetApi.Models;

namespace PetApi.Controllers;

[ApiController]
[Route("v1/categoria")]
public class CategoriaController : CotrollerBase    
{
    private readonly PetDbContext _context;

    public TodoController(PetDbContext context)
    {
        _context = context;
    }

    // GET ALL:
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
    {
        try
        {
            if (_context.Categoria == null)
            {
                return NotFound("01xE1 - Conteúdos não encontrados");
            }
            return await _context.Categoria.ToListAsync();
        }
        catch
        {

            return StatusCode(500, "01xE2 - Falha interna no servidor");
        }
       
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        try
        {
            var Categoria = await _context.Categoria.FindAsync(id);

            if (Categoria == null)
                return NotFound("01xE3 - Categoria não encontrada");
            
            return Categoria;
        }
        catch 
        {
            return StatusCode(500, "01xE2 - Falha interna no servidor");
        }
       
    }
    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Categoria categoria)
    {
        if (id != categoria.Id)
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
            if (!TodoExists(id))
            {
                return NotFound("01xE3 - Categoria não encontrada");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }



}