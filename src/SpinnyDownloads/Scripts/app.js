//
// Toggle Button Processing
//
function toggleButtonProcessing(btn, processing, btnText) {
    var btnForm = $(btn).closest('form');
    var spacing = '';
    if (processing) {
        $(btn).find('.fa').addClass('fa-spin fa-spinner');
        $(btn).find('.fas').addClass('fa-spin fa-spinner');
        spacing = '&nbsp;&nbsp;';
        $(btnForm).find('.app-button').prop("disabled", true);
    }
    else {
        $(btn).find('.fa').removeClass('fa-spin fa-spinner');
        $(btn).find('.fas').removeClass('fa-spin fa-spinner');
        $(btnForm).find('.app-button').prop("disabled", false);
    }
    $(btn).find('span').html(spacing + btnText);
}

$(document).ready(function () {

    $(document).on('click', '#btn-demo-spinner', function (e) {

        var btn = $(this);
        var ogText = btn.find('span').text();
        var submitText = btn.data('submit-text');

        toggleButtonProcessing(btn, true, submitText);

        setTimeout(function (e) {
            console.log('delayed callback');
            toggleButtonProcessing(btn, false, ogText);
        }, 2000);
    });

    $(document).on('click', '#btn-download-from-ajax', function (e) {

        var btn = $(this);
        var ogText = btn.find('span').text();
        var submitText = btn.data('submit-text');

        toggleButtonProcessing(btn, true, submitText);

        $.ajax({
            type: 'POST',
            url: '/Home/DownloadFile'
        }).done(function (response) {

            var bytes = new Uint8Array(response.FileContents);
            var blob = new Blob([bytes], { type: response.ContentType });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = response.FileDownloadName;
            link.click();

            toggleButtonProcessing(btn, false, ogText);

        }).fail(function (response) {

            console.log('error');
            toggleButtonProcessing(btn, false, ogText);
        });
    });
});