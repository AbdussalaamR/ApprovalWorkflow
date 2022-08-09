const myCentreForm = document.querySelector(".centreForm");

const Cname = document.querySelector('#centreName');
const Cdescription = document.querySelector('#centre');

myCentreForm.addEventListener('submit', (e) =>{
    e.preventDefault();
});

function CreateCentre() {
    const formData = new FormData(myCentreForm);
    
     const data =  {
        name : Cname.value,
        description: Cdescription.value
     };
    console.log(data);

    fetch('https://localhost:5001/ResponsibilityCentre/CreateCentre',{
        method: 'POST',
        body: formData

    }).then((res) => res.json())
    .then(data => alert(data.message)

    )
}