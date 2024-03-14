<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Armado_pedido_Mayorista.aspx.cs" Inherits="ProyectoLastLink.Pages.Armado_pedido_Mayorista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    

    
   

   
  <div clss="contanier box p-12 has-background-Light">
              <div class="steps" id="stepsDemo">
                <div class="step-item">
                  <div class="step-marker">1</div>
                  <div class="step-details">
                    <p class="step-title">Proceso Batch</p>
                  </div>
                </div>
                <div class="step-item ">
                  <div class="step-marker">2</div>
                  <div class="step-details">
                    <p class="step-title">Retiro Productos Armado</p>
                  </div>
                </div>
           
                <div class="step-item is-active is-success">
                  <div class="step-marker">3</div>
                  <div class="step-details">
                    <p class="step-title  has-text-danger">Generación Armado Mayorista</p>
                  </div>
                </div>


              </div>


  </div>

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
    
                             <asp:DropDownList ID="Pedido" runat="server" CssClass="input is-success" AutoPostBack="true">

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
                    <asp:Button runat="server" ID="Seleccionar_Seller" CssClass="button is-link" OnClick="Seleccionar_Seller_Click" Visible="true"  Text="Seleccionar Pedido"/>
                    <br />
       
                </div>
              </div>
                
              </div>
             </div>
        </asp:Panel>
        <br /> 
               <div class="row ">
                  
                   
                   
                   
                   
                   
                   
                   <asp:Panel ID="Panel1" runat="server" DefaultButton="btnEnviar" Visible="false">
                  
                        <article class="message is-link">
                                   <div class="message-header">
                                     <p>Armado de Pedidos Mayoristas</p>
                                   </div>
                                   <div class="message-body">
                                       <div class="icon-text">
                                       <span class="icon has-text-warning">
                                         <i class="fas fa-exclamation-triangle"></i>
                                       </span>
                                         <span class="has-text-danger" >  Ahora debe Pistolear los codigos de Barras de cada producto y Embalar<br />
                                    </span>
                                     </div>
                                       
                                      
                                   </div>
                        </article>

                  
                       
                       <asp:TextBox ID="producto_lectura" runat="server"  CssClass="input is-success" type="text"> </asp:TextBox>
                    <asp:Button runat="server" ID="btnEnviar" CssClass="button is-static"  Text="Pistolee el Articulo" ValidationGroup='Registrar' OnClick="verificar_Click"  CausesValidation="true"/>
                       <br />
                   </asp:Panel>


           <asp:Panel ID="Panel4" runat="server" Visible="false">
            <div class="row ">
                <div class="col-md-4 col-sm-4 col-xs-4">
                 <div class="field">
          
                     <div class="control has-icons-left has-icons-right">

                         <asp:TextBox runat="server" ID="cantidad_pistoleo" CssClass="input is-success"  type="text" placeholder="Ingrese Cantidad">
          
                         </asp:TextBox>
                         <span class="icon is-small is-left"> 
                              <i class="fas fa-user"></i> 
                         </span> 
                     </div>
                    </div>
                 </div>
                   <div class="col-md-4 col-sm-4 col-xs-4">
                   <div class="field">
                       <div class="control has-icons-left has-icons-right">
                         <asp:TextBox runat="server" ID="Nombre_Articulo" CssClass="input is-success" type="text" placeholder="Nombre Articulo" ReadOnly="true">
  
                         </asp:TextBox>
                         <span class="icon is-small is-left"> 
                              <i class="fas fa-user"></i> 
                         </span> 
                     </div>
                   </div>
                   </div>
   
           <div class="col-md-4 col-sm-4 col-xs-4">
               <div class="field">
                   <div class="control has-icons-left has-icons-right">
                       <asp:TextBox runat="server" ID="Marca" CssClass="input is-success" type="text" placeholder="Marca" ReadOnly="true">
                        </asp:TextBox>
                       <span class="icon is-small is-left"> 
                            <i class="fas fa-envelope"></i> 
                       </span> 
                   </div>
                   </div>
               </div>
           </div>
                  <div class="row">
                   <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                     <div class="field">
                       <div class="control">
                                        <br />
                      <asp:Button runat="server" ID="Ingreso_Bodega" CssClass="button is-link" OnClick="Ingreso_Bodega_Click"  Text="Aceptar Cantidad"/>
                                   <br />
   
                       </div>
                     </div>
       
                   </div>
                  </div>
                </asp:Panel>










                 </div>
         <br />
                          <asp:Panel ID="Panel3" runat="server" Visible="false">
                            <div class="row ">
                                <div class="col-md-4 col-sm-4 col-xs-4">
                                 <div class="field">
          
                                     <div class="control has-icons-left has-icons-right">

                                         <asp:TextBox runat="server" ID="cantidad" CssClass="input is-success"  type="text" placeholder="Ingrese Cantidad de Empaques Realizados">
          
                                         </asp:TextBox>
                                         <span class="icon is-small is-left"> 
                                              <i class="fas fa-user"></i> 
                                         </span> 
                                     </div>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Cantidad de Package Obligatorio" ControlToValidate="cantidad" BorderColor="Red" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
   
                                    </div>
                                 </div>
                           </div>
                          <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                       <div class="field">
                        <div class="control">
                      <br />
                      <asp:Button runat="server" ID="Ingreso_Impresion" CssClass="button is-link" OnClick="Ingreso_Impresion_Click" Text="Registrar Cantidad de Empaques"/>
                                   <br />
   
                       </div>
                     </div>
       
                   </div>
                  </div>
                </asp:Panel>




    <br />
      <div class="row align-content-center">
    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id"  BackColor="#D2FFE9" BorderStyle="None">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
              <asp:BoundField DataField="id" HeaderText="Id">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="id_seller" HeaderText="Id Seller">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Proveedor" HeaderText="Nombre Seller">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Id_Plat" HeaderText="Pedido">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="sku_ped" HeaderText="SKU" >
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Fecha_venta" HeaderText="Fecha Venta" >
                <HeaderStyle BackColor="#99FFCC" />
           </asp:BoundField>
<asp:BoundField HeaderText="Producto" DataField="Nombre_Prod_ped">
<HeaderStyle BackColor="#99FFCC"></HeaderStyle>
</asp:BoundField>
              <asp:BoundField DataField="cantidad_ped" HeaderText="Cantidad">
             <HeaderStyle BackColor="#99FFCC" />
               <HeaderStyle BackColor="#99FFCC" />
               </asp:BoundField>
              <asp:BoundField DataField="Cantidad_pistoleo" HeaderText="Cantidad Reserva">
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


               <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                   <div class="field">
                      <div class="control">
                         <br />
                         <asp:Button runat="server" ID="Armado" CssClass="button is-link" Visible="false"  OnClick="Registrar_Click1"  Text="Cerrar Armado y Etiquetar"/>
                         <asp:Button runat="server" ID="Otro" CssClass="button is-link" Visible="false"  OnClick="Otro_Click"  Text="Seguir Con Otro Pedido"/>
                        
                          <br />
           
                     </div>
                   </div>
       
                </div>
               </div>





</div>
</div>
 </asp:Content>

