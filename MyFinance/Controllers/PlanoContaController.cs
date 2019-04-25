using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
   
    public class PlanoContaController : Controller
    {
        ////RECEBER VIA INJEÇÃO DE DEPENDENCIA
        IHttpContextAccessor HttpContextAccessor;

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        //VIEW LISTAR NOVO PLANO DE CONTA
        [HttpGet]
        public IActionResult MeusPlanosContas()
        {
            //RETORNAR OS DADOS PARA VIEW
            PlanoContaModel objPlanoConta = new PlanoContaModel(HttpContextAccessor);
            ViewBag.ListaPlanoConta = objPlanoConta.ListaPlanoConta();
            return View();
        }

        //VIEW CADASTRAR PLANO DE CONTAS VIA POST
        [HttpPost]
        public IActionResult CadPlanConta(PlanoContaModel novaConta)
        {
            if (ModelState.IsValid)
            {
                novaConta.HttpContextAccessor = HttpContextAccessor;
                novaConta.CadastrarPlanoCota();
                return RedirectToAction("MeusPlanosContas");
            }
            return View();
        }

        public IActionResult CadPlanConta()
        {
            return View();
        }

        //EXCLUIR CONTAS
        [HttpGet]
        public IActionResult ExcluirPlanoConta(int id)
        {
            PlanoContaModel objPlanoConta = new PlanoContaModel(HttpContextAccessor);
            objPlanoConta.Excluir(id);
            return RedirectToAction("MeusPlanosContas");
        }
    }
}