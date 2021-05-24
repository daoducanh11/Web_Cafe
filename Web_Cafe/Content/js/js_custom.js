//Trang Admin
$(document).ready(function () {
    $(".delete-row").click(function () {
        $(this).parents("tr").remove();
    });
});    