let id = 1;

function addField (){
    const mainDiv = document.createElement('div');
    mainDiv.className = "myField";
    mainDiv.id = id;

    const contentLabel = document.createElement('label')
    contentLabel.id = "form_label";
    contentLabel.innerText = "FormLabel";

    const labelInput = document.createElement('input');
    labelInput.id = "inputId"
    labelInput.name = "labelinput"

    const TypeLabel = document.createElement('label')
    TypeLabel.id = "form_abel";
    TypeLabel.innerText = "FormType";

    const typeInput = document.createElement('select');
    typeInput.id = "inputId"
    typeInput.name = "typeinpt"
    typeInput.innerHTML = `<option value="1">text</option>
    <option value="2">number</option>
    <option value="3">email</option>
    <option value="4">radio</option>
    <option value="5">checkbox</option>
    <option value="6">date</option>
    <option value="7">tel</option>
    <option value="8">month</option>
    `
    
    const defaultLabel = document.createElement('label')
    defaultLabel.id = "form_abel";
    defaultLabel.innerText = "Default value";

    const defaultInput = document.createElement('input');
    defaultInput.id = "inputId"
    defaultInput.className = "defaultClass";
    defaultInput.name = "defaultipt";


    const removeBtn = document.createElement('button')
    removeBtn.innerText = "Remove";
    removeBtn.id = id;
    removeBtn.addEventListener('click', (e) => {
        const divToRemove = document.getElementById(e.target.id);
        data.removeChild(divToRemove);
    })

    mainDiv.append(contentLabel);
    mainDiv.append(labelInput);
    mainDiv.append(TypeLabel);
    mainDiv.append(typeInput);
    mainDiv.append(defaultLabel);
    mainDiv.append(defaultInput);
    mainDiv.append(removeBtn);

    const data = document.querySelector('#data');
    data.appendChild(mainDiv);
    id++;
}






const formTitle = document.querySelector('#title');
const description = document.querySelector('#descr');
const Rcentre = document.querySelector('#centre');
const data = document.querySelector('.data');



const myadminform = document.querySelector('#adminForm');

myadminform.addEventListener('submit', (e) =>{
e.preventDefault();
});


  function sendDataToBackEnd()
  {
    const formTag = document.querySelector("#adminForm");
    console.log(formTag)
    checkDefault();
    const data = new FormData(formTag);
  
    console.log(data);
    fetch('https://localhost:5001/Form/Create',{
        method : 'POST',
        body : data,

    }).then((res) => res.json())
    .then(resp => {
      console.log(resp);
      alert(resp.message);
    })
  }
  
  function checkDefault(){
    const allDefault = document.querySelectorAll('.defaultClass')

    allDefault.forEach(elem => {
        if (elem.value === '') {
            elem.value = "null"
        }
    })
  }
    