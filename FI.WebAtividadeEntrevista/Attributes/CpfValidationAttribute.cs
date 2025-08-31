using System;
using System.ComponentModel.DataAnnotations;
using FI.AtividadeEntrevista.Utils;

namespace WebAtividadeEntrevista.Attributes
{
    /// <summary>
    /// Atributo de validação para CPF
    /// </summary>
    public class CpfValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var cpf = value.ToString();
            return CpfValidador.ValidarCpf(cpf);
        }

        public override string FormatErrorMessage(string name)
        {
            return "CPF inválido";
        }
    }
}