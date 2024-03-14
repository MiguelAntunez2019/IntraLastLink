<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Graficos_seller.aspx.cs" Inherits="ProyectoLastLink.Pages.Graficos_seller" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
  google.charts.load('current', {'packages':['corechart']});
  google.charts.setOnLoadCallback(drawChart);

  function drawChart() {

    var data = google.visualization.arrayToDataTable(<%=obtenerDatos()%>);
         var options = {
          title: 'Estadisticas Pedidos',
          pieHole: 0.4,
          legend: { position: "labeled", textStyle: { color: "red", fontSize: 18 } },
         
      };
      var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
  }
</script>



<script type="text/javascript">
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart2);

    function drawChart2() {

        var data = google.visualization.arrayToDataTable(<%=obtenerDatos2()%>);
        var options = {
            title: 'Estadisticas Ventas $',
             is3D: true,
            pieHole: 0.4,
            legend: { position: "labeled", textStyle: { color: "red", fontSize: 18 } },

        };
        var chart = new google.visualization.PieChart(document.getElementById('piechart2'));

        chart.draw(data, options);
    }
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
 <div class="container is-widescreen ">

     <div class="notification is-primary is-light">
   <div class="columns is-desktop">
      
      
       <div class="column">
              <div class="card has-background-primary is-light  has-text-white">
                
                <div id="piechart" style="width: 1000px; height: 500px;"></div>

              </div>
            </div>
        

          
      
        

</div>
</div>
 </div>

  
</asp:Content>
