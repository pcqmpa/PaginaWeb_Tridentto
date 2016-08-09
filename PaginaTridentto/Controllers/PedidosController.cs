using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaginaTridentto.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pago()
        {
            return View();
        }

        public ActionResult Carro_Compras()
        {
            return View();
        }

        public ActionResult ConfirmarDireccion()
        {
            return View();
        }

        public ActionResult AddProducto()
        {
            return View();
        }
    }
}