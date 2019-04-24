using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {        
        //METODO PARA VALIDAR LOGIN VIA POST
        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {
            bool login = usuario.ValidarLogin();
            if (login)
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", usuario.Nome);
                HttpContext.Session.SetString("IdUsuarioLogado", usuario.Id.ToString());

                return RedirectToAction("Menu_Inicial","Home");
            }
            else
            {
                TempData["MenssagemLoginInvalido"] = "Dados de acesso inválidos!!";
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult Login(int? id)
        {
            //QUEBAR A SESSAO DO USUARIO LOGADO
            if (id != null)
            {
                if (id == 0)
                {
                    HttpContext.Session.SetString("NomeUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("IdUsuarioLogado", string.Empty);
                }
            }
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        //METODO PARA CADASTRAR USUARIO VIA POST
        [HttpPost]        
        public IActionResult CadastrarUsuario(UsuarioModel usuario)
        {
            //VALIDAR TODOS OS CAMPOS ANTES DE GRAVAR NO BANCO
            if (ModelState.IsValid)
            {
                //CHAMADA DO METODO DA CLASSE USUARIOMODEL
                usuario.CadastrarUsuario();
                return RedirectToAction("Cadastro_Sucesso", "Usuario");
            }
            return View();
        }

        public IActionResult Cadastro_Sucesso()
        {
            return View();
        }
    }
}