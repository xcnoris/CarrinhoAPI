using CarrinhoAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrinhoAPI.Models
{
    public class CongregacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da Congregação é Obrigatorio!")]
        [StringLength(200, ErrorMessage = "Nome da Congregação deve ter no Maximo 200 Caracteres!")]
        public string Nome { get; set; }

        public SituacaoGeral Situacao { get; set; }

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
            // Validação adicional para o enum SituacaoGeral
            if (!Enum.IsDefined(typeof(SituacaoGeral), this.Situacao))
            {
                throw new ValidationException("A situação do carrinho não é válida.");
            }
        }
    }
}
