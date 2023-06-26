function successMessage(message, url){
    Notiflix.Notify.success(message);
    if(url !== undefined && url !== null && url !== ''){
        setTimeout(function(){
            location.href = url;
        }, 2000);
    }
}

function errorMessage(message){
    
}

function warningMessage(message){
    
}