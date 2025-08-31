using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            // Verifica se o CPF já existe para este cliente
            var cpfSemFormatacao = CpfValidador.RemoverFormatacao(model.CPF);
            if (bo.VerificarExistencia(cpfSemFormatacao, model.IdCliente))
            {
                Response.StatusCode = 400;
                return Json("CPF já cadastrado para este cliente");
            }

            model.Id = bo.Incluir(new Beneficiario()
            {
                CPF = cpfSemFormatacao,
                Nome = model.Nome,
                IdCliente = model.IdCliente
            });

            return Json(new { success = true, message = "Beneficiário incluído com sucesso", id = model.Id });
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            // Verifica se o CPF já existe para este cliente (excluindo o próprio beneficiário)
            var cpfSemFormatacao = CpfValidador.RemoverFormatacao(model.CPF);
            if (bo.VerificarExistencia(cpfSemFormatacao, model.IdCliente, model.Id))
            {
                Response.StatusCode = 400;
                return Json("CPF já cadastrado para este cliente");
            }

            bo.Alterar(new Beneficiario()
            {
                Id = model.Id,
                CPF = cpfSemFormatacao,
                Nome = model.Nome,
                IdCliente = model.IdCliente
            });

            return Json(new { success = true, message = "Beneficiário alterado com sucesso" });
        }

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();
                bo.Excluir(id);

                return Json(new { success = true, message = "Beneficiário excluído com sucesso" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { success = false, message = "Erro ao excluir beneficiário" });
            }
        }

        [HttpGet]
        public JsonResult ListarPorCliente(long idCliente)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();
                var beneficiarios = bo.ListarPorCliente(idCliente);

                var result = beneficiarios.Select(b => new
                {
                    Id = b.Id,
                    CPF = CpfValidador.AplicarFormatacao(b.CPF),
                    Nome = b.Nome,
                    IdCliente = b.IdCliente
                }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { success = false, message = "Erro ao listar beneficiários" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Consultar(long id)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();
                var beneficiario = bo.Consultar(id);

                if (beneficiario != null)
                {
                    var result = new
                    {
                        Id = beneficiario.Id,
                        CPF = CpfValidador.AplicarFormatacao(beneficiario.CPF),
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                Response.StatusCode = 404;
                return Json(new { success = false, message = "Beneficiário não encontrado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { success = false, message = "Erro ao consultar beneficiário" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}