
( function (melc, $, escopo) {

    if (!melc.desenhoDetalhe) { melc.desenhoDetalhe = {} }

    _model = Object;

    melc.desenhoDetalhe = function () {
        return {
            iniciar: function (model) {
                _model = model;
                escopo = melc.desenhoDetalhe

                $('#status').val(model.Status)
                $('#prioridade').val(model.Prioridade)

                $('#descricao').change(function () {
                    enableBtn();
                })

                $('#status').change(function () {
                    enableBtn();
                })

                $('#conjunto').change(function () {
                    enableBtn();
                })

                $('#numeroConjunto').change(function () {
                    enableBtn();
                })

                $('#quantidade').change(function () {
                    enableBtn();
                })

                $('#prioridade').change(function () {
                    enableBtn();
                })

                escopo.updateDesenhoInfo();
                escopo.btnFormatoChange();
                escopo.updateLucrosImpostos();

                function enableBtn() {
                    $('#desenhoInfoBtn').removeAttr('disabled');
                }
                
            },
            addArquivos: function (id) {
                $("#inputNewFile").click();

                $("#inputNewFile").change(function (e) {
                    showSpinner();
                    let formData = new FormData();
                    let input = $("#inputNewFile")[0];

                    formData.append("desenhoId", id)

                    for (i = 0; i < input.files.length; i++) {
                        formData.append("formFiles", input.files[i]);
                    }

                    $.ajax({
                        url: "/Desenhos/SalvarArquivos",
                        type: 'POST',
                        data: formData,
                        success: function (data) {
                            if (data.success != null && !data.success) {
                                hideSpinner();
                                customAlert(data.message)
                                return
                            }

                            if (data.success) {
                                window.location.href = data.message;
                                return
                            }

                            document.write(data);
                        },
                        contentType: false,
                        processData: false
                    })
                })
            },
            updateDesenhoInfo: function () {

                $("#desenhoInfoBtn").click(function (e) {
                    e.preventDefault();
                    updateDesenho();
                })

                function updateDesenho() {
                    showSpinner();
                    let formData = new FormData();

                    formData.append("Id", _model.Id)
                    formData.append("Status", $('#status').val())
                    formData.append("Descricao", $('#descricao').val())
                    formData.append("Quantidade", $('#quantidade').val())
                    formData.append("Conjunto", $('#conjunto').val())
                    formData.append("NumeroConjunto", $('#numeroConjunto').val())
                    formData.append("Prioridade", $('#prioridade').val())

                    $.ajax({
                        url: "/Desenhos/UpdateInfo",
                        type: 'POST',
                        data: formData,
                        success: function (data) {
                            if (data.success != null && !data.success) {
                                hideSpinner();
                                customAlert(data.message)
                                return
                            }

                            if (data.success) {
                                window.location.reload();
                                return
                            }

                            document.write(data);
                        },
                        contentType: false,
                        processData: false
                    })
                }
            },
            updateLucrosImpostos() {
                $("#salvarPercentuais").click(function (e) {
                    e.preventDefault();
                    updateLucrosImpostos();
                })

                function updateLucrosImpostos() {
                    showSpinner();
                    let formData = new FormData();

                    formData.append("Id", _model.Id)
                    formData.append("Lucro", $('#lucros').val().toLocaleString('pt-BR'))
                    formData.append("Impostos", $('#impostos').val().toLocaleString('pt-BR'))

                    $.ajax({
                        url: "/Desenhos/UpdateLucrosImpostos",
                        type: 'POST',
                        data: formData,
                        success: function (data) {
                            if (data.success != null && !data.success) {
                                hideSpinner();
                                customAlert(data.message)
                                return
                            }

                            if (data.success) {
                                window.location.reload();
                                return
                            }

                            document.write(data);
                        },
                        contentType: false,
                        processData: false
                    })
                }
            },
            showUpdatePecaNormalizada: function (id) {
                $.ajax({
                    type: 'GET',
                    url: `/PecaNormalizada/Get/${id}`,
                    success: function (data) {

                        if (data.success != null && !data.success) {
                            customAlert(data.message)
                            return
                        }
                        
                        $('#updateNomePecaNormalizada').val(data.data.title)
                        $('#updateQuantidadePecaNormalizada').val(data.data.quantidade)
                        $('#updatePrecoPecaNormalizada').val(data.data.preco.toLocaleString('pt-BR'))
                        $('#updatePecaNormalizadaId').val(id)
                        $(updatePecaNormalizada).modal('show')
                    }
                })
            },
            updatePecaNormalizada: function () {
                $(updatePecaNormalizada).modal('hide')
                showSpinner();
                let formData = new FormData();

                formData.append("Id", $('#updatePecaNormalizadaId').val())
                formData.append("DesenhoId", $('#desenhoId').val())
                formData.append("Title", $('#updateNomePecaNormalizada').val())
                formData.append("Quantidade", $('#updateQuantidadePecaNormalizada').val())
                formData.append("Preco", $('#updatePrecoPecaNormalizada').val().toLocaleString('pt-BR'))

                $.ajax({
                    url: "/PecaNormalizada/Update",
                    type: 'POST',
                    data: formData,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        if (data.success) {
                            window.location.reload();
                            return
                        }

                        document.write(data);
                    },
                    contentType: false,
                    processData: false
                })
            },
            novaPecaNormalizada: function () {
                $(novaPecaNormalizada).modal('show')
            },
            novoFrete: function () {
                $(novoFrete).modal('show')
            },
            inserirNovaPecaNormalizada: function () {
                $(novaPecaNormalizada).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("DesenhoId", $('#desenhoId').val())
                formData.append("Title", $('#nomePecaNormalizada').val())
                formData.append("Quantidade", $('#quantidadePecaNormalizada').val())
                formData.append("Preco", $('#precoPecaNormalizada').val().toLocaleString('pt-BR'))

                $.ajax({
                    type: 'POST',
                    url: "/PecaNormalizada/Create",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        window.location.reload();
                    }
                })
            },
            inserirNovoFrete: function () {
                $(novoFrete).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("DesenhoId", $('#desenhoId').val())

                if ($('#nomeFrete').val() !== "") {
                    formData.append("Titulo", $('#nomeFrete').val())
                }
                else {
                    formData.append("Titulo", "Frete")
                }
                
                formData.append("Valor", $('#valorFrete').val().toLocaleString('pt-BR'))

                $.ajax({
                    type: 'POST',
                    url: "/FreteDesenho/Create",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        window.location.reload();
                    }
                })
            },
            novoServico: function () {
                $.ajax({
                    type: 'GET',
                    url: "/TipoServico/GetAll",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        $.each(data.data, function (i, item) {
                            let optionExists = $(`#tiposervico option[value='${item.id}']`).length > 0;

                            if (!optionExists) {
                                $("#tiposervico").append($('<option>', {
                                    value: item.id,
                                    text: item.servico
                                }));
                            }
                        });
                    }
                })

                $(novoservico).modal('show')
            },
            inserirNovoServico: function () {
                $(novoservico).modal('hide')

                showSpinner();

                let formData = new FormData();

                formData.append("DesenhoId", $('#desenhoId').val())
                formData.append("TipoServicoId", $('#tiposervico').val())
                formData.append("QuantidadePecas", $('#quantidadePecas').val())
                formData.append("Horas", $('#horas').val())

                $.ajax({
                    type: 'POST',
                    url: "/ServicoDesenho/Create",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        window.location.reload();
                    }
                })
            },
            novoMaterial: function () {
                $.ajax({
                    type: 'GET',
                    url: "/Desenhos/GetMateriais",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        $.each(data.data, function (i, item) {
                            let optionExists = $(`#materiais option[value='${item.id}']`).length > 0;
                            debugger
                            if (!optionExists) {
                                $("#materiais").append($('<option>', {
                                    value: item.id,
                                    text: `${item.nome} (${item.densidade})`
                                }));
                            }
                        });
                    }
                })

                $(novoMaterial).modal('show')
            },
            inserirNovoMaterialDesenho: function () {
                $(novoMaterial).modal('hide')

                showSpinner();


                let formData = new FormData();

                formData.append("DesenhoId", $('#desenhoId').val())
                formData.append("MaterialId", $('#materiais').val())
                formData.append("Quantidade", $('#quantidadeId').val())
                formData.append("Largura", $('#larguraId').val())
                formData.append("Altura", $('#alturaId').val())
                formData.append("Comprimento", $('#comprimentoId').val())
                formData.append("Expessura", $('#expessuraId').val())
                formData.append("ExpessuraSuperior", $('#expessuraSuperiorId').val())
                formData.append("Diametro", $('#diametroId').val())
                formData.append("TipoSolido", $('#formatos').val())

                $.ajax({
                    type: 'POST',
                    url: "/Desenhos/InserirMaterial",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        window.location.reload();
                    }
                })
            },
            btnFormatoChange: function () {
                $('#formatos').change(function () {
                    let valueSelect = $('#formatos').val();

                    escopo.resetInputsDimensoes();

                    if (valueSelect === "0" || valueSelect === "5" || valueSelect === "7") {
                        $('#divAltura').removeAttr('hidden');
                        $('#divLargura').removeAttr('hidden');
                        $('#divExpessura').removeAttr('hidden');
                        $('#divComprimento').removeAttr('hidden');
                    }
                    else if (valueSelect === "1") {
                        $('#divExpessura').removeAttr('hidden');
                        $('#divDiametro').removeAttr('hidden');
                        $('#divComprimento').removeAttr('hidden');
                    }
                    else if (valueSelect === "2") {
                        $('#divDiametro').removeAttr('hidden');
                        $('#divComprimento').removeAttr('hidden');
                    }
                    else if (valueSelect === "3") {
                        $('#divAltura').removeAttr('hidden');
                        $('#divComprimento').removeAttr('hidden');
                    }
                    else if (valueSelect === "4") {
                        $('#divAltura').removeAttr('hidden');
                        $('#divLargura').removeAttr('hidden');
                        $('#divComprimento').removeAttr('hidden');
                    }
                    else if (valueSelect === "6") {
                        $('#divAltura').removeAttr('hidden');
                        $('#divLargura').removeAttr('hidden');
                        $('#divComprimento').removeAttr('hidden');
                        $('#divExpessura').removeAttr('hidden');
                        $('#divExpessuraSuperior').removeAttr('hidden');
                    }
                })
            },
            resetInputsDimensoes: function () {
                $('#larguraId').val('')
                $('#divLargura').attr('hidden', true);
                $('#alturaId').val('')
                $('#divAltura').attr('hidden', true);
                $('#expessuraId').val('')
                $('#divExpessura').attr('hidden', true);
                $('#comprimentoId').val('')
                $('#divComprimento').attr('hidden', true);
                $('#diametroId').val('')
                $('#divDiametro').attr('hidden', true);
                $('#expessuraSuperiorId').val('')
                $('#divExpessuraSuperior').attr('hidden', true);
            },
            deleteMaterial: function (id, nomeMaterial) {
                event.stopPropagation();
                let endpoint = "MaterialDesenho";
                customConfirm(`Você deseja excluir o material <b>${nomeMaterial}</b>?`,
                    escopo.confirmDelete,
                    { endpoint, id },
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            deleteServico: function (id, tipoServico) {
                event.stopPropagation();
                let endpoint = "ServicoDesenho";
                customConfirm(`Você deseja excluir o serviço <b>${tipoServico}</b>?`,
                    escopo.confirmDelete,
                    { endpoint, id },
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            deleteFrete: function (id, frete) {
                event.stopPropagation();
                let endpoint = "FreteDesenho";
                customConfirm(`Você deseja excluir o frete <b>${frete}</b>?`,
                    escopo.confirmDelete,
                    { endpoint, id },
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            deletePecaNormalizada: function (id, pecaNormalizada) {
                event.stopPropagation();
                let endpoint = "PecaNormalizada";
                customConfirm(`Você deseja excluir a peça <b>${pecaNormalizada}</b>?`,
                    escopo.confirmDelete,
                    { endpoint, id },
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            confirmDelete: function (data) {
                $.ajax({
                    method: "DELETE",
                    url: `/${data.data.endpoint}/Delete/${data.data.id}`,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            hideSpinner();
                            customAlert(data.message)
                            return
                        }

                        if (data.success) {
                            window.location.reload();
                            return
                        }

                        document.write(data);
                    }
                });
            },
            openCamera: async function (idPeca) {
                $(novaFoto).modal('show')
                $('#cameraVideo').prop('hidden', false);
                $('#btnCloseCamera').prop('hidden', false);
                $('#btnOpenCamera').prop('hidden', true);
                _stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
                $('#video')[0].srcObject = _stream;
            },
            closeCamera: async function () {
                $('#cameraVideo').prop('hidden', true);
                $('#btnOpenCamera').prop('hidden', false);
                $('#btnCloseCamera').prop('hidden', true);
                $('#cameraCanvas').prop('hidden', true);
                _stream.getTracks()[0].stop();
                $('#video')[0].srcObject = null;
            },
            shoot: function () {
                $('#cameraVideo').prop('hidden', true);
                $('#cameraCanvas').prop('hidden', false);
                $('#canvasPhoto')[0].getContext('2d').drawImage($('#video')[0], 0, 0, $('#canvasPhoto')[0].width, $('#canvasPhoto')[0].height);
                _stream.getTracks()[0].stop();
            },
            reShoot: function () {
                escopo.closeCamera();
                escopo.openCamera();
            },
            salvarFoto: function (id) {
                let file = null;
                $('#canvasPhoto')[0].toBlob(function (blob) {

                    let formData = new FormData();

                    if (!isCanvasEmpty($('#canvasPhoto')[0])) {
                        showSpinner();

                        let nomeFoto = $('#nomeFoto').val();

                        if (nomeFoto == "")
                            nomeFoto = "nova-foto"

                        file = new File([blob], `${nomeFoto}.png`, { type: 'image/png' });
                        formData.append("FormFiles", file);

                        formData.append("desenhoId", id)

                        $.ajax({
                            url: "/Desenhos/SalvarArquivos",
                            type: 'POST',
                            data: formData,
                            success: function (data) {
                                if (data.success != null && !data.success) {
                                    hideSpinner();
                                    customAlert(data.message)
                                    return
                                }

                                if (data.success) {
                                    window.location.href = data.message;
                                    return
                                }

                                document.write(data);
                            },
                            contentType: false,
                            processData: false
                        })
                    }
                  
                }, 'image/png');

                function isCanvasEmpty(canvas) {
                    const blankCanvas = document.createElement('canvas');
                    blankCanvas.width = canvas.width;
                    blankCanvas.height = canvas.height;
                    return canvas.toDataURL() === blankCanvas.toDataURL();
                }
            },
            verPdf: function (id) {

                $.ajax({
                    type: 'GET',
                    url: `/ArquivoDesenho/Get/${id}`,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        $("#ver-pdf").attr('src', `data:application/pdf;base64, ${data.data.base64}`)
                        $(verPdf).modal('show')
                    }
                })
            },
            baixarPdf: function (id) {
                $.ajax({
                    type: 'GET',
                    url: `/ArquivoDesenho/Get/${id}`,
                    success: function (data) {

                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        var blob = new Blob([base64ToArrayBuffer(data.data.base64)], { type: "application/pdf" });
                        var link = document.createElement('a');
                        link.href = window.URL.createObjectURL(blob);
                        var fileName = data.data.nomeArquivo;
                        link.download = fileName;
                        link.click();
                    }
                });

                function base64ToArrayBuffer(base64) {
                    var binaryString = window.atob(base64);
                    var binaryLen = binaryString.length;
                    var bytes = new Uint8Array(binaryLen);
                    for (var i = 0; i < binaryLen; i++) {
                        var ascii = binaryString.charCodeAt(i);
                        bytes[i] = ascii;
                    }
                    return bytes;
                }
            },
            excluirArquivo: function (id, nomeArquivo) {
                event.stopPropagation();
                let endpoint = "ArquivoDesenho";
                customConfirm(`Você deseja excluir o arquivo <b>${nomeArquivo}</b>?`,
                    escopo.confirmDelete,
                    { endpoint, id },
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
        }
    }()

})(window.melc = window.melc || { _isNamespace: true }, jQuery);