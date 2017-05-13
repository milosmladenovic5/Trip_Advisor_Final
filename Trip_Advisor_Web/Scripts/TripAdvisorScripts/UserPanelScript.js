{
    //$('#followBtn').on('click', function () {
    //    var action = $('#followBtn').attr("name");
    //    var userIdH = Math.floor($('#userIdHid').val());


    //    if (action == "Follow") {
    //        $.post("/User/Follow", { userId: userIdH }, function (data) {
    //            $('#followBtn').attr("name", "Unfollow");
    //            $('#followBtn').text("Unfollow");
    //        });
    //    }
    //    else {
    //        $.post("/User/Unfollow", { userId: userIdH }, function (data) {
    //            $('#followBtn').attr("name", "Follow");
    //            $('#followBtn').text("Follow");
    //        });
    //    }

    //});

    function followOrUnfollow(userIdH)
    {
        var action = $('#followBtn').attr("name");
        
        if (action == "Follow") {
            $.post("/User/Follow", { userId: userIdH }, function (data) {
                $('#followBtn').attr("name", "Unfollow");
                $('#followBtn').text("Unfollow");
            });
        }
        else {
            $.post("/User/Unfollow", { userId: userIdH }, function (data) {
                $('#followBtn').attr("name", "Follow");
                $('#followBtn').text("Follow");
            });
        }
    }

    function selectTags(useridH) {
        //var userIdH = Math.floor($('#userIdHid').val());

        var parentContainter = document.getElementById('tagContainer');
        var interestTagNames = new Array();

        var children = parentContainter.children;

        for (var i = 0; i < children.length; i++) {
            if (children[i].getAttribute("type") == "checkbox") {
                if (children[i].checked == true) {
                    interestTagNames.push(children[i].getAttribute("value"));
                }
            }
        }

        var userId = useridH;

        $.post("/User/AddInterestTags", { userId: userId, interestTagNames: interestTagNames }, function (data) {
            $('#mdl').modal('hide');
            $('#tagContainer').empty();
        })



    }

    $('#interests').click(function () {
        $.post("/User/ReturnInterestTags", function (data) {

            $.each(data, function (index) {
                var label = document.createElement("label");
                label.innerHTML = data[index].tagName;
                label.for = "check" + index;


                var input = document.createElement("input");
                input.type = "checkbox";
                input.value = data[index].tagName;
                input.name = data[index].tagName;
                input.checked = data[index].userHasIt;
                label.id = "check" + index;

               

                //var option = document.createElement("option");
                //option.value = data[index].tagName;
                //option.label = data[index].tagName;
                //option.selected = data[index].userHasIt;
                

                var parent = document.getElementById("tagContainer");
                parent.appendChild(input);
                parent.appendChild(label);
                if ((index + 1) % 2 == 0) { // ruzno i dalje, popravi ili kgjb vec
                    var br = document.createElement("br");
                    parent.appendChild(br);
                }

            });
        });

    });

    $('#mdl').on('hidden.bs.modal', function () {
        window.alert('closing modal event fired!');
        $('#tagContainer').empty();
    });
}