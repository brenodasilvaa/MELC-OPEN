
( function (melc, $, escopo) {

    if (!melc.clientes) { melc.clientes = {} }

    melc.clientes = function () {
        return {
            iniciar: function () {
                escopo = melc.clientes
            },
            getPedidos: function (id) {
                showSpinner();
                window.location.assign(`Pedidos/${id}`)
            },
            excluirCliente: function (id, clienteNome) {
                customConfirm(`Você deseja excluir o cliente <b>${clienteNome}</b>?`,
                    escopo.confirmDeleteCliente,
                    id,
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            confirmDeleteCliente: function (e) {
                $.ajax({
                    method: "DELETE",
                    url: `Clientes/Delete/${e.data}`,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.data)
                            return
                        }

                        if (data.success) {
                            window.location.reload();
                            return
                        }

                        document.write(data);
                    }

                })
            },
            editarCliente: function () {
                window.location.assign(`Clientes/Update/${id}`)
            },
            novoCliente: function () {
                $(novoCliente).modal('show')
            },
            criarNovoCliente: function () {
                $(novoCliente).modal('hide')
                showSpinner();

                let formData = new FormData();

                formData.append("Nome", $('#empresa').val())
                formData.append("Cnpj", $('#cnpj').val())
                formData.append("Rua", $('#rua').val())
                formData.append("Numero", $('#numero').val())
                formData.append("Cidade", $('#cidade').val())

                $.ajax({
                    type: 'POST',
                    url: "/Clientes/Create",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.data)
                            return
                        }

                        window.location.reload();
                    }
                })
            },
            editarCliente: function (id) {

                $.ajax({
                    type: 'GET',
                    url: `/Clientes/Get/${id}`,
                    success: function (data) {

                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        $('#update-empresa').val(data.data.nome)
                        $('#update-cnpj').val(data.data.cnpj.replace(/\D/g, ''));
                        $('#update-rua').val(data.data.endereco.rua)
                        $('#update-numero').val(data.data.endereco.numero)
                        $('#update-cidade').val(data.data.endereco.cidade)
                        $('#update-cliente-id').val(id)

                        $(updateCliente).modal('show')
                    }
                })
            },
            updateCliente: function () {
                $(updateCliente).modal('hide')
                showSpinner();

                let formData = new FormData();

                formData.append("Id", $('#update-cliente-id').val())
                formData.append("Nome", $('#update-empresa').val())
                formData.append("Cnpj", $('#update-cnpj').val())
                formData.append("Rua", $('#update-rua').val())
                formData.append("Numero", $('#update-numero').val())
                formData.append("Cidade", $('#update-cidade').val())

                $.ajax({
                    type: 'POST',
                    url: "/Clientes/Update",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.data)
                            return
                        }

                        window.location.reload();
                    }
                })
            }
        }
    }()

})(window.melc = window.melc || { _isNamespace: true }, jQuery);