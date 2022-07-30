const loadingSpinner = "&nbsp;<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>";
let checked = []

function toggle(source) {
    let checkboxes = document.getElementsByName('userCheckBox');
    for (let i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
        addToChecked(checkboxes[i]);
    }
}

function addToChecked(item) {
    if (item.checked) {
        checked.push(item.value)
    } else {
        let index = checked.indexOf(item.value);
        if (index !== -1) {
            checked.splice(index, 1);
        }
    }
}

function deleteUsers() {
    if (checked.length > 0) {
        $("#delete-button").append(loadingSpinner);
        $.ajax({
            url: '/Admin/DeleteUsers',
            type: 'POST',
            data: {
                userIds: checked
            },
            timeout: 10000,
            success: function () {
                location.reload();
            },
            error: function (httpObj) {
                AjaxErrorHandler(httpObj);
            }
        });
    }
}

function changeStatus(isActive) {
    if (checked.length > 0) {
        if (isActive) {
            $("#unblock-button").append(loadingSpinner);
        } else {
            $("#block-button").append(loadingSpinner);
        }
        $.ajax({
            url: '/Admin/ChangeUserStatus',
            type: 'POST',
            data: {
                userIds: checked,
                isActive: isActive
            },
            timeout: 10000,
            success: function () {
                location.reload();
            },
            error: function (httpObj) {
                AjaxErrorHandler(httpObj);
            }
        });
    }
}

function AjaxErrorHandler(httpObj) {
    if (httpObj.status === 401) {
        window.location.reload("/Identity/Account/Login");
    } else {
        ErrorModal();
    }
}

function ErrorModal() {
    $('.spinner-border').remove();
    $('#exampleModal').modal('show');
}





