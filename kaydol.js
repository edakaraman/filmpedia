function register() {
    var username = document.getElementById('name').value;
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    var accept = document.getElementById('accept').checked;
  
    if (username == "" || email == "" || password == "") {
      alert("Please fill in all fields.");
    } else if (!validateEmail(email)) {
      alert("Please enter a valid email.");
    } else if (!accept) {
      alert("Please accept the terms and conditions.");
    } else {
      alert("Registration successful!");
    }
  
    function validateEmail(email) {
      var re = /\S+@\S+\.\S+/;
      return re.test(email);
    }
  }
  