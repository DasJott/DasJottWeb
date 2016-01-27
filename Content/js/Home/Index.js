$(function() {
  $('.infopanel').first().removeClass('panel-default').addClass('panel-primary');
  fadePanels();

  $('#clickMe').click(function() {
    loadSpinner.show();
    var url = "/home/newsticker";
    var data = '';
    $('#ajaxTickerBody').load(url, data, function( responseText, textStatus, jqXHR ) {
      $('.infopanel').hide();
      loadSpinner.hide();
      $('#ajaxTicker').fadeIn();
    });
  });
  loadSpinner.hide();
});

function fadePanels()
{
  var animDur = 300;
  var animEasing = "easeOutQuint";

  var count = 0;
  $('.infopanel').each(function() {
    $(this).delay(count*animDur).fadeIn(
      {
        duration: animDur,
        easing: animEasing
      }
    );
    ++count;
  });
}
