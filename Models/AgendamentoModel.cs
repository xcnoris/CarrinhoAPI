using CarrinhoAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrinhoAPI.Models
{
    public class AgendamentoModel
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Situação do Agendamento é obrigatória!")]
        public int SituacaoId { get; set; }
        public virtual SituacaoAgendamentoModel? Situacao { get; set; }


        [Required(ErrorMessage = "Categoria do Agendamento é obrigatória!")]
        public int CategoriaId { get; set; }
        public virtual CategoriaAgendamentoModel? Categoria { get; set; }

        [Required(ErrorMessage = "Pessoa do Agendamento é obrigatória!")]
        public int EntidadeId { get; set; }
        public virtual EntidadeModel? Entidade { get; set; }

        [Required(ErrorMessage = "Carrinho do Agendamento é obrigatório!")]
        public int CarrinhoId { get; set; }
        public virtual CarrinhoModel? Carrinho { get; set; }

        [Required(ErrorMessage = "Dia da semana do Agendamento é obrigatório!")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "Hora de início do Agendamento é obrigatória!")]
        public TimeSpan Hora1 { get; set; }

        [Required(ErrorMessage = "Hora de término do Agendamento é obrigatória!")]
        public TimeSpan Hora2 { get; set; }

        [Required(ErrorMessage = "Local do Agendamento é obrigatório!")]
        public int LocalPregacaoId { get; set; }
        public virtual LocalPregacaoModel? LocalPregacao { get; set; }

        public DateTime Data_Criacao { get; set; }
        public DateTime? Data_Atualizacao { get; set; }

        public AgendamentoModel()
        {
            Data_Criacao = DateTime.Now;
        }

        public void ValidarClasse()
        {
            // Captura os resultados dos testes de validação dos campos
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                StringBuilder sbrErrors = new StringBuilder();
                foreach (var validationResult in results)
                {
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }
                throw new ValidationException(sbrErrors.ToString());
            }

            
            // Validação extra: Hora2 deve ser maior que Hora1
            if (Hora2 <= Hora1)
            {
                throw new ValidationException("Hora de término deve ser maior que a hora de início.");
            }
        }
    }
}
