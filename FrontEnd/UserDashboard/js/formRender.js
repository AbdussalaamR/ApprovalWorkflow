async function getForms() {
    try {
        let res = await fetch("https://localhost:5001/Form/GetForms");
        console.log(res);
        return await res.json();
    } catch (error) {
        console.log(error);
    }
}
async function renderForms() {
    let forms = await getForms();
    let formListContainer = document.querySelector('#form-element');
    let formRequest = document.createElement("ul");
    formRequest.className = "nav flex-column sub-menu";
    formRequest.id = "form_list";
    
    forms.data.forEach(form =>{
        formRequest.innerHTML += `<li class="my-forms nav-item" id="${form.id}" style="color:white">${form.name}</li>`
        console.log("formId", form.id);
        
    });
    
    formListContainer.appendChild(formRequest);
    console.log("Procssinnnnnnnnnng");
    redirect();
}



const redirect = () =>{
    let getmyForm = document.querySelectorAll(".my-forms");
    console.log("processing");
    getmyForm.forEach( x =>{
        x.addEventListener("click", (e) => {
            console.log(e.target.id);
            window.location.href = `/UserDashboard/renderForm.html?id=${e.target.id}`
        })
     }

    )
}

  





