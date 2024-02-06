
( function (melc, $, escopo) {

    if (!melc.material) { melc.material = {} }

    _model = Object;

    melc.material = function () {
        return {
            iniciar: function () {
                escopo = melc.material
            },
            new: function () {
                $("#novoMaterial").modal("show");
            },
            inserir: function () {
                $(novoMaterial).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("Nome", $('#material').val())
                formData.append("Densidade", $('#densidade').val())
                formData.append("Preco", $('#preco').val())

                $.ajax({
                    type: 'POST',
                    url: "/Material/Create",
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
            update: function (id) {
                $.ajax({
                    type: 'GET',
                    url: `/Material/Get/${id}`,
                    success: function (data) {

                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        $('#updateMaterialNome').val(data.data.nome)
                        $('#updateDensidade').val(data.data.densidade.toLocaleString('pt-BR'))
                        $('#updatePreco').val(data.data.preco.toLocaleString('pt-BR'))
                        $('#updateMaterialId').val(id)
                        $("#updateMaterial").modal("show");
                    }
                })
            },
            atualizar: function () {
                $(updateMaterial).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("Id", $('#updateMaterialId').val())
                formData.append("Nome", $('#updateMaterialNome').val())
                formData.append("Preco", $('#updatePreco').val())
                formData.append("Densidade", $('#updateDensidade').val())

                $.ajax({
                    type: 'POST',
                    url: "/Material/Update",
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