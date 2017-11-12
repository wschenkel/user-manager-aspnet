using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorUsuarios.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Login, string Senha)
        {

            using (var db = new GerenciadorUsuariosContext())
            {
         
                var loginUsuario = db.Usuarios.FirstOrDefault(d => d.Login == Login && d.Senha == Senha);

                if (loginUsuario != null)
                {
                    Session["nomeUsuario"] = loginUsuario.Nome;
                    Session["Logado"] = "true";
                    Session["usuarioId"] = loginUsuario.idUsuario;

                    foreach (var direito in loginUsuario.Direitos)
                    {
                  
                        if ((int)direito.idDireito == 1)
                        {
                            Session["Admin"] = "true";
                        } else
                        {
                            Session["Admin"] = "false";
                        }
                    }
                } else
                {
                    return RedirectToAction("Index", "Home");
                }

                db.Dispose();
            }

            return RedirectToAction("Index", "Menu");
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(FormCollection form)
        {

            using (var db = new GerenciadorUsuariosContext())
            {
                var novoUsuario = new Usuario
                {
                    Nome = form["Nome"],
                    Login = form["Login"],
                    Senha = form["Senha"],
                    Email = form["Email"],
                    Direitos = { db.Direitos.FirstOrDefault(d => d.Nome == "user") }
                };

                db.Usuarios.Add(novoUsuario);

                var log = new Log
                {
                    idUsuario = novoUsuario.idUsuario,
                    Acao = "Cadastrou-se.",
                    DataAcao = DateTime.Now
                };

                db.Log.Add(log);

                db.SaveChanges();

                Session["nomeUsuario"] = novoUsuario.Nome;
                Session["Logado"] = "true";
                Session["Admin"] = "false";
                Session["usuarioId"] = novoUsuario.idUsuario;

                db.Dispose();
            }

            return RedirectToAction("Index", "Menu");
        }
    }
}