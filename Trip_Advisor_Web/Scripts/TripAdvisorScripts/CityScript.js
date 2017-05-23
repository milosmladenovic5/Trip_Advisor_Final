{
    function searchByTags()
    {
        if ($('#tagSearch').val()[$('#tagSearch').val().length - 1] != '#' && ($('#tagSearch').val().length == 2))
        {
            var tagFirstLetter = $('#tagSearch').val()[1];
            var firstLetter = { firstLetter: tagFirstLetter }

            if ($('#tagSearch').val()[0] == '#') {
                $.post("/Search/ReturnAllTagsByName", firstLetter, function (data) {
                    $('#tagList').empty();
                    $.each(data, function (index) {
                        var option = document.createElement("option");
                        option.value = "#" + data[index];

                        var parent = document.getElementById("tagList");
                        $('#tagList').empty();
                        parent.appendChild(option);
                    });
                });
            }
        }


        if ($('#tagSearch').val()[($('#tagSearch').val().length) - 3] == ' ' && $('#tagSearch').val()[$('#tagSearch').val().length - 2] == '#')
        {
            //ako je blanko pretposlednji uneti znak, a poslednji #, znaci da kucamo sledeci tag

            var uneseniTagovi1 = $('#tagSearch').val();

            var uneseniTagovi = uneseniTagovi1.substring(0, uneseniTagovi1.length - 1);

            var tagFirstLetter = $('#tagSearch').val()[$('#tagSearch').val().length - 1];
            var firstLetter = { firstLetter: tagFirstLetter }

            $.post("/Search/ReturnAllTagsByName", firstLetter, function (data) {
                $('#tagList').empty();
                $.each(data, function (index) {
                    var option = document.createElement("option");
                    option.value = uneseniTagovi + data[index];

                    var parent = document.getElementById("tagList");
                    $('#tagList').empty();
                    parent.appendChild(option);
                });
            });
        }

    }


    function inputFocus(i) {
        if (i.value == i.defaultValue) { i.value = ""; i.style.color = "#000"; }
    }
    function inputBlur(i) {
        if (i.value == "") { i.value = i.defaultValue; i.style.color = "#888"; }
    }
}