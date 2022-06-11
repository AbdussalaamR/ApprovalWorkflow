const Fname = document.querySelector('#fname');
const Lname = document.querySelector('#lname');
const email = document.querySelector('#email');
const username = document.querySelector('#user_name');
const password = document.querySelector('#password');

const SigninEmail = document.querySelector('#signInEmail');
const SignInpassword = document.querySelector('#SignInPassword');

const myformvalue = document.querySelector('#contactForm');
myformvalue.addEventListener('submit', (e) =>{
e.preventDefault();
});


let create = ()=> {
    console.log("4")
    Data = {
    userName: username.value,
    password: password.value,
    firstName: Fname.value,
    lastName: Lname.value,
    email: email.value
      };
    console.log("1")
    fetch('https://localhost:5001/apiUser/CreateUser',
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
    fetch('https://localhost:5001/apiUser/Login',
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
        window.localStorage.setItem("token", data.token);
        if (data.status == true) {
            window.location.href = "https://technext.github.io/skydash/index.html#";
        }
    })
    .catch(err => {
    console.log(err);
    console.log("3");
    })
}