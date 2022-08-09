const roleForm = document.querySelector("#roleForm");
roleForm.addEventListener('submit', (e) => {
    e.preventDefault();
})

const roleTiltle = document.getElementById("role-title");
const roleDescription = document.getElementById("descr");

function CreateRole(){
    
    Data = {
        name: roleTiltle.value,
        description: roleDescription.value
    };
    console.log("processing")

    fetch('https://localhost:5001/Role/CreateRole', {
    method : 'POST',
    headers: {
        "content-type": "application/json"
    },
    body:JSON.stringify(Data)
    
}).then((res) => res.json())
.then(response => {
    console.log(response);
    alert(response.message);
})
.catch(err => {
    console.log(err);
    console.log("3");
    })
}

