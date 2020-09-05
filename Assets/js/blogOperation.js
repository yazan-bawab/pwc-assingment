fetchAllBlogs = async () => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');


    let response = await fetch("http://localhost:5000/api/Blogs", {
        headers: headers,
        method: 'GET',
    });
    if (response.ok)
        return await response.json();
    else
        return [];
}

createNewBlog = async (title, content, base64Image) => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Origin, Content-Type, X-Auth-Token');

    let clientIpAddress = await getClientIpAddress();
    let response = await fetch("http://localhost:5000/api/Blogs", {
        headers: headers,
        method: 'POST',
        body: JSON.stringify({
            title: title,
            content: content,
            base64Image: base64Image,
            token: localStorage.getItem('token'),
            ipAddress: clientIpAddress
        })
    });
    if (!response.ok) {
        alert("Something went wrong please try again later");
        return;
    }
    window.location.replace("http://127.0.0.1:5500/Pages/blog.html");
}

fetchSingleBlog = async (blogId) => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');


    let response = await fetch("http://localhost:5000/api/Blogs/" + blogId, {
        headers: headers,
        method: 'GET',
    });
    if (response.ok)
        return await response.json();
    else
        return {};
}

updateBlog = async (blogId, title, content, base64Image) => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Origin, Content-Type, X-Auth-Token');

    let clientIpAddress = await getClientIpAddress();
    let response = await fetch("http://localhost:5000/api/Blogs", {
        headers: headers,
        method: 'PUT',
        body: JSON.stringify({
            blogId: blogId,
            blog: {
                title: title,
                content: content,
                base64Image: base64Image,
            },
            token: localStorage.getItem('token'),
            ipAddress: clientIpAddress
        })
    });
    if (!response.ok) {
        alert("Something went wrong please try again later");
        return;
    }
    window.location.replace("http://127.0.0.1:5500/Pages/single-blog.html?blogId=" + blogId);
}