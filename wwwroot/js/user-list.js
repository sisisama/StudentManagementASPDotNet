var dropdown = document.getElementsByClassName("dropdown-btn");
var i;

for (i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var dropdownContent = this.nextElementSibling;
        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        } else {
            dropdownContent.style.display = "block";
        }
    });
}

$('.btn-delete').click(function () {
    var id = $(this).data('id');
    Notiflix.Confirm.show(
        'Notiflix Confirm',
        'Do you agree with me?',
        'Yes',
        'No',
        function okCb() {
            $.ajax({
                url: "/User/DeleteUser",
                type: "POST",
                dataType: "json",
                data: {id: id},
                success: function (result) {
                    console.log({result});
                    console.log(result);
                    if(result.respType === 0){
                        // when call is sucessfull
                        // successMessage(result.respDesp, "/User/UserList");
                        successMessage(result.respDesp);
                        $('#tbTable').load('/User/UserTable');
                    }
                },
                error: function (err) {
                    // check the err for error details
                }
            });
        },
        function cancelCb() {

        },
        {},
    );
})