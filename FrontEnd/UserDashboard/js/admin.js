const welcomeName = document.getElementById("welcome_name")
const welcomeEmail = document.getElementById("user_mail");
const approvalCount = document.querySelector(".text-primary");
const requestCount = document.querySelector(".text-sec");

const toDay = document.getElementById("mydate");

 var currentUser = window.localStorage.getItem("userName");
 console.log(currentUser);
 var currentEmail = window.localStorage.getItem("email1");
welcomeName.textContent = `${currentUser}`;
welcomeEmail.textContent = `${currentEmail}`;

toDay.textContent = new Date();
var url = window.location.href.split('=')[1];

var roleBullet1 = document.querySelector("#roleFunction1");
var roleBullet2 = document.querySelector("#roleFunction2");
var roleBullet3 = document.querySelector("#newFormTemplate");

console.log(url);

if (  url != "Admin") {
    // console.log(roleBullet1);
    // console.log(roleBullet2);
    roleBullet1.style.display = 'none';
    roleBullet2.style.display = 'none';
    roleBullet3.style.display = 'none';
}


async function getFormsForApproval() {
    const hyD = window.localStorage.getItem("userId");

    const formUrl = `https://localhost:5001/Form/GetFormsByApproval/${hyD}`;
    console.log("hyd", {hyD});
    try {
        let res = await fetch(formUrl);
        console.log(res);
        return await res.json(); 
    } catch (error) {
        console.log(error);
    }
}
async function getCount() {
    let formss = await getFormsForApproval();
     console.log(formss);
let count = 0;
formss.data.forEach(form => {
    if (form.status == "In Progress") {
        console.log("count", form)
     count++;
     console.log(count);
    }
})
approvalCount.textContent = `${count}`;  
}
getCount();

async function getmyRequests() {
    const hyD = window.localStorage.getItem("userId");
    const formUrl = `https://localhost:5001/Form/GetFormsByUser/${hyD}`;
    try {
        let res = await fetch(formUrl);
        console.log(res);
        return await res.json(); 
    } catch (error) {
        console.log(error);
    }
}

async function getRequestCount() {
    let formss = await getmyRequests();
    // console.log(formss);
let count = 0;
formss.data.forEach(form => {
    console.log("Request count", form)
     count++;
     console.log(count);
})
requestCount.textContent = `${count}`;  
}
getRequestCount();


async function getUsers() {
     try {
         let res = await fetch("https://localhost:5001/apiUser/GetAllUsers");
         console.log(res);
         let resJson = await res.json();
         console.log(resJson);
         return resJson;
    } catch (error) {
         console.log(error);
     }
 }

 async function renderUsers() {
    console.log("i am here");
     let users = await getUsers();
     console.log("users", users)
     let i = 1;
     let tableBody = document.createElement("table");
     let tableHead = document.createElement("thead");
     tableBody.appendChild(tableHead);
    users.data.forEach(user => {
        tableHead.innerHTML +=`
    <tr>
    <td>${i}</td>
    <td>${user.firstName} ${user.lastName}</td>
    <td>${user.email}</td>
    </tr>`
    i++;
     });

     let container = document.querySelector('.container');
     
     container.appendChild(tableBody);
 }

  //renderUsers();













 

 






