
( function (melc, $, escopo) {

    if (!melc.percentuais) { melc.percentuais = {} }

    _model = Object;

    melc.percentuais = function () {
        return {
            iniciar: function () {
                escopo = melc.percentuais;

                $('#lucros').change(function () {
                    enableBtn();
                })

                $('#impostos').change(function () {
                    enableBtn();
                })

                function enableBtn() {
                    $('#salvarPercentuais').removeAttr('disabled');
                }
            },
            new: function () {
                $("#novopercentuais").modal("show");
            },
            atualizar: function () {
                showSpinner();

                let formData = new FormData();
                formData.append("Id", $('#percentualId').val())
                formData.append("Lucro", $('#lucros').val().toLocaleString('pt-BR'))
                formData.append("Impostos", $('#impostos').val().toLocaleString('pt-BR'))

                $.ajax({
                    type: 'POST',
                    url: "/Percentuais/Update",
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