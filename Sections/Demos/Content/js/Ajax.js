$(function() {

  $('#getNewsTicker').click(function() {
    window.loadSpinner.show();
    var url = "/demos/newsticker";
    var data = '';
    $('#ajaxTickerBody').load(url, data, function( responseText, textStatus, jqXHR ) {
      window.loadSpinner.hide();
      $('#ajaxTicker').fadeIn();
    });
  });

  logger.debug("Demos/Ajax loaded");
});

