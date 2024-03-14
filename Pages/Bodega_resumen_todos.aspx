<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Bodega_resumen_todos.aspx.cs" Inherits="ProyectoLastLink.Pages.Bodega_resumen_todos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
           <style>
    body2 {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        margin: 0;
    }

    .contenedor2 {
        background-color: #FFFFFF;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        padding: 30px;
        width: 100%;
        max-width: 1200px;
    }

    .titulo2{
        font-size: 15px;
        font-weight: bold;
        margin-bottom: 10px;
    }

    .tarjeta2 {
        background-color: #f4f4f4;
        border-radius: 5px;
        padding: 20px;
        margin-bottom: 20px;
        width:100%;
        box-sizing: border-box;
    }

    .tarjeta .titulo2 {
        font-size: 15px;
        font-weight: bold;
        margin-bottom: 5px;
    }

    .tarjeta .valor2 {
        font-size: 10px;
        font-weight: bold;
        color: #007bff;
    }
   

</style>	

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
  google.charts.load('current', {'packages':['corechart']});
  google.charts.setOnLoadCallback(drawChart);

  function drawChart() {

    var data = google.visualization.arrayToDataTable(<%=obtenerDatos()%>);
         var options = {
          title: 'Estadisticas Productos Por Bodega',
          is3D: true,
          pieHole: 0.4,
          legend: { position: "labeled", textStyle: { color: "red", fontSize: 14 } },
         
      };
      var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
  }
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       
           <div class="container is-widescreen">

               <div class="row ">
                 <div class="col-md-24">
                     
                      <h1 class="title has-text-centered  has-text-success"><img src="../imagenes/LOGO1.png" height="500" width="250"/></h1>
                 
                    </div>
             </div>
<br>


  
        <div class="notification is-primary is-light">
           <div class="columns is-desktop">
      

               
                           <div class="column">
                        <div class="contenedor2">
                            <div class="titulo2">Sin SKU Bodega</div>
                              <div class="column">
                              <div class="titulo2"><img src="../imagenes/sinSku.png" height="100" width="80"/></div>
                              <br>
                                  <div class="tarjeta2">
                          <div class="titulo2">Totales</div>
                           <div class="valor2"><p style="color:red;font-size:xx-large;align-content:center">
                               <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="LinkButton1_Click"><%=Session["contador_sinSku"].ToString() %></asp:LinkButton>
                              </p></div>
                          </div>
                               </div>   
                                   </div>
         </div>

</p>
      
       <div class="column">
                <div class="contenedor2">
                    <div class="titulo2">Sin Stock Bodega</div>
                       <div class="column">
                       <div class="titulo2"><img src="../imagenes/sinstockfi.png" height="100" width="80"/></div>
                       <br>
                      <div class="tarjeta2">
                        <div class="titulo2">Totales</div>
                         <div class="valor2"><p style="color:red;font-size:xx-large;align-content:center">
                             <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><%=Session["contador_SinStock"].ToString() %></asp:LinkButton>
                            </p></div>
                        </div>
                          

                       </div>   
                 </div>
       </div>

               
               <div class="column">
                  <div class="contenedor2">
                      <div class="titulo2">Bajo Minimo Bodega</div>
                        <div class="column">
                        <div class="titulo2"><img src="../imagenes/7132915.png" height="100" width="80"/></div>
                        <br>
                            
                       <div class="tarjeta2">
                         <div class="titulo2">Totales</div>
                          <div class="valor2"><p style="color:orange;font-size:xx-large;align-content:center">
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"><%=Session["contador_BajoMinimo"].ToString() %></asp:LinkButton>

                               </p></div>
                        
                           </div>
                             
                        </div>   
                 </div>
               </div>


               
               <div class="column">
                <div class="contenedor2">
                     <div class="titulo2">Productos Bodega</div>
                      <div class="column">
                      <div class="titulo2"><img src="../imagenes/todos.png" height="100" width="80"/></div>
                      <br>
                          
                     <div class="tarjeta2">
                       <div class="titulo2">Totales</div>
                        <div class="valor2"><p style="color:green;font-size:xx-large;align-content:center">
                              <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"><%=Session["Total_Productos_Stock"].ToString() %></asp:LinkButton>


                                            </p></div>
                       </div>
                        
                      </div>   
               </div>
            </div>          
               



                   

</div>
        
</div>
            
  
  
     <div class="notification is-primary is-light">
   <div class="columns is-desktop notification is-primary is-light">
      
      
       <div class="column">
              <div class="notification is-primary is-light">
                
                <div id="piechart" style="width: 1100px; height: 300px;"></div>

              </div>
            </div>

         
      
        

</div>
</div>
 
 
 </div>
            
    

  
    
         
       
</asp:Content>
