async function getDCentres(){
    try {
        let res = await fetch("https://localhost:5001/ResponsibilityCentre/GetCentres");
        console.log(res);
        return res.json();
    } catch (error) {
        console.log(error)
    }
}


async function showDCentres(){
    let centres = await getDCentres();
    console.log(centres);
    
    let appCentre = document.querySelector("#Appcentre");
centres.data.forEach(element => {
    
    appCentre.innerHTML += `<option value=${element.id}>${element.name}</option>`
    console.log(element.id);
});

}

showDCentres()


const approvalMail = document.getElementById("approverMail");
const approvlRole = document.getElementById("approverRole");
const sequencePosition = document.getElementById("sequenceNum");
const centreId = document.getElementById("Appcentre");

var approvalForm = document.querySelector('.approvalForm');
approvalForm.addEventListener('submit', (e)=>{
    e.preventDefault();
});

function createApproval() {
    Data = {
        sequence: sequencePosition.value,
        email: approvalMail.value,
        approvalRole: approvlRole.value,
        responsibilityCentreId: centreId.value
    };
    console.log(Data);
    console.log("processiiiiiiiing");
    
    fetch('https://localhost:5001/Approval/Create', {
        method: 'POST',
        headers: {
            "content-type": "application/json"
        },
        body:JSON.stringify(Data)

    }).then((res) => res.json())
    .then(resp => {
        console.log("Done");
        alert(resp.message);
    })
    
}