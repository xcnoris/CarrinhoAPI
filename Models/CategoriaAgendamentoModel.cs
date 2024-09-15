using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrinhoAPI.Models
{
    public class CategoriaAgendamentoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da categoria é Obrigatorio!")]
        [StringLength(150, ErrorMessage = "Nome da categoria Deve ter no Maximo 150 Caracteres!")]
        public string Nome { get; set; }

        public DateTime Data_Criacao { get; set; }
        public DateTime? Data_Atualizacao { get; set; }

        public void ValidarClasse()
        {
            // Captura os results dos testes de validação dos campos
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            // retorna um false caso algum dos teste de problema
            bool isValid = Validator.TryValidateObject(this, context, results, true);
            // se retorna false, ele entra no loop
            if (isValid == false)
            {
                StringBuilder sbrErrors = new StringBuilder();
                foreach (var validationResult in results)
                {
                    // Adiciona no string(stringBuilder) todos os erros retornados das validaçoes
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }
                // Add as mensagens de erro para um exeção do tipo validationexception.
                // E força a mensagem da exceção
                throw new ValidationException(sbrErrors.ToString());
            }
        }
    }
}
