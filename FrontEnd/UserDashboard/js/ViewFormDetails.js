var heading = document.querySelector(".formTitle");
//var description = document.querySelector(".formDescription");
var details = document.querySelector(".formInfo");
const decisionInput = document.querySelector('#decision');
    decisionInput.innerHTML = `<option value="1">Approve</option>
    <option value="2">Reject</option>
    <option value="3">Revise</option>
    `


const urlHyd = window.location.href.split('=')[1];
console.log("urlid", urlHyd);
const FORMURL = `https://localhost:5001/Form/GetUserForm/${urlHyd}`;


const getForm = async () => {

    const { data: form } = await (await fetch(FORMURL)).json();
    console.log(form)
    return form;
    

}


const viewFormDetails = async () => {
    const form = await getForm();
    console.log(form)
    heading.innerHTML = `${form.name}`;

    let formContainer = document.createElement('div');

    form.questionFieldUserForm.forEach(element => {
        
        
        formContainer.innerHTML += `<h4>${element.fieldQue}: </h4>
        <p>${element.response}</p>`
    });
    if (form.uplodedDocs.Count == 0)
    {
        formContainer.innerHTML += `<caption>No attached file</caption>`
    }
    formContainer.innerHTML += `<caption>Attached document(s) by Applicant</caption>`
    const myTable = document.createElement("div");
    const commentTable = document.createElement("div");
    myTable.className = "form-floating mb-3";
    const table1 = document.createElement("table");
    table1.className = "table-bordered table-striped";
    table1.innerHTML =
        `<thead>
             <tr>
                <th>#</th>
            
                <th>Description</th>
                <th>File Type</th>
                <th>Action</th>
            </tr>
        </thead>`
;

        let i = 1;
    form.uplodedDocs.forEach(element => {
        
        //formContainer.classList.add("mb-3") 
        table1.innerHTML += `<tbody>
        <tr>
        <th>${i}</th>
        <td>${element.description}</td>
        <td>${element.extension}</td>
        
        <td>
            <a type="button" class="btn btn-primary" id="${element.id}" onclick="DownloadDoc(${element.id})">Download</a>
        </td>
    </tr>
</tbody>
`
i++;
    });
    myTable.appendChild(table1);


    formContainer.appendChild(myTable);
    details.appendChild(formContainer);

    

}
viewFormDetails();





    async function DownloadDoc(elem) {
        const formUrl = `https://localhost:5001/Form/DownloadFileFromDatabase/${elem}`;
        console.log(elem);;
        await fetch(formUrl)
        .then(response => response.blob())
        .then(blob => {
        console.log("blob", blob);
            var  url = URL.createObjectURL(blob);
            console.log("url", url);
            var link = document.getElementById(elem);
            link.href = url;
            document.body.appendChild(link);
            link.click();
        document.body.removeChild(link);
        URL.revokeObjectURL(url);
        })
        
    }

    const approverForm = document.querySelector(".approver-Form");
    approverForm.addEventListener('submit', (e) =>{
          e.preventDefault();

          submitApprovedForm()
        });
        
        function submitApprovedForm()
          {
            console.log(approverForm)
            const data = new FormData(approverForm);
            data.append("formId", urlHyd)
            console.log(data.entries( ));
            fetch('https://localhost:5001/Form/ApproveForm',{
                method : 'POST',
                body : data,
        
            }).then((res) => res.json())
            .then(resp => console.log(resp))
          }
    