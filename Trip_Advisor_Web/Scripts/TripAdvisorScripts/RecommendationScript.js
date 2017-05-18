{
    window.onload = function () {


        function deleteRec(recId, placeId) {
            $.post("/Place/DeleteRecommendation", { recommendationId: recId, placeId: placeId }, function (data) {
                if (data) {
                    $('#recommendRequest').toggle();

                    var parent = document.getElementById("recommendations");
                    var child = document.getElementById(recId);
                    parent.removeChild(child);
                }
            });
        }
    }
}