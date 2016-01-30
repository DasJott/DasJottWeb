$(function() {
  // smooth page changes
  setupAsyncSectionLoading(100, 300);

  // load start page
  loadFirstPage(500);

  /////////////////////////////////////////////////////////////////////////////

  // load first page
  function loadFirstPage(fadeIn)
  {
    var $content = $('#bodyContent');
    var $a = $('#mainMenu > li > a').first();
    var url = $a.attr("href")
    var title = $a.text();
    loadContent($content, url, title, 100, fadeIn);
  }

  // smooth page changes from main menu
  function setupAsyncSectionLoading(fadeOut, fadeIn) {
    var $content = $('#bodyContent');

    // load pages by ajax
    $('#mainMenu > li').click(function() {
      $('#bodyContentError').hide();

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
            loadContent($content, url, title, fadeOut, fadeIn);
          }
        });
      }
      return false;
    });
  }

  function loadContent($content, url, title, fadeIn) {
    if (!fadeIn) { fadeIn = 300; }

    logger.debug("Ajax loading");

    $content.load(url, '', function(responseText, textStatus, jqXHR) {
      logger.debug("loading complete: " + textStatus);
      if (textStatus === "error") {
        var win = window.open("", "Error");
        win.document.write(responseText);
        $('#bodyContentError').text("An error occured").fadeIn();
      } else {
        document.title = title + " - " + document.title;
        $content.fadeIn(fadeIn);
      }
    })

  }

  logger.debug("Start/Index loaded");
});

