using APIRequest.Context;
using APIRequest.Filtros;
using APIRequest.Models;
using APIRequest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as categorias com seus respectivos produtos.
        /// </summary>
        /// <returns>Lista de categorias com seus produtos associados.</returns>
        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            try
            {
                var categorias = await _context.Categorias
                                               .Include(c => c.Produtos)
                                               .ToListAsync();

                if (!categorias.Any())
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter categorias: {ex.Message}");
            }
        }


        [HttpGet("UsandoFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet("SemUsarFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoSemFromServices(IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        /// <summary>
        /// Obtém todas as categorias.
        /// </summary>
        /// <returns>Lista de categorias.</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categorias = await _context.Categorias.ToListAsync();

                if (!categorias.Any())
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter categorias: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém uma categoria pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>Detalhes da categoria com o ID especificado.</returns>
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {


            // throw new Exception("Erro ao obter categoria pelo id");
            string[] teste = null;
            if (teste.Length > 0)
            {
                return BadRequest();
            }

            try
            {
                var categoria = await _context.Categorias
                                              .FirstOrDefaultAsync(c => c.CategoriaID == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com ID {id} não encontrada.");
                }

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter categoria: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoria">Dados da categoria a ser criada.</param>
        /// <returns>Categoria criada.</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    return BadRequest("Dados da categoria não fornecidos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("ObterCategoria", new { id = categoria.CategoriaID }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar categoria: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <param name="id">ID da categoria a ser atualizada.</param>
        /// <param name="categoria">Dados atualizados da categoria.</param>
        /// <returns>Categoria atualizada.</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaID)
                {
                    return BadRequest("O ID da categoria não corresponde ao ID fornecido.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoriaExistente = await _context.Categorias.FindAsync(id);
                if (categoriaExistente == null)
                {
                    return NotFound($"Categoria com ID {id} não encontrada.");
                }

                // Atualiza os dados da categoria
                categoriaExistente.Nome = categoria.Nome; // Supondo que o nome seja a única propriedade editável
                _context.Entry(categoriaExistente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(categoriaExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar categoria: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove uma categoria pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria a ser removida.</param>
        /// <returns>Categoria removida.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var categoria = await _context.Categorias
                                              .FirstOrDefaultAsync(c => c.CategoriaID == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com ID {id} não encontrada.");
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover categoria: {ex.Message}");
            }
        }
    }
}
