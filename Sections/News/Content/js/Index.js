$(function() {
  // first one is primary panel
  $('.newspanel').first().removeClass('panel-default').addClass('panel-primary');
  fadeNewspanels();

  logger.debug("News/Index loaded");
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
