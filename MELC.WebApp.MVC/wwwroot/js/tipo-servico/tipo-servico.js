
( function (melc, $, escopo) {

    if (!melc.tipoServico) { melc.tipoServico = {} }

    _model = Object;

    melc.tipoServico = function () {
        return {
            iniciar: function () {
                escopo = melc.desenhoDetalhe
            },
            new: function () {
                $("#novoTipoServico").modal("show");
            },
            inserir: function () {
                $(novoTipoServico).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("Servico", $('#servico').val())
                formData.append("Valor", $('#preco').val())

                $.ajax({
                    type: 'POST',
                    url: "/TipoServico/Create",
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
                    url: `/TipoServico/Get/${id}`,
                    success: function (data) {

                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        $('#updateServico').val(data.data.servico)
                        $('#updatePreco').val(data.data.valor.toLocaleString('pt-BR'))
                        $('#updateTipoServicoId').val(id)
                        $("#updateTipoServico").modal("show");
                    }
                })
            },
            atualizar: function () {
                $(updateTipoServico).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("Id", $('#updateTipoServicoId').val())
                formData.append("Servico", $('#updateServico').val())
                formData.append("Valor", $('#updatePreco').val())

                $.ajax({
                    type: 'POST',
                    url: "/TipoServico/Update",
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