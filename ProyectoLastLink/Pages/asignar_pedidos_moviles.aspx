<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="asignar_pedidos_moviles.aspx.cs" Inherits="ProyectoLastLink.Pages.asignar_pedidos_moviles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  	 <link rel="stylesheet" type="text/css" href="../css/style.css" />
      <script src="https://polyfill.io/v3/polyfill.js?features=default"></script>
  

<script>

    function initMap() {
        alert("PASO")
        const myLatLng = { lat: -33.4721356, lng: -70.694388 };
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 12,
            center: myLatLng
        });

        new google.maps.Marker({
            position: myLatLng,
            map,
            title: "Hello World!"
        });

        const drawingManager = new google.maps.drawing.DrawingManager({

                drawingControl: true,
                drawingControlOptions: {
                position: google.maps.ControlPosition.TOP_CENTER,
                drawingModes: [
                google.maps.drawing.OverlayType.POLYGON,
                ],
            },

        });



        drawingManager.setMap(map); 
    }

    window.initMap = initMap;
    

</script>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
 <div id="map"></div>
	 <script  src="https://maps.googleapis.com/maps/api/js?key=AIzaSyATOguNja2nQ_HDkOngnZxMGMGsxvWdjTc&callback=initMap&libraries=drawing&v=weekly"  defer></script>
 </asp:Content>
