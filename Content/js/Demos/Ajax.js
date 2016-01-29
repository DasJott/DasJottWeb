$(function() {

  $('#getNewsTicker').click(function() {
    window.loadSpinner.show();
    var url = "/home/newsticker";
    var data = '';
    $('#ajaxTickerBody').load(url, data, function( responseText, textStatus, jqXHR ) {
      window.loadSpinner.hide();
      $('#ajaxTicker').fadeIn();
    });
  });

});

