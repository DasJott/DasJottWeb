/* global bootbox */
$(function() {
  setMenuSelection();
  setupAbout();

  // smooth page changes
  setSmoothMenu(100, 300);
});

window.loadSpinner = new JottLoadSpinner();
//loadSpinner.show();
$('.chosen-select').chosen();

// smooth page changes from main menu
function setSmoothMenu(fadeOut, fadeIn) {
  if (!fadeOut)  { fadeOut = 300; }
  if (!fadeIn) { fadeIn = fadeOut; }

  $('#bodyContent').fadeIn(fadeIn);
  $('#mainMenu > li').click(function() {
    var $elem = $(this);
    var $prev = $elem.parent().find("li.active");
    if (!$elem.hasClass('active')) {
      $prev.removeClass('active');
      $elem.addClass('active');
      $('#bodyContent').fadeOut(fadeOut, function() {
        document.location.pathname = $elem.find('a').attr("href");
      });
      return false;
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
