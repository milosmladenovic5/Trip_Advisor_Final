{
    window.onload = function () {
      
    }
    function initMap() {
        var long = Math.floor($('#centLong').val());
        var lati = Math.floor($('#centLat').val());

        var uluru = { lat: lati, lng: long };
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 8,
            center: uluru
        });
        var marker = new google.maps.Marker({
            position: uluru,
            map: map
        });
    }




}