
//This file contains code relevant to nav bar events that can be called from anywhere - log in, register, along with certain ajax requests

//**ON LOAD FUNCTION*****
$(document).ready(function() {

    //hide error labels
    hideErrors();
    hideFormErrors();

    //hide welcome message until log in
    $("#welcomeMessage").hide();
    $("#thankYouMessage").hide();

});

function hideErrors() {
    //hide errors on load until needed
    $("#firstNameError").hide();
    $("#lastNameError").hide();
    $("#emailError").hide();
    $("#passError").hide();
    $("#passError2").hide();
    $("#checkboxError").hide();
    $("#logInError").hide();
    $("#clientLogInError").hide();
    $("#deleteBtn").hide();
    $("#alreadRegisteredError").hide();
}

function clearRegisterForm() {
    //clears form if closed and not completed
    $("#firstName").val("");
    $("#lastName").val("");
    $("#registrationEmail").val("");
    $("#registrationPass").val("");
    $("#registrationPass2").val("");
    $("#checkbox").prop("checked", false);
}

//close button reset
$(document).on("click", "#closeBtnRegister", function() {
    hideErrors();
});

$(document).on("click", "#closeBtnLogIn", function() {
    $("#logInError").hide();
    let logInEmail = $("#logInEmail").val("");
    let logInPass = $("#logInPass").val("");
});


//*******REGISTER****** log in
$(document).on("click", "#submitBtnRegister", function() {
    hideErrors();
    //grab values
    let firstName = $("#firstName").val();
    let lastName = $("#lastName").val();
    let email = $("#registrationEmail").val();
    let pass1 = $("#registrationPass").val();
    let pass2 = $("#registrationPass2").val();

    if ((validateName(firstName, lastName) && validateEmail(email)) && (passValidate(pass1, pass2) && checkChecked()) && notAlreadyRegistered(email)) { //entries are valid and email is not already registered

        sendMemberToDatabase(firstName, lastName, email, pass1);
        //reset register form
        clearRegisterForm()

        //hide errors again
        hideErrors();

        loggingIn();

        //hide modal and register link
        $("#registerModal").modal("hide");
        $("#register").hide();

        //show confirmation Modal and populate
        $("#registrationConfirmationModal").modal("toggle");S
 
    } else {
        //do nothing
    }
});

function notAlreadyRegistered(inputEmail) {
    userbase = JSON.parse(localStorage.getItem("userbase"));
    var output = true;
    $.each(userbase, function (email, pass) {
        if (inputEmail === email) {
            $("#alreadRegisteredError").show(); //show error if email already used in registered account
            output = false;
        } 
    });
    return output;
}

function sendMemberToDatabase(firstName, lastName, email, pass) {;
    var json;
    var member = new Object();
    member.firstName = firstName;
    member.lastName = lastName;
    member.email = email;
    member.password = pass;
    //store entered values in a member object

    localStorage.setItem("currentUser", JSON.stringify(member.email));
    localStorage.setItem("loggedIn", JSON.stringify(true));
    json = JSON.stringify(member);

    $.ajax({
        type: "POST",
        url: "/User/Register",
        data: json,
        contentType: "application/json; charset=utf-8"
    }); //send member object to controller 
}

//****VALIDATION FUNCTIONS*******
function validateName(firstName, lastName) {

    var valid = true;
    //check if fields are empty
    if (firstName == "") {
        $("#firstNameError").show();
        $("#firstNameError").text("Please enter a first name");
        valid = false;
    } else {
        //if not check if chars are valid
        if (!/^[a-zA-Z]*$/g.test(firstName)) {
            $("#firstNameError").show();
            $("#firstNameError").text("Please enter valid letters only");
            valid = false;
        }
    }

    if (lastName == "") {
        $("#lastNameError").show();
        $("#lastNameError").text("Please enter a last name");
        valid = false;
    } else {
        if (!/^[a-zA-Z]*$/g.test(lastName)) {
            $("#lastNameError").show();
            $("#lastNameError").text("Please enter valid letters only");
            valid = false;
        }
    }
    return valid;
}


function validateEmail(email) {
    //check if email enteres is in correct email format
    var format = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    if (format.test(email)) {
        return true;
    } else {
        $("#emailError").show();
        $("#emailError").text("Please enter a valid email address");
        return false;
    }
}

function passValidate(pass1, pass2) {
    let valid = true;
    if (pass1.length < 1) {
        $("#passError").show();
        $("#passError").text("Please enter a password");
        valid = false;
    }

    if (pass2.length < 1) {
        $("#passError2").show();
        $("#passError2").text("Please enter a password");
        valid = false;
    }

    if (pass1.length > 0 && pass2.length > 0) {
        if (passwordMatch(pass1, pass2)) {
            //do nothing and valid stays true if still true
        } else {
            valid = false;
        }
    }

    return valid;
}

function passwordMatch(pass1, pass2) {
    if (pass1 === pass2) {
        return true;
    } else {
        $("#passError").show();
        $("#passError2").show();
        $("#passError").text("Passwords must match");
        $("#passError2").text("Passwords must match");
        return false;
    }
}

function checkChecked() {
    if ($("#checkbox").is(":checked")) {
        return true;
    } else {
        return false;
    }
}

//SCRIPT FOR CAUSE VIEW
function clearCauseForm() {
    $("#causeTitle").val("");
    $("#causeDescription").val("");
    $("#memberAction").val("1"); //reset cause form after use
}

function hideFormErrors() {
    $("#causeDescriptionError").hide();
    $("#causeTitleError").hide();
}

//***CAUSE FORM CREATION BUTTONS****
//buttons that tailor the category of new causes depending on what one is clicked
$(document).on("click", "#taxAvoidanceImg", function() {
    $("#category").text("Tax Avoidance");
    startNewCauseAttempt();
});

$(document).on("click", "#discriminationImg", function() {
    $("#category").text("Discrimination");
    startNewCauseAttempt();
});

$(document).on("click", "#employmentImg", function() {
    $("#category").text("Employment Practices");
    startNewCauseAttempt();
});

$(document).on("click", "#environmentalImg", function() {
    $("#category").text("Environmental Impact");
    startNewCauseAttempt();
});

$(document).on("click", "#otherImg", function() {
    $("#category").text("Other");
    startNewCauseAttempt();
});


function startNewCauseAttempt() {
    if (JSON.parse(localStorage.getItem("loggedIn"))) {
        openNewCauseForm();
    } else {
        $("#logInModal").modal("toggle");
    }
}

function openNewCauseForm() {
    $("#causeFormModal").modal("toggle");
}

//*****CAUSE SUBMIT BUTTON****
$(document).on("click", "#submitBtnCause", function() {
    hideFormErrors();
    let title = $("#causeTitle").val();
    let description = $("#causeDescription").val();
    //validate inputs
    if (validateTitle(title) && validateDescription(description)) { //only need to validate title and description entry

        sendCauseToDatabase();//send to controller for db storage
        clearCauseForm();
        hideErrors();

        //hide modal
        $("#causeFormModal").modal("hide");

        //display and populate confirmation modal
        $("#confirmationModal").modal("toggle");


    }
});


function sendCauseToDatabase() {
    var json;
    var cause = new Object();
    var member = new Object();
    member.email = JSON.parse(localStorage.getItem("currentUser"));
    
    cause.title = $("#causeTitle").val();
    cause.description = $("#causeDescription").val();
    cause.category = $("#category").text();
    cause.action = $("#memberAction option:selected").text();
    cause.signatureCount = 0; //set to 0 as default
    cause.image = $("#imageFile").val();
    cause.causeOwner = member;
    //save entered details in new object

    json = JSON.stringify(cause);

    $.ajax({
        type: "POST",
        url: "/Cause/Upload",
        data: json,
        contentType: "application/json; charset=utf-8"
    }); //send object to controller for storage
}

    function validateTitle(title) {
        var valid = true;
        //check if fields are empty
        if (title == "") {
            $("#causeTitleError").show();
            $("#causeTitleError").text("Please enter a title");
            valid = false;
        } else {
            //do nothing
        }

        return valid;
    }

    function validateDescription(description) {
        var valid = true;
        //check if fields are empty
        if (description == "") {
            $("#causeDescriptionError").show();
            $("#causeDescriptionError").text("Please enter a description");
            valid = false;
        } else {
            //do nothing
        }
        return valid;
    }


