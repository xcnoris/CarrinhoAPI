﻿using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarrinhoAPI.Models
{
    public class Carrinho
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "Nome do Carrinho é Obrigatorio!")]
        [StringLength(30, ErrorMessage = "Nome do Carrinho Deve ter no Maximo 30 Caracteres!")]
        public string Nome { get; set; }
        public string Congregacao_ID { get; set; }
        public string Congregacao_Nome { get; set; }

        [Required(ErrorMessage = "Situação do Carrinho é Obrigatorio!")]

        public string Situacao { get; set; }

        [Required(ErrorMessage = "Codigo do Carrinho é obrigatorio!")]
        public string Codigo_Carrinho { get; set; }

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
            if (this.Situacao == "0")
            {
                throw new ValidationException("Situação não pode ser todas!");
            }

        }
    }
}
