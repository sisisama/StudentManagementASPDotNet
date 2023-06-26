$('#btnUpdate').click(function (e) {
    e.preventDefault();

    const url = new URL(location.href);
    const searchParams = url.searchParams;
    let userId = searchParams.get('userId');

    let userName = $('#userName').val();
    let email = $('#email').val();
    let password = $('#password').val();
    let userRole = $('#userRole').val();
    if (userName === null) {
        return false;
    }
    
    if (email === null) {
        return false;
    }
    
    let reqModel = {
        userName : userName,
        email : email,
        password : password,
        role : userRole,
        id: userId
    };

    $.ajax({
        url: "/User/Update/" + userId,
        type: "POST",
        dataType: "json",
        data: {reqModel : reqModel},
        success: function (result) {
            if(result.respType === 0){
                successMessage(result.respDesp, "/User/UserList");
            }
            else{
                errorMessage(result.respDesp);
            }
        },
        error: function (err) {
            // check the err for error details
        }
    });
})