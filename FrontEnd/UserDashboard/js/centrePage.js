let centList = document.querySelector('.centreList');

async function getCenters(){
    try {
        let res = await fetch("https://localhost:5001/ResponsibilityCentre/GetCentres");
        let returnvalue = await res.json();
        console.log(returnvalue);
        return returnvalue;
    } catch (error) {
        console.log(error)
        
    }
}

 async function getApprovalsInCent(centreId){
    try {
        let res =  await fetch(`https://localhost:5001/Approval/GetApprovalsInCentre/${centreId}`);
        let app = await res.json();
        return app;
    } catch (error) {
        console.log(error)
    }
}

 


async function centresWtApprovals() {
    let existingCentres = await getCenters();
    
    console.log(existingCentres);

    
    let num = 1;
   
    existingCentres.data.forEach( async(elem) =>  {
        let approvals = await  getApprovalsInCent(elem.id);
        console.log(approvals);
        let approvalList = document.createElement("ol");
        approvalList.className = "approval_list";

        centList.innerHTML += `<h3>${num}. ${elem.name}</h3>
        <p>${elem.description}</p>`;

        
        approvals.data.forEach(app =>{
            
            approvalList.innerHTML += `<li>${app.approvalName} (${app.approvalRole})</li>`
        });


        centList.appendChild(approvalList);
       num++;
    });
}
// async function createList(centId) {
//     let approvalList = document.createElement("ol");
//     approvalList.className = "approval_list";


//     let approvals = await  getApprovalsInCent(centId);
//         console.log(approvals);

//         approvals.data.forEach(app =>{
            
//             approvalList.innerHTML += `<li>${app.approvalName}</li>`
//         });
//         console.log(approvalList);
//         return approvalList;
// }
centresWtApprovals()