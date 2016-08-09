using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginaTridentto.Models
{
    public class pedidos_modelo
    {

    }

    public class Pagos
    {
        public int merchantId { get; set; }

        /*Numero de la Factura o cedula del comprador*/
        public string referenceCode { get; set; }
        public string description { get; set; }

        /*Valor de la comra*/
        public double amount { get; set; }

        /*Iva*/
        public int tax { get; set; }

        /*Base para liquidar el iva*/
        public double taxReturnBase { get; set; }

        /*Llave Publica*/
        public string signature { get; set; }

        public int accountId { get; set; }

        /*Moneda*/
        public string currency { get; set; }

        /*Nombre del comprador*/
        public string buyerFullName { get; set; }

        /*Email*/
        public string buyerEmail { get; set; }

        /*Direccion de entrega de la mercancia*/
        public string shippingAddress { get; set; }

        /*Ciudad de Entrega*/
        public string shippingCity { get; set; }

        /*Pais de Entrega*/
        public string shippingCountry { get; set; }

        /*Telefono del Comprador*/
        public string telephone { get; set; }


    }

}
