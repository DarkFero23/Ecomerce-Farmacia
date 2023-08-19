<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Farmacia.About" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">

        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>

       
        <br >
        <asp:Label ID="Label8" runat="server" Text="Dni" Visible="true" ></asp:Label>
        <asp:TextBox ID="txt_dni_actu" placeholder="Ingrese DNI" runat="server" Visible="true" Width="230px" ></asp:TextBox>
        <br >
        <asp:Label ID="Label4" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txt_usuario_actu" placeholder="Ingrese nombre" runat="server" Width="248px"></asp:TextBox>
        <br >
        <asp:Label ID="Label5" runat="server" Text="Contraseña"></asp:Label>
        <asp:TextBox ID="txt_contra_actu" placeholder="Ingrese contraseña" runat="server"  Width="228px"></asp:TextBox>
       <%-- <asp:Button ID="btnMostrarContrasena" runat="server" Text="Mostrar" OnClick="btnMostrarContrasena_Click" />--%>
       <%-- <asp:Label ID="lbl_contra_actu" runat="server" Visible="false"></asp:Label>--%>
        <br >
        <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txt_email_actu" placeholder="Ingrese email" runat="server" Width="243px"></asp:TextBox>
        <br >
        <asp:Button ID="button_save" runat="server" Text="Guardar" OnClick="BT_guardar" Visible="false" />
        <asp:Button ID="button_reset" runat="server" Text="Limpiar" OnClick="BT_reset" />
        <asp:Button ID="button_create" runat="server" Text="Crear" OnClick="BT_crear" />
        <asp:Button ID="button_cancelar" runat="server" Text="Cancelar" OnClick="BT_cancelar" Visible="false" />

        <br >

        <br >
        <br >
        <br >
       
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
   
    <Columns>
        <asp:BoundField DataField="idUsuario" HeaderText="ID" />
        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="contraseña" HeaderText="Contraseña" />
        <asp:BoundField DataField="correo" HeaderText="Correo" />
        <asp:BoundField DataField="DNI" HeaderText="DNI" />
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CommandName="Actualizar" CommandArgument='<%# Eval("DNI") %>' OnClick="BTN_ACTUALIZAR" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("DNI") %>' OnClick="BTN_ELIMINAR" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
    </main>
</asp:Content>
