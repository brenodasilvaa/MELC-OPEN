
( function (melc, $, escopo) {

    if (!melc.desenhosPrioridades) { melc.desenhosPrioridades = {} }

    _stream = MediaStream;

    melc.desenhosPrioridades = function () {
        return {
            iniciar: function () {
                escopo = melc.desenhosPrioridades
                $('#prioridadesDataTable').DataTable({
                    aoColumnDefs: [{
                        'bSortable': false,
                        'aTargets': [-1] /* 1st one, start by the right */
                    }],
                    dom: '<lf<t>p>',
                    order: [[1, 'desc']],
                    columnDefs: [
                        { "targets": [0, 4, 6], "searchable": false }
                    ],
                    language: {
                        lengthMenu: 'Mostrar _MENU_ itens por página',
                        zeroRecords: 'Não há peças com prioridade',
                        info: 'Mostrando página _PAGE_ de _PAGES_',
                        infoEmpty: 'Não há peças com prioridade',
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
            },
            getDesenhoDetalhe: function (id) {
                showSpinner();
                window.location.href = `/Desenhos/GetDetalhe/${id}`
            }
        }
    }()

})(window.melc = window.melc || { _isNamespace: true }, jQuery);