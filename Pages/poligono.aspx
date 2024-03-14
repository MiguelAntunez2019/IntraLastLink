<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="poligono.aspx.cs" Inherits="ProyectoLastLink.Pages.poligono1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyATOguNja2nQ_HDkOngnZxMGMGsxvWdjTc&callback=initMap&libraries=drawing,geometry&v=weekly" defer></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

     <script>
         //creo esta variable para poder ir a dejar las coordenadas y despues actualizar el textarea
         let dibujaunpoligono = null;
         //se inicia el mapa en las coordenadas que tu me pasaste
         function initMap() {
             const mapa = new google.maps.Map(document.getElementById("map"), {
                 center: { lat: -34.397, lng: 150.644 },
                 zoom: 8,
             });
             //creo una variable para dibujar el poligono esta informacion la saque desde el drawinmanager https://developers.google.com/maps/documentation/javascript/drawinglayer?hl=es-419
             const dibuja = new google.maps.drawing.DrawingManager({
                 drawingControl: true,
                 drawingControlOptions: {
                     position: google.maps.ControlPosition.TOP_CENTER,
                     drawingModes: [google.maps.drawing.OverlayType.POLYGON],
                 },
                 polygonOptions: {
                     fillColor: '#FF0000',
                     fillOpacity: 0.5,
                     strokeWeight: 2,
                     clickable: true,
                     editable: true,
                     draggable: true,
                     zIndex: 1,
                 },
             });

             dibuja.setMap(mapa);

             // esta funcion me permite controlar los eventos que suceden en el google maps
             // https://developers.google.com/maps/documentation/javascript/dds-boundaries/handle-events?hl=es-419
             //para saber que hacia el overlay fui a https://developers.google.com/maps/documentation/javascript/drawinglayer?hl=es-419

             //Un evento overlaycomplete: se pasa un literal de objeto, que contiene el OverlayType y una referencia a la superposición, como argumento
             google.maps.event.addListener(dibuja, 'overlaycomplete', function (event) {
                 if (event.type === google.maps.drawing.OverlayType.POLYGON) {
                     const coordenada = event.overlay.getPath().getArray();

                     dibujaunpoligono = coordenada;


                     //paso la informacion de las coordenadas
                     ActualizaTextArea(dibujaunpoligono);
                 }
             });

             //aqui inicio desde el lo que se establece en el mapa la coordenada inicial

             const initialMapCenter = mapa.getCenter();


             const marker = new google.maps.Marker({
                 position: { lat: initialMapCenter.lat() + 0.1, lng: initialMapCenter.lng() + 0.1 },
                 map: mapa,
                 title: 'Marcador de Verificación'
             });
         }
         //aqui lleno las variables que se obtienen desde el mapa y lleno en un textarea solo para mostrar la información
         function ActualizaTextArea(coordenada) {
             const textarea = document.getElementById('coordenadaTextarea');
             textarea.value = "Polygon coordenada:\n" + JSON.stringify(coordenada);
         }
         //controlo que siempre se dibuje un poligono antes de presionar el boton validar
         function chequearlocacion() {
            
             if (!dibujaunpoligono) {
                 alert('Dibuja un polígono primero.');
                 return;
             }

             //inicia un marcador cercano a un punto de inicio en el mapa
             const marcadorDePosicion = new google.maps.LatLng(-34.3, 150.7);
             //aqui valido si la coordenada se encuentra dentro del poligono
             const dentro = google.maps.geometry.poly.containsLocation(marcadorDePosicion, new google.maps.Polygon({ paths: dibujaunpoligono }));

             if (dentro) {
                 //mensaje cualquiera
                 alert('El marcador está dentro del polígono.');
             } else {
                 //mensaje cualquiera
                 alert('El marcador está fuera del polígono.');
             }
         }
     </script>
      <!--seccion para dibujar el text area en el codigo-->
  <div id="map" style="height: 400px;"></div>

  <textarea id="coordenadaTextarea" rows="5" cols="50" readonly></textarea>
  <button onclick="chequearlocacion()">Verificar</button>
  
   

</asp:Content>
