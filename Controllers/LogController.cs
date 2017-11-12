using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorUsuarios.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {

            if (Session["logado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Log> listLogs = new List<Log>();

            using (var db = new GerenciadorUsuariosContext())
            {
                foreach (var log in db.Log.ToList())
                {
                    var novoLog = new Log();
                    novoLog.Usuario = log.Usuario;
                    novoLog.Acao = log.Acao;
                    novoLog.DataAcao = log.DataAcao;
                    listLogs.Add(novoLog);
                }

                db.Dispose();
            }

            return View(listLogs);
        }

        [HttpPost]
        public ActionResult BuscarLog(string nome)
        {
            List<Log> listLogs = new List<Log>();

            using (var db = new GerenciadorUsuariosContext())
            {
                var query = db.Log.Where(l => l.Usuario.Nome == nome).ToList();

                foreach (var logItem in query.ToList())
                {
                    var log = new Log();
                    log.Usuario = logItem.Usuario;
                    log.Acao = logItem.Acao;
                    log.DataAcao = logItem.DataAcao;

                    listLogs.Add(log);
                }

                db.Dispose();
            }

            return View("Resultado", listLogs);
        }
    }
}