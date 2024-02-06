
( function (melc, $, escopo) {

    if (!melc.usuarios) { melc.usuarios = {} }

    melc.usuarios = function () {
        return {
            iniciar: function () {
                escopo = melc.usuarios
            },
            deleteUser: function(id, email) {
                customConfirm(`Você deseja excluir o usuário <b>${email}</b>?`,
                    escopo.confirmDeleteUser,
                    id,
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            update: function (id) {
                $.ajax({
                    type: 'GET',
                    url: `/User/Get/${id}`,
                    success: function (data) {

                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }
                        $('#updateUserId').val(id)
                        $('#editFullName').val(data.data.fullName)
                        $('#editUserName').val(data.data.userName)
                        $('#editIsAdmin').prop('checked', data.data.isAdmin);
                        $("#editUser").modal("show");
                    }
                })
            },
            updateUser: function () {
                $("#editUser").modal("hide");
                showSpinner();
                let formData = new FormData();
                formData.append("Id", $('#updateUserId').val())
                formData.append("UserName", $('#editUserName').val())
                formData.append("FullName", $('#editFullName').val())
                formData.append("IsAdmin", $('#editIsAdmin').is(":checked"))

                $.ajax({
                    url: "/User/Update",
                    type: 'POST',
                    data: formData,
                    success: function (response) {
                        if (response.success) {
                            updateAplicacao();
                        }
                        else {

                            customAlert(response.message)
                        }

                    },
                    contentType: false,
                    processData: false
                })

                function updateAplicacao() {
                    $.ajax({
                        url: "/User/UpdateAplicacao",
                        type: 'POST',
                        data: formData,
                        success: function (response) {
                            hideSpinner();
                            if (response.success) {
                                window.location.reload();
                            }
                            else {
                                customAlert(response.message)
                            }

                        },
                        contentType: false,
                        processData: false
                    })
                }
            },
            confirmDeleteUser: function (e) {
                debugger
                $.ajax({
                    method:"DELETE",
                    url: "/User/Delete",
                    data: { id: e.data },
                    success: function (response) {
                        if (response.success)
                            window.location.reload();
                        else
                            customConfirm(response.Message)
                    }
                })
            },
            newUser: function () {
                $(novoUser).modal('show')
            },
            inserirUsuario: function () {
                $(novoUser).modal('hide')
                showSpinner();
                let formData = new FormData();
                formData.append("UserName", $('#userName').val())
                formData.append("FullName", $('#fullName').val())
                formData.append("Senha", $('#senha').val())
                formData.append("SenhaConfirmacao", $('#senhaConfirmacao').val())
                formData.append("IsAdmin", $('#isAdmin').is(":checked"))

                $.ajax({
                    url: "/User/Register",
                    type: 'POST',
                    data: formData,
                    success: function (response) {
                        hideSpinner();
                        if (response.success) {
                            registrarAplicacao();
                            window.location.reload();
                        }
                        else {
                            
                            customAlert(response.message)
                        }

                    },
                    contentType: false,
                    processData: false
                })

                function registrarAplicacao() {
                    $.ajax({
                        url: "/User/RegisterAplicacao",
                        type: 'POST',
                        data: formData,
                        success: function (response) {
                            hideSpinner();
                            if (response.success) {
                                window.location.reload();
                            }
                            else {
                                customAlert(response.message)
                            }

                        },
                        contentType: false,
                        processData: false
                    })
                }
            }
        }
    }()

})(window.melc = window.melc || { _isNamespace: true }, jQuery);