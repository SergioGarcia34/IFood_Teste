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
        private readonly IRepository _repository;

        public ProdutosController(AppDBContext context, IRepository repository)   
        {
            _repository = repository;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            try {

                var model = await _repository.SelectAll<Produto>();
                return model;

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
                var produto = await  _repository.SelectById<Produto>(id);

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
                    await _repository.UpdateAsync<Produto>(produto);              
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

                await _repository.CreateAsync<Produto>(produto);

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
                var produto = await _repository.SelectById<Produto>(id);

                if (produto == null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync<Produto>(produto);

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

    }
}
