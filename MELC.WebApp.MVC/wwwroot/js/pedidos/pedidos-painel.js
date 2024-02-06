
( function (melc, $, escopo) {

    if (!melc.pedidos) { melc.pedidos = {} }

    _model = Object;

    melc.pedidos = function () {
        return {
            iniciar: function (model) {
                _model = model;
                escopo = melc.pedidos

                $('#pedidoDataTable').DataTable({
                    aoColumnDefs: [{
                        'bSortable': false,
                        'aTargets': [-1] /* 1st one, start by the right */
                    }],
                    dom: '<lf<t>p>',
                    order: [[1, 'desc']],
                    language: {
                        lengthMenu: 'Mostrar _MENU_ itens por página',
                        zeroRecords: 'Não há pedidos',
                        info: 'Mostrando página _PAGE_ de _PAGES_',
                        infoEmpty: 'Não há pedidos',
                        infoFiltered: '(filtered from _MAX_ total records)',
                        search: 'Filtrar',
                        paginate: {
                            first: 'Primeiro',
                            last: 'Último',
                            next: 'Próximo',
                            previous: 'Anterior'
                        }
                    }
                });

                $('#descricao').change(function () {
                    enableBtn();
                })
                $('#status').val(model.Status)
                $('#status').change(function () {
                    enableBtn();
                })

                escopo.updatePedidoInfo();

                function enableBtn() {
                    $('#pedidoInfoBtn').removeAttr('disabled');
                }
            },
            updatePedidoInfo: function () {

                $("#pedidoInfoBtn").click(function (e) {
                    e.preventDefault();
                    showSpinner();
                    let formData = new FormData();

                    formData.append("Id", _model.Id)
                    formData.append("Status", $('#status').val())
                    formData.append("Descricao", $('#descricao').val())

                    $.ajax({
                        url: "/Pedidos/UpdateInfo",
                        type: 'POST',
                        data: formData,
                        success: function (data) {
                            if (data.success != null && !data.success) {
                                hideSpinner();
                                customAlert(data.data)
                                return
                            }

                            window.location.reload();
                        },
                        contentType: false,
                        processData: false
                    })
                })
            },
            getPedidoDetalhe: function (id) {
                showSpinner();
                window.location.assign(`/Pedidos/GetDetalhe/${id}`)
            },
            novoPedido: function () {
                $(novoPedido).modal('show')
            },
            updatePedido: function (id) {
                event.stopPropagation();
            },
            excluirPedido: function (id, clienteId, pedidoNome) {
                event.stopPropagation();
                customConfirm(`Você deseja excluir o pedido <b>${pedidoNome}</b>?`,
                    escopo.confirmDeletePedido,
                    { id, clienteId},
                    () => {
                        $(staticBackdrop).modal('hide')
                        return false
                    }
                )
            },
            confirmDeletePedido: function (e) {
                $.ajax({
                    method: "DELETE",
                    url: `/Pedidos/Delete/${e.data.id}`,
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
            criarNovoPedido: function () {
                $(novoPedido).modal('hide')
                showSpinner();
                let formData = new FormData();
                formData.append("ClienteId", $('#clienteId').val())
                formData.append("Title", $('#pedido').val())
                formData.append("NumeroPedido", $('#numeroPedido').val())
                formData.append("DataDeEntrega", $('#datepicker').val())
                formData.append("Descricao", $('#descricao').val())

                $.ajax({
                    type: 'POST',
                    url: "/Pedidos/Create",
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
            novoFaturamento: function (id) {

                $.ajax({
                    type: 'GET',
                    url: `/Desenhos/GetByPedidoIdFaturamento/${id}/2`,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        let htmlString = ``;
                        data.data.forEach(element => {
                            let htmlStringAgrupador = ``;
                            htmlStringAgrupador += `<h2 style="border-color:black; border-bottom:dashed; border-width:2px" class="accordion-header" id="flush-headingOne">
                                            <button class="accordion-button collapsed" type="button" data-coreui-toggle="collapse" data-coreui-target="#${element.id}" aria-expanded="false" aria-controls="${element.id}">
	                                            ${element.agrupador}
                                            </button>
                                            </h2>
                                            <div id='${element.id}' class="accordion-collapse collapse" aria-labelledby="flush-headingOne">
	                                        <div class="accordion-body">`

                            htmlStringAgrupador += `<div class="input-group">
			                                    <div class="input-group-text">
				                                    <input onClick="melc.pedidos.selecionarTodos('${element.id}')"  class="form-check-input" type="checkbox" value="false">
			                                    </div>
			                                    <input type="text" disabled class="form-control" placeholder="Selecionar todos">
		                                        </div>`

                            element.faturamentos.forEach(faturamento => {
                                htmlStringAgrupador += `<div class="input-group">
			                                    <div class="input-group-text">
				                                    <input ${faturamento.ready ? '' : 'disabled' } id="${faturamento.id}" name="checkBoxPecasFaturamento" class="form-check-input" type="checkbox" value="false">
			                                    </div>
			                                    <input type="text" disabled class="form-control" placeholder='${faturamento.title}'>
                                                <label ${faturamento.ready ? 'hidden' : '' } class="label form-control btn btn-danger">Há pendências de dados nesta peça</label>
                                                <button type="reset" onclick="melc.pedidos.goToDesenho('${faturamento.id}')" ${faturamento.ready ? 'hidden' : '' } class=" btn btn-danger btnLookDesenho">
                                                    <svg class="icon">
                                                        <use xlink:href="/vendors/coreui/icons/svg/free.svg#cil-zoom"></use>
                                                    </svg>
                                                </button>
		                                    </div>`
                            });

                            htmlString += htmlStringAgrupador;
                            htmlString += `	</div> </div>`;
                        });

                        $('#accordionDesenhosFinalizados').html(htmlString);
                    }
                })

                $.ajax({
                    type: 'GET',
                    url: `/Desenhos/GetByPedidoIdFaturamento/${id}/4`,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        let htmlString = ``;
                        data.data.forEach(element => {
                            let htmlStringAgrupador = ``;
                            htmlStringAgrupador += `<h2 style="border-color:black; border-bottom:dashed; border-width:2px" class="accordion-header" id="flush-headingOne">
                                            <button class="accordion-button collapsed" type="button" data-coreui-toggle="collapse" data-coreui-target="#${element.id}" aria-expanded="false" aria-controls="${element.id}">
	                                            ${element.agrupador}
                                            </button>
                                            </h2>
                                            <div id='${element.id}' class="accordion-collapse collapse" aria-labelledby="flush-headingOne">
	                                        <div class="accordion-body">`

                            htmlStringAgrupador += `<div class="input-group">
			                                    <div class="input-group-text">
				                                    <input onClick="melc.pedidos.selecionarTodos('${element.id}')"  class="form-check-input" type="checkbox" value="false">
			                                    </div>
			                                    <input type="text" disabled class="form-control" placeholder="Selecionar todos">
		                                        </div>`

                            element.faturamentos.forEach(faturamento => {
                                htmlStringAgrupador += `<div class="input-group">
			                                    <div class="input-group-text">
				                                    <input ${faturamento.ready ? '' : 'disabled' } id="${faturamento.id}" name="checkBoxPecasFaturamento" class="form-check-input" type="checkbox" value="false">
			                                    </div>
			                                    <input type="text" disabled class="form-control" placeholder='${faturamento.title}'>
                                                <label ${faturamento.ready ? 'hidden' : '' } class="label form-control btn btn-danger">Há pendências de dados nesta peça</label>
                                                <button type="reset" onclick="melc.pedidos.goToDesenho('${faturamento.id}')" ${faturamento.ready ? 'hidden' : '' } class=" btn btn-danger btnLookDesenho">
                                                    <svg class="icon">
                                                        <use xlink:href="/vendors/coreui/icons/svg/free.svg#cil-zoom"></use>
                                                    </svg>
                                                </button>		                                    
                                                </div>`
                            });

                            htmlString += htmlStringAgrupador;
                            htmlString += `	</div> </div>`;
                        });

                        $('#accordionDesenhosFaturados').html(htmlString);
                    }
                })

                $(novoFaturamento).modal('show')
            },
            selecionarTodos: function (id) {
                let inputGroups = $(`#${id}`).children()[0].children
                let checkConjunto = false;

                for (var i = 0; i < inputGroups.length; i++) {
                    let checkbox = inputGroups[i].children[0].children[0]

                    if (checkbox.disabled == true)
                        continue;

                    if (i == 0)
                        checkConjunto = checkbox.checked
                    else
                        checkbox.checked = checkConjunto;
                }
            },
            faturar: function () {
                let desenhosId = new Array();

                $("input[name='checkBoxPecasFaturamento']").each(function () {
                    if (this.checked == true)
                        desenhosId.push(this.id);
                });

                $(novoFaturamento).modal('hide')
                showSpinner();

                let formData = new FormData();

                desenhosId.forEach(element => {
                    formData.append("DesenhosIds", element)
                })

                formData.append("Title", $("#nomeFaturamento").val())
                formData.append("PedidoId", $("#pedidoId").val())

                $.ajax({
                    type: 'POST',
                    url: "/Faturamento/Create",
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
            visualizarFaturamento: function (id) {

                $.ajax({
                    type: 'GET',
                    url: `/Faturamento/Get/${id}`,
                    success: function (data) {
                        if (data.success != null && !data.success) {
                            customAlert(data.data)
                            return
                        }

                        $("#pdf-faturamento").attr('src', `data:application/pdf;base64, ${data.data.base64}`)
                        $(verFaturamento).modal('show')
                    }
                })
            },
            baixarPdfFaturamento: function (id) {
                $.ajax({
                    type: 'GET',
                    url: `/Faturamento/Get/${id}`,
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
            goToDesenho: function (id) {
                window.location.href = `/Desenhos/GetDetalhe/${id}`
            }
        }
    }()

})(window.melc = window.melc || { _isNamespace: true }, jQuery);