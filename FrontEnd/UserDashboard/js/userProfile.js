const firstName = document.getElementById("fName");
const lastName = document.getElementById("lName");
const eMail = document.getElementById("email");
const roles = document.getElementById("role");


async function GetUser() {
    const hyD = window.localStorage.getItem("userId");
    console.log(hyD);
    const formUrl = `https://localhost:5001/apiUser/GetUser/${hyD}`;
    try {
        let res = await fetch(formUrl);
        console.log(res);
        const useR = await res.json();
        console.log(useR);
        return useR; 
    } catch (error) {
        console.log(error);
    }
}

async function renderUser() {
    let userProfile = await GetUser();
    console.log(userProfile);
    firstName.textContent = `${userProfile.firstName}`;
    lastName.textContent = `${userProfile.lastName}`;
    eMail.textContent = `${userProfile.email}`
    userProfile.userRoles.forEach(element => {
        roles.innerHTML += `
        <li>${element.name}</li>
        `
    });

}
renderUser();