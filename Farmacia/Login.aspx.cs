using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ServiceReference1.WebService1SoapClient conexion = new ServiceReference1.WebService1SoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Cuando apretemos el boton de login , sucedera lo siguiente: Invocaremos a la funcion VERRIFICARCREDENCIALES
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            // Realizar la verificación de inicio de sesión utilizando el servicio web
            bool loginSuccessful = VerificarCredenciales(username, password);

            if (loginSuccessful)
            {
                // Inicio de sesión exitoso
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                Response.Redirect("Default.aspx");
              
            }
            else
            {
                //Muestra el mensaje de error y limpia los txtbox
                lblErrorMessage.Visible =true;
                lblErrorMessage.Text = "Los datos ingresados son incorrectos";
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
               
            }
        }
        protected void lnkCreateAccount_Click(object sender, EventArgs e)
        {
            //Este boton sirve para crear una cuenta , visibilidad y display
            h5Title.InnerText = "Register";
            lblNoAccount.Visible = false;
            lnkCreateAccount.Visible = false;
            txtUserName.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            lblCrear.Visible = true;
            //Visibilidad
            txtNombre.Style["display"] = "block";
            txtContrasena.Style["display"] = "block";
            txtCorreo.Style["display"] = "block";
            txtDNI.Style["display"] = "block";
            btnCreateAccount.Style["display"] = "block";
            btnCancel.Style["display"] = "block";
        }
        private bool VerificarCredenciales(string username, string password)
        {
            // Llamar al método del servicio web para verificar las credenciales del usuario
            string result = conexion.listarUsuarios();

            // Analizar el resultado JSON del servicio web
            JObject json = JObject.Parse(result);
            JArray usuariosArray = (JArray)json["Table"];

            // Verificar si existe un usuario con las credenciales proporcionadas
            foreach (JToken usuarioToken in usuariosArray)
            {
                string usernameDB = usuarioToken["CORREO"].ToString(); 
                string passwordDB = usuarioToken["CONTRASEÑA"].ToString();

                if (username == usernameDB && password == passwordDB)
                {
                    return true; // Credenciales válidas
                }
            }

            return false; // Credenciales inválidas
        }
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // Verificar si todos los campos están llenos
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtContrasena.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtDNI.Text))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Todos los campos deben ser llenados.";
                return;
            }

            string nombre = txtNombre.Text;
            string contra = txtContrasena.Text;
            string corr = txtCorreo.Text;
            string dni = txtDNI.Text;
            bool dniExists = VerificarCredenciales2(dni);

            try
            {
                if (!dniExists)
                {
                    //Llama al WS y lo hace funcionar 
                    string resultado = conexion.crearUsuarios(nombre, corr, contra, dni);
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    //Si el dni ya esta creado , me da esta exepcion
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "El DNI ya está registrado.";
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                // Manejar la excepción si es necesario
            }
        }

        private bool VerificarCredenciales2(string dni)
        {
            // Llamar al método del servicio web para obtener la lista de usuarios
            string result = conexion.listarUsuarios();

            // Analizar el resultado JSON del servicio web
            JObject json = JObject.Parse(result);
            JArray usuariosArray = (JArray)json["Table"];

            // Verificar si existe un usuario con el DNI proporcionado
            foreach (JToken usuarioToken in usuariosArray)
            {
                string dniDB = usuarioToken["DNI"].ToString();

                if (dni == dniDB)
                {
                    return true; // DNI válido
                }
            }

            return false; // DNI no encontrado
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //LO unico maneja este boton , es visivivlida de entorno y limpia los txt box
            h5Title.InnerText = "Login";
            lblNoAccount.Visible = true;
            lnkCreateAccount.Visible = true;
            txtUserName.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;
            lblCrear.Visible = false;
            txtNombre.Style["display"] = "none";
            txtContrasena.Style["display"] = "none";
            txtCorreo.Style["display"] = "none";
            txtDNI.Style["display"] = "none";
            btnCreateAccount.Style["display"] = "none";
            btnCancel.Style["display"] = "none";
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtDNI.Text = string.Empty;
            lblErrorMessage.Visible = false;
            Page_Load(sender, e);
        }
    }
}