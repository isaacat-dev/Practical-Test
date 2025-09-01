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
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistencia(CpfValidador.RemoverFormatacao(model.CPF)))
            {
                Response.StatusCode = 400;
                return Json($"Já existe cliente cadastrado com CPF: {model.CPF}");
            }
            else
            {
                if (model.Beneficiarios != null && model.Beneficiarios.Any())
                {
                    var cpfSemFormatacao = CpfValidador.RemoverFormatacao(model.CPF);
                    var cpfsBeneficiarios = model.Beneficiarios.Select(b => CpfValidador.RemoverFormatacao(b.CPF)).ToList();

                    if (cpfsBeneficiarios.Contains(cpfSemFormatacao))
                    {
                        Response.StatusCode = 400;
                        return Json("CPF do beneficiário não pode ser igual ao CPF do cliente");
                    }

                    model.Id = bo.Incluir(new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        CPF = CpfValidador.RemoverFormatacao(model.CPF),
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone
                    });


                    BoBeneficiario boBeneficiario = new BoBeneficiario();

                    foreach (var beneficiario in model.Beneficiarios)
                    {
                        beneficiario.IdCliente = model.Id;
                        boBeneficiario.Incluir(new Beneficiario()
                        {
                            CPF = CpfValidador.RemoverFormatacao(beneficiario.CPF),
                            Nome = beneficiario.Nome,
                            IdCliente = beneficiario.IdCliente
                        });
                    }
                }
                else
                {
                    model.Id = bo.Incluir(new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        CPF = CpfValidador.RemoverFormatacao(model.CPF),
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone
                    });
                }

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            var clienteExistente = bo.Consultar(model.Id);
            var cpfSemFormatacao = CpfValidador.RemoverFormatacao(model.CPF);

            if (clienteExistente.CPF != cpfSemFormatacao && bo.VerificarExistencia(cpfSemFormatacao))
            {
                Response.StatusCode = 400;
                return Json($"Já existe cliente cadastrado com CPF: {model.CPF}");
            }

            BoBeneficiario boBeneficiario = new BoBeneficiario();

            if (model.Beneficiarios != null && model.Beneficiarios.Any())
            {
                var cpfsBeneficiarios = model.Beneficiarios.Select(b => CpfValidador.RemoverFormatacao(b.CPF)).ToList();

                if (cpfsBeneficiarios.Contains(cpfSemFormatacao))
                {
                    Response.StatusCode = 400;
                    return Json("CPF do beneficiário não pode ser igual ao CPF do cliente");
                }
            }

            bo.Alterar(new Cliente()
            {
                Id = model.Id,
                CEP = model.CEP,
                Cidade = model.Cidade,
                CPF = cpfSemFormatacao,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone
            });

            var beneficiariosAtuais = boBeneficiario.ListarPorCliente(model.Id);

            var idsRecebidos = model.Beneficiarios.Where(b => b.Id > 0).Select(b => b.Id).ToList();

            foreach (var b in beneficiariosAtuais)
            {
                if (!idsRecebidos.Contains(b.Id))
                    boBeneficiario.Excluir(b.Id);
            }

            foreach (var beneficiario in model.Beneficiarios)
            {
                beneficiario.IdCliente = model.Id;
                if (beneficiario.Id > 0)
                {
                    boBeneficiario.Alterar(new Beneficiario()
                    {
                        Id = beneficiario.Id,
                        CPF = CpfValidador.RemoverFormatacao(beneficiario.CPF),
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    });
                }
                else
                {
                    boBeneficiario.Incluir(new Beneficiario()
                    {
                        CPF = CpfValidador.RemoverFormatacao(beneficiario.CPF),
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    });
                }
            }

            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    CPF = CpfValidador.AplicarFormatacao(cliente.CPF),
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };

                var beneficiarios = boBeneficiario.ListarPorCliente(id);
                model.Beneficiarios = beneficiarios.Select(b => new BeneficiarioModel()
                {
                    Id = b.Id,
                    CPF = CpfValidador.AplicarFormatacao(b.CPF),
                    Nome = b.Nome,
                    IdCliente = b.IdCliente
                }).ToList();


            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}