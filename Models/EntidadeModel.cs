using CarrinhoAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrinhoAPI.Models
{
    public class EntidadeModel
    {

        public int Id { get; set; }

        //[Required(ErrorMessage = "CPF do cliente é obrigatorio!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Codigo do cliente aceita somente numericos!")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF do cliente deve ter 11 digitos!")]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "Nome do cliente é obrigatorio!")]
        [StringLength(250, ErrorMessage = "Nome do Cliente deve ter no Maximo 70 Caracteres!")]
        public string Nome { get; set; }

        [StringLength(10, ErrorMessage = "Cep deve ter no maximo 10 caracteres!")]
        public string? CEP { get; set; }
        public string? Cidade_Nome { get; set; }
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF do cliente deve ter 2 digitos!")]
        public string? UF { get; set; }

        [StringLength(250, ErrorMessage = "Endereço do Endereço deve ter no Maximo 250 Caracteres!")]
        public string? Endereco { get; set; }
        public string? Endereco_Numero { get; set; }

        [StringLength(250, ErrorMessage = "Complemento do Endereço deve ter no Maximo 250 Caracteres!")]
        public string? Endereco_Complemento { get; set; }
        public string? Bairro { get; set; }
       
        [StringLength(2, MinimumLength = 2, ErrorMessage = "DDD do Cliente deve ter no Maximo 2 Caracteres!")]
        public string? DDD_Celular { get; set; }

        [StringLength(9, ErrorMessage = "Numero de Celular deve ter no Maximo 9 Caracteres!")]
        public string? Celular { get; set; }
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Sexo do Cliente deve ter no Maximo 1 Caracteres!")]
        public string? Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Email { get; set; }
        public int CongregacaoId { get; set; }
        public virtual CongregacaoModel? Congregacao {  get; set; }
        public DateTime? Data_Cadastro { get; set; }
        public void ValidarClass()
        {
            // Captura os results dos testes de validação dos campos
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            // retorna um false caso algum dos teste de problema
            bool isValid = Validator.TryValidateObject(this, context, results, true);
            // se retorna false, ele entra no loop
            if (!isValid)
            {
                StringBuilder sbrErrors = new StringBuilder();
                foreach (var validationResult in results)
                {
                    // Adiciona no string(stringBuilder) todos os erros 
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }
                // Add as mensagens de erro para um exeção do tipo 
                // E força a mensagem da exceção
                throw new ValidationException(sbrErrors.ToString());
            }
            // Valida CPF
            if (CPF is not null)
            {
                if (!MetodosGerais.ValidaCPF(this.CPF))
                {
                    throw new ValidationException("Cpf Inválido!");
                }
            }
        }

    }
}
