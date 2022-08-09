

async function getFormsForApproval() {
    const hyD = window.localStorage.getItem("userId");
    const formUrl = `https://localhost:5001/Form/GetFormsByApproval/${hyD}`;
    try {
        let res = await fetch(formUrl);
        console.log(res);
        return await res.json(); 
    } catch (error) {
        console.log(error);
    }
}

async function renderApprovalForm() {
    let forms = await getFormsForApproval();
    console.log(forms);
    let i = 1;
    let tableBody = document.querySelector('.approval_tab');
    forms.data.forEach(form =>{
        
        tableBody.innerHTML += `<tr class = "my-approvals" id = "${form.id}">
        <td>${i}</td>
        <td>${form.name}</td>
        <td>${form.respoCentreName}</td>
        <td class = "myFormId approvalStatus" id = "${form.id}")">View Details</td>
        </tr>
        `
        i++;
    })
    viewForm();
}

renderApprovalForm();

function viewForm() {
    let myUserForm = document.querySelectorAll(".myFormId");
    console.log("loading.........");
    myUserForm.forEach( x =>{
        x.addEventListener("click", (e) => {
            console.log(e.target.id);
            window.location.href = `/UserDashboard/formDetails.html?id=${e.target.id}`
        })
     }

    )
}

