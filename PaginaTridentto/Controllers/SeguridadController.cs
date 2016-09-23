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
                Session["idUsuario"] = datos.Id;
                Session["usuario"] = datos.StrUsuario;
                Session["nombre"] = datos.StrNombre;
                Session["email"] = datos.StrEmail;
                Session["grupo"] = datos.IdGrupo;


                if (datos != null)
                {
                    return Json(new
                    {
                        datos = datos
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

            /*Validamos si esta logueado si no lo redireccionamos al login*/


            string usuario = Session["usuario"].ToString();

            if (usuario != "")
            {
                /*Cargamos los paise*/

                

                /*Cargamos Los Departamentos Primero*/

                var vc = new Clases.MaestrosDao();

                var paises = vc.ListaPaises();
                var dpt = vc.ListaDepartamentos();

                ViewData["paises"] = paises;
                ViewData["departamentos"] = dpt;

                ViewBag.usuario = Session["usuario"].ToString();
                ViewBag.nombre = Session["nombre"].ToString();


                return View();
            }
            else
            {
                return RedirectToAction("Index", "Seguridad");
            }

            /*Fin*/


        }
        [HttpPost]
        public ActionResult AddOtrosDatosUsuario(DatosComplemenatriosUsuario modelo)
        {

            var vc = new Clases.MaestrosDao();

            var paises = vc.ListaPaises();
            var dpt = vc.ListaDepartamentos();

            ViewData["paises"] = paises;
            ViewData["departamentos"] = dpt;

            ViewBag.usuario = Session["usuario"].ToString();
            ViewBag.nombre = Session["nombre"].ToString();

            /*Actualizamos la informacion*/

            Int64 idUsuario =Convert.ToInt64(Session["idUsuario"]);

            modelo.IdUsuario = idUsuario;

            var res = vc.AddDatosComplementariosUsuario(modelo, out _strMensaje);

            if (res)
            {
                return Json(new
                {
                    mensaje = _strMensaje

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

        public ActionResult ConsultaOtrosDatosUsuario(Int64 idUsuario)
        {
            var vc = new Clases.MaestrosDao();

            var datos = vc.ConsultaOtrosDatosUsuario(idUsuario);

            if (datos != null)
            {
                return Json(new
                {
                    xdato = datos
                });
            }
            else
            {
                return Json(new
                {
                    xdato = string.Empty
                });
            }
        }

        public ActionResult CambioPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambioPassword(string passNew,string passConfirmar)
        {
            var vc = new Clases.SeguridadDao();

            var validar = passNew.Equals(passConfirmar);
            if (!validar)
            {
                return Json(new
                {
                    Error = "Los dos Pasword no coinciden"
                });
            }
            else
            {
                Int64 idUsuario = Convert.ToInt64(Session["idUsuario"]);

                var res = vc.CambioPassword(idUsuario, passNew, out _strMensaje);

                if (res)
                {
                    return Json(new
                    {
                        mensaje = _strMensaje
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

         
            
        }

        public ActionResult Direcciones()
        {

            return View();
        }

        public ActionResult OrdenesUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ciudades(int id)
        {
            var vc = new Clases.MaestrosDao();

            var lista = vc.ListaCiudades(id);
            if (lista != null)
            {
                return Json(new
                {
                    xlista=lista
                });
            }
            else
            {
                return Json(new
                {
                    Error = "No hay datos para cargar"
                });
            }
        }
    }
}