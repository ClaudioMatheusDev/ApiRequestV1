using APIRequest.Context;
using APIRequest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIRequest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                var produtos = _context.Produtos.ToList();
                if (produtos is null || !produtos.Any())
                {
                    return NotFound("Produtos não encontrados!");
                }
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter produtos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um produto pelo ID.
        /// </summary>
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == id);
                if (produto is null)
                {
                    return NotFound("Produto não encontrado!");
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                if (produto is null)
                    return BadRequest("Produto não inserido!");

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return CreatedAtRoute("ObterProduto", new { id = produto.ProdutoID }, produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoID)
                {
                    return BadRequest("Produto não atualizado!");
                }

                _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                return Ok("Produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == id);

                if (produto is null)
                {
                    return NotFound("Produto não encontrado!");
                }
                else
                {
                    _context.Produtos.Remove(produto);
                    _context.SaveChanges();
                    return Ok(produto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover o produto: {ex.Message}");
            }
        }
    }
}
