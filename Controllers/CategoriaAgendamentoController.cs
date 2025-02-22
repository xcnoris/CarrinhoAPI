
using DataBase.APPCarrinho.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.APPCarrinho.agendamentos.Categoria_Agendamento;
using Modelos.APPCarrinho.Modelos.carrinho;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaAgendamentoController : ControllerBase
    {

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<List<CategoriaAgendamentoModels>>> BuscarTodos([FromServices] DAL<CategoriaAgendamentoModels> dalCategoriaAgendamentoModel)
        {
            try
            {
                IEnumerable<CategoriaAgendamentoModels> listCategoriaAgendamentoModel = await dalCategoriaAgendamentoModel.ListarAsync();
                return listCategoriaAgendamentoModel.ToList();
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
        public async Task<ActionResult<CategoriaAgendamentoModels>> BuscarPorId([FromServices] DAL<CategoriaAgendamentoModels> dalCategoriaAgendamentoModel, int id)
        {
            try
            {
                CategoriaAgendamentoModels CategoriaAgendamento = await dalCategoriaAgendamentoModel.BuscarPorAsync(c => c.Id.Equals(id));

                if (CategoriaAgendamento is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                return CategoriaAgendamento;
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
        public async Task<ActionResult<CategoriaAgendamentoModels>> Adicionar(
            [FromServices] DAL<CategoriaAgendamentoModels> dalCarrinho,
            [FromBody] CategoriaAgendamentoModels categoriaAgendamentoModel)
        {
            try
            {
               categoriaAgendamentoModel.DataCriacao = DateTime.Now;

                await dalCarrinho.AdicionarAsync(categoriaAgendamentoModel);
                return categoriaAgendamentoModel;
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
        public async Task<ActionResult<CarrinhoModels>> Atualizar(
            [FromServices] DAL<CategoriaAgendamentoModels> dalCategoriaAgendamento,
            int id,
            [FromBody] CategoriaAgendamentoModels CategoriaAgendamento)
        {  
            try
            {

                     // Primeiro, recupera a entidade existente pelo ID
                    CategoriaAgendamentoModels entidadeExistente = await dalCategoriaAgendamento.RecuperarPorAsync(c => c.Id.Equals(id));

                    if (entidadeExistente == null)
                    {
                        // Retorna 404 Not Found se a entidade não existir
                        return NotFound($"Id {id} não existe no banco de dados!");
                    }

                    // Atualiza os campos da entidade existente com os novos dados
                    entidadeExistente.Nome = CategoriaAgendamento.Nome;  // Exemplo de campo a ser atualizado
                    entidadeExistente.DataAtualizacao = DateTime.Now;      // Atualize outros campos conforme necessário


                    // Chama o método do DAL para atualizar a entidade no banco de dados
                    await dalCategoriaAgendamento.AtualizarAsync(entidadeExistente);

                    // Retorna a entidade atualizada dentro de um Ok()
                    return Ok(entidadeExistente);
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


        [HttpDelete("Remover/{id}")]
        public async Task<ActionResult<bool>> Remover([FromServices] DAL<CategoriaAgendamentoModels> dalCategoriaAgendamento, int id)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                CategoriaAgendamentoModels categoriaAgendamento = await dalCategoriaAgendamento.RecuperarPorAsync(c => c.Id.Equals(id));

                if (categoriaAgendamento == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound();
                }
                try
                {
                    // Chama o método do DAL para remover a entidade
                    await dalCategoriaAgendamento.DeletarAsync(categoriaAgendamento);
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
