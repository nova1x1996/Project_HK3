var nameError = document.getElementById("name_error");
var detailsError = document.getElementById("details_error");
var priceError = document.getElementById("price_error");



function Name() {
    let name = document.getElementById("name").value;
    if (name.length == 0) {
        nameError.innerHTML = "Name is required";
        return false;
    }

    nameError.innerHTML = '';
    return true;
}
function Details() {
    let details = document.getElementById("details").value;
    if (details.length == 0) {
        detailsError.innerHTML = "Details is required";
        return false;
    }

    detailsError.innerHTML = '';
    return true;
}
function Price() {
    let price = document.getElementById("price").value;
    if (price.length == 0) {
        priceError.innerHTML = "Price is required";
        return false;
    }else if (price <= 100 || price >= 1000) {
        priceError.innerHTML = "Price must be from 100 to 1000";
        return false;
    }
    priceError.innerHTML = '';
    return true;
}

function Update() {
    if (!Name() || !Details() || !Price()) {
        return false;
    }
}
