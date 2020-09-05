//on page load script
$(document).ready(async () => {
    let token = localStorage.getItem("token");
    if (token == null)
        window.location.replace("http://127.0.0.1:5500/Pages/blog.html");

    if (getUrlParameter('blogId') == undefined)
        return;
    let userInfo = await getLogInUserTypeAndId();
    let blog = await fetchSingleBlog(getUrlParameter('blogId'));
    if (userInfo.type == "ADMIN" || userInfo.id == blog.ownerUserId) {
        $('.imagePreview').css("background-image", "url(" + blog.base64Image + ")");
        $('#blogHeader').val(blog.title);
        $('.single-textarea').val(blog.content);
    } else {
        $('.uploadFile').attr("disabled", true);
        $('#blogHeader').attr("disabled", true);
        $('.imagePreview').css("background-image", "url(" + blog.base64Image + ")");
        $('#blogHeader').val(blog.title);
        $('.generic-blockquote').html(blog.content);
        $('#preContent').removeClass('hide').addClass('show');
    }
});
// saving event
$('#doneEditing').click(async () => {
    let imageBase64 = $('.imagePreview').css("background-image").replace("url(\"", "").replace("\")", "");
    let title = $('#blogHeader').val();
    let content = $('.single-textarea').val();

    if (getUrlParameter('blogId') == undefined)
        await createNewBlog(title, content, imageBase64);
    else
        await updateBlog(getUrlParameter('blogId'), title, content, imageBase64);
});
//logout event
$('#logout').click(async () => {
    await logOut();
});

//upload image
$(function () {
    $(document).on("change", ".uploadFile", function () {
        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader)
            return;

        if (/^image/.test(files[0].type)) {
            var reader = new FileReader();
            reader.readAsDataURL(files[0]);

            reader.onloadend = function () {
                uploadFile.closest(".imgUp").find('.imagePreview').css("background-image",
                    "url(" + this.result + ")");
            }
        }
    });
});