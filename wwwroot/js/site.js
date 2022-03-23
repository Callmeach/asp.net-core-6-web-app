// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var PlaceHolder = $('#PlaceHolderHere');
    $('button[data-toogle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolder.html(data);
            PlaceHolder.find('.modal').modal('show');
        })
    })
    PlaceHolder.on('Click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolder.find('.modal').modal('hide');
        })
    })
});
$(function () {
    var dir = $('#addPublication');
    $('button[data-toogle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            dir.html(data);
            dir.find('.modal').modal('show');
        })
    })
    
});



