function jobrolesforteam() {
        var teamid = $("#TeamId").val();
        var jobroledropdown = $("#Jobrole");
        $.ajax({
            type: "Post",
            url: "/Employee/GetJobRole",
            data: '{team:"' + teamid + '"}',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (jsonteam) {
                console.log(jsonteam);
                jobroledropdown.html('');
                jobroledropdown.append($('<option></option>').html('-Select Jobrole-'));
                $.each(JSON.parse(jsonteam), function (i, jobroles) {
                    jobroledropdown.append($('<option></option>').val(jobroles.jobname).html(jobroles.jobname));
                    console.log(jobroles.jobname);
                });
            }

        });
    }



