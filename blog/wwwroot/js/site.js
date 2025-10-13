
// Pré-visualização de imagem
function previewPostImage(event) {
    const input = event.target;
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const previewWrapper = document.getElementById('postPreviewWrapper');
            const previewImg = document.getElementById('postPreview');
            previewImg.src = e.target.result;
            previewWrapper.style.display = 'block';
        }
        reader.readAsDataURL(input.files[0]);
    }
}

// Fechar toast
$('.btn-close').click(function () {
    $(".toast-shor").hide();
});

function chamarTabela(id) {
    // Adiciona ordenação customizada para a última coluna (índice 6)
    $.fn.dataTable.ext.order['feito-marcado'] = function (settings, col) {
        return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
            // Verifica se a linha tem a classe 'bg-secondary' (marcado)
            return $(td).closest('tr').hasClass('bg-secondary') ? 1 : 0;
        });
    };

    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "order": [
            [6, 'asc'], // 1º: marcados vão para o final
            [0, 'asc'], // 2º: ordena pelo ID
        ],
        "columnDefs": [
            {
                targets: 6, // Última coluna (ações)
                orderDataType: 'feito-marcado',
                orderable: false, // opcional: geralmente ações não são ordenáveis
                searchable: false // opcional: geralmente ações não são pesquisáveis
            }
        ],
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

// Inicializa a tabela
chamarTabela("#table-user");

