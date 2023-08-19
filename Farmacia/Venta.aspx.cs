using Microsoft.Ajax.Utilities;
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
    public partial class WebForm2 : System.Web.UI.Page
    {
        ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NombresProductos"] == null)
            {
                Session["NombresProductos"] = new List<string>();
            }

            if (Session["Cantidades"] == null)
            {
                Session["Cantidades"] = new List<string>();
            }

            if (Session["PreciosProductos"] == null)
            {
                Session["PreciosProductos"] = new List<decimal>();
            }

            if (Session["DNIs"] == null)
            {
                Session["DNIs"] = new List<string>();
            }

            if (!IsPostBack)
            {
                // Crear una nueva lista para almacenar los nombres de los productos
                List<string> nombresProductos = new List<string>();
                List<string> Cantidades = new List<string>();
                List<decimal> preciosProductos = new List<decimal>();
                List<string> dnisSelectedList = new List<string>();
                

                // Guardar la lista en una variable de sesión
                Session["NombresProductos"] = nombresProductos;
                Session["Cantidades"] = Cantidades;
                Session["PreciosProductos"] = preciosProductos;
                Session["DNIs"]= dnisSelectedList;
             

                BindGridView();
            }
            if (!IsPostBack)
            {
                try
                {
                    string maxIDConjunto = conexion.ObtenerMaxIDConjunto();
                    string datosLeidos = conexion.listarProductos();
                    JObject jsonProductos = JObject.Parse(datosLeidos);
                    JArray categoriasArray = (JArray)jsonProductos["Table"];

                    // Obtén los nombres de las categorías del arreglo JSON
                    List<string> categoriasList = new List<string>();
                    foreach (JToken categoriaToken in categoriasArray)
                    {
                        string nombreCategoria = categoriaToken["NOMBRE_PRODUCTO"].ToString();
                        categoriasList.Add(nombreCategoria.Substring(nombreCategoria.LastIndexOf(":") + 1).Trim());
                    }

                    // Asigna los nombres de las categorías al DropDownList
                    producto_categoria.DataSource = categoriasList;
                    producto_categoria.DataBind();

                    string datosLeer = conexion.listarUsuarios();
                    JObject jsonUsuarios = JObject.Parse(datosLeer);
                    JArray usuariosArray = (JArray)jsonUsuarios["Table"];

                    // Obtén los DNIs de los usuarios del arreglo JSON
                    List<string> dnisSelectedList = new List<string>();
                    foreach (JToken usuarioToken in usuariosArray)
                    {
                        string dniUsuario = usuarioToken["DNI"].ToString();
                        dnisSelectedList.Add(dniUsuario);
                    }

                    // Asigna los DNIs al DropDownList
                    dnis_drop.DataSource = dnisSelectedList;
                    dnis_drop.DataBind();



                }
                catch (Exception ex)
                {
                    String mensaje = ex.Message;
                    // Maneja el error de obtener las categorías aquí
                }
            }
            try
            {
            

            }
            catch (Exception ex)
            {

                String mensaje = ex.Message;

            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener el DNI seleccionado en el DropDownList
            string dniSeleccionado = dnis_drop.SelectedItem.Text;

            // Obtener el nombre del producto seleccionado en el DropDownList
            string nombreProducto = producto_categoria.SelectedItem.Text;
            string cantidad = TextBoxCantidad.Text;

            // Verificar si se ha seleccionado un producto
            if (!string.IsNullOrEmpty(nombreProducto))
            {
                if (string.IsNullOrEmpty(cantidad))
                {
                    etiquetaAdvertencia.Visible = true;
                    etiquetaAdvertencia.Text = "Por favor, ingrese la cantidad.";
                    return;
                }

                // Verificar el stock del producto
                int stockSuficiente = VerificarStock(nombreProducto, int.Parse(cantidad));
                if (stockSuficiente >= int.Parse(cantidad))
                {
                    // Obtener la lista de nombres de productos, cantidades, DNIs y precios desde la variable de sesión
                    List<string> nombresProductos = (List<string>)Session["NombresProductos"];
                    List<string> cantidades = (List<string>)Session["Cantidades"];
                    List<string> dnisSelectedList = (List<string>)Session["DNIs"];
                    List<decimal> preciosProductos = (List<decimal>)Session["PreciosProductos"];
                    List<double> preciosUnitarios = (List<double>)Session["PreciosUnitarios"];



                    // Verificar si el producto ya existe en la lista
                    int indiceExistente = nombresProductos.IndexOf(nombreProducto);

                    if (indiceExistente != -1)
                    {
                        // Si el producto ya existe, incrementar la cantidad en lugar de agregar un nuevo registro
                        int cantidadExistente = int.Parse(cantidades[indiceExistente]);
                        int nuevaCantidad = cantidadExistente + int.Parse(cantidad);

                        // Verificar si la nueva cantidad supera el stock disponible
                        if (nuevaCantidad <= stockSuficiente)
                        {
                            cantidades[indiceExistente] = nuevaCantidad.ToString();

                            // Recalcular el precio acumulado
                            decimal precio = preciosProductos[indiceExistente];
                            decimal precioAcum = precio * nuevaCantidad;
                            preciosProductos[indiceExistente] = precioAcum;
                        }
                        else
                        {
                            etiquetaAdvertencia.Visible = true;
                            etiquetaAdvertencia.Text = "La cantidad solicitada supera el stock disponible.";
                            TextBoxCantidad.Attributes["oninput"] = "ocultarAdvertencia();";
                            TextBoxCantidad.ClientIDMode = ClientIDMode.Static;
                            return;
                        }
                    }
                    else
                    {
                        // Si el producto no existe, agregar un nuevo registro a las listas
                        nombresProductos.Add(nombreProducto);
                        cantidades.Add(cantidad);
                        dnisSelectedList.Add(dniSeleccionado); // Agregado
                        decimal precio = ObtenerPrecioProducto(nombreProducto);
                        decimal precioAcum = precio * int.Parse(cantidad);
                        preciosProductos.Add(precioAcum);
                    }

                    // Actualizar las variables de sesión con las listas modificadas
                    Session["NombresProductos"] = nombresProductos;
                    Session["Cantidades"] = cantidades;
                    Session["DNIs"] = dnisSelectedList; // Agregado
                    Session["PreciosProductos"] = preciosProductos;

                    // Actualizar el GridView con los nombres de los productos
                    BindGridView();
                    ActualizarVisibilidadBotonVender();
                }
                else
                {
                    etiquetaAdvertencia.Visible = true;
                    etiquetaAdvertencia.Text = "La cantidad solicitada supera el stock disponible.";
                }
            }
        }
        private void BindGridView()
        {
            // Obtener la lista de nombres de productos, cantidades y DNIs desde la variable de sesión
            List<string> nombresProductos = (List<string>)Session["NombresProductos"];
            List<string> cantidades = (List<string>)Session["Cantidades"];
            List<string> dnisSelectedList = (List<string>)Session["DNIs"]; // Agregado

            // Crear un DataTable y agregar las columnas necesarias
            DataTable dt = new DataTable();
            dt.Columns.Add("DNI", typeof(string)); // Nueva columna para el DNI
            dt.Columns.Add("NombreProducto", typeof(string));
            dt.Columns.Add("Cantidades", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Precio_Acum", typeof(double));

            // Agregar los DNIs, nombres de productos y cantidades como filas y calcular el precio acumulado
            for (int i = 0; i < nombresProductos.Count; i++)
            {
                dt.Rows.Add(dnisSelectedList[i], nombresProductos[i], cantidades[i], ObtenerPrecioProducto(nombresProductos[i]), ObtenerPrecioAcumulado(ObtenerPrecioProducto(nombresProductos[i]), int.Parse(cantidades[i])));
            }

            // Enlazar el DataTable al GridView
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnVender_Click(object sender, EventArgs e)
        {
            // Obtener los datos necesarios del GridView y el DNI seleccionado
            List<string> nombresProductos = (List<string>)Session["NombresProductos"];
            List<string> cantidades = (List<string>)Session["Cantidades"];
            List<decimal> preciosProductos = (List<decimal>)Session["PreciosProductos"];
            string dniSeleccionado = dnis_drop.SelectedItem.Text;

            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Realizar la llamada a conexion.crearVenta para cada producto en los datos almacenados
            for (int i = 0; i < nombresProductos.Count; i++)
            {
                string nombreProducto = nombresProductos[i];
                string cantidad = cantidades[i];
                decimal precioUnitario = ObtenerPrecioProducto(nombreProducto); // Obtener el precio unitario del producto
                decimal precioAcumulado = precioUnitario * int.Parse(cantidad); // Cálculo del precio acumulado

                // Llamar a la función conexion.crearVenta con los datos obtenidos
                string val = conexion.crearVenta(dniSeleccionado, nombreProducto, int.Parse(cantidad), (double)precioUnitario, (double)precioAcumulado, fechaActual);

                // Realizar alguna acción adicional si es necesario
            }
            //IncrementarNumeroConjunto();

            // Limpiar los datos almacenados después de la venta
            Session["NombresProductos"] = null;
            Session["Cantidades"] = null;
            Session["PreciosProductos"] = null;

            // Realizar alguna acción adicional si es necesario
            LimpiarGridView();
            dnis.Visible = true;
            dnis_drop.Visible = true;
            btnSeguir.Visible = true;

            productos_agre.Visible = false;
            producto_categoria.Visible = false;
            Label2.Visible = false;
            TextBoxCantidad.Visible = false;
            btnAgregar.Visible = false;
            DNI.Visible = false;
            txb_DNIS.Visible = false;
            nombres.Visible = false;
            txb_nombres.Visible = false;
            correo.Visible = false;
            txb_correo.Visible = false;
            //sujeto a cambios//
            etiquetaAdvertencia.Visible = false;
        }

        private int ObtenerNumeroConjunto()
        {
            // Verificar si el número de conjunto está almacenado en la sesión
            if (Session["NumeroConjunto"] != null)
            {
                return (int)Session["NumeroConjunto"];
            }
            else
            {
                // Si no está almacenado en la sesión, establecer el valor inicial y guardarlo en la sesión
                int numeroConjunto = 1;
                Session["NumeroConjunto"] = numeroConjunto;
                return numeroConjunto;
            }
        }

        private void IncrementarNumeroConjunto()
        {
            // Obtener el número de conjunto actual
            int numeroConjunto = ObtenerNumeroConjunto();

            // Incrementar el número de conjunto en 1
            numeroConjunto++;

            // Actualizar el número de conjunto en la sesión
            Session["NumeroConjunto"] = numeroConjunto;

        }
        private void ActualizarVisibilidadBotonVender()
        {
            // Verificar si hay datos en el GridView
            bool tieneDatos = GridView1.Rows.Count > 0;

            // Establecer la visibilidad del botón "Vender"
            btnVender.Visible = tieneDatos;
        }
        private void LimpiarGridView()
        {

            // Limpiar las variables de sesión
            // Session["NombresProductos"] = null;
            //Session["Cantidades"] = null;
            //Session["PreciosProductos"] = null;
            //Session["DNIs"] = null;

            // Limpiar el GridView
            GridView1.DataSource = null;
            GridView1.DataBind();

            // Actualizar la visibilidad del botón "Vender"
            ActualizarVisibilidadBotonVender();
        }
        private int VerificarStock(string nombreProducto, int cantidad)
        {
            string datosLeidos = conexion.listarProductos();
            JObject json = JObject.Parse(datosLeidos);
            JArray productosArray = (JArray)json["Table"];

            foreach (JToken productoToken in productosArray)
            {
                string nombre = productoToken["NOMBRE_PRODUCTO"].ToString();
                int stock = int.Parse(productoToken["STOCK"].ToString());

                if (nombre == nombreProducto)
                {
                    // Devolver el stock actual del producto
                    return stock;
                }
            }

            // El producto no fue encontrado en la lista
            return 0;
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }
        private decimal ObtenerPrecioProducto(string nombreProducto)
        {
            try
            {
                string datosLeidos = conexion.listarProductos();
                JObject json = JObject.Parse(datosLeidos);
                JArray productosArray = (JArray)json["Table"];

                foreach (JToken productoToken in productosArray)
                {
                    string nombre = productoToken["NOMBRE_PRODUCTO"].ToString();
                    if (nombre.Substring(nombre.LastIndexOf(":") + 1).Trim() == nombreProducto)
                    {
                        decimal precio = Convert.ToDecimal(productoToken["PRECIO"]);
                        return precio;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de obtener el precio del producto aquí
                // Puedes mostrar un mensaje de error o realizar alguna otra acción
            }

            return 0; // Si no se encuentra el producto o hay un error, retorna 0 como precio por defecto
        }
     
        private decimal ObtenerPrecioAcumulado(decimal precio, int cantidad)
        {
            // Calcula el precio acumulado multiplicando el precio fijo por la cantidad
            return precio * cantidad;
        }

        protected void dni_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSeguir_Click(object sender, EventArgs e)
        {
            dnis.Visible = false;
            dnis_drop.Visible = false;
            btnSeguir.Visible = false;

            productos_agre.Visible = true;
            producto_categoria.Visible = true;
            Label2.Visible = true;
            TextBoxCantidad.Visible = true;
            btnAgregar.Visible = true;
            DNI.Visible = true;
            txb_DNIS.Visible = true;
            nombres.Visible = true;
            txb_nombres.Visible = true;
            correo.Visible = true;
            txb_correo.Visible = true;
            //btnVender.Visible = true;
            // Obtener el DNI seleccionado en el DropDownList
            string dniSeleccionado = dnis_drop.SelectedItem.Text;

            // Obtener la lista de usuarios
            string datosUsuarios = conexion.listarUsuarios();
            JObject jsonUsuarios = JObject.Parse(datosUsuarios);
            JArray usuariosArray = (JArray)jsonUsuarios["Table"];

            // Buscar el usuario que coincida con el DNI seleccionado
            JObject usuarioSeleccionado = usuariosArray.FirstOrDefault(u => u["DNI"].ToString() == dniSeleccionado) as JObject;

            if (usuarioSeleccionado != null)
            {
                // Extraer los valores del usuario encontrado
                string nombre = usuarioSeleccionado["NOMBRE"].ToString();
                string correo = usuarioSeleccionado["CORREO"].ToString();

                // Asignar los valores a los TextBox correspondientes
                txb_DNIS.Text = dniSeleccionado;
                txb_nombres.Text = nombre;
                txb_correo.Text = correo;
            }
            else
            {
                // Manejar el caso en el que no se encuentre el usuario correspondiente
                // Puedes mostrar un mensaje de error o realizar alguna otra acción
            }
        }
        protected void producto_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}