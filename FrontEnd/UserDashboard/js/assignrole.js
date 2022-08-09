async function getRoles() {
    try {
        let res = await fetch("https://localhost:5001/Role/GetAll");
        console.log(res)
        return await res.json();
    } catch (error) {
        console.log(error);
    }
}

async function renderRoles() {
    let users = await getRoles();
    console.log(users)
    let html = document.querySelector("#role-descr");
    users.data.forEach(role => {
    html.innerHTML += `<option value="${role.id}">${role.name}</option>`;

    });

}

renderRoles();

var roleForm = document.querySelector("#role-form");
var staffEmail = document.querySelector("#roleTitle");
var roleId = document.querySelector("#role-descr");
roleForm.addEventListener("submit", (e)=>{
    e.preventDefault();
});


async function assignRole() {
console.log("coming.........")
    Data ={
        email: staffEmail.value,
        roleId: roleId.value
    }

    fetch('https://localhost:5001/apiUser/AssignRoleToUser',{
        method: 'POST',
        headers:{
            "content-type": "application/json"
        },
        body:JSON.stringify(Data)
    }).then(res => res.json())
    .then(resp => {
        alert(resp.message);
    })
    .catch(err => {
        console.log(err);
        console.log("3");
        })
}