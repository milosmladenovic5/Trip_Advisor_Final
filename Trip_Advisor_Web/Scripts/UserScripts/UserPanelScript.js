{
    $('#followBtn').on('click', function () {
        var action = $('#followBtn').attr("name");
        var userIdH = Math.floor($('#userIdHid').val());


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

    });

    function selectTags() {
        var userIdH = Math.floor($('#userIdHid').val());

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
        $.post("/Home/ReturnAllInterestTags", function (data) {
            $.each(data, function (index) {
                var label = document.createElement("label");
                label.innerHTML = data[index];
                label.for = "check" + index;


                var input = document.createElement("input");
                input.type = "checkbox";
                input.value = data[index];
                input.name = data[index];
                label.id = "check" + index;

                var br = document.createElement("br");

                var parent = document.getElementById("tagContainer");
                parent.appendChild(input);
                parent.appendChild(label);
                parent.appendChild(br);

            });
        });

    });
}