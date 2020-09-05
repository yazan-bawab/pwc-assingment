constructContentView = (content) => {
    let acctualLength = content.length;
    let index = 0;
    content = "<p>" + content;
    while (index < acctualLength) {
        index = content.indexOf('.', index + 1);
        content = content.substr(0, index + 1) + "</p><p>" + content.substr(index + 1);
    }
    return content;
}


reConstructBlogPresentation = async (blogInfo) => {
    $('.img-fluid').attr("src", blogInfo.base64Image);
    $('#creatorName').html(blogInfo.creatorName + "<i class=\"lnr lnr-user\"></i>");
    $('#updatedDate').html(blogInfo.updatedDate + "<i class=\"lnr lnr-calendar-full\"></i>");
    $('#title').html(blogInfo.title);
    $('#content').html(constructContentView(blogInfo.content.trim()));
    $('#edit').attr("href", "content-creator.html?blogId=" + blogInfo.id);
}

// on load page
$(document).ready(async () => {
    let token = localStorage.getItem("token");
    if (token != null) {
        $('#logInTab').removeClass("show").addClass("hide");
        $('#logOutTab').removeClass("hide").addClass("show");
        $('#edit').removeClass("hide").addClass("show");
    }

    if (getUrlParameter('blogId') == undefined)
        window.location.replace("http://127.0.0.1:5500/Pages/blog.html");

    let blogInfo = await fetchSingleBlog(getUrlParameter('blogId'));
    reConstructBlogPresentation(blogInfo);
});

//logout event
$('#logOutTab').click(async () => {
    await logOut();
});