function JottLoadSpinner() {

  this.fadeDuration = 100;

  if ($('#div_overlay').length === 0) {
    var sOverlay = '<div id="div_overlay"><div class="loader">Loading</div></div>';

    $('body').append($(sOverlay).css({
      "left": "0px",
      "top": "0px",
      "width": "100%",
      "height": "100%",
      "z-index": "2000",
      "background": "black",
      "opacity": 0.4,
      "display": "none"
    }));
  }

  this.show = function (elem) {
    var $div = $('#div_overlay').detach();
    var $target = $('body');

    if (elem) {
      $target = $(elem).css("position", "relative");
      $div.css({
        "position": "absolute",
      });
    } else {
      $div.css({
        "position": "fixed",
      });
    }

    if ($div.parent().prop('tagName') != $target.prop('tagName')) {
      $div.appendTo($target);
    }

    $div.fadeIn(this.fadeDuration);
  }
  this.hide = function () {
    $('#div_overlay').fadeOut(this.fadeDuration);
  }
}

