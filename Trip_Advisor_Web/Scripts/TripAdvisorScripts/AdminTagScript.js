{
    function updateTag(tagId) {
        $('#warnings').empty();
        var tagName = $("#" + tagId).val();
        var oldName = $("#" + tagId).attr("name");

        var warnings = document.getElementById('warnings');
        var div = document.createElement('div');
        var strong = document.createElement('strong');

        if (tagName != "" && tagName != oldName) {
            $.post("/Administrator/UpdateTag", { id: tagId, newName: tagName }, function (data) {

                if (data) {
                    div.classList.add("alert");
                    div.classList.add("alert-success");
                    strong.innerHTML = "Successfuly updated tag!";
                    div.appendChild(strong);
                    warnings.appendChild(div);


                }
                else {
                    div.classList.add("alert");
                    div.classList.add("alert-danger");
                    strong.innerHTML = "Tag name already exists!";
                    div.appendChild(strong);
                    warnings.appendChild(div);

                }
            });
        }
        else {
            div.classList.add("alert");
            div.classList.add("alert-danger");
            strong.innerHTML = "Tag name empty or not changed!";
            div.appendChild(strong);
            warnings.appendChild(div);
        }
    }

    function deleteTag(tagId)
    {
        $.post("/Administrator/DeleteTag", { id: tagId }, function (data) {
            if (data) {
                var parent = document.getElementById("tgc");
                var child = document.getElementById("cont" + tagId);
                parent.removeChild(child);
            }
         
        });
    }

    $('#newTag').keyup(function () {

        $("#ntLink").attr('href', '/Administrator/CreateTag/?newTag=' + $(this).val());

    })
}
