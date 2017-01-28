window.onload = function ()
{
    $('#dropdownSelect li a').on('click', function () {
        var selected = $(this).html();
        $('#searchInput').attr("placeholder", "Search " + selected);
        $('#entityType').val(selected);
    });

    if ($('#searchInput').length <= 2) {
        function ajaxCallFillOptions() {
            var selectedItem = $('#searchInput').attr("placeholder");
            var afterBlanko = selectedItem.substr(selectedItem.indexOf(" ") + 1);

            if (afterBlanko == "User") {
                if (($('#searchInput').val().length == 1)) {
                    var firstLetter = $('#searchInput').val()[0];

                    $.post("/Search/ReturnAllUsersByFirstLetter", { firstLetter: firstLetter }, function (data) {
                        // $('#nameList').empty();
                        $.each(data, function (index) {
                            var option = document.createElement("option");
                            option.value = data[index];

                            var parent = document.getElementById("nameList");
                            parent.appendChild(option);
                        });
                    });
                }
            }
            else if (afterBlanko == "Place") {
                if (($('#searchInput').val().length == 1)) {
                    var firstLetter = $('#searchInput').val()[0];

                    $.post("/Search/ReturnAllPlacesByName", { firstLetter: firstLetter }, function (data) {
                        // $('#nameList').empty();
                        $.each(data, function (index) {
                            var option = document.createElement("option");
                            option.value = data[index];

                            var parent = document.getElementById("nameList");
                            parent.appendChild(option);
                        });
                    });
                }
            }
            else if (afterBlanko == "Country") {
                if (($('#searchInput').val().length == 1)) {
                    var firstLetter = $('#searchInput').val()[0];

                    $.post("/Search/ReturnAllCountriesByName", { firstLetter: firstLetter }, function (data) {
                        // $('#nameList').empty();
                        $.each(data, function (index) {
                            var option = document.createElement("option");
                            option.value = data[index];

                            var parent = document.getElementById("nameList");
                            parent.appendChild(option);
                        });
                    });
                }
            }
            else if (afterBlanko == "City") {
                if (($('#searchInput').val().length == 1)) {
                    var firstLetter = $('#searchInput').val()[0];

                    $.post("/Search/ReturnAllCitiesByName", { firstLetter: firstLetter }, function (data) {
                        // $('#nameList').empty();
                        $.each(data, function (index) {
                            var option = document.createElement("option");
                            option.value = data[index];

                            var parent = document.getElementById("nameList");
                            parent.appendChild(option);
                        });
                    });
                }
            }

        }
    }


    function inputFocus(i) {
        if (i.value == i.defaultValue) { i.value = ""; i.style.color = "#000"; }
    }
    function inputBlur(i) {
        if (i.value == "") { i.value = i.defaultValue; i.style.color = "#888"; }
    }
}