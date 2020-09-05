Register = async (inputName, inputEmail, inputPassword, type) => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Origin, Content-Type, X-Auth-Token');

    let response = await fetch("https://localhost:5001/api/Users", {
        headers: headers,
        method: 'POST',
        body: JSON.stringify({
            name: inputName,
            email: inputEmail,
            password: inputPassword,
            type: type
        })
    });
    if (response.ok)
        window.location.replace("http://127.0.0.1:5500/Pages/login.html");
    else
        alert(JSON.stringify(response.text()));
}

logIn = async (email, password) => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Origin, Content-Type, X-Auth-Token');

    let clientIpAddress = await getClientIpAddress();
    let response = await fetch("https://localhost:5001/api/Users/login", {
        headers: headers,
        method: 'POST',
        body: JSON.stringify({
            email: email,
            password: password,
            ipAddress: clientIpAddress
        })
    });
    if (!response.ok) {
        alert(await response.text());
        return;
    }

    localStorage.setItem("token", await response.text());
    window.location.replace("http://127.0.0.1:5500/Pages/blog.html");
}

logOut = async () => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Origin, Content-Type, X-Auth-Token');
    let clientIpAddress = await getClientIpAddress();
    let response = await fetch("http://localhost:5000/api/Users/logout", {
        headers: headers,
        method: 'POST',
        body: JSON.stringify({
            tokenId: localStorage.getItem('token'),
            ipAddress: clientIpAddress
        })
    });
    if (!response.ok) {
        alert(await response.text());
        return;
    }
    localStorage.removeItem("token");
    window.location.replace("http://127.0.0.1:5500/Pages/blog.html");
}

getLogInUserTypeAndId = async () => {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Origin, Content-Type, X-Auth-Token');
    let clientIpAddress = await getClientIpAddress();
    let response = await fetch("http://localhost:5000/api/Users/info", {
        headers: headers,
        method: 'POST',
        body: JSON.stringify({
            token: localStorage.getItem('token'),
            ipAddress: clientIpAddress
        })
    });
    if (response.ok) {
        repsonseResult = await response.json();
        return repsonseResult;
    }
    result = {
        id: '',
        type: 'WRITER'
    }
}