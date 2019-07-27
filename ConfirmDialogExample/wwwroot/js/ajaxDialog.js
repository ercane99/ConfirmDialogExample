////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// This file contains ajax request functions to be used when ajax call is necessary.  
// The file also contains related message dialogs(using bootbox) to be displayed as response to ajax requests
// 20190826
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to make ajax calls from the project.
// RESULT:
// If the result is successful, display success message dialog and redirect to "received url" in "json result from action"
// If the result is NOT successful, display the message dialog. Enable the sender button after user close the message dialog.
// PARAMS:
// -senderButton is the button calls the function and used for enable and disable the button
// -sendData is the data to be sent within the ajax request. It is the parameter of the action.
// -callUrl is the url for the ajax post
// -requestVerificationToken is needed to be able to reach controller action which uses ValidateAntiForgeryToken.
function postAjaxAndDisplayResultDialog(senderButton, sendData, callUrl, requestVerificationToken) {

    $.ajax({
        url: callUrl,
        type: "POST",
        data: sendData,
        dataType: "json",
        headers: { 'RequestVerificationToken': requestVerificationToken },
        beforeSend: function () {
            //$(preloader).show();//display preloader.
        },
        success: function (data) {
            //$(preloader).hide();//hide preloader.

            if (data.state === 0) {
                DisplaySuccessMessage(data.msg, data.redirectUrlInSuccess);// display result returned from method in controller (successful or failed)
            } else {
                DisplayFailureMessage(data.msg);// display result returned from method in controller (successful or failed)
                $(senderButton).attr("disabled", false);//Enable sender button 
            }
        },
        failure: function (response) {
            $(preloader).hide();//hide preloader
            DisplayFailureMessage(response);// display failure response of ajax call
            $(senderButton).attr("disabled", false);//Enable sender button
        }
    });
}


/// Display given message in a dialog which has a green check circle in title.
/// As an addition, if redirectUrl is not empty, it redirects to that page, 
/// after user closed the dialog box
function DisplaySuccessMessage(msg, redirectUrl) {
    bootbox.dialog({
        title: "<i class=\"fa fa-check-circle\" style=\"color: green;\"></i> Successful",
        message: msg,
        buttons: {
            main: {
                label: "OK",
                className: "btn-primary",
                callback: function () {
                    if (redirectUrl === undefined || redirectUrl === null || redirectUrl === "") {
                        return;
                    } else {
                        location.href = redirectUrl;//Go to redirectUrl after user closed displaying success message
                    }
                }
            }
        }
    });
}

/// Display given message in a dialog which has a red exclamation mark in title
function DisplayFailureMessage(msg) {
    bootbox.dialog({
        title: "<i class=\"fa fa-exclamation-circle\" style=\"color: red;\"></i> Failed",
        message: msg,
        buttons: {
            main: {
                label: "OK",
                className: "btn-primary"
            }
        }
    });
}
