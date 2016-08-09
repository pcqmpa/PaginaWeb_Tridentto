using PaginaTridentto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaginaTridentto.Controllers
{
    public class SeguridadController : Controller
    {

        private string _strMensaje;
        // GET: Seguridad
        public ActionResult Index()
        {
            ViewBag.Error = "";
            ViewBag.mensaje = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario modelo)
        {

            /*Validamos si tiene datos para cargar*/

            if (string.IsNullOrEmpty(modelo.StrEmail) & string.IsNullOrEmpty(modelo.StrNombre) & string.IsNullOrEmpty(modelo.StrPassword))
            {
                ViewBag.Error = "Debe de ingresar los datos correctamenta";
            }
            else
            {
                var vc = new Clases.SeguridadDao();


                var res = vc.AddUsuario(modelo, out _strMensaje);



                if (res)
                {
                    ViewBag.mensaje = _strMensaje;
                }
                else
                {
                    ViewBag.Error = _strMensaje;
                }
            }



            return View();
        }

        public ActionResult ValidarUser(string usuario,string password)
        {
            var vc = new Clases.SeguridadDao();
            var datos = vc.ValidarUsuario(usuario, password, out _strMensaje);

            if (datos != null)
            {
                return Json(new
                {
                    datos=datos
                });
            }
            else
            {
                return Json(new
                {
                    Error = _strMensaje
                });
            }

        }

        public ActionResult Cuentas()
        {
            return View();
        }

        public ActionResult Direcciones()
        {
            return View();
        }

        public ActionResult OrdenesUsuario()
        {
            return View();
        }
    }
}