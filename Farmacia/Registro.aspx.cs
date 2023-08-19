using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            string datosLeidos = conexion.listarVentas();   
            DataTable dt = JObject.Parse(datosLeidos)["Table"].ToObject<DataTable>();

            GridView1.DataSource = dt;
            GridView1.DataBind();
        
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = Calendar1.SelectedDate;

            string datosLeidos = conexion.listarVentas();
            JObject response = JObject.Parse(datosLeidos);
            JArray ventas = response["Table"] as JArray;

            if (ventas != null)
            {
                JArray ventasFiltradas = new JArray();

                foreach (JObject venta in ventas)
                {
                    string fechaVentaStr = venta["FECHA"].ToString();
                    DateTime fechaVenta;

                    if (DateTime.TryParseExact(fechaVentaStr, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaVenta))
                    {
                        if (fechaVenta.Date == fechaSeleccionada.Date)
                        {
                            ventasFiltradas.Add(venta);
                        }
                    }
                    else
                    {
                        // Manejar el caso en que la cadena de fecha no sea válida
                    }
                }

                string datosFiltrados = ventasFiltradas.ToString();

                GridView1.DataSource = ventasFiltradas;
                GridView1.DataBind();
            }
            else
            {
                // Manejar el caso en que no se encuentren datos de ventas en la respuesta JSON
            }





        }

    }
}