constructArtical = (element) => {
    return "<article class=\"row blog_item\"><div class=\"col-md-3\"><div class=\"blog_info text-right\">" +
        "<ul class=\"blog_meta list\"><li><a href=\"#\">" +
        element.creatorName +
        "<i class=\"lnr lnr-user\"></i></a></li><li><a href=\"#\">" +
        element.updatedDate +
        "<i class=\"lnr lnr-calendar-full\"></i></a></li></ul></div></div><div class=\"col-md-9\">" +
        "<div class=\"blog_post\"><img class=\"blogs-img\" src=\"" + element.base64Image + "\"><div class=\"blog_details\">" +
        "<a href=\"single-blog.html?blogId=" + element.id + "\"><h2>" + element.title + "</h2></a><p>" +
        element.content.substring(0, 120) +
        "...</p><a href=\"single-blog.html?blogId=" + element.id + "\" class=\"white_bg_btn\">View More</a>" +
        "</div></div></div></article>";
}

$(document).ready(async () => {
    let token = localStorage.getItem("token");
    if (token != null) {
        $('#logInTab').removeClass("show").addClass("hide");
        $('#logOutTab').removeClass("hide").addClass("show");
        $('#createNew').removeClass("hide").addClass("show");
    }
    let blogs = await fetchAllBlogs();
    let blogsDom = "";
    blogs.forEach(element => {
        blogsDom += constructArtical(element);
    });
    $('.blog_left_sidebar').html(blogsDom);
});
$('#logOutTab').click(async () => {
    await logOut();
});