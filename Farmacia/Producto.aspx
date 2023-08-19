<%@ Page Title="Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="Farmacia.Contact" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        
       <div class="container">
    <div class="text-center">
        <h1 id="CRUD para Productos">CRUD para Productos</h1>
        <p>Se puede: Crear , Leer , Actualizar y Eliminar los datos de los productos.</p>
    </div>
</div>

<div class="container d-flex flex-column justify-content-center align-items-center">
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="Nombre Producto:" Visible="true"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="producto_far" placeholder="Ingrese nombre del producto" runat="server" CssClass="form-control" Visible="true" Width="384px"></asp:TextBox>
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
                <asp:Label ID="Label2" runat="server" Text="Nombre Nuevo Producto:" Visible="false" CssClass="control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="producto_nuevo" placeholder="Ingrese nombre del producto" runat="server" Visible="false" Width="384px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label4" runat="server" Text="Descripción:" CssClass="control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="producto_des" placeholder="Ingrese descripción del producto" runat="server" Width="384px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label5" runat="server" Text="Precio:" CssClass="control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="producto_precio" placeholder="Ingrese el precio" runat="server" Width="384px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label6" runat="server" Text="Categoría:" CssClass="control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
    <div class="col-md-12 text-center form-inline">
        <asp:DropDownList ID="producto_categoria" runat="server" Width="384px" OnSelectedIndexChanged="producto_categoria_SelectedIndexChanged" ></asp:DropDownList>
    </div>
        </div>
    </div>

    <br />

    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label1" runat="server" Text="Stock:" CssClass="control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:TextBox ID="producto_stock" placeholder="Ingrese stock del producto" runat="server" Width="384px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />

</div>


    <div style="text-align: center;">
        <asp:Button ID="button_save" runat="server" Text="Guardar" OnClick="BT_guardar" Visible="false" CssClass="btn btn-primary" style="font-size: 16px; padding: 10px 20px;" />
        <asp:Button ID="button_reset" runat="server" Text="Limpiar" OnClick="BT_reset" CssClass="btn btn-secondary" style="font-size: 16px; padding: 10px 20px;" />
        <asp:Button ID="button_create" runat="server" Text="Crear" OnClick="BT_crear" CssClass="btn btn-success" style="font-size: 16px; padding: 10px 20px;" /> 
        <asp:Button ID="button_cancelar" runat="server" Text="Cancelar" OnClick="BT_cancelar" Visible="false" CssClass="btn btn-danger" style="font-size: 16px; padding: 10px 20px;"/>
    </div>
 <br />
  <br />
         

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="idProducto" HeaderText="ID" />
        <asp:BoundField DataField="nombre_producto" HeaderText="Nombre del Producto" />
        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
        <asp:BoundField DataField="precio" HeaderText="Precio" />
        <asp:BoundField DataField="nombre_categoria" HeaderText="Categoría" />
        <asp:BoundField DataField="stock" HeaderText="Stock" />
        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <div class="btn-group" role="group">
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary" Text="Actualizar" CommandName="Actualizar" CommandArgument='<%# Eval("nombre_producto") %>' OnClick="BTN_ACTUALIZAR" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("nombre_producto") %>' OnClick="BTN_ELIMINAR" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este elemento?');" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>

    </main>
</asp:Content>
