using CarrinhoAPI.Models.Enums;
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
        public async Task<ActionResult<List<CarrinhoModel>>> BuscarTodos([FromServices] DAL<CarrinhoModel> dalCarrinho)
        {
            try
            {
                IEnumerable<CarrinhoModel> listCarrinho = await dalCarrinho.ListarAsync();
                return listCarrinho.ToList();
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
        public async Task<ActionResult<CarrinhoModel>> BuscarPorId([FromServices] DAL<CarrinhoModel> dalCarrinho, int id)
        {
            try
            {
                CarrinhoModel carrinho = await dalCarrinho.BuscarPorAsync(c => c.Id.Equals(id));

                if (carrinho is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                return carrinho;
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
        public async Task<ActionResult<CarrinhoModel>> Adicionar([FromServices] DAL<CarrinhoModel> dalCongregacao, [FromBody] CarrinhoModel carrinho)
        {
            try
            {
                carrinho.ValidarClasse(); // Validação do objeto
                await dalCongregacao.AdicionarAsync(carrinho);
                return carrinho;
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
        public async Task<ActionResult<CarrinhoModel>> Atualizar([FromServices] DAL<CarrinhoModel> dalCarrinho,int id, [FromBody] CarrinhoModel carrinho)
        {
          
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                var entidadeExistente = await dalCarrinho.RecuperarPorAsync(c => c.Id.Equals(id));

                if (entidadeExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                carrinho.ValidarClasse();

                // Atualiza os campos da entidade existente com os novos dados
                entidadeExistente.Nome = carrinho.Nome;  // Exemplo de campo a ser atualizado
                entidadeExistente.Codigo_Carrinho = carrinho.Codigo_Carrinho;      // Atualize outros campos conforme necessário
                entidadeExistente.CongregacaoId = carrinho.CongregacaoId;
                entidadeExistente.Situacao = carrinho.Situacao;

                // Chama o método do DAL para atualizar a entidade no banco de dados
                await dalCarrinho.AtualizarAsync(entidadeExistente);

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
        public async Task<ActionResult<bool>> Remover([FromServices] DAL<CarrinhoModel> dalCarrinho, int id)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                CarrinhoModel entidadeExistente = await dalCarrinho.RecuperarPorAsync(c => c.Id.Equals(id));

                if (entidadeExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound();
                }
                try
                {
                    // Chama o método do DAL para remover a entidade
                    await dalCarrinho.DeletarAsync(entidadeExistente);
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
