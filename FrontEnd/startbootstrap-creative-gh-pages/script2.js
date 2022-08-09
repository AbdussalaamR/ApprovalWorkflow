const Fname = document.querySelector('#fname');
const Lname = document.querySelector('#lname');
const email = document.querySelector('#email');
//const username = document.querySelector('#user_name');
const password = document.querySelector('#password');


const SigninEmail = document.querySelector('#signInEmail');
const SignInpassword = document.querySelector('#SignInPassword');

const myformvalue = document.querySelector('#contactForm');
myformvalue.addEventListener('submit', (e) =>{
e.preventDefault();
});


const myformvalue2 = document.querySelector('#contactForm2');
myformvalue2.addEventListener('submit', (e) =>{
e.preventDefault();
});


let create = ()=> {
    console.log("4")
    Data = {
    //userName: username.value,
    password: password.value,
    firstName: Fname.value,
    lastName: Lname.value,
    email: email.value
      };
    console.log("1")
    fetch("https://localhost:5001/apiUser/CreateUser",{
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body:JSON.stringify(Data)
    })
    .then(res => res.json())
    .then(data => {
        alert(data.message);
    })
    .catch(err => {
    console.log(err);

    console.log("3");
    })
}

let signIn = ()=> {
    console.log("4")
    Data = {
    password: SignInpassword.value,
    email: SigninEmail.value
      };
    console.log("1")
    fetch("https://localhost:5001/apiUser/Login",
    {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body:JSON.stringify(Data)
    })
    .then(res => res.json())
    .then(data => {
        console.log("2")
        
        console.log(data);
        
        console.log(data.userRoles[data.userRoles.length - 1]);
       if (data.status == true) {
            window.location.href = `/regal/regal/index.html?role=${data.userRoles[data.userRoles.length - 1].name}`;
        }
        else(alert(data.message));
        window.localStorage.setItem("token", data.token);
        window.localStorage.setItem("userName", data.userName);
        window.localStorage.setItem("email1", data.email);
        window.localStorage.setItem("userId", data.userId)
        var currentUser = window.localStorage.getItem("userName");
        console.log(currentUser);
    })
    .catch(err => {
    console.log(err.status);
    console.log("3");
    })
}

