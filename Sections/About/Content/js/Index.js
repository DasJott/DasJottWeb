$(function() {
  $('.showAboutModal').click(function() {
    loadSpinner.show();
    $('#modalContainer').load('~/about/modal', '', function(response, status, xhr) {
      if (status === "error") {
        $(this).text("Error loading about :(");
      } else {
        setupTabs();
      }
      loadSpinner.hide();
    });
  });


  /////////////////////////////////////////////////////////////////////////////

  function setupTabs() {
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

  logger.debug("About/Index loaded");
});
