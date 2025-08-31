using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Utils
{
    /// <summary>
    /// Classe utilitária para validação de CPF
    /// </summary>
    public static class CpfValidador
    {
        /// <summary>
        /// Valida se o CPF informado é válido
        /// </summary>
        /// <param name="cpf">CPF a ser validado</param>
        /// <returns>True se o CPF for válido, False caso contrário</returns>
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            // Remove formatação
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            // Verifica se tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Verifica se todos os caracteres são números
            if (!cpf.All(char.IsDigit))
                return false;

            // Calcula os dígitos verificadores
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Remove a formatação do CPF
        /// </summary>
        /// <param name="cpf">CPF formatado</param>
        /// <returns>CPF sem formatação</returns>
        public static string RemoverFormatacao(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            return cpf.Replace(".", "").Replace("-", "").Trim();
        }

        /// <summary>
        /// Aplica formatação ao CPF
        /// </summary>
        /// <param name="cpf">CPF sem formatação</param>
        /// <returns>CPF formatado</returns>
        public static string AplicarFormatacao(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            cpf = RemoverFormatacao(cpf);

            if (cpf.Length != 11)
                return cpf;

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }
    }
}