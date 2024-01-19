using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;
using PetApi.ViewModels;
using PetApi.ViewModels.CategoriaVM;
using PetApi.ViewModels.ProdutoVM;

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
                return NotFound(new ResultViewModel<Produto>("Não existe nenhum produto"));
            var produtos = await _context.Produtos.ToListAsync();
            return Ok(new ResultViewModel<List<Produto>>(produtos));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Produto>("Falha interna do servidor"));
        }
    }
    // GET BY ID:
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Produto>("Falha interna no servidor"));
        try
        {
            var produto = await _context.Produtos.FindAsync(id);
             if (produto == null)
                return NotFound(new ResultViewModel<Produto>("Não existe nenhum produto"));

            produto.Categoria = await _context.Categorias.FindAsync(produto.CategoriaId);
            produto.SubCategoria = await _context.SubCategorias.FindAsync(produto.SubCategoriaId);
            produto.UnidadeMedida = await _context.UnidadeMedidas.FindAsync(produto.UnidadeMedidaId);
            
            return Ok(new ResultViewModel<Produto>(produto));
        }
        catch 
        {
            return StatusCode(500, new ResultViewModel<Produto>("Falha interna do servidor"));
        }
       
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(CreateProdutoVM produtoVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Categoria>("Falha interna no servidor"));

        if (_context.Produtos == null)
            return Problem("Entidade adiciona 'ApplicationDbContext.Produto' é nula.");
        try
        {
            var produto = new Produto
            {
                ProdutoId = 0,
                Ativo = 0,
                Nome = produtoVM.Nome,
                PrecoCusto = produtoVM.PrecoCusto,
                PrecoVenda = produtoVM.PrecoVenda,
                CategoriaId = produtoVM.CategoriaId,
                UnidadeMedidaId = produtoVM.UnidadeMedidaId,
                SubCategoriaId = produtoVM.SubCategoriaId,
                Categoria = await _context.Categorias.FindAsync(produtoVM.CategoriaId),
                UnidadeMedida = await _context.UnidadeMedidas.FindAsync(produtoVM.UnidadeMedidaId),
                SubCategoria = await _context.SubCategorias.FindAsync(produtoVM.SubCategoriaId)
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, new ResultViewModel<Produto>(produto));
        }
        catch
        {
             return StatusCode(500, new ResultViewModel<Produto>("Falha interna do servidor"));
        }
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateProdutoVM produtoVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Produto>("Falha interna no servidor"));

        var produto = new Produto
        {
            Ativo = produtoVM.Ativo,
            Nome = produtoVM.Nome,
            PrecoCusto = produtoVM.PrecoCusto,
            PrecoVenda = produtoVM.PrecoVenda
        };

        if (id != produto.ProdutoId)
            return BadRequest(new ResultViewModel<Produto>(""));
        
        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
                return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

            return BadRequest(new ResultViewModel<Produto>("Falha ao alterar produto"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Produto>("Falha interna do servidor"));
        }

        return Ok(new ResultViewModel<Produto>("Produto alterado"));
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Produto>("Falha interna no servidor"));

        if (_context.Produtos == null)
            return NotFound(new ResultViewModel<Produto>("Produto nulo"));

        try
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Produto>("Erro ao excluir produto"));
        }
        return Ok(new ResultViewModel<Produto>("Produto excluído"));
    }
    private bool ProdutoExists(int id)
    {
        return (_context.Produtos?.Any(e => e.ProdutoId == id)).GetValueOrDefault();
    }
    
}