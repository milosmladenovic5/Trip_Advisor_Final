{
    function inboxClick(userId) {
        $('#listOfMessages').empty();

        $.post("/User/GetAllReceivedMessagesOfUser", { userId:userId }, function (data) {
            $.each(data, function (index) {

                var li = document.createElement("li");
                
                var tdSubject = document.createElement("td");
               // tdSubject.innerHTML = data[index].Subject;
                tdSubject.colSpan = 2;
                tdSubject.classList = "messageCard";

                var aSubject = document.createElement("a");
                aSubject.innerHTML = data[index].Subject;
                aSubject.onclick = function ()
                {
                    var text = data[index].Text;
                    var sender = data[index].SenderUsername;
                    var date = data[index].SendingDate;
                    var subject = data[index].Subject;
                    
                    displayMessage(text, sender, subject, date);
                };

                aSubject.href = "#";
                
                tdSubject.appendChild(aSubject);

                var tdDate = document.createElement("td");
                tdDate.innerHTML = data[index].SendingDate;
                tdDate.classList = "messageCard";

                var tdSender = document.createElement("td");
                tdSender.innerHTML = data[index].SenderUsername;
                tdSender.classList = "messageCard";

                var table = document.createElement("table");
                table.style.tableLayout = "fixed";
                table.style.width = "100%";
                table.style.marginBottom = "10px";
                table.style.marginTop = "10px";
                table.style.border = "1px dashed black";

                var trUp = document.createElement("tr");
                trUp.appendChild(tdSubject);
                trUp.style.borderBottom = "1 px rgba(0,0,0,0.6)";

                var trDown = document.createElement("tr");
                trDown.appendChild(tdDate);
                trDown.appendChild(tdSender);

                table.appendChild(trUp);
                table.appendChild(trDown);

                li.appendChild(table);

                document.getElementById("listOfMessages").appendChild(li);               

            });
        });

        function displayMessage(messageText, sender, subject, date) {
             
            $("#fromContainer").empty();
            $("#subjectContainer").empty();
            $("#dateContainer").empty();
            $("#messageContainer").empty();

            document.getElementById("fromContainer").innerHTML = sender;
            document.getElementById("subjectContainer").innerHTML = subject;
            document.getElementById("dateContainer").innerHTML = date;
            document.getElementById("messageContainer").innerHTML = messageText;
        }
    }



}