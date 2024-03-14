<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="ProyectoLastLink.Pages.Inicio" %>
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
              title: 'Estadisticas Pedidos',
              is3D: true,
              pieHole: 0.4,
              legend: { position: "labeled", textStyle: { color: "red", fontSize: 14 } },
             
          };
          var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
      }
    </script>



<%--    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart2);
        function drawChart2() {
            var data = google.visualization.arrayToDataTable(<%=obtenerDatos2()%>);
            var options = {
                title: 'Estadisticas Ventas $',
                 is3D: true,
                pieHole: 0.4,
                legend: { position: "labeled", textStyle: { color: "red", fontSize: 14 } },
            };
            var chart = new google.visualization.PieChart(document.getElementById('piechart2'));
            chart.draw(data, options);
        }
    </script>--%>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['bar'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerDatos_barra()%>);

            var options = {
                Animation: {
                    ondurationchange: 1000,
                    easing:'out'
                },
                
                chart: {
                    title: 'Pedidos por fecha segun Filtro',
                    fontSize:12,
                    subtitle: 'Pedidos/Migración',
                   

                }
            };

            var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

            chart.draw(data, google.charts.Bar.convertOptions(options));

        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    

      
           <div class="container is-widescreen">

               <div class="row ">
                 <div class="col-md-24">
                     
                <% if (Session["usuario"].ToString()=="mamaporque") { %>
                     <h1 class="title has-text-centered  has-text-success"><img src="../imagenes/mamaporque.png" height="1000" width="500"/></h1>
             
                 <% }%>

                  <% if (Session["usuario"].ToString()=="gron") { %>
                       <h1 class="title has-text-centered  has-text-success"><img src="../imagenes/logo_gron_header.png" height="1000" width="500"/></h1>
                           
                   <% }%>

                <% if (Session["usuario"].ToString()=="happymom") { %>
                     <h1 class="title has-text-centered  has-text-success"><img src="../imagenes/descarga.png" height="800" width="300"/></h1>
         
                 <% }%>
                 
     
             
                    </div>
             </div>

                             <%if (Session["Id_rol"].ToString() == "1") { %>

                                      <div class="row">
                                           <asp:Panel ID="Panel11" runat="server" >
              
                                                <article class="message is-success">
                                                           <div class="message-body">
                                                               <div class="icon-text">
                                                               <span class="icon has-text-warning">
                                                                 <i class="fas fa-exclamation-triangle"></i>
                                                               </span>
                                                                 <span class="has-text-danger" >Ver Resumenes</span>
                                                             </div>
                                                               <br />
                                                           <asp:Button runat="server" ID="Button1" class="button is-medium" Text="Ver Resumen Bodega" OnClick="Bodega_Resumen_Click" Visible="True"/>    
                                                           <asp:Button runat="server" ID="Button2" class="button is-medium" Text="Ver Resumen Ventas" OnClick="Ventas_Resumen_Click" Visible="True"/>    
 
                                                     </div>          
                                                </article>
                
            
                                           </asp:Panel>
        

             
                                      </div>
           
 
                                 <br />
                               <% }%>



                          <div class="row ">
                                <asp:Panel ID="Panel3" runat="server">
               
                                                             <article class="message is-success">
                                                                  <div>
                                                                        <asp:Button runat="server" ID="Button3" class="button is-info is-outlined" OnClick="Button3_Click" Text="Buscar por Mes"  Visible="True"/>    
                                                                        <asp:Button runat="server" ID="Button4" class="button is-info is-outlined" OnClick="Button4_Click" Text="Buscar por Rango" Visible="True"/>    
                                                                    </div>          
                                                             </article>
                                </asp:Panel>
                           </div>
                               <br>

                 <asp:Panel ID="PorMes" runat="server" Visible="false">
        
                             <div class="col-md-12 col-sm-12 col-xs-12">
                               <div class="field">
               
              
                                 <div class="control has-icons-left has-icons-right">

                                  <div class="select"> 
    
                                     <asp:DropDownList ID="Pedido" runat="server" CssClass="input is-success" AutoPostBack="true" >

                                     </asp:DropDownList>
     
                                 </div> 

                           
                                </div>
                                </div>
                             </div>
                      
                </asp:Panel>



<br>
                      <asp:Panel ID="PorRango" runat="server" Visible="false">
                           <div class="row ">
                                     <div class="col-md-4 col-sm-4 col-xs-4">
                                      <div class="field">
                                          <div class="control has-icons-left has-icons-right">
                                              <asp:TextBox runat="server" ID="fecha1" CssClass="input is-success"  type="date" placeholder="Ingrese Fecha Desde">
                                             </asp:TextBox>
                                              <span class="icon is-small is-left"> 
                                                   <i class="fas fa-calendar"></i> 
                                              </span> 
                                          </div>
                                        </div>
                                      </div>



                                      <div class="col-md-4 col-sm-4 col-xs-4">
                                        <div class="field">
                                            <div class="control has-icons-left has-icons-right">
                                              <asp:TextBox runat="server" ID="fecha2" CssClass="input is-success" type="date" placeholder="Ingrese Fecha Hasta">
  
                                              </asp:TextBox>
                                              <span class="icon is-small is-left"> 
                                                   <i class="fas fa-calendar"></i> 
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
                                            <asp:Button runat="server" ID="Registrar" CssClass="button is-link" OnClick="Registrar_Click"  Text="Tomar Rango"  />
                                            <br />
           
                                        </div>
                                   </div>
       
                              </div>
                            </div>
                        </asp:Panel>
<br>


  
        <div class="notification is-primary is-light">
           <div class="columns is-desktop">
      

               
                           <div class="column">
                        <div class="contenedor2">
                            <div class="titulo2">Sin Preparar</div>
                              <div class="column">
                              <div class="titulo2"><img src="../imagenes/7132915.png" height="100" width="80"/></div>
                              <br>
          
                                         <div class="tarjeta2">
                                           <div class="titulo2">Totales</div>
                                            <div class="valor2"><p class="is-size-4 align-content-center">
                                                  <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click"><%=Session["cantidad_pendientes"].ToString() %></asp:LinkButton>

                                                 </p></div>
      
                                             </div>
                                               </div>   
                                   </div>
         </div>



      
       <div class="column">
                <div class="contenedor2">
                    <div class="titulo2">Ingresados</div>
                       <div class="column">
                       <div class="titulo2"><img src="../imagenes/3190405.png" height="100" width="80"/></div>
                       <br>
                      <div class="tarjeta2">
                        <div class="titulo2">Total</div>
                         <div class="valor2"><p class="is-size-4 align-content-center">
                             <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"><%=Session["cantidad_ingresado"].ToString() %></asp:LinkButton>
                            </p></div>
                        </div>
                           <div class="tarjeta2">
                               <div class="titulo2">Acumulados</div>
                                  <div class="valor2"><p class="is-size-4 align-content-center">
                                         <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click"><%=Session["cantidad_ingresado_acumulados"].ToString() %></asp:LinkButton>
                         
                                                                    
                                  
                                  
                                  </p></div>
                          </div>

                       </div>   
                 </div>
       </div>

               
               <div class="column">
                  <div class="contenedor2">
                      <div class="titulo2">Armados</div>
                        <div class="column">
                        <div class="titulo2"><img src="../imagenes/7132915.png" height="100" width="80"/></div>
                        <br>
                            
                       <div class="tarjeta2">
                         <div class="titulo2">Todos</div>
                          <div class="valor2"><p class="is-size-4 align-content-center">
                                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"><%=Session["cantidad_Armados"].ToString() %></asp:LinkButton>

                               </p></div>
                        
                           </div>
                             
                        </div>   
                 </div>
               </div>


               
               <div class="column">
                <div class="contenedor2">
                     <div class="titulo2">En Ruta</div>
                      <div class="column">
                      <div class="titulo2"><img src="../imagenes/enruta.png" height="100" width="80"/></div>
                      <br>
                          
                     <div class="tarjeta2">
                       <div class="titulo2">Todos</div>
                        <div class="valor2"><p class="is-size-4 align-content-center">
                              <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"><%=Session["cantidad_ruta"].ToString() %></asp:LinkButton>


                                            </p></div>
                       </div>
                        
                      </div>   
               </div>
            </div>


       
               <div class="column">
                   <div class="contenedor2">
                         <div class="column">
                             <div class="titulo2">Entregados</div>
                         <div class="titulo2"><img src="../imagenes/entregado.png" width="80"/></div>
                         <br>
                           
                        <div class="tarjeta2">
                          <div class="titulo2">Total</div>
                           <div class="valor2"><p class="is-size-4 align-content-center">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><%=Session["cantidad_entregados"].ToString() %></asp:LinkButton>


                                               </p></div>
                          </div>
                           <div class="tarjeta2">
                         <div class="titulo2">Acumulados</div>
                            <div class="valor2"><p class="is-size-4 align-content-center">
                                   <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click"><%=Session["cantidad_entregados_acumulados"].ToString() %></asp:LinkButton>
                         
                                          
        
        
                                </p></div>
                        </div>
                  </div>   
                  </div>
               </div>


               <div class="column">
                   
                    <div class="contenedor2">
                          <div class="column">
                              <div class="titulo2">No Entregados</div>
                          <div class="titulo2"><img src="../imagenes/anulado.png" height="80" width="80"/></div>
                          <br>
                           
            
                         <div class="tarjeta2">
                           <div class="titulo2">Totales</div>
                            <div class="valor2"><p class="is-size-4 align-content-center">
                                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><%=Session["cantidad_rechazados"].ToString() %></asp:LinkButton>


                                      </p></div>
                           </div>
                                <div class="tarjeta2">
                                 <div class="titulo2">Acumulados</div>
                                    <div class="valor2"><p class="is-size-4 align-content-center">
                                      <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click"><%=Session["cantidad_rechazados_acumulados"].ToString() %></asp:LinkButton>
 
                  
        
        
                                        </p></div>
                                </div>
                          </div>   
                   </div>
                </div>
 </div>
     
       <div class="contenedor2">
          <div id="columnchart_material" style="width: 1000px; height: 500px;align-content:center"></div>

       </div>
     
    </div>
 </div>
  
  
      
</asp:Content>
