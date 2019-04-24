using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        //CONTA
        public IActionResult MinhaConta()
        {
            //RETORNAR OS DADOS PARA VIEW
            ContaModel objConta = new ContaModel(HttpContextAccessor);
            ViewBag.ListaConta = objConta.ListaConta();
            return View();
        }

        //CADASTRAR CONTA
        [HttpPost]
        public IActionResult CadastraConta(ContaModel novaConta)
        {
            //VALIDAR SE OS DADOS DO FORMULARIO ESTA TODO PREENCHIDO
            if (ModelState.IsValid)
            {                
                novaConta.InsertNovaConta();
                return RedirectToAction("MinhaConta");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CadastraConta()
        {
            return View();
        }

        //EXCLUIR CONTAS
        [HttpGet]
        public IActionResult ExcluirConta(int id)
        {
            ContaModel objConta = new ContaModel(HttpContextAccessor);
            objConta.Excluir(id);
            return RedirectToAction("MinhaConta");
        }
    }
}