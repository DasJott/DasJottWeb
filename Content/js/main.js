/* global bootbox */
$(function() {

  setMenuSelection();
  setupAbout();

  // load all links within the page async
  //setAsyncContentLoading(100, 300);

  // smooth page changes
  setupAsyncSectionLoading(100, 300);
  console.debug("main.js loaded");
});

///////////////////////////////////////////////////////////////////////////////////////////////////

window.loadSpinner = new JottLoadSpinner();

$('.chosen-select').chosen();

// smooth page changes from main menu
function setupAsyncSectionLoading(fadeOut, fadeIn) {
  if (!fadeOut)  { fadeOut = 300; }
  if (!fadeIn) { fadeIn = fadeOut; }

  var $content = $('#bodyContent');

  $content.fadeIn(fadeIn);
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
        console.debug("menu navigation: " + url);
        if (url == '/') {
          document.location.pathname = url;
        } else {
          console.debug("Ajax loading");
          $content.load(url, '', function(responseText, textStatus, jqXHR) {
            console.debug("loading complete: " + textStatus);
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

function setupAbout() {
  function selectTab(num) {
    var $tabs = $('ul#mainAboutSections > li');
    var $contents = $('div#mainAboutContents > div');

    // reset previous selection
    $tabs.removeClass('active');
    $contents.hide();

    // make new selecction
    $($tabs.get(num)).addClass('active');
    $($contents.get(num)).fadeIn(100);
  }

  // mainAbout section chooser
  $('ul#mainAboutSections > li').each(function(idx, elem) {
    $(elem).click(function(e) {
      selectTab(idx);
    })
  })

  $('#mainAbout').on('show.bs.modal', function (e) {
    selectTab(0);
  });
}
