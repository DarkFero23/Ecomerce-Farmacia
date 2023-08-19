using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia
{
    public partial class Contact : System.Web.UI.Page
    {
        ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string datosLeidos = conexion.listarProductos();
                    JObject json = JObject.Parse(datosLeidos);
                    JArray categoriasArray = (JArray)json["Table"];

                    // Obtén los nombres de las categorías del arreglo JSON
                    List<string> categoriasList = new List<string>();
                    //Buscamos dentro de todos los datos de categoria y sacamos sus nombres"
                    foreach (JToken categoriaToken in categoriasArray)
                    {
                        string nombreCategoria = categoriaToken["NOMBRE_CATEGORIA"].ToString();
                        //TRIM = quita espacios.
                        categoriasList.Add(nombreCategoria.Substring(nombreCategoria.LastIndexOf(":") + 1).Trim());
                    }

                    // Asigna los nombres de las categorías al DropDownList
                    producto_categoria.DataSource = categoriasList;
                    producto_categoria.DataBind();

                    // Agrega una opción por defecto
                    producto_categoria.Items.Insert(0, new ListItem("Seleccione una categoría", ""));
                }
                catch (Exception ex)
                {
                    String mensaje = ex.Message;
                    // Maneja el error de obtener las categorías aquí
                }
                try
            {
                 //Actulizamos el GridView
                string datosLeidos = conexion.listarProductos();
                DataTable dt = JObject.Parse(datosLeidos)["Table"].ToObject<DataTable>();
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                String mensaje = ex.Message;

                }
            }
        }

        protected void BT_guardar(object sender, EventArgs e)
        {
            //Asginacion de varibles de los text box 
            string prod_nombre_ac = producto_far.Text;
            string prod_nombre_dc = producto_nuevo.Text;
            string prod_des = producto_des.Text;
            double prod_prec = double.Parse(producto_precio.Text);
            string prod_cate = producto_categoria.Text;
            int prod_stock = int.Parse(producto_stock.Text);


            try
            {
                //Aca se ejecuta la logia de Actaulizar de nuestra base de datos con lo asignado
                String resultado = conexion.actualizarProducto(prod_nombre_ac, prod_nombre_dc, prod_des, prod_prec, prod_cate, prod_stock);
                //Para limpair los espacios que estan en lo s tesxtbox
                producto_far.Text = string.Empty;
                producto_nuevo.Text = string.Empty;
                producto_des.Text = string.Empty;
                producto_precio.Text = string.Empty;
                producto_categoria.Text = string.Empty;
                producto_stock.Text = string.Empty;
                //Volver a redirigirnos a la misma URL (osea al mismo liugar)
                Response.Redirect(Request.Url.AbsoluteUri);


            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

            }

        }

        protected void BT_reset(object sender, EventArgs e)
        {
            //Es limpiar los Textbox
            producto_far.Text = string.Empty;
            producto_nuevo.Text = string.Empty;
            producto_des.Text = string.Empty;
            producto_precio.Text = string.Empty;
            producto_categoria.Text = string.Empty;
            producto_stock.Text = string.Empty;
        }

        protected void BT_crear(object sender, EventArgs e)
        {
            //Invocamos al web service de crear y simplemente creamos 
            string prod_nombre_ac = producto_far.Text;
            string prod_des = producto_des.Text;
            double prod_prec = double.Parse(producto_precio.Text);
            string prod_cate = producto_categoria.Text;
            int prod_stock = int.Parse(producto_stock.Text);
            try
            {
                //aca se llama
                String resultado = conexion.crearProducto(prod_nombre_ac, prod_des, prod_prec, prod_cate, prod_stock);

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

            }
        }
        protected void BT_cancelar(object sender, EventArgs e)
        {
            //Simplemete lo que va a hacer es mostrar la visibilidad de lo demas botones y limpiar los txtbox
            button_save.Visible = false;
            button_reset.Visible = true;
            button_create.Visible = true;
            button_cancelar.Visible = false;
            Label8.Visible = true;
            producto_far.Visible = true;
            Label2.Visible= false;
            producto_nuevo.Visible = false;

            producto_far.Text = string.Empty;
            producto_categoria.Text = string.Empty;
            producto_des.Text = string.Empty;
            producto_nuevo.Text = string.Empty;
            producto_precio.Text = string.Empty;
            producto_stock.Text = string.Empty;
          



        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BTN_ELIMINAR(object sender, EventArgs e)
        {
            //Invoca al WS de eliminar y eliminar por el Nombre poducto
            Button btnEliminar = (Button)sender;
            GridViewRow row = (GridViewRow)btnEliminar.NamingContainer;
            string nom_produ = row.Cells[1].Text;
            try
            {
                string resultado = conexion.eliminarProducto(nom_produ);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

            }
        }
        protected void BTN_ACTUALIZAR(object sender, EventArgs e)
        {
            //Este boton lo que hace es pasar los datos del gridview a los txtbox y lo hace bien
            Button btnActualizar = (Button)sender;
            GridViewRow row = (GridViewRow)btnActualizar.NamingContainer;
            string prod_nombre = row.Cells[1].Text; //Necesario

            // Obtener los valores de la fila seleccionada
            string idProducto = row.Cells[0].Text; // ID Producto
            string prod_des = row.Cells[2].Text; // Descripcion
            string prod_prec = row.Cells[3].Text; //Precio 
            string prod_cate = row.Cells[4].Text; // Categoria
            string prod_stock= row.Cells[5].Text; // Stock

            // Asignar los valores a los TextBox correspondientes
            producto_nuevo.Text= prod_nombre;
            producto_far.Text = prod_nombre;
            producto_des.Text = prod_des;
            producto_precio.Text = prod_prec;
            producto_categoria.Text = prod_cate;
            producto_stock.Text = prod_stock;
            //Visibilidad de botones 
            button_create.Visible = false;
            button_save.Visible = true;
            button_cancelar.Visible = true;
            button_reset.Visible = false;
            Label8.Visible = false;
            producto_far.Visible = false;
            Label2.Visible = true;
            producto_nuevo.Visible = true;

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }

        protected void producto_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}