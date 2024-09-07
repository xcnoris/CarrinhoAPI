using CarrinhoAPI.Models;
using CarrinhoAPI.Repository.DataBase;
using CarrinhoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongregacaoController : ControllerBase
    {
        public CongregacaoController(IEndPointCrud<Congregacao> repository)
        {
     
        }

        [HttpGet("BuscarTodos")]
        public async Task<List<Congregacao>> BuscarTodos([FromServices] DAL<Congregacao> dalCongregacao)
        {
            IEnumerable<Congregacao> listCongregacoes = await dalCongregacao.ListarAsync();
            return listCongregacoes.ToList();
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<Congregacao> BuscarPorId([FromServices] DAL<Congregacao> dalCongregacao, int id)
        {
            
            return await dalCongregacao.BuscarPorAsync(c => c.Id.Equals(id));
        }

        [HttpPost("Adicionar")]
        public async Task<Congregacao> Adicionar([FromServices] DAL<Congregacao> dalCongregacao, [FromBody] Congregacao congregacao)
        {
            await dalCongregacao.AdicionarAsync(congregacao);
            return congregacao;
        }
        [HttpPut("Atualizar/{id}")]
        public async Task<ActionResult<Congregacao>> Atualizar([FromServices] DAL<Congregacao> dalCongregacao,int id, [FromBody] Congregacao congregacao)
        {
            // Primeiro, recupera a entidade existente pelo ID
            var entidadeExistente = await dalCongregacao.RecuperarPorAsync(c => c.Id.Equals(id));

            if (entidadeExistente == null)
            {
                // Retorna 404 Not Found se a entidade não existir
                return NotFound($"Id {id} não existe no banco de dados!");
            }

            // Atualiza os campos da entidade existente com os novos dados
            entidadeExistente.Nome = congregacao.Nome;  // Exemplo de campo a ser atualizado
                                                        // Atualize outros campos conforme necessário

            // Chama o método do DAL para atualizar a entidade no banco de dados
            await dalCongregacao.AtualizarAsync(entidadeExistente);

            // Retorna a entidade atualizada dentro de um Ok()
            return Ok(entidadeExistente);
        }


        [HttpDelete("Remover/{id}")]
        public async Task<ActionResult<bool>> Remover([FromServices] DAL<Congregacao> dalCongregacao, int id)
        {
            // Primeiro, recupera a entidade existente pelo ID
            Congregacao entidadeExistente = await dalCongregacao.RecuperarPorAsync(c => c.Id.Equals(id));

            if (entidadeExistente == null)
            {
                // Retorna 404 Not Found se a entidade não existir
                return NotFound();
            }
            try
            {
                // Chama o método do DAL para remover a entidade
                await dalCongregacao.DeletarAsync(entidadeExistente);
                // Retorna 204 No Content se a remoção foi bem-sucedida
                return NoContent();
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico ou detalhado se a remoção falhar
                // Você pode logar a exceção ex para fins de diagnóstico
                return StatusCode(500, "Erro ao tentar remover a entidade.");
            }
        }

    }
}
