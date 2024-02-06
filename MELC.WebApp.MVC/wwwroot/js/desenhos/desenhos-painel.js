
( function (melc, $, escopo) {

    if (!melc.desenhos) { melc.desenhos = {} }

    _stream = MediaStream;

    melc.desenhos = function () {
        return {
            iniciar: function () {
                escopo = melc.desenhos
                $('#desenhoDataTable').DataTable({
                    aoColumnDefs: [{
                        'bSortable': false,
                        'aTargets': [-1] /* 1st one, start by the right */
                    }],
                    dom: '<lf<t>p>',
                    order: [[1, 'desc'], [2, 'desc']],
                    columnDefs: [
                        { "targets": [0, 4, 5], "searchable": false }
                    ],
                    language: {
                        lengthMenu: 'Mostrar _MENU_ itens por página',
                        zeroRecords: 'Não há peças',
                        info: 'Mostrando página _PAGE_ de _PAGES_',
                        infoEmpty: 'Não há peças',
                        infoFiltered: '(filtered from _MAX_ total records)',
                        search: 'Filtrar',
                        paginate: {
                            first: 'Primeiro',
                            last: 'Último',
                            next: 'Próximo',
                            previous: 'Anterior'
                        }
                    },
                    rowGroup: {
                        dataSrc: 1,
                        emptyDataGroup: 'Sem conjunto'
                    }
                });
            },
            novoDesenho: function () {
                $(novoDesenho).modal('show')
            },
            criarNovoDesenho: function () {
                $(novoDesenho).modal('hide')
                showSpinner();
                debugger
                let formData = new FormData();
                formData.append("PedidoId", $('#pedidoId').val())
                formData.append("Title", $('#title').val())
                formData.append("Quantidade", $('#quantidadePecas').val() == "" ? 0 : $('#quantidadePecas').val())
                formData.append("Descricao", $('#descricaoDesenho').val())
                formData.append("Conjunto", $('#conjunto').val())
                formData.append("NumeroConjunto", $('#numeroConjunto').val())
                formData.append("NumeroDesenho", $('#numeroDesenho').val())
                debugger

                let file = null;
                $('#canvasPhoto')[0].toBlob(function (blob) {

                    if (!isCanvasEmpty($('#canvasPhoto')[0])) {
                        file = new File([blob], `${$('#title').val()}.png`, { type: 'image/png' });
                        formData.append("FormFiles", file);
                    }

                    let input = $("#inputFiles")[0];

                    for (i = 0; i < input.files.length; i++) {
                        formData.append("FormFiles", input.files[i]);
                    }

                    $.ajax({
                        type: 'POST',
                        url: "/Desenhos/Create",
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
                }, 'image/png');

                function isCanvasEmpty(canvas) {
                    const blankCanvas = document.createElement('canvas');
                    blankCanvas.width = canvas.width;
                    blankCanvas.height = canvas.height;
                    return canvas.toDataURL() === blankCanvas.toDataURL();
                }
               
            },
            getDesenhoDetalhe: function (id) {
                showSpinner();
                window.location.assign(`/Desenhos/GetDetalhe/${id}`)
            },
            excluirDesenho: function (id, pedidoId, desenhoNome) {
                event.stopPropagation();
                customConfirm(`Você deseja excluir o desenho <b>${desenhoNome}</b>?`,
                    escopo.confirmDeleteDesenho,
                    { id, pedidoId },
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            updateDesenho: function (id) {
                event.stopPropagation();
                window.location.href = `Desenhos/Update?id=${id}`
            },
            confirmDeleteDesenho: function(e) {
                $.ajax({
                    method:"DELETE",
                    url: `/Desenhos/Delete/${e.data.id}`,
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
            openCamera: async function () {
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
            }
        }
    }()

})(window.melc = window.melc || { _isNamespace: true }, jQuery);