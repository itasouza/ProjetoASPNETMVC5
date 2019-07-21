using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Modelos;

namespace ASPNETMVC5.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona

        [HttpGet]
        public ActionResult Create()
        {

            using (Repositorio<Persona_Tipo> obj = new Repositorio<Persona_Tipo>())
            {
                obj.exception += obj_Exception;
                ViewBag.Persona_Tipo = obj.Filter(x => true);
            }

            using (Repositorio<Estatu> obj = new Repositorio<Estatu>())
            {
                obj.exception += obj_Exception;
                ViewBag.Estatus = obj.Filter(x => true);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Persona person)
        {
            Thread.Sleep(5000);
            using (Repositorio<Persona> obj = new Repositorio<Persona>())
            {
                obj.exception += obj_Exception;
                obj.Create(person);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateComAjax(Persona person)
        {
            Thread.Sleep(5000);
            bool result = false;
            string mensagem = "Erro al criar o regsitro";
            using (Repositorio<Persona> obj = new Repositorio<Persona>())
            {
                obj.exception += obj_Exception;
                if(obj.Create(person) != null)
                {
                    result = true;
                    mensagem = "Registro criado!";
                }
            }

            return Json(new { result = result, mensagem = mensagem }, JsonRequestBehavior.AllowGet);
        }


        private void obj_Exception(object sende, ExceptionEventArgs e)
        {
            if (e.EntityValidationErrors != null)
            {
                throw new DbEntityValidationException(e.Mensagem,
                                                      e.EntityValidationErrors, e.InnerException){ Source = e.fonte };
            }
            else
                throw new Exception(e.Mensagem, e.InnerException) { Source = e.fonte };
         
        }

    }
}