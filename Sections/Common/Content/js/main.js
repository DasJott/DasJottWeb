/* global JottLoadSpinner */
/* global logger */
/* global bootbox */
$(function() {

  setMenuSelection();

  logger.debug("main.js loaded");
});

///////////////////////////////////////////////////////////////////////////////////////////////////

window.loadSpinner = new JottLoadSpinner();
window.logger = new Utils.Logger();

$('.chosen-select').chosen();

// make sure, the correct menu item is selected
function setMenuSelection() {
  var path = Utils.urlPath();
  if (path != "") {
    $('#mainMenu > li > a').each(function(idx, a) {
      var link = $(a).attr("href");
      if (link.endsWith(path)) {
        $(a).parent().addClass('active');
        document.title = $(a).text() + " - " + document.title;
      }
    });
  } else {
    $('#mainMenu > li').first().addClass('active');
  }
}

