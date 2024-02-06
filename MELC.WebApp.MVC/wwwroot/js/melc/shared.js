

function customConfirm(message, yesCallBack, dataItem, noCallBack) {
    $(dlgConfirmMessage).html('<div class="confirm">' + message + '</div>')
    
    const btnNo = $('#btnNo')
    const btnYes = $('#btnYes')
    if (noCallBack) {
        btnNo.unbind('click')
        btnNo.on('click', null, dataItem, noCallBack)
    }

    btnYes.unbind('click')
    btnYes.on('click', null, dataItem, yesCallBack)

    $('#staticBackdrop').modal('show')
}

function customAlert(message) {

    let returnMessage = "";
    
    if (Array.isArray(message)) {
        let ul = "<ul>"

        for (var i = 0; i < message.length; i++) {
            ul += `<li> ${message[i]} </li>`
        }

        ul += "</ul>"

        returnMessage = ul;
    }
    else {
        returnMessage = message;
    }
    

    $(dlgAlert).html('<div role"alert" class="alert alert-danger">' + returnMessage + '</div>')
    $('#staticAlert').modal('show')
}

function showSpinner() {
    $('#staticSpinner').modal('show')
}

function hideSpinner() {
    $('#staticSpinner').modal('hide')
}
