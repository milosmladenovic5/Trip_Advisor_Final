{
    window.onload = function ()
    {
        function SomeFunction()
        {

            if ($("#recommendationText").length == 0) {
                var recommendations = $('#recommendationAdd');

                var labelText = $(document.createElement('label'));
                labelText.text('Recommendation text: ');

                var labelRating = $(document.createElement('label'));
                labelRating.text('Enter your numeric rating of place, enter numbers in range of 1 to 10: ');


                var textBox = $(document.createElement('textarea'));
                var ratingText = $("<input type='text'>");

                textBox.attr('id', 'recommendationText');
                textBox.attr('class', 'submits');


                ratingText.attr('id', 'ratingValue');
                ratingText.attr('name', 'ratingValue');
                ratingText.attr('class', 'submits');

            }

        };
    }

    $('#recommendationText').change(function () {

        $('#recommendationComment').val($('#recommendationText').text);
        $('#recommendationTe').val($('#ratingValue').text);

    });


    $(document).on('input propertychange', "textarea[id='recommendationText']", function () {
        var text = $('#recommendationText').text;
        $('#recommendationComment').val($('#recommendationText').val());
    });

    $('#ratingValue').on('input', function () {
        // Code here
        var text = 5;
        $('#recommendationRating').val($('#ratingValue').val());

    });


    $("#ratingValue").on('change keydown paste input', function () {
        var text = 5;

        $('#recommendationRating').val($('#ratingValue').val());
    });

    function DeleteRec(placeId, recId)
    {
        $.post("/User/AddInterestTags", { recommendationId: recId, placeId: placeId }, function (data) {
          $('#recommendRequest').Show();
          $('#recommendations').remove('#' + recId);
        })
    }
}