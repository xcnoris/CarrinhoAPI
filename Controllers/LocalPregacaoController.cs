using CarrinhoAPI.Models;
using CarrinhoAPI.Repository.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalPregacaoController : ControllerBase
    {

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<List<LocalPregacaoModel>>> BuscarTodos(
            [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao)
        {
            try
            {
                IEnumerable<LocalPregacaoModel> listLocalPregacao = await dalLocalPregacao.ListarAsync();
                return listLocalPregacao.ToList();
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar buscar a entidade. " + ex.Message);
            }
        }


        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<LocalPregacaoModel>> BuscarPorId(
            [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao,
            int id)
        {
            try
            {
                LocalPregacaoModel localPregacao = await dalLocalPregacao.BuscarPorAsync(c => c.Id.Equals(id));

                if (localPregacao is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                return localPregacao;
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar buscar a entidade. " + ex.Message);
            }
        }


        [HttpPost("Adicionar")]
        public async Task<ActionResult<LocalPregacaoModel>> Adicionar(
           [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao,
           [FromServices] DAL<CongregacaoModel> dalCongregacao,
           [FromBody] LocalPregacaoModel localPregacao)
        {
            try
            {
                CongregacaoModel congregaccao = await dalCongregacao.BuscarPorAsync(c => c.Id.Equals(localPregacao.CongregacaoId));
                if (congregaccao is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {localPregacao.CongregacaoId} da Congregação não existe no banco de dados!");
                }
                else
                {
                    localPregacao.Data_Cadastro = DateTime.Now;
                    // Valida a entidade antes de prosseguir
                    localPregacao.ValidarClasse();

                    // Adiciona a entidade ao banco de dados
                    await dalLocalPregacao.AdicionarAsync(localPregacao);

                    // Retorna a entidade adicionada
                    return Ok(localPregacao);
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
                return StatusCode(500, "Erro ao tentar adicionar a entidade. " + ex.Message);
            }
        }



        [HttpPut("Atualizar/{id}")]
        public async Task<ActionResult<LocalPregacaoModel>> Atualizar(
            [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao,
            [FromServices] DAL<CongregacaoModel> dalCongregacao,
            int id,
            [FromBody] LocalPregacaoModel localPregacao)
        {
            try
            {
                CongregacaoModel congregaccao = await dalCongregacao.BuscarPorAsync(c => c.Id.Equals(localPregacao.CongregacaoId));
                if (congregaccao is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {localPregacao.CongregacaoId} da Congregação não existe no banco de dados!");
                }
                else
                {

                    // Primeiro, recupera a entidade existente pelo ID
                    var entidadeExistente = await dalLocalPregacao.RecuperarPorAsync(c => c.Id.Equals(id));

                    if (entidadeExistente == null)
                    {
                        // Retorna 404 Not Found se a entidade não existir
                        return NotFound($"Id {id} não existe no banco de dados!");
                    }
                    localPregacao.ValidarClasse();
                    // Atualiza os campos da entidade existente com os novos dados
                    entidadeExistente.Nome = localPregacao.Nome;
                    entidadeExistente.Descricao = localPregacao.Descricao;
                    entidadeExistente.Endereco = localPregacao.Endereco;
                    entidadeExistente.Complemento = localPregacao.Complemento;
                    entidadeExistente.Bairro = localPregacao.Bairro;
                    entidadeExistente.Cidade = localPregacao.Cidade;
                    entidadeExistente.UF = localPregacao.UF;
                    entidadeExistente.Situacao = localPregacao.Situacao;
                    entidadeExistente.CongregacaoId = localPregacao.CongregacaoId;
                    // Atualize outros campos conforme necessário

                    // Chama o método do DAL para atualizar a entidade no banco de dados
                    await dalLocalPregacao.AtualizarAsync(entidadeExistente);

                    // Retorna a entidade atualizada dentro de um Ok()
                    return Ok(entidadeExistente);
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
                return StatusCode(500, "Erro ao tentar atualizar a entidade. " + ex.Message);
            }
        }



        [HttpDelete("Remover/{id}")]
        public async Task<ActionResult<bool>> Remover(
            [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao,
            int id)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                LocalPregacaoModel entidadeExistente = await dalLocalPregacao.RecuperarPorAsync(c => c.Id.Equals(id));

                if (entidadeExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound();
                }
         
                // Chama o método do DAL para remover a entidade
                await dalLocalPregacao.DeletarAsync(entidadeExistente);
                // Retorna 204 No Content se a remoção foi bem-sucedida
                return NoContent();
              
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
