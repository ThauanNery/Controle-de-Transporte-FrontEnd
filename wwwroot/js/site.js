// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Adiciona ação para o botão de confirmar
$('.btn-close').click(function () {
    $('.alert').hide('hide');
})

    $(document).ready(function () {
        $('#cnpjInput').mask('00.000.000/0000-00', { reverse: true });
    });

let table = new DataTable('#tabela', {
    "ordering": true,
    "paging": true,
    "searching": true,
    columnDefs: [
        { "orderable": true, "targets": 0 }, // Habilitar ordenação apenas para a primeira coluna
        { "orderable": false, "targets": '_all' } // Desabilitar ordenação para todas as outras colunas
    ],
    "oLanguage": {
        "sEmptyTable": "Nenhum registro encontrado na tabela",
        "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
        "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "Mostrar _MENU_ registros por pagina",
        "sLoadingRecords": "Carregando...",
        "sProcessing": "Processando...",
        "sZeroRecords": "Nenhum registro encontrado",
        "sSearch": "Pesquisar",
        "oPaginate": {
            "sNext": "Proximo",
            "sPrevious": "Anterior",
            "sFirst": "Primeiro",
            "sLast": "Ultimo"
        },
        "oAria": {
            "sSortAscending": ": Ordenar colunas de forma ascendente",
            "sSortDescending": ": Ordenar colunas de forma descendente"
        }
    }
});

document.getElementById("confirmDelete").onclick = function () {
    // Aqui você pode adicionar a lógica para excluir o item
    console.log("Item excluído!");
    $('#myModal').modal('hide'); // Fecha o modal
}