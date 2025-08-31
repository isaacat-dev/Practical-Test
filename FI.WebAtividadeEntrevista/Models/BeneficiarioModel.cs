using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAtividadeEntrevista.Attributes;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Benefici�rio
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required(ErrorMessage = "CPF � obrigat�rio")]
        [CpfValidation(ErrorMessage = "CPF inv�lido")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "Nome � obrigat�rio")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no m�ximo 100 caracteres")]
        public string Nome { get; set; }

        /// <summary>
        /// IdCliente
        /// </summary>
        public long IdCliente { get; set; }
    }
}