using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    /// <summary>
    /// Classe de neg�cio para Benefici�rio
    /// </summary>
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo benefici�rio
        /// </summary>
        /// <param name="beneficiario">Objeto de benefici�rio</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um benefici�rio
        /// </summary>
        /// <param name="beneficiario">Objeto de benefici�rio</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o benefici�rio pelo id
        /// </summary>
        /// <param name="id">id do benefici�rio</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.Consultar(id);
        }

        /// <summary>
        /// Lista benefici�rios por cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiario> ListarPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.ListarPorCliente(idCliente);
        }

        /// <summary>
        /// Excluir o benefici�rio pelo id
        /// </summary>
        /// <param name="id">id do benefici�rio</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.Excluir(id);
        }

        /// <summary>
        /// Verifica se existe benefici�rio com o CPF para o cliente
        /// </summary>
        /// <param name="cpf">CPF do benefici�rio</param>
        /// <param name="idCliente">Id do cliente</param>
        /// <param name="idBeneficiario">Id do benefici�rio (para exclus�o na verifica��o)</param>
        /// <returns></returns>
        public bool VerificarExistencia(string cpf, long idCliente, long? idBeneficiario = null)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.VerificarExistencia(cpf, idCliente, idBeneficiario);
        }

        /// <summary>
        /// Exclui todos os benefici�rios de um cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        public void ExcluirPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.ExcluirPorCliente(idCliente);
        }
    }
}