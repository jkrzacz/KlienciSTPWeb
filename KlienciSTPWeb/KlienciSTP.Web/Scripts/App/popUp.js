$(function () {

    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {
        var url = $(e.target).attr("href");
        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');


        $('#myModalContent').load(this.href, function () {


            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');

            bindForm(url,this);
        });

        return false;
    });


});

function bindForm(url, dialog) {

    $('form', dialog).submit(function () {
        $.ajax({
            url: url,
            type: "POST",
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    //Refresh
                    location.reload();
                } else {
                    $('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}