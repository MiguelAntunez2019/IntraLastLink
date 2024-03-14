<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ConsultaBach.aspx.cs" Inherits="ProyectoLastLink.Pages.ConsultaBach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       
        
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
                                       Permite Consultar por Fecha los Batch          <br />
                                   </div>
                        </article>
                       </asp:Panel>
    </div>
    <br/>
          <asp:Panel ID="Panel_Fecha" runat="server" Visible="true">   
          
               <div class="row align-content-center">

                    <div class="col-md-3 col-sm-3 col-xs-3   pad-adjust">
                      <div class="field">
                          <div class="control has-icons-left has-icons-right">
                                      <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged">
                                          <TitleStyle BackColor="#99FFCC" />
                                      </asp:Calendar>
                          </div>     
                      </div>
                    </div>
                  <div class="col-md-3 col-sm-3 col-xs-3   pad-adjust">
                   <div class="field">
                       <div class="control has-icons-left has-icons-right">
                           <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged">
                               <TitleStyle BackColor="#99FFCC" />
                           </asp:Calendar>
                            <br />
                       </div>     
                   </div>
                 <div class="col-md-24 col-sm-24 col-xs-24  pad-adjust">
                  <div class="field">
                      <div class="control has-icons-left has-icons-right">
                     <asp:Button runat="server" ID="Button1" CssClass="button is-warning" OnClick="Button1_Click"  Text="Buscar Por Fecha"/>
                      </div>     
                  </div>
                </div>
           </div>
         </div>
      </asp:Panel> 









      <div class="row align-content-center">
           <div class="row align-content-center">
    <div class="col-md-6 col-sm-6 col-xs-6   pad-adjust">
     <div class="field">
         <div class="control has-icons-left has-icons-right">
              <asp:TextBox runat="server" ID="BuscarNombre" CssClass="input is-success" type="text" placeholder="Buscador" Visible="false"  ForeColor="Red"></asp:TextBox>
                <asp:TextBox runat="server" ID="BuscarNombre2" CssClass="input is-success" type="text" placeholder="Buscador"  Visible="false" ForeColor="Red"></asp:TextBox>
            
           
         </div>     
     </div>
   </div>

     <div>
             <asp:Label runat="server" ID="lblNoresult" Visible="false" Text="Sin Resultados" CssClass="message" />
         </div>
    </div>

          </br>

      





    <div class="col-md-24 col-sm-24 col-xs-24 pad-adjust">
      <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  AllowPaging="True" PageSize="20"  OnPageIndexChanging="GridView1_PageIndexChanging"  BackColor="#D2FFE9" BorderStyle="None">
          <AlternatingRowStyle BackColor="WhiteSmoke" />
          <Columns>
              <asp:BoundField DataField="codigo_seller" HeaderText="Codigo Seller">
              <HeaderStyle BackColor="#99FFCC" />
              </asp:BoundField>
              <asp:BoundField DataField="Id_plat" HeaderText="Id Pedido">
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
