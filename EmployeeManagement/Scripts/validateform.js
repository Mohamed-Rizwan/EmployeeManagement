function validate() {
    var name = document.getElementById("Name");
    var gender = document.getElementById("gender");
    var email = document.getElementById("Email");
    var phone = document.getElementById("PhoneNumber");
    var jobrole = document.getElementById("Jobrole");
    var team = document.getElementById("TeamId");
    var doj = document.getElementById("DateofJoining");
    var txtmail = document.getElementById("textemail"); 
    var txtphone = document.getElementById("textphone");

    if (name.value.trim() == "") {
        name.style.border = "solid 1px red";
        document.getElementById("lblname").style.visibility = "visible";

    }
    if (gender.value.trim() == "") {
        gender.style.border = "solid 1px red";
        document.getElementById("lblgender").style.visibility = "visible";

    }
    if (email.value.trim() == "") {
        email.style.border = "solid 1px red";
        document.getElementById("lblemail").style.visibility = "visible";

    }
    if (phone.value.trim() == "") {
        phone.style.border = "solid 1px red";
        document.getElementById("lblphone").style.visibility = "visible";

    }
    if (jobrole.value.trim() == "") {
        jobrole.style.border = "solid 1px red";
        document.getElementById("lbljobrole").style.visibility = "visible";

    }
    if (team.value.trim() == "") {
        team.style.border = "solid 1px red";
        document.getElementById("lblteam").style.visibility = "visible";

    }
    if (doj.value.trim() == "") {
        doj.style.border = "solid 1px red";
        document.getElementById("lbldoj").style.visibility = "visible";

    }
    if (name.value.trim() == "" || gender.value.trim() == "" || email.value.trim() == "" || phone.value.trim() == "" || jobrole.value.trim() == "" || team.value.trim() == "" || doj.value.trim() == "") {
        return false;
    }
    else if (txtmail.value != "" || txtphone.value != "") {
        return false;
    }
    else {
        return true;
    }
}

function namevalidation(inputtext) {

    var textfeild = document.getElementById("textname");
    if (inputtext.value == "") {
        textfeild.textContent = 'Name Required';
        textfeild.style.color = 'red';
        inputtext.style.border = 'solid 1px red';
        
    }
    else {
        textfeild.textContent = '';
        inputtext.style.border = 'solid 1px grey';
        
    }
}


function emailvalidation(inputtext) {
    var textfeild = document.getElementById("textemail");
    var regx = /^([a-zA-Z0-9\.\-]+)@([a-zA-Z0-9-]+).([a-z]{2,20})$/;
    if (regx.test(inputtext.value)) {
        textfeild.textContent = '';
        inputtext.style.border = 'solid 1px grey';
        
    }
    else {
        textfeild.textContent = 'Invalid Email ';
        textfeild.style.color = 'red';
        inputtext.style.border = 'solid 1px red';
       
       
    }
}

function phonenumbervalidation(inputtext) {
    var textfeild = document.getElementById("textphone");
    var regx = /^[6-9]\d{9}$/;
    if (regx.test(inputtext.value)) {
        textfeild.textContent = '';
        inputtext.style.border = 'solid 1px grey';
            
        }

    else {
        textfeild.textContent = 'Invalid PhoneNumber ';
        textfeild.style.color = 'red';
        inputtext.style.border = 'solid 1px red';


    }
}

function checkemail() {
    var Email = $("#Email").val();
    $.ajax({
        type: "POST",
        url: "/Employee/checkemail",
        data: '{email:"' + Email + '"}',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var msg = $("#textemail");
            if (result) {
                msg.html("Email ID already Exist");
                msg.css("color", "red");
            }
            else {
                msg.html(" ");

            }

        },
        error: function () {
            alert("Hello")
        }



    });
    /*alert("AJAX Completed")*/
}




    

