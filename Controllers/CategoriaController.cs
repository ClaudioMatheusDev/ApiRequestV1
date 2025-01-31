using APIRequest.Context;
using APIRequest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRequest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as categorias com seus produtos.
        /// </summary>
        /// <returns>Uma lista de categorias com seus produtos.</returns>
        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            return await _context.Categorias.Include(p => p.Produtos).ToListAsync();
        }

        /// <summary>
        /// Obtém todas as categorias.
        /// </summary>
        /// <returns>Uma lista de categorias.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            return await _context.Categorias.ToListAsync();
        }

        /// <summary>
        /// Obtém uma categoria pelo ID.
        /// </summary>
        /// <param name="id">O ID da categoria.</param>
        /// <returns>A categoria correspondente ao ID fornecido.</returns>
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaID == id);
            if (categoria is null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }
            return Ok(categoria);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoria">A categoria a ser criada.</param>
        /// <returns>Retorna a categoria criada.</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados da categoria não fornecidos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaID }, categoria);
        }

        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">O ID da categoria a ser atualizada.</param>
        /// <param name="categoria">Os novos dados da categoria.</param>
        /// <returns>Retorna a categoria atualizada.</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return BadRequest("ID da categoria não corresponde ao ID fornecido.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

        /// <summary>
        /// Remove uma categoria pelo ID.
        /// </summary>
        /// <param name="id">O ID da categoria a ser removida.</param>
        /// <returns>Retorna a categoria removida.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaID == id);
            if (categoria is null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
    }
}