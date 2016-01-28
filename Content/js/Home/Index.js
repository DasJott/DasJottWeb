$(function() {
  $('.infopanel').first().removeClass('panel-default').addClass('panel-primary');
  fadeNewspanels();

  $('#clickMe').click(function() {
    window.loadSpinner.show();
    var url = "/home/newsticker";
    var data = '';
    $('#ajaxTickerBody').load(url, data, function( responseText, textStatus, jqXHR ) {
      $('.infopanel').hide();
      window.loadSpinner.hide();
      $('#ajaxTicker').fadeIn();
    });
  });
  window.loadSpinner.hide();
});

function fadeNewspanels()
{
  var animDur = 300;
  var animEasing = "easeOutQuint";

  var count = 0;
  $('.newspanel').each(function() {
    $(this).delay(count*animDur).fadeIn(
      {
        duration: animDur,
        easing: animEasing
      }
    );
    ++count;
  });
}
