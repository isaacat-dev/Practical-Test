using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    /// <summary>
    /// Classe de negócio para Beneficiário
    /// </summary>
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.Consultar(id);
        }

        /// <summary>
        /// Lista beneficiários por cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiario> ListarPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.ListarPorCliente(idCliente);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.Excluir(id);
        }

        /// <summary>
        /// Verifica se existe beneficiário com o CPF para o cliente
        /// </summary>
        /// <param name="cpf">CPF do beneficiário</param>
        /// <param name="idCliente">Id do cliente</param>
        /// <param name="idBeneficiario">Id do beneficiário (para exclusão na verificação)</param>
        /// <returns></returns>
        public bool VerificarExistencia(string cpf, long idCliente, long? idBeneficiario = null)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.VerificarExistencia(cpf, idCliente, idBeneficiario);
        }

        /// <summary>
        /// Exclui todos os beneficiários de um cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        public void ExcluirPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.ExcluirPorCliente(idCliente);
        }
    }
}