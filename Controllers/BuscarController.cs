using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorUsuarios.Controllers
{
    public class BuscarController : Controller
    {
        // GET: Buscar
        public ActionResult Index()
        {
            
            if (Session["logado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Usuario> listUsuarios = new List<Usuario>();

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

        [HttpPost]
        public ActionResult BuscarContato(string nome, string selDireito)
        {

            List<Usuario> listUsuarios = new List<Usuario>();

            using (var db = new GerenciadorUsuariosContext())
            {
                var query = db.Usuarios.Where(u => u.Nome == nome && u.Direitos.FirstOrDefault(d => d.Nome == selDireito).Nome == selDireito).ToList();
              
                foreach (var usuario in query.ToList())
                {
                    var user = new Usuario();
                    user.idUsuario = usuario.idUsuario;
                    user.Nome = usuario.Nome;
                    user.Login = usuario.Login;
                    user.Senha = usuario.Senha;
                    user.Email = usuario.Email;
                    user.Direitos = usuario.Direitos;

                    listUsuarios.Add(user);
                }

                db.Dispose();
            }

            return View("Resultado", listUsuarios);
        }
    }
}