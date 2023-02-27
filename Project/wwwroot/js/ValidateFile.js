var validFile = ["jpg", "png", "jpeg", "gif"];
function ValidateFile() {
    var file = document.getElementById("file").value;
    var fileError = document.getElementById("img_error");
    var extension = file.substring(file.lastIndexOf(".") + 1, file.length).toLowerCase();
    var isvalidFile = false;
    for (var i = 0; i < validFile.length; i++) {
        if (extension == validFile[i]) {
            isvalidFile = true;
            break;
        }
    }
    if (!isvalidFile) {
        fileError.innerHTML = "Invalid file. Please upload a file with extension: " + validFile.join(", ");
    }
    return isvalidFile;
}