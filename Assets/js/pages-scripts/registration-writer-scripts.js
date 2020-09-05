//on page load script
$(document).ready(async () => {
    let token = localStorage.getItem("token");
    if (token != null)
        window.location.replace("http://127.0.0.1:5500/Pages/blog.html");
});

// regestar a writer user event
$('#createUser').click(async () => {
    if ($('#name').val() == "") {
        alert("cannot create user without a name");
        return;
    }

    if ($('#email').val() == "") {
        alert("cannot create user without an Email");
        return;
    }

    if (!isValidEmailAddress($('#email').val())) {
        alert("Please enter a valid Email");
        return;
    }

    if ($('#password').val() == "") {
        alert("cannot create user without a password");
        return;
    }

    if ($('#password').val() != $('#pass').val()) {
        alert("password missmatch");
        return;
    }
    $("#createUser").attr("disabled", true);
    await Register(
        $('#name').val(),
        $('#email').val(),
        $('#password').val(),
        "WRITER"
    );
    $("#createUser").attr("disabled", false);
})