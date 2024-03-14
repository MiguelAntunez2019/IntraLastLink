<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Lista_productos_Bach.aspx.cs" EnableEventValidation="false" Inherits="ProyectoLastLink.Pages.Lista_productos_Bach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
          
    <div clss="contanier box p-12 has-background-Light">
            <div class="steps" id="stepsDemo">
              <div class="step-item ">
                <div class="step-marker">1</div>
                <div class="step-details">
                  <p class="step-title">Proceso Batch</p>
                </div>
              </div>
              <div class="step-item  is-active is-success ">
                <div class="step-marker">2</div>
                <div class="step-details">
                  <p class="step-title has-text-danger ">Retiro Productos Armado</p>
                </div>
              </div>
         
              <div class="step-item">
                <div class="step-marker">3</div>
                <div class="step-details">
                  <p class="step-title">Generación Armado</p>
                </div>
              </div>


            </div>


</div>
      <br />
<br />
    
    <div class="container box p-12   has-background-light"> 
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Información Para  Detalle de Pedido</h1>
              <p class="title has-text-centered  has-text-success"></p>
           </div>
      </div>
       
               <div class="row ">
                   <asp:Panel ID="Panel1" runat="server">
                  
                        <article class="message is-success">
                                   <div class="message-body">
                                       <div class="icon-text">
                                       <span class="icon has-text-warning">
                                         <i class="fas fa-exclamation-triangle"></i>
                                       </span>
                                         <span class="has-text-danger" >Warning</span>
                                     </div>
                                       <br />
                                       Permite Conocer el Total de los Productos para crear Pedido, Para Crear detalle en Documento Presione el Botón de abajo          <br />
                                   </div>
                        </article>
                            <div class="row ">
                              <div class="col-md-12">
                             <asp:Button runat="server" ID="Button2" class="button is-medium is-fullwidth" OnClick="Button2_Click" Text="Crear Documento Para Retiro Bodega"/>    </div>
                            </div> 
                   </asp:Panel>
    </div>
    <br/>
      <div class="row align-content-center">
         <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  AllowPaging="True" PageSize="100"  OnPageIndexChanging="GridView1_PageIndexChanging"  BackColor="#D2FFE9" BorderStyle="None">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
              <asp:BoundField DataField="codigo_seller" HeaderText="Codigo Seller">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Nombre_prod_ped" HeaderText="Nombre Producto">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="suma_cantidad" HeaderText="Total Cantidad">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Sku_ped" HeaderText="Sku ">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="ubicacion" HeaderText="Ubicación">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Fecha" HeaderText="Fecha Batch">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
          </Columns>
          <FooterStyle BackColor="White" ForeColor="#000066" />
          <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
          <RowStyle ForeColor="#000066"/>
          <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
          <SortedAscendingCellStyle BackColor="#F1F1F1" />
          <SortedAscendingHeaderStyle BackColor="#007DBB" />
          <SortedDescendingCellStyle BackColor="#CAC9C9" />
          <SortedDescendingHeaderStyle BackColor="#00547E" />
      </asp:GridView>
    </div>
</div>
</div>



 </asp:Content>













  
    

  


