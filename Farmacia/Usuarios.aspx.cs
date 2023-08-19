using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Farmacia
{
    public partial class About : Page
    {

        ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string datosLeidos = conexion.listarUsuarios();
                DataTable dt = JObject.Parse(datosLeidos)["Table"].ToObject<DataTable>();
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                String mensaje = ex.Message;

            }
        }

        protected void BT_actualizar_usuario_Click(object sender, EventArgs e)
        {

        }

        protected void BT_eliminar_usuario_Click(object sender, EventArgs e)
        {

        }

        protected void BT_crear_usuario_Click(object sender, EventArgs e)
        {

        }

        protected void BT_actualizar_Tabla_Click(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            
        }
        protected void BTN_ELIMINAR(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            GridViewRow row = (GridViewRow)btnEliminar.NamingContainer;
            string dni = row.Cells[4].Text; 
            try
            {
                string resultado = conexion.eliminarUsuario(dni);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                
            }
        }
        protected void BTN_ACTUALIZAR(object sender, EventArgs e)
        {
            Button btnActualizar = (Button)sender;
            GridViewRow row = (GridViewRow)btnActualizar.NamingContainer;
            string dni = row.Cells[4].Text; // Reemplaza el número 4 con el índice correcto si es necesario

            // Obtener los valores de la fila seleccionada
            string idUsuario = row.Cells[0].Text; // ID de Usuario
            string nombre = row.Cells[1].Text; // Nombre
            string contraseña = row.Cells[2].Text; // Contraseña
            string correo = row.Cells[3].Text; // Correo

            // Asignar los valores a los TextBox correspondientes

            txt_dni_actu.Text = dni;
            txt_usuario_actu.Text = nombre;
            txt_contra_actu.Text = contraseña;
            txt_email_actu.Text = correo;

            button_create.Visible = false;
            button_save.Visible = true;
            button_cancelar.Visible = true;
            button_reset.Visible = false;
            Label8.Visible = false;
            txt_dni_actu.Visible = false;
        }

        protected void BT_guardar(object sender, EventArgs e)
        {
            String dni = txt_dni_actu.Text;
            String nom = txt_usuario_actu.Text;
            String correo = txt_email_actu.Text;
            String contra = txt_contra_actu.Text;


            try
            {
                String resultado = conexion.actualizarUsuario(dni, nom, correo, contra);
                txt_dni_actu.Text = string.Empty;
                txt_usuario_actu.Text = string.Empty;
                txt_email_actu.Text = string.Empty;
                txt_contra_actu.Text = string.Empty;
                Response.Redirect(Request.Url.AbsoluteUri);
            

            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

            }

        }

        protected void BT_reset(object sender, EventArgs e)
        {
            txt_dni_actu.Text = string.Empty;
            txt_usuario_actu.Text = string.Empty;
            txt_email_actu.Text = string.Empty;
            txt_contra_actu.Text = string.Empty;
        }

        protected void BT_crear(object sender, EventArgs e)
        {
            String usuario = txt_usuario_actu.Text;
            String email = txt_email_actu.Text;
            String contra = txt_contra_actu.Text;
            String dni = txt_dni_actu.Text;
            try
            {
                String resultado = conexion.crearUsuarios(usuario, email, contra, dni);
        
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

            }
        }
        protected void BT_cancelar(object sender, EventArgs e)
        {
         
            button_save.Visible = false;
            button_reset.Visible = true;
            button_create.Visible = true;
            button_cancelar.Visible = false;
            Label8.Visible = true;
            txt_dni_actu.Visible = true;
            txt_dni_actu.Text = string.Empty;
            txt_usuario_actu.Text = string.Empty;
            txt_email_actu.Text = string.Empty;
            txt_contra_actu.Text = string.Empty;
        }
      
       


    }
}