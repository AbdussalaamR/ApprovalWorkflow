async function getCentres(){
    try {
        let res = await fetch("https://localhost:5001/ResponsibilityCentre/GetCentres");
        console.log(res);
        return res.json();
    } catch (error) {
        console.log(error)
    }
}


async function showCentres(){
    let centres = await getCentres();
    console.log(centres);
    let selectCentre = document.querySelector("#centre");
    
centres.data.forEach(element => {
    selectCentre.innerHTML += `<option value="${element.id}">${element.name}</option>`
    console.log(element.id);
    
});

}

showCentres()

