using CarrinhoAPI.Models;
using CarrinhoAPI.Repository.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadeController : ControllerBase
    {

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<List<EntidadeModel>>> BuscarTodos([FromServices] DAL<EntidadeModel> EntidadeDAL)
        {
            try
            {
                IEnumerable<EntidadeModel> listEntidades = await EntidadeDAL.ListarAsync();
                return listEntidades.ToList();
            }
            catch (ValidationException ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar buscar a entidade. " + ex.Message);
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<EntidadeModel>> BuscarPorId([FromServices] DAL<EntidadeModel> EntidadeDAL, int id)
        {
            try
            {
                EntidadeModel entidade = await EntidadeDAL.BuscarPorAsync(c => c.Id.Equals(id));

                if (entidade is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                return entidade;
            }
            catch (ValidationException ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar buscar a entidade. " + ex.Message);
            }

        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult<EntidadeModel>> Adicionar(
            [FromServices] DAL<EntidadeModel> dalEntidade,
            [FromServices] DAL<CongregacaoModel> dalCongregacao,
            [FromBody] EntidadeModel entidade)
        {
            try
            {
                CongregacaoModel congregaccao = await dalCongregacao.BuscarPorAsync(c => c.Id.Equals(entidade.CongregacaoId));
                if (congregaccao is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {entidade.CongregacaoId} da Congregação não existe no banco de dados!");
                }
                else
                {
                    entidade.ValidarClass(); // Validação do objeto
                    entidade.Data_Cadastro = DateTime.Now;
                    await dalEntidade.AdicionarAsync(entidade);
                    return entidade;
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
        public async Task<ActionResult<EntidadeModel>> Atualizar(
            [FromServices] DAL<EntidadeModel> dalEntidade,
            [FromServices] DAL<CongregacaoModel> dalCongregacao,
            int id,
            [FromBody] EntidadeModel entidade)
        {
          
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                var entidadeExistente = await dalEntidade.RecuperarPorAsync(c => c.Id.Equals(id));

                if (entidadeExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                else
                {
                    CongregacaoModel congregaccao = await dalCongregacao.BuscarPorAsync(c => c.Id.Equals(entidade.CongregacaoId));
                    if (congregaccao is null)
                    {
                        // Retorna 404 Not Found se a entidade não existir
                        return NotFound($"Id {entidade.CongregacaoId} da Congregação não existe no banco de dados!");
                    }
                    else
                    {
                        entidade.ValidarClass();
                        // Atualiza os campos da entidade existente com os novos dados
                        entidadeExistente.CPF = entidade.CPF;
                        entidadeExistente.Nome = entidade.Nome;  // Exemplo de campo a ser atualizado
                        entidadeExistente.Endereco = entidade.Endereco;
                        entidadeExistente.Endereco_Numero = entidade.Endereco_Numero;
                        entidadeExistente.Endereco_Complemento = entidade.Endereco_Complemento;
                        entidadeExistente.Bairro = entidade.Bairro;
                        entidadeExistente.Cidade_Nome = entidade.Cidade_Nome;
                        entidadeExistente.UF = entidade.UF;
                        entidadeExistente.DDD_Celular = entidade.DDD_Celular;
                        entidadeExistente.Celular = entidade.Celular;
                        entidadeExistente.Sexo = entidade.Sexo;
                        entidadeExistente.DataNascimento = entidade.DataNascimento;
                        entidadeExistente.Email = entidade.Email;
                        entidadeExistente.CongregacaoId = entidade.CongregacaoId;

                        // Chama o método do DAL para atualizar a entidade no banco de dados
                        await dalEntidade.AtualizarAsync(entidadeExistente);

                        // Retorna a entidade atualizada dentro de um Ok()
                        return Ok(entidadeExistente);
                    }
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
        public async Task<ActionResult<bool>> Remover([FromServices] DAL<EntidadeModel> dalEntidade, int id)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                EntidadeModel entidadeExistente = await dalEntidade.RecuperarPorAsync(c => c.Id.Equals(id));

                if (entidadeExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound();
                }
                try
                {
                    // Chama o método do DAL para remover a entidade
                    await dalEntidade.DeletarAsync(entidadeExistente);
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
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest("Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, "Erro ao tentar remover a entidade. " + ex.Message);
            }
        }

    }
}
