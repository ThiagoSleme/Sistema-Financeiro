using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        public IActionResult Transacoes()
        {
            return View();
        }
    }
}