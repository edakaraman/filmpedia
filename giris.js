function login(){
    var username = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    if(username == "admin" || password == "12345"){
        alert("Login successful!");
    }
    else if (username == "" || password == "") {
        alert("Please fill in all fields.");
    }
    else{
        alert("Login failed!");
    }
}