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
        IHttpContextAccessor HttpContextAccessor;

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }



        [HttpGet]
        public IActionResult NovoPlanoCont()
        {
            //RETORNAR OS DADOS PARA VIEW
            PlanoContasModel objConta = new PlanoContasModel(HttpContextAccessor);
            ViewBag.ListaPlanoConta = objConta.ListaPlanoConta();
            return View();
        }


        [HttpPost]
        public IActionResult NovoPlanoConta(PlanoContasModel novaConta)
        {
            if (ModelState.IsValid)
            {
                novaConta.HttpContextAccessor = HttpContextAccessor;
                novaConta.CadastrarPlanoCota();
                return RedirectToAction("NovoPlanoCont");
            }
            return View();
        } 
        
        public IActionResult CadPlanConta()
        {
            return View();
        }
    }
}