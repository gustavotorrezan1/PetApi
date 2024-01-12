using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;

namespace PetApi.Controllers;

[ApiController]
[Route("v1/produto")]
public class ProdutoController : ControllerBase    
{
    private readonly PetDbContext _context;

    public ProdutoController(PetDbContext context)
    {
        _context = context;
    }

    // GET ALL:
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        try
        {
            if (_context.Produtos == null)
            {
                return NotFound();
            }
            return await _context.Produtos.ToListAsync();
        }
        catch(Exception e)
        {
            return StatusCode(500, e);
        }
       
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        try
        {
            var Produto = await _context.Produtos.FindAsync(id);

            if (Produto == null)
                return NotFound();
            
            return Produto;
        }
        catch 
        {
            return StatusCode(500);
        }
       
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        if (_context.Produtos == null)
            return Problem("Entidade adiciona 'ApplicationDbContext.Produto' é nula.");
        
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest("01xE4 - Id diferente do Id da produto");
        }

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
                return NotFound("");
            

            return BadRequest("01xE5 - Falha ao alterar produto");
            
        }

        return NoContent();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        if (_context.Produtos == null)
        {
            return NotFound();
        }
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool ProdutoExists(int id)
    {
        return (_context.Produtos?.Any(e => e.ProdutoId == id)).GetValueOrDefault();
    }




}