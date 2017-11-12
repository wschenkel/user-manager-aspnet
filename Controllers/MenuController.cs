using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorUsuarios.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            if (Session["logado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Usuario> listUsuarios =  new List<Usuario>();

            using (var db = new GerenciadorUsuariosContext())
            {
                foreach (var usuarios in db.Usuarios.ToList())
                {
                    var user = new Usuario();
                    user.idUsuario = usuarios.idUsuario;
                    user.Nome = usuarios.Nome;
                    user.Login = usuarios.Login;
                    user.Senha = usuarios.Senha;
                    user.Email = usuarios.Email;
                    user.Direitos = usuarios.Direitos;

                    listUsuarios.Add(user);
                }

                db.Dispose();
            }

            return View(listUsuarios);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {

            List<Usuario> listUsuarios = new List<Usuario>();
            var userSelect = new Usuario();

            using (var db = new GerenciadorUsuariosContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(d => d.idUsuario == id);
                userSelect.idUsuario = usuario.idUsuario;
                userSelect.Nome = usuario.Nome;
                userSelect.Login = usuario.Login;
                userSelect.Senha = usuario.Senha;
                userSelect.Email = usuario.Email;
                userSelect.Direitos = usuario.Direitos;

                db.Dispose();
            }

            return View(userSelect);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            string selPermissao;

            using (var db = new GerenciadorUsuariosContext())
            { 
                var encontrarUsuario = db.Usuarios.Find(id);

                if (encontrarUsuario != null) {
                    encontrarUsuario.Nome = form["Nome"];
                    encontrarUsuario.Login = form["Login"];
                    encontrarUsuario.Senha = form["Senha"];
                    encontrarUsuario.Email = form["Email"];

                    selPermissao = form["selpermissao"].ToString();
               
                    encontrarUsuario.Direitos = db.Direitos.Where(d => d.Nome == selPermissao).ToList();

                    var log = new Log
                    {
                        idUsuario = (long)Session["usuarioId"],
                        Acao = Session["nomeUsuario"] + " editou as Informações de " + encontrarUsuario.Nome,
                        DataAcao = DateTime.Now
                    };

                    db.Log.Add(log);
                    db.SaveChanges();
                }

                db.Dispose();
            }

            return RedirectToAction("Index");
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            
            using (var db = new GerenciadorUsuariosContext())
            {

                var usuarioRemover = db.Usuarios.SingleOrDefault(x => x.idUsuario == id);
                string nomeRemovido = usuarioRemover.Nome;


                if (usuarioRemover != null)
                {

                    var log = new Log
                    {
                        idUsuario = (long)Session["usuarioId"],
                        Acao = Session["nomeUsuario"] + " Removeu o usuário " + nomeRemovido,
                        DataAcao = DateTime.Now
                    };

                    db.Log.Add(log);

                    db.Usuarios.Remove(usuarioRemover);

                    db.SaveChanges();
                    db.Dispose();
                }
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            if (Session["logado"] != null)
            {
                Session["nomeUsuario"] = null;
                Session["Logado"] = null;
                Session["admin"] = null;
                Session["usuarioId"] = null;
            }

            return RedirectToAction("Index","Home");
        }

    }
}
