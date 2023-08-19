<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Venta.aspx.cs" Inherits="Farmacia.WebForm2" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
        
       <div class="container">
    <div class="text-center">
        <h1 id="Venta">Venta</h1>
        <p>Proceso de venta .</p>
    </div>
</div>
         <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="DNI" runat="server" CssClass="control-label" Text="DNI" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="txb_DNIS" runat="server" Width="384px"  CssClass="form-control" Visible =" false" ReadOnly="true" ></asp:TextBox>
            </div>
        </div>
    </div>
         <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="nombres" runat="server" CssClass="control-label" Text="Nombres" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="txb_nombres" runat="server" Width="384px"  CssClass="form-control" Visible =" false" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
    </div>
         <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="correo" runat="server" CssClass="control-label" Text="Correo" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="txb_correo" runat="server" Width="384px" CssClass="form-control" Visible =" false" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
    </div>
<div class="container d-flex flex-column justify-content-center align-items-center">
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="productos_agre" runat="server" CssClass="control-label" Text="Productos para agregar a la lista" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:DropDownList ID="producto_categoria" runat="server" Width="384px" OnSelectedIndexChanged="producto_categoria_SelectedIndexChanged" Visible="false" ></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="dnis" runat="server" CssClass="control-label" Text="Escoja un DNI para hacer la venta:" Visible="true"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:DropDownList ID="dnis_drop" runat="server" Width="384px" OnSelectedIndexChanged="dni_SelectedIndexChanged" ></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="etiquetaAdvertencia" runat="server" Text="" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </div>

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="Cantidad:" Visible="false" CssClass="control-label"></asp:Label>
            </div> 
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="TextBoxCantidad" placeholder="Ingrese la cantidad" runat="server" Visible="false" Width="384px" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary" Visible="false" />
                </div>
            </div>
    <br />

     <div class="row mt-3">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSeguir" runat="server" Text="Seguir" OnClick="btnSeguir_Click" CssClass="btn btn-primary" Visible="true" />
                </div>
            </div>

            <br />

   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
    <Columns>
         <asp:BoundField DataField="DNI" HeaderText="DNI" />
        <asp:BoundField DataField="NombreProducto" HeaderText="Nombre Producto" />
         <asp:BoundField DataField="Cantidades" HeaderText="Cantidades" />
        <asp:BoundField DataField="Precio" HeaderText="Pre_Unidad" />
         <asp:BoundField DataField="Precio_Acum" HeaderText="Precio_Acum" />
        
        
        

    </Columns>
</asp:GridView>

    <div class="row mt-3">
    <div class="col-md-12 text-center">
        <asp:Button ID="btnVender" runat="server" Text="Vender" OnClick="btnVender_Click" CssClass="btn btn-primary" Visible="false" />
    </div>
</div>

    </main>
</asp:Content>
