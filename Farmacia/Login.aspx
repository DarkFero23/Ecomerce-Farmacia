<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Farmacia.WebForm1" %>

<!DOCTYPE html>
<html lang="en">
<head>
    
  <meta charset="UTF-8">
  <title>Login Page in HTML with CSS Code Example</title>
  <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet">
  <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">
  <link rel="stylesheet" href="./style.css">
  <script>
    function hideErrorMessage() {
        var lblError = document.getElementById('<%= lblErrorMessage.ClientID %>');
        lblError.style.display = 'none';
    }
  </script>
  <style>
       /*Entonce lo que tenemos aca es todo el CSS*/
    body {
      background-image: linear-gradient(135deg, #FAB2FF 10%, #1904E5 100%);
      background-size: cover;
      background-repeat: no-repeat;
      background-attachment: fixed;
      font-family: "Open Sans", sans-serif;
      color: #333333;
    }

    .box-form {
      margin: 0 auto;
      width: 80%;
      background: #FFFFFF;
      border-radius: 10px;
      overflow: hidden;
      display: flex;
      flex: 1 1 100%;
      align-items: stretch;
      justify-content: space-between;
      box-shadow: 0 0 20px 6px #090b6f85;
    }

    @media (max-width: 980px) {
      .box-form {
        flex-flow: wrap;
        text-align: center;
        align-content: center;
        align-items: center;
      }
    }

    .box-form div {
      height: auto;
    }

    .box-form .left {
      color: #FFFFFF;
      background-size: cover;
      background-repeat: no-repeat;
      background-image: url("https://static3.depositphotos.com/1003495/163/i/600/depositphotos_1638092-stock-photo-pills.jpg");
      overflow: hidden;
    }

    .box-form .left .overlay {
      padding: 30px;
      width: 100%;
      height: 100%;
      background: #5961f9ad;
      overflow: hidden;
      box-sizing: border-box;
    }

    .box-form .left .overlay h1 {
      font-size: 10vmax;
      line-height: 1;
      font-weight: 900;
      margin-top: 40px;
      margin-bottom: 20px;
    }

    .box-form .left .overlay span p {
      margin-top: 30px;
      font-weight: 900;
    }

    .box-form .left .overlay span a {
      background: #3b5998;
      color: #FFFFFF;
      margin-top: 10px;
      padding: 14px 50px;
      border-radius: 100px;
      display: inline-block;
      box-shadow: 0 3px 6px 1px #042d4657;
    }

    .box-form .left .overlay span a:last-child {
      background: #1dcaff;
      margin-left: 30px;
    }

    .box-form .right {
      padding: 40px;
      overflow: hidden;
    }

    @media (max-width: 980px) {
      .box-form .right {
        width: 100%;
      }
    }

    .box-form .right h5 {
      font-size: 6vmax;
      line-height: 0;
    }

    .box-form .right p {
      font-size: 14px;
      color: #B0B3B9;
    }

    .box-form .right .inputs {
      overflow: hidden;
    }

    .box-form .right input {
      width: 100%;
      padding: 10px;
      margin-top: 25px;
      font-size: 16px;
      border: none;
      outline: none;
      border-bottom: 2px solid #B0B3B9;
    }

    .box-form .right .remember-me--forget-password {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    .box-form .right .remember-me--forget-password input {
      margin: 0;
      margin-right: 7px;
      width: auto;
    }

    .box-form .right button {
      float: right;
      color: #fff;
      font-size: 16px;
      padding: 12px 35px;
      border-radius: 50px;
      display: inline-block;
      border: 0;
      outline: 0;
      box-shadow: 0px 4px 20px 0px #49c628a6;
      background-image: linear-gradient(135deg, #70F570 10%, #49C628 100%);
       transition: all 0.2s linear;
    }

    label {
      display: block;
      position: relative;
      margin-left: 30px;
    }

    label::before {
      content: ' \f00c';
      position: absolute;
      font-family: FontAwesome;
      background: transparent;
      border: 3px solid #70F570;
      border-radius: 4px;
      color: transparent;
      left: -30px;
      transition: all 0.2s linear;
    }

    label:hover::before {
      font-family: FontAwesome;
      content: ' \f00c';
      color: #fff;
      cursor: pointer;
      background: #70F570;
    }

    label:hover::before .text-checkbox {
      background: #70F570;
    }

    label span.text-checkbox {
      display: inline-block;
      height: auto;
      position: relative;
      cursor: pointer;
      transition: all 0.2s linear;
    }

    label input[type="checkbox"] {
      display: none;
    }
    .box-form .right button:hover {
    transform: translateY(-2px);
  }

  .box-form .right button:active {
    transform: translateY(1px);
    box-shadow: none;
  }
  .highlight-label {
  font-weight: bold;
  font-size: 18px;
  color: #FF0000; /* Puedes ajustar el color según tus preferencias */
}
  .mensaje {
    color: red; /* Color de texto para mensajes de error */
    font-weight: bold; /* Estilo de fuente en negrita para mensajes */
}
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div class="box-form">
      <div class="left">
        <div class="overlay">
          <h1>Farmacia Sylas</h1>
          <p>Todas las medeicinas que necesites, incluyendo , produsctos de belleza , para todo tipo</p>
        </div>
      </div>
      <div class="right">
        <h5 id="h5Title"  runat="server">Login</h5>
          <asp:Label ID="lblNoAccount" runat="server" Text="¿No tienes una cuenta?, se parte de nuestra familia y:"></asp:Label>
      <asp:LinkButton ID="lnkCreateAccount" runat="server" Text="Crea una cuenta" OnClick="lnkCreateAccount_Click" CssClass="link-button"></asp:LinkButton>
 <div class="inputs">
     <asp:Label ID="lblCrear" runat="server" Text="Para crear una cuenta llene los siguientes campos" Visible="false" CssClass="highlight-label"></asp:Label>
     <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
  <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre" CssClass="input-field" style="display: none;"></asp:TextBox>
  <br />
  <asp:TextBox ID="txtCorreo" runat="server" placeholder="Correo" CssClass="input-field" style="display: none; width: 590px;"></asp:TextBox>
  <br />
  <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="input-field" style="display: none;"></asp:TextBox>
  <br />
     <!-- Entonces lo que tenemos aca es un regex que es Que hace que no podamos ingresar un DNI que no tenga menso de 8 digitos y a al vez mas de 8 -->
  <asp:TextBox ID="txtDNI" runat="server" placeholder="DNI" CssClass="input-field" style="display: none;" pattern="[0-9]{8}"></asp:TextBox>
     <asp:Button ID="btnCreateAccount" runat="server" Text="Crear cuenta" OnClick="btnCreateAccount_Click" CssClass="button" Style="display: none;" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" CssClass="button" Style="display: none;" />
</div>

        <div class="inputs">
          <asp:TextBox ID="txtUserName" runat="server" placeholder="Correo" onkeyup="hideErrorMessage()" AutoPostBack="false"></asp:TextBox>
            <div>
    <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="error-message" ForeColor="Red"></asp:Label>
</div>

          <br>
          <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Contraseña" ></asp:TextBox>
        </div>
        <br><br>
        <div class="remember-me--forget-password">
       
        </div>
        <br>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
   
    </div>
  </form>
</body>
</html>
