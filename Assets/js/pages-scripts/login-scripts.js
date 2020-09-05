//on page load script
$(document).ready(async () => {
    let token = localStorage.getItem("token");
    if (token != null)
        window.location.replace("http://127.0.0.1:5500/Pages/blog.html");
});

//logIn event
$('#logIn').click(async () => {
    if ($('#email').val() == "") {
        alert("cannot login without an Email");
        return;
    }

    if (!isValidEmailAddress($('#email').val())) {
        alert("Please enter a valid Email");
        return;
    }

    if ($('#password').val() == "") {
        alert("cannot login without a password");
        return;
    }

    await logIn(
        $('#email').val(),
        $('#password').val()
    );
});