using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IfoodAPI.Models;
using Microsoft.AspNetCore.Authorization;
using IfoodAPI.Repository;
using Microsoft.Data.SqlClient;

namespace IfoodAPI.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly iFoodDBContext _context;

        public ProdutosController(iFoodDBContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            try { 
                return await _context.Produto.ToListAsync();
            }
            catch (SqlException)
            {
                return Problem(detail: "Erro no Banco de dados ", title: "GetProduto");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "GetProduto");
            }
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            try
            {
            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }
                return produto;
            }

            catch (SqlException)
            {
                return Problem(detail: "Erro no Banco de dados ", title: "GetProduto");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "GetProduto");
            }

        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            try
            {
                if (id != produto.IdProd)
                {
                    return BadRequest();
                }

                _context.Entry(produto).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (SqlException)
            {
                return Problem(detail: "Erro no Banco de dados ", title: "PutProduto");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "PutProduto");
            }

            return NoContent();
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            try {
                _context.Produto.Add(produto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduto", new { id = produto.IdProd }, produto);
            }
            catch (SqlException)
            {
                return Problem(detail: "Erro no Banco de dados ", title: "PostProduto");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "PostProduto");
            }
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            try
            {
                var produto = await _context.Produto.FindAsync(id);
                if (produto == null)
                {
                    return NotFound();
                }

                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();

                return produto;
            }
            catch (SqlException)
            {
                return Problem(detail: "Erro no Banco de dados ", title: "DeleteProduto");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "DeleteProduto");
            }

        }

        private bool ProdutoExists(int id)
        {   
            return _context.Produto.Any(e => e.IdProd == id);
        }
    }
}
