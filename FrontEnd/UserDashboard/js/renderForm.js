var loggedInUserId = window.localStorage.getItem("userId");


const urlId = window.location.href.split('=')[1];

const FORMURL = `https://localhost:5001/Form/GetForm/${urlId}`;

const formElement = document.querySelector('#form-element');

const heading = document.getElementById("formTitle");
const description = document.getElementById("formDesc")

const getForm = async () => {

    const { data: form } = await (await fetch(FORMURL)).json();
    console.log(form)
    return form;

}

const generateFormContainer = (form) => {

    // const formContainer = document.createElement('div');

    // formContainer.classList.add("form-container");
    // const heading = `<h4>${form.name}<h4>`
    // const description = `<h4>${form.description}<h4>`

    heading.textContent = `${form.name}`

    description.textContent = `${form.description}`

    // formContainer.innerHTML = heading;
    // formContainer.innerHTML += description;

    // return formContainer;

}

const generateFormInputs = (form) => {

    const userForm = document.createElement('form');
    userForm.classList.add('user-form');
    userForm.id = "userForm";
    userForm.innerHTML = ` <input class="form-control" type="hidden" name = "userId" value = "${loggedInUserId}"/>
                            <input class="form-control" type="hidden" name = "formId" value = "${urlId}"/>`

    const inputContainer = document.createElement('div');

    inputContainer.classList.add('input-container');

    const { questionFieldForm: formInputs } = form;
    let i = 0;
    formInputs.forEach(formInput => {
        i++;
        let formInputContainer = document.createElement('div');
        formInputContainer.classList.add("mb-3")
        let input = document.createElement('input');
        
        formInputContainer.innerHTML += `<label for=${formInput.fieldName} class="form-label"> ${formInput.fieldName} </label>
        <input class="form-control" type="hidden" name = "fieldQue" value = "${formInput.fieldName}"/>`
            console.log(formInput.inputType)
        switch (formInput.inputType) {
            case 1:
                input = `<input class="form-control" type='text' id="hi"  name = "response" placeholder=${formInput.defaultVale}  />`
                formInputContainer.innerHTML += input
                break;
            case 2:
             input = `<input class="form-control" type='number' id=${formInput.fieldName}  name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break; 
            case 3:
             input = `<input class="form-control" type='email' id=${formInput.fieldName} name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break;
            case 4:
             input = `<input class="form-control" type='radio' id=${formInput.fieldName} name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break;
            case 5:
             input = `<input class="form-control" type='checkbox' id=${formInput.fieldName} name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break;
            case 6:
            input = `<input class="form-control" type='date' id=${formInput.fieldName} name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break;
            case 7:
            input = `<input class="form-control" type='tel' id=${formInput.fieldName} name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break;
            case 8:
            input = `<input class="form-control" type='month' id=${formInput.fieldName} name = "response" placeholder=${formInput.defaultVale} />`
                formInputContainer.innerHTML += input
                break;
    
        }
     

        inputContainer.appendChild(formInputContainer);
        

    })

    userForm.appendChild(inputContainer);
    userForm.innerHTML+=`<label class="form-label">Attach document(s)</label>
    <label class="form-label">File Description</label>
    
    <input type="text" name="fileDescription"/>
    <input type="file" name="UplodedDocs" multiple/><br><br>
    <div class="d-grid"><button class="btn btn-primary" id="submitButton" type="submit">Submit</button></div>`
        return userForm;
    //return inputContainer;
}


const renderForm = async () => {
    const form = await getForm();

    console.log(form)
    generateFormContainer(form);
    // const formContainer = generateFormContainer(form);

    const formInputs = generateFormInputs(form);
    // console.log(formInputs);

    // formElement.appendChild(formContainer);
    formElement.appendChild(formInputs);
    console.log(formElement);
    sendFilledFormToBackEnd()
}

renderForm();




function sendFilledFormToBackEnd()
  {

    const fTag = document.querySelector("#userForm");
 console.log(fTag);
 fTag.addEventListener('submit', (e) =>{
  e.preventDefault();
  SendForm()
  });
    // console.log(fTag)

    
  }

  function SendForm()
  {
    
    const filledForm = document.querySelector("#userForm");
    console.log(filledForm);
    //const id = document.querySelector("#hi");
    const data = new FormData(filledForm);
    //console.log(id.value);
    fetch('https://localhost:5001/Form/FillForm',{
        method : 'POST',
        body : data,

    }).then((res) => res.json())
    .then(resp => {
        console.log(resp);
        alert(resp.message); 
    })
  }
 


