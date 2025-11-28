const emailInput = document.getElementById("email");
const passwordInput = document.getElementById("password");
const confirmpasswordInput = document.getElementById("confirmpassword");

const errorMsg = document.getElementById("errorMsg");

const submitBtn = document.getElementById("submitBtn");


submitBtn.onclick = function(ev){
    ev.preventDefault();
    // if(passwordInput.value == confirmpasswordInput.value){
    //     console.log("Parollar eynidir");
    // }
    let isValid = true;
    errorMsg.innerText = "";
    if(passwordInput.value != confirmpasswordInput.value){
        errorMsg.innerText = "Parollar eyni deyil!";
        isValid = false;
    }


    if(passwordInput.value.length < 6){
        errorMsg.innerText += "Parol-un uzunlugu 6dan boyuk olmalidir\n";
        isValid = false;
    }
    if(!passwordInput.value.includes("!")){
        errorMsg.innerText += "Parol-da ! isaresi olmalidir\n";
        isValid = false;
    }

    if(isValid){
        //
    }
}