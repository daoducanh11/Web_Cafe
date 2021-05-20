//Trang Admin
(function ($) {
    $(document).ready(function () {
        var url = window.location.pathname;
        var arr = url.split('/');
        var urlParent = "/" + arr[1] + "/" + arr[2];
        $('.sidebar').find('[href="' + urlParent + '"]').addClass('active');
        if (arr.length == 4) {
            //$('.sidebar').find('[href="' + urlParent + '"]')[0].parentElement.addClass('menu-is-opening');
            //$('.sidebar').find('[href="' + urlParent + '"]')[0].parentElement.addClass('menu-open');
            var urlChild = "/" + arr[1] + "/" + arr[2] + "/" + arr[3];
            $('.sidebar').find('[href="' + urlChild + '"]')[0].parentElement.parentElement.style.display = "block";
            $('.sidebar').find('[href="' + urlChild + '"]').addClass('active');
        }
    });
})(jQuery);