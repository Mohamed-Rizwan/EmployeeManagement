function Delete(url) {
    if (confirm('Are you sure of Dropping the project')) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (result) {
                $("#tblproject").html(result);
                /*location.reload();*/
               /* return false;*/
            }
    })
    }
    
}