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
let i = 1;
async function renderRequests() {
    let forms = await getmyRequests();
    console.log(forms);
    
    let tableBody = document.querySelector('.approval_tab');
    forms.data.forEach(form =>{
        
        tableBody.innerHTML += `<tr id = "${form.id}">
        <td>${i}</td>
        <td>${form.name}</td>
        <td>${form.respoCentreName}</td>
        <td>${form.formWith}</td>
        <td id = "approvalStatus">${form.status}</td>
        </tr>
        `
        i++;
    })
    
}

renderRequests();