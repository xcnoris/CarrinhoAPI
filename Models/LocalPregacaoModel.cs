using CarrinhoAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrinhoAPI.Models
{
    public class LocalPregacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do Local de pregação é Obrigatorio!")]
        [StringLength(250, ErrorMessage = "Nome do Local da Pregação deve ter no Maximo 250 Caracteres!")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "Descrição do local da pregação deve ter no Maximo 500 Caracteres!")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Endereço é Obrigatorio!")]
        public string Endereco { get; set; }

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "Bairro é Obrigatorio!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade é Obrigatoria!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "UF é Obrigatoria!")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF do local da pregação deve ter no Maximo 2 Caracteres!")]
        public string UF { get; set; }

        
        [Required(ErrorMessage = "Situação do Local de pregação é Obrigatorio!")]
        public SituacaoGeral Situacao { get; set; }
        [Required(ErrorMessage = "Id da Congregação é Obrigatorio!")]
        public int CongregacaoId { get; set; }

        [Required(ErrorMessage ="Data Criação é ")]
        public DateTime Data_Cadastro { get; set; }

        public virtual CongregacaoModel? Congregacao { get; set; }





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
