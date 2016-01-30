/* global JottLoadSpinner */
/* global logger */
/* global bootbox */
$(function() {

  setMenuSelection();

  // smooth page changes
  setupAsyncSectionLoading(100, 300);
  logger.debug("main.js loaded");
});

///////////////////////////////////////////////////////////////////////////////////////////////////

window.loadSpinner = new JottLoadSpinner();
window.logger = new Utils.Logger();

$('.chosen-select').chosen();

// smooth page changes from main menu
function setupAsyncSectionLoading(fadeOut, fadeIn) {
  if (!fadeOut)  { fadeOut = 300; }
  if (!fadeIn) { fadeIn = fadeOut; }

  var $content = $('#bodyContent');

  // load pages by ajax
  $('#mainMenu > li').click(function() {
    var $elem = $(this);
    var $prev = $elem.parent().find("li.active");
    if (!$elem.hasClass('active')) {
      $prev.removeClass('active');
      $elem.addClass('active');
      $content.fadeOut(fadeOut, function() {
        var $a = $elem.find('a');
        var url = $a.attr("href")
        var title = $a.text();
        logger.debug("menu navigation: " + url);
        if (url == '/') {
          document.location.pathname = url;
        } else {
          logger.debug("Ajax loading");
          $content.load(url, '', function(responseText, textStatus, jqXHR) {
            logger.debug("loading complete: " + textStatus);
            if (textStatus === "error") {
              var win = window.open("", "Error");
              win.document.write(responseText);
            }
            document.title = title + " - " + document.title;
            $content.fadeIn(fadeIn);
          })
        }
      });
    }
    return false;
  });
}

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

