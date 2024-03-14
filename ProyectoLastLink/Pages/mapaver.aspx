<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="mapaver.aspx.cs" Inherits="ProyectoLastLink.Pages.mapaver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
     <style>
		 
		#map {
		  height: 100%;
		}

		/* 
		 * Optional: Makes the sample page fill the window. 
		 */
		html,
		body {
		  height: 100%;
		  margin: 0;
		  padding: 0;
		}

		gmp-map {
		  height: 400px;
		}
  
  </style>

   <script>

       function initMap() {
           console.log("Maps JavaScript API loaded.");
       }

       window.initMap = initMap;
   </script>
     <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
  
 <div class="container is-fullhd">
  <div class="notification">
        <div class="columns">
          <div class="column">
              
                 <gmp-map center="-33.4726900,-70.6472400" zoom="11" map-id="DEMO_MAP_ID">
                       <asp:Repeater ID="Repeater1" runat="server">
                         <ItemTemplate>
                          
                             <gmp-advanced-marker position="<%# DataBinder.Eval(Container.DataItem,"Posicion") %>"   title="<%# DataBinder.Eval(Container.DataItem,"info") %>"></gmp-advanced-marker>


                           </ItemTemplate>
                        </asp:Repeater>
                 
                 </gmp-map>

              <script
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyATOguNja2nQ_HDkOngnZxMGMGsxvWdjTc&callback=initMap&libraries=marker&v=beta"
      defer
    ></script>
            
          </div>
          <div class="column">
       <asp:GridView ID="GridView1"  runat="server" Width="100%" AutoGenerateColumns="False"  DataKeyNames="id_Lista_planificacion" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" BackColor="#D2FFE9" BorderStyle="None">
    <AlternatingRowStyle BackColor="WhiteSmoke" />
    <Columns>
        <asp:BoundField DataField="id_Lista_planificacion" HeaderText="Id" ReadOnly="True" >
        <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
        <asp:BoundField DataField="Id_Plat" HeaderText="Pedido">
        <FooterStyle BackColor="#99FFCC" />
        <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="True" />
        </asp:BoundField>
        <asp:BoundField DataField="Direccion" HeaderText="Dirección">
        <FooterStyle BackColor="#99FFCC" Font-Bold="True" Font-Italic="True" />
        <HeaderStyle BackColor="#EBEBEB" />
        </asp:BoundField>
       
        <asp:BoundField DataField="Comuna" HeaderText="Comuna" >
        <FooterStyle BackColor="#99FFCC" />
        <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="True" />
        </asp:BoundField>
        
        
        <asp:CommandField ButtonType="Button" ShowEditButton="True" HeaderText="Modificar" >
        <ControlStyle  CssClass="button is-success is-light" Font-Size="small"/>
        <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" Font-Italic="False" />
        </asp:CommandField>
    </Columns>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#007DBB" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>




  </div>
 
        </div>
  </div>

      <br />
     <div class="row ">
       <div class="col-md-12">
      <asp:Button runat="server" ID="Button2" class="button is-large is-fullwidth" OnClick="Button2_Click" Text="Asignar Rutas a Choferes"/>    </div>
  
     </div> 


</div>
    
    
   
    <br />
    







   
</asp:Content>
