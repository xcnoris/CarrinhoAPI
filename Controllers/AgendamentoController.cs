
using DataBase.APPCarrinho.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.APPCarrinho.agendamentos.Categoria_Agendamento;
using Modelos.APPCarrinho.agendamentos.LocalPregacao;
using Modelos.APPCarrinho.agendamentos.Situacao;
using Modelos.APPCarrinho.Class.agendamentos;
using Modelos.APPCarrinho.Class.clientes;
using Modelos.APPCarrinho.Modelos.carrinho;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<List<AgendamentoModels>>> BuscarTodos([FromServices] DAL<AgendamentoModels> dalAgendamento)
        {
            try
            {
                IEnumerable<AgendamentoModels> listAgendamento = await dalAgendamento.ListarAsync();
                return listAgendamento.ToList();
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
        public async Task<ActionResult<AgendamentoModels>> BuscarPorId([FromServices] DAL<AgendamentoModels> dalAgendamento, int id)
        {
            try
            {
                AgendamentoModels agendamento = await dalAgendamento.BuscarPorAsync(c => c.Id.Equals(id));

                if (agendamento is null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound($"Id {id} não existe no banco de dados!");
                }
                return agendamento;
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


        //[HttpPost("Adicionar")]
        //public async Task<ActionResult<AgendamentoModels>> Adicionar(
        //    [FromServices] DAL<AgendamentoModels> dalAgendamento,
        //    [FromServices] DAL<CarrinhoModels> dalCarrinho,
        //    [FromServices] DAL<CategoriaAgendamentoModels> dalCategoriaAgendamento,
        //    [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao,
        //    [FromServices] DAL<SituacaoAgendamentoModels> dalSituacaoAgendamento,
        //    [FromServices] DAL<EntidadeModels> dalEntidade,
        //    [FromBody] AgendamentoModels agendamento)
        //{
        //    try
        //    {
        //        CarrinhoModels Carrinho = await dalCarrinho.BuscarPorAsync(c => c.Id.Equals(agendamento.CarrinhoId));
        //        CategoriaAgendamentoModels CategoriaAgendamento = await dalCategoriaAgendamento.BuscarPorAsync(c => c.Id.Equals(agendamento.CategoriaId));
        //        LocalPregacaoModel LocalPregacao = await dalLocalPregacao.BuscarPorAsync(c => c.Id.Equals(agendamento.LocalPregacaoId));
        //        SituacaoAgendamentoModels SituacaoAgendamento = await dalSituacaoAgendamento.BuscarPorAsync(c => c.Id.Equals(agendamento.SituacaoId));
        //        EntidadeModels Entidade = await dalEntidade.BuscarPorAsync(x => x.Id == agendamento.EntidadeId);

        //        // Verifica se todas as entidades existem
        //        if (Carrinho == null)
        //            return NotFound($"Id {agendamento.CarrinhoId} do Carrinho não existe no banco de dados!");

        //        if (CategoriaAgendamento == null)
        //            return NotFound($"Id {agendamento.IdCategoria} da Categoria de Agendamento não existe no banco de dados!");

        //        if (LocalPregacao == null)
        //            return NotFound($"Id {agendamento.LocalPregacaoId} do Local de Pregação não existe no banco de dados!");

        //        if (SituacaoAgendamento == null)
        //            return NotFound($"Id {agendamento.IdSituacao} da Situação do Agendamento não existe no banco de dados!");
        //        if(Entidade == null)
        //            return NotFound($"Id {agendamento.EntidadeId} da Entidade do Agendamento não existe no banco de dados!");

        //        // Atribui a data de criação
        //        agendamento.DataCriacao = DateTime.Now;

        //        // Validação do objeto
        //        agendamento.ValidarClasse();

        //        // Adiciona o agendamento no banco de dados
        //        await dalAgendamento.AdicionarAsync(agendamento);

        //        return CreatedAtAction(nameof(Adicionar), new { id = agendamento.Id }, agendamento); // Retorna 201 Created
        //    }
        //    catch (ValidationException ex)
        //    {
        //        // Retorna um erro de validação com o detalhe da mensagem
        //        return BadRequest("Erro de validação: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Retorna um erro genérico com a mensagem da exceção
        //        return StatusCode(500, "Erro ao tentar adicionar a entidade. " + ex.Message);
        //    }a
        //}a
        //a

        //[HttpPut("Atualizar/{id}")]
        //public async Task<ActionResult<CarrinhoModels>> Atualizar(
        //    [FromServices] DAL<AgendamentoModels> dalAgendamento,
        //    [FromServices] DAL<CarrinhoModels> dalCarrinho,
        //    [FromServices] DAL<CategoriaAgendamentoModel> dalCategoriaAgendamento,
        //    [FromServices] DAL<LocalPregacaoModel> dalLocalPregacao,
        //    [FromServices] DAL<SituacaoAgendamentoModel> dalSituacaoAgendamento,
        //    [FromServices] DAL<EntidadeModel> dalEntidade,

        //    [FromBody] AgendamentoModels agendamento,
        //    int id)
        //{  
        //    try
        //    {
        //        // Verifica se o agendamento existe
        //        var agendamentoExistente = await dalAgendamento.BuscarPorAsync(a => a.Id.Equals(id));
        //        if (agendamentoExistente == null)
        //        {
        //            return NotFound($"Agendamento com Id {id} não existe no banco de dados!");
        //        }

        //        // Busca as entidades relacionadas
        //        CarrinhoModels carrinho = await dalCarrinho.BuscarPorAsync(c => c.Id.Equals(agendamento.CarrinhoId));
        //        CategoriaAgendamentoModel categoriaAgendamento = await dalCategoriaAgendamento.BuscarPorAsync(c => c.Id.Equals(agendamento.CategoriaId));
        //        LocalPregacaoModel localPregacao = await dalLocalPregacao.BuscarPorAsync(c => c.Id.Equals(agendamento.LocalPregacaoId));
        //        SituacaoAgendamentoModel situacaoAgendamento = await dalSituacaoAgendamento.BuscarPorAsync(c => c.Id.Equals(agendamento.SituacaoId));
        //        EntidadeModel Entidade = await dalEntidade.BuscarPorAsync(x => x.Id == agendamento.EntidadeId);

        //        // Valida se as entidades relacionadas existem
        //        if (carrinho == null)
        //            return NotFound($"Carrinho com Id {agendamento.CarrinhoId} não encontrado!");

        //        if (categoriaAgendamento == null)
        //            return NotFound($"Categoria de Agendamento com Id {agendamento.IdCategoria} não encontrada!");

        //        if (localPregacao == null)
        //            return NotFound($"Local de Pregação com Id {agendamento.LocalPregacaoId} não encontrado!");

        //        if (situacaoAgendamento == null)
        //            return NotFound($"Situação de Agendamento com Id {agendamento.IdSituacao} não encontrada!");
        //        if (Entidade == null)
        //            return NotFound($"Id {agendamento.EntidadeId} da Entidade do Agendamento não existe no banco de dados!");

        //        // Atualiza os campos da entidade existente com os novos dados
        //        agendamentoExistente.SituacaoId = agendamento.SituacaoId;
        //        agendamentoExistente.EntidadeId = agendamento.EntidadeId;
        //        agendamentoExistente.CategoriaId = agendamento.CategoriaId;
        //        agendamentoExistente.CarrinhoId = agendamento.CarrinhoId;
        //        agendamentoExistente.DataAgendamento = agendamento.DataAgendamento;
        //        agendamentoExistente.Hora1 = agendamento.Hora1;
        //        agendamentoExistente.Hora2 = agendamento.Hora2;
        //        agendamentoExistente.LocalPregacaoId = agendamento.LocalPregacaoId;
        //        agendamentoExistente.Data_Atualizacao = DateTime.Now;

        //        // Valida a entidade antes de atualizar
        //        agendamentoExistente.ValidarClasse();

        //        // Chama o método do DAL para atualizar a entidade no banco de dados
        //        await dalAgendamento.AtualizarAsync(agendamentoExistente);

        //        // Retorna a entidade atualizada
        //        return Ok(agendamentoExistente);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return BadRequest("Erro de validação: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Erro ao tentar atualizar a entidade. " + ex.Message);
        //    }
        //}


        [HttpDelete("Remover/{id}")]
        public async Task<ActionResult<bool>> Remover([FromServices] DAL<AgendamentoModels> dalAgendamento, int id)
        {
            try
            {
                // Primeiro, recupera a entidade existente pelo ID
                AgendamentoModels AgendamentoExistente = await dalAgendamento.RecuperarPorAsync(c => c.Id.Equals(id));

                if (AgendamentoExistente == null)
                {
                    // Retorna 404 Not Found se a entidade não existir
                    return NotFound();
                }
                try
                {
                    // Chama o método do DAL para remover a entidade
                    await dalAgendamento.DeletarAsync(AgendamentoExistente);
                    // Retorna 204 No Content se a remoção foi bem-sucedida
                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Retorna um erro genérico ou detalhado se a remoção falhar
                    // Você pode logar a exceção ex para fins de diagnóstico
                    return StatusCode(500, $"Erro ao tentar remover a entidade. {ex.Message}");
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
