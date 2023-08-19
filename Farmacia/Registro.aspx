<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Registro.aspx.cs" Inherits="Farmacia.WebForm3" EnableEventValidation="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
        
       <div class="container">
    <div class="text-center">
        <h1 id="registro">Registro Ventas</h1>
    </div>
</div>
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="idVenta" HeaderText="idVenta" Visible="false"/>
        <asp:BoundField DataField="DNI" HeaderText="Nombre del DNI" />
        <asp:BoundField DataField="nombre_producto" HeaderText="nombre_producto" />
        <asp:BoundField DataField="Cantidades" HeaderText="Cantidades" />
        <asp:BoundField DataField="precio_uni" HeaderText="precio_uni" />
        <asp:BoundField DataField="precio_acumulado" HeaderText="precio_acumulado" />
        <asp:BoundField DataField="fecha" HeaderText="fecha" />

           
    </Columns>

</asp:GridView>
    </main>
</asp:Content>