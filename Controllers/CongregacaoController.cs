using CarrinhoAPI.Models;
using CarrinhoAPI.Repository.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongregacaoController : ControllerBase
    {

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<List<CongregacaoModel>>> BuscarTodos([FromServices] DAL<CongregacaoModel> dalCongregacao)
        {
            try
            {
                IEnumerable<CongregacaoModel> listCongregacoes = await dalCongregacao.ListarAsync();
                return listCongregacoes.ToList();
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro de validação: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar buscar a entidade. " + ex.Message);
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<CongregacaoModel>> BuscarPorId([FromServices] DAL<CongregacaoModel> dalCongregacao, int id)
        {
            try
            {
                CongregacaoModel congregacao = await dalCongregacao.BuscarPorAsync(c => c.Id.Equals(id));

                if (congregacao is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                return congregacao;
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro de validação: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar buscar a entidade. " + ex.Message);
            }
        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult<CongregacaoModel>> Adicionar(
           [FromServices] DAL<CongregacaoModel> dalCongregacao,
           [FromBody] CongregacaoModel congregacao)
        {
            try
            {
                congregacao.DataCriacao = DateTime.Now;
                // Valida a entidade antes de prosseguir
                congregacao.ValidarClasse();

                // Adiciona a entidade ao banco de dados
                await dalCongregacao.AdicionarAsync(congregacao);

                // Retorna a entidade adicionada
                return Ok(congregacao);
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro de validação: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar adicionar a entidade. " + ex.Message);
            }
        }


        [HttpPut("Atualizar/{id}")]
        public async Task<ActionResult<CongregacaoModel>> Atualizar(
            [FromServices] DAL<CongregacaoModel> dalCongregacao,
            int id,
            [FromBody] CongregacaoModel congregacao)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                var entidadeExistente = await dalCongregacao.RecuperarPorAsync(c => c.Id.Equals(id));

                if (entidadeExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                congregacao.ValidarClasse();
                // Atualiza os campos da entidade existente com os novos dados
                entidadeExistente.Nome = congregacao.Nome;  // Exemplo de campo a ser atualizado
                entidadeExistente.Situacao = congregacao.Situacao;                          // Atualize outros campos conforme necessário

                // Chama o método do DAL para atualizar a entidade no banco de dados
                await dalCongregacao.AtualizarAsync(entidadeExistente);

                // Retorna a entidade atualizada dentro de um Ok()
                return Ok(entidadeExistente);
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro de validação: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar atualizar a entidade. " + ex.Message);
            }
        }


        [HttpDelete("Remover/{id}")]
        public async Task<ActionResult<bool>> Remover([FromServices] DAL<CongregacaoModel> dalCongregacao, int id)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                CongregacaoModel entidadeExistente = await dalCongregacao.RecuperarPorAsync(c => c.Id.Equals(id));

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
                    return StatusCode(500, "Erro ao tentar remover a entidade." + ex.Message);
                }
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro de validação: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar remover a entidade. " + ex.Message);
            }
        }

    }
}
