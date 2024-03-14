<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="sacar_productos.aspx.cs" Inherits="ProyectoLastLink.Pages.sacar_productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
  
        <br />
  <div clss="contanier box p-12 has-background-Light">
              <div class="steps" id="stepsDemo">
                <div class="step-item">
                  <div class="step-marker">1</div>
                  <div class="step-details">
                    <p class="step-title">Productos Nuevos</p>
                  </div>
                </div>
                <div class="step-item ">
                  <div class="step-marker">2</div>
                  <div class="step-details">
                    <p class="step-title">Ingreso a Bodega Por Unidad</p>
                  </div>
                </div>
                <div class="step-item">
                  <div class="step-marker">3</div>
                  <div class="step-details">
                    <p class="step-title">Ingreso a Bodega Por Lotes</p>
                  </div>
                </div>
                <div class="step-item is-active is-success">
                  <div class="step-marker">4</div>
                  <div class="step-details">
                    <p class="step-title  has-text-danger">Salida de Bodega</p>
                  </div>
                </div>


              </div>


  </div>
  
  <br />
<br />
    <div class="container box p-12   has-background-light"> 
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Salida de Productos de Bodega</h1>
              <p class="title has-text-centered  has-text-success">&nbsp;</p>
           </div>
      </div>

          <asp:Panel ID="Panel2" runat="server">
               
                     <div class="col-md-12 col-sm-12 col-xs-12">
                       <div class="field">
     
                         <div class="control has-icons-left has-icons-right">
        
                          <div class="select"> 
    
                             <asp:DropDownList ID="comuna_seller" runat="server" CssClass="input is-success" AutoPostBack="true">

                             </asp:DropDownList>
     
                         </div> 
                        </div>
                        </div>
                     </div>
              <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                 <div class="field">
                <div class="control">
                         <br />
                    <asp:Button runat="server" ID="Seleccionar_Seller" CssClass="button is-link" OnClick="Seleccionar_Seller_Click" Visible="true"  Text="Seleccionar Seller"/>
                    <br />
       
                </div>
              </div>
                
              </div>
             </div>
        </asp:Panel>
       <br /> <br /> 
               <div class="row ">
                   <asp:Panel ID="Panel1" runat="server" DefaultButton="btnEnviar" Visible="false">
                  
                        <article class="message is-link">
                                   <div class="message-header">
                                     <p>Salida de Productos</p>
                                   </div>
                                   <div class="message-body">
                                       <div class="icon-text">
                                       <span class="icon has-text-warning">
                                         <i class="fas fa-exclamation-triangle"></i>
                                       </span>
                                         <span class="has-text-danger" >Warning</span>
                                     </div>
                                       <br />
                                       <span class="icon">
                                         <i class="fas fa-arrow-right">
                                         </i>
                                      </span>
                                       Ahora debe Pistolear los codigos de Barras de cada producto
                                       <br />
                                        <span class="icon">
                                            <i class="fas fa-arrow-right">
                                            </i>
                                         </span>
                                       Para Ingresar Articulos en Otro Seller debe Ingresar Nuevamente y cambiar Seller o presionar <strong> <a href="Movimientos_Bodega.aspx">AQUI</a></strong>
                                             <br />
                                   </div>
                        </article>

                  
                       
                       <asp:TextBox ID="producto_lectura" runat="server"  CssClass="input is-success" type="text"> </asp:TextBox>
                    <asp:Button runat="server" ID="btnEnviar" CssClass="button is-static"  Text="Pistolee el Articulo" ValidationGroup='Registrar' OnClick="verificar_Click"  CausesValidation="true"/>
                       <br />
                </asp:Panel>
    </div>
    <br />
      <div class="row align-content-center">
    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="Id_movimiento"  BackColor="#D2FFE9" BorderStyle="None">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
              <asp:BoundField DataField="Id_movimiento" HeaderText="Id">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="nombre_seller" HeaderText="Nombre Seller">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Nombre" HeaderText="Nombre ">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="marca" HeaderText="Marca">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
             <HeaderStyle BackColor="#99FFCC" />
               </asp:BoundField>
              <asp:BoundField DataField="sku" HeaderText="SKU" >
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="fecha" HeaderText="Fecha Entrada" >
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