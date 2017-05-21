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

    function suspendOrUns(userIdH) {
        var action = $('#suspendBtn').attr("name");

        if (action == "Suspend") {
            $.post("/Administrator/UpdateUserStatusFLAG", { userId: userIdH, newFlag: 0 }, function (data) {
                if (data) {
                    $('#suspendBtn').attr("name", "Unsuspend");
                    $('#suspendBtn').text("Unsuspend");
                    $('#suspendBtn').removeClass("btn-danger");
                    $('#suspendBtn').addClass("btn-primary");
                    if($('#modBtn').attr("name") == "Unmod")
                    {
                        $('#modBtn').attr("name", "Mod");
                        $('#modBtn').text("Promote to moderator");
                        $('#modBtn').removeClass("btn-danger");
                        $('#modBtn').addClass("btn-primary");
                    }
                }
            });
        }
        else {
            $.post("/Administrator/UpdateUserStatusFLAG", { userId: userIdH, newFlag: 2 }, function (data) {
                if (data) {
                    $('#suspendBtn').attr("name", "Suspend");
                    $('#suspendBtn').text("Suspend");
                    $('#suspendBtn').removeClass("btn-primary")
                    $('#suspendBtn').addClass("btn-danger");
                }
            });
        }
    }

    function modOrUnmod(userIdH) {
        var action = $('#modBtn').attr("name");

        if (action == "Mod") {
            $.post("/Administrator/UpdateUserStatusFLAG", { userId: userIdH, newFlag: 9 }, function (data) {
                if (data) {
                    $('#modBtn').attr("name", "Unmod");
                    $('#modBtn').text("Demote");
                    $('#modBtn').removeClass("btn-primary")
                    $('#modBtn').addClass("btn-danger");

                    if ($('#suspendBtn').attr("name") == "Unsuspend") {
                        $('#suspendBtn').attr("name", "Suspend");
                        $('#suspendBtn').text("Suspend");
                        $('#suspendBtn').removeClass("btn-primary");
                        $('#suspendBtn').addClass("btn-danger");
                    }

                }
            });
        }
        else {
            $.post("/Administrator/UpdateUserStatusFLAG", { userId: userIdH, newFlag: 2 }, function (data) {
                if (data) {
                    $('#modBtn').attr("name", "Mod");
                    $('#modBtn').text("Promote to moderator");
                    $('#modBtn').removeClass("btn-danger");
                    $('#modBtn').addClass("btn-primary");
                }
            });
        }
    }

    function selectTags(useridH) {
        //var userIdH = Math.floor($('#userIdHid').val());

        var parentContainter = document.getElementById('tagContainer');
        var interestTagNames = new Array();

        var children = parentContainter.children;

        for (var j = 0; j < children.length; j++) {
            for (var i = 0; i < children[j].children.length; i++) {
                if (children[j].children[i].getAttribute("type") == "checkbox") {
                    if (children[j].children[i].checked == true) {
                        interestTagNames.push(children[j].children[i].getAttribute("value"));
                    }
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

                var li = document.createElement("li");
                li.appendChild(input);
                li.appendChild(label);

                //var option = document.createElement("option");
                //option.value = data[index].tagName;
                //option.label = data[index].tagName;
                //option.selected = data[index].userHasIt;
                

                var parent = document.getElementById("tagContainer");

                parent.appendChild(li);
                //parent.appendChild(label);
                //if ((index + 1) % 2 == 0) { // ruzno i dalje, popravi ili kgjb vec
                //    var br = document.createElement("br");
                //    parent.appendChild(br);
                //}

            });
        });

    });

    $('#mdl').on('hidden.bs.modal', function () {
        $('#tagContainer').empty();
    });

    $('#modalCompose').on('hidden.bs.modal', function () {
        $('#inputSubject').val('');
        $('#inputBody').val('');
        $('#warnings').empty();
    });



    function SendMessage()
    {
        $('#warnings').empty();
        var sub = $('#inputSubject').val();
        var bd = $('#inputBody').val();
        var rec = $('#receiver').val();

        var warnings = document.getElementById('warnings');  
        var div = document.createElement('div');
        var strong = document.createElement('strong');

        if (sub === "" || bd === "")
        {
            div.classList.add("alert");
            div.classList.add("alert-danger");
            strong.innerHTML = "Empty field!";
            div.appendChild(strong);
            warnings.appendChild(div);
            return;
        }

        $.post("/User/SendMessage", { receiver: rec, subject: sub, body: bd }, function (data) {

            if (data) {
                div.classList.add("alert");
                div.classList.add("alert-success");
                strong.innerHTML = "Message sent!";
                div.appendChild(strong);
                warnings.appendChild(div);
                //$('#modalCompose').modal('hide');

            }
            else {
                div.classList.add("alert");
                div.classList.add("alert-danger");
                strong.innerHTML = "Error sending your message!";
                div.appendChild(strong);
                warnings.appendChild(div);

            }
        });
    }
}