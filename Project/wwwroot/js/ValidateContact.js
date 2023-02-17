var emailError = document.getElementById("email_error");
var firstnameError = document.getElementById("firstname_error");
var lastnameError = document.getElementById("lastname_error");
var phoneError = document.getElementById("phone_error");
var commentsError = document.getElementById("comments_error");

function FirstName() {
    var firstName = document.getElementById("first_name").value;
    if (firstName.length == 0) {
        firstnameError.innerHTML = "Please enter first name";
        return false;
    }

    firstnameError.innerHTML = '';
    return true;
}
function LastName() {
    var lastName = document.getElementById("last_name").value;
    if (lastName.length == 0) {
        lastnameError.innerHTML = "Please enter last name";
        return false;
    }

    lastnameError.innerHTML = '';
    return true;
}
function Email() {
    var email = document.getElementById("email").value;
    if (email.length == 0) {
        emailError.innerHTML = "Please enter email";
        return false;
    }
    if (!email.match(/^\w+[@]\w+[.]\w{3}$/)) {
        emailError.innerHTML = "Please enter a valid email";
        return false;
    }
    emailError.innerHTML = '';
    return true;
}
function Phone() {
    var phone = document.getElementById("phone").value;
    if (phone.length == 0) {
        phoneError.innerHTML = "Please enter phone";
        return false;
    }
    if (isNaN(phone)) {
        phoneError.innerHTML = "Phone must be number";
        return false;
    }
    if (phone.length < 10 || phone.length > 10) {
        phoneError.innerHTML = "Phone must have 10 digits";
        return false;
    }
    phoneError.innerHTML = '';
    return true;
}
function Comments() {
    var comments = document.getElementById("comments").value;
    if (comments.length == 0) {
        commentsError.innerHTML = "Please enter comments";
        return false;
    }

    commentsError.innerHTML = '';
    return true;
}
function Send() {
    if (!FirstName() || !LastName() || !Email() || !Phone() || !Comments()) {
        return false;
    }
}