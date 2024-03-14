<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="BuscarPedidoporSKU.aspx.cs" Inherits="ProyectoLastLink.Pages.BuscarPedidoporSKU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       <div class="container box p-12   has-background-light"> 
      <div class="row ">
          <div class="col-md-12">
      <h1 class="title has-text-centered  has-text-success">Consultar Pedido por Sku</h1>
              <p class="title has-text-centered  has-text-success">&nbsp;</p>
           </div>
      </div>
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <br />
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
                                       Permite Realizar Busqueda de Registrso de Entrada y Salida segun Criterio
                                       <br />
                                   </div>
                        </article>
                   </asp:Panel>
    </div>
    <br />
      <div class="row align-content-center">
           <div class="row align-content-center">
    <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
     <div class="field">
         <div class="control has-icons-left has-icons-right">
              <asp:TextBox runat="server" ID="BuscarNombre" CssClass="input is-success" type="text" placeholder="Buscador"  ForeColor="Red"></asp:TextBox>
              <span class="icon is-small is-left"> 
                <i class="fas fa-user"></i> 
              </span> 
         </div>     
     </div>
   </div>

     <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
      <div class="field">
          <div class="control has-icons-left has-icons-right">
              <asp:Button runat="server" ID="Button1" CssClass="button is-warning" OnClick="Button1_Click"  Text="Buscar SKU"/>
               
              <br />
              <br />
           </div>     
         </div>

         <div>
             <asp:Label runat="server" ID="lblNoresult" Visible="false" Text="Sin Resultados" CssClass="message" />
         </div>
         </div>
        </div>
    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="Id_producto" AllowPaging="True" PageSize="20"  OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="#D2FFE9" BorderStyle="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
              <asp:BoundField DataField="id_producto" HeaderText="Id">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Marca" HeaderText="Marca">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Nombre" HeaderText="Nombre ">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Stock_inicial" HeaderText="Stock Inicial">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Valor_minimo" HeaderText="Valor Minimo">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="sku" HeaderText="SKU" >
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="id_seller" HeaderText="Id Seller" >
                <HeaderStyle BackColor="#99FFCC" />
           </asp:BoundField>
              <asp:BoundField DataField="Nombre_seller" HeaderText="Nombre Seller">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación">
              <FooterStyle BackColor="#99FFCC" />
              <HeaderStyle BackColor="#99FFCC" />
               </asp:BoundField>
              <asp:CommandField ShowSelectButton="True" HeaderText="Ver +" ButtonType="Button" SelectText="Detalles" >
              <ControlStyle CssClass="button is-danger is-light" Font-Size="Small" />
              <HeaderStyle BackColor="#99FFCC" />
              </asp:CommandField>
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




    
    
          <asp:LinkButton  ID="LinkButton1" runat="server" CssClass="modalPopup" Style="display:none">LinkButton</asp:LinkButton>
             <div class="container" >
                 <asp:Panel ID="Panel2" class="has-background-light"   runat="server" Height="700px" ScrollBars="Auto" Width="1000px">
                    <div class="row ">
                        <div class="col-md-12">
                        <h2 class="title has-text-centered  has-text-success">Listado Movimientos
                            <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                            </h2>
                            <p class="title has-text-centered  has-text-success">&nbsp;</p>
                        </div>
                    </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 pad-adjust">
                             <div class="field">
                               <div class="control">
                            <br/>

                    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
                      <asp:GridView ID="GridView11"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id" AllowPaging="True" PageSize="20"  OnPageIndexChanging="GridView11_PageIndexChanging"  BackColor="#D2FFE9" BorderStyle="None">
                          <AlternatingRowStyle BackColor="WhiteSmoke" />
                          <Columns>
                              <asp:BoundField DataField="id" HeaderText="Id">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Origen" HeaderText="Proveedor" Visible="False">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Proveedor" HeaderText="Seller">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Id_Plat" HeaderText="Pedido">
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Fecha_Venta" HeaderText="Fecha">
                             <HeaderStyle BackColor="#99FFCC" />
                               </asp:BoundField>
                              <asp:BoundField DataField="Nombre_Comprador" HeaderText="Nombre" >
                              <HeaderStyle BackColor="#99FFCC" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Direccion" HeaderText="Dirección" >
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

                                   <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
                                      <div class="field">
                                         <div class="control has-icons-left has-icons-right">
                                             <br />
                                              <br />
                                           <asp:Label runat="server" ID="Label1" Visible="false" Text="Sin Resultados" CssClass="message" />
                     
                                             <asp:Button runat="server" ID="Button2" CssClass="button is-danger" OnClick="Button1_Click"  Text="Cerrar Ventana"/>
                                                
                                                          </div>     
                                                        </div>

                                    <div>
                                      </div>
                                    </div>
                                   
                      </div>
                   </div>
   
                           </div>
                        </div>
            </asp:Panel>
          </div>
           <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modal-background" TargetControlID="LinkButton1" PopupControlID="Panel2" CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>

 
      


</div>
</div>



 </asp:Content>

