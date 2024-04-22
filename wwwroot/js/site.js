// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Adiciona ação para o botão de confirmar 
document.getElementById("confirmDelete").onclick = function () {
    // Aqui você pode adicionar a lógica para excluir o item
    console.log("Item excluído!");
    $('#myModal').modal('hide'); // Fecha o modal
}