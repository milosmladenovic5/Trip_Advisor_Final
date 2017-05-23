{
    window.onload = function () {
      
    }
    function initMap() {
        var long = Math.floor($('#centLong').val());
        var lati = Math.floor($('#centLat').val());

        //var long = $('#centLong').val();
        //var lati = $('#centLat').val();

        var place = { lat: lati, lng: long };
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 8,
            center: place
        });
        var marker = new google.maps.Marker({
            position: place,
            map: map
        });
    }




}