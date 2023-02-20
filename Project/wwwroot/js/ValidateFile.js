var validFilesTypes = ["jpg", "png"];
function ValidateFile() {
    var file = document.getElementById("file").value;
   
    var fileError = document.getElementById("file_error");
   
    var ext = file.substring(file.lastIndexOf(".") + 1, file.length).toLowerCase();
    var isValidFile = false;
    for (var i = 0; i < validFilesTypes.length; i++) {
        if (ext == validFilesTypes[i]) {
            isValidFile = true;
            break; 
        }
    }
    if (file.length == 0) {
        fileError.innerHTML = "Please choose a file";
        return false;
    }
   
    if (!isValidFile) {
        fileError.innerHTML = "Invalid file. Please upload a file with extension: " + validFilesTypes.join(", ");
    }
    return isValidFile;
}