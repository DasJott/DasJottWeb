$(function() {
  $('#bodyContent').load('/News/Index', '', function() {
    $('#bodyContent').fadeIn();
  });

  logger.debug("Start/Index loaded");
});

