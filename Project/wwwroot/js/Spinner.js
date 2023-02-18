document.getElementById("send-loading").style.display = "none";

var replyError = document.getElementById("reply_error");

function Reply() {
    var reply = document.getElementById("reply_input").value;
    if (reply == "") {
        replyError.innerHTML = "Please reply customer";
        return false;
    }
    replyError.innerHTML = "";
    return true;
}
function Loading() {
    if (!Reply()) {
        return false;
    } else {
        document.getElementById("send-loading").style.display = "block";
       
        setTimeout(() => {
            document.getElementById("send-loading").style.display = "none";
          
        }, 5000)
    }
   
}