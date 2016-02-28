function Utils() {}

/**
 * Format a string simliar to C# style, but on the string itself
 */
String.prototype.format = function() {
  var str = this;
  for (var i = 0; i < arguments.length; i++) {
    str = str.replace("{" + i + "}", arguments[i]);
  }
  return str;
}

// get url param by name
Utils.urlParam = function (name) {
  var value = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
  return value != null && value.length > 0 ? value[1] : "";
}

Utils.urlPath = function(fromString) {
  var path = new RegExp('^http[s]?://[^/]+/([^?#]*)$').exec(fromString ? fromString : window.location.href);
  return path != null && path.length > 0 ? path[1] : "";
}

// restore form from serialized data
Utils.restoreForm = function (form, data, bChange) {
  for (var i in data) {
    var name = i;
    var value = data[i];

    var $e = $(form).find("[name=\"" + name + "\"]");
    if ($e.is(":radio")) {
      $e.filter("[value=\"" + value + "\"]").prop("checked", true);
    }
    else if ($e.is(":checkbox") && value) {
      $e.prop("checked", true);
    }
    else if ($e.is("select")) {
      $e.find("[value=\"" + value + "\"]").prop("selected", true);
    }
    else {
      $e.val(value);
    }
    if (bChange) {
      $e.change().trigger('chosen:updated');
    }
  }
}

Utils.checkNumber = function (elem) {
  var num = $(elem).val();
  num = num.replace(/[^0-9]+/g, '');
  $(elem).val(num);
}

Utils.Logger = function(active) {
  if (typeof(active) === 'undefined') { active = true; }

  this.debug = function(txt) {
    if (console && console.debug && active) {
      console.debug(txt);
    }
  }
  this.log = function(txt) {
    if (console && console.log && active) {
      console.log(txt);
    }
  }
  this.warning = function(txt) {
    if (console && console.warning && active) {
      console.warning(txt);
    }
  }
  this.error = function(txt) {
    if (console && console.error && active) {
      console.error(txt);
    }
  }
}

Utils.UIMsg = function(active) {
  if (typeof(active) === 'undefined') { active = true; }

  var theTimer = null;
  var fadingTime = 300;

  $('#generalAlert').hide();

  var pushMessage = function(mode, title, txt, duration)
  {
    var time = typeof(duration) === 'undefined' ? 4000 : duration;

    var $elem = $('#generalAlert');

    // we need this functionality more than once
    var startTimer = function() {
      if (time > 0) {
        theTimer = setTimeout(function() {
          // showtime is over
          $elem.fadeOut(fadingTime, function() {
            // we finished fading out
            $elem.removeClass(mode).html('');
            theTimer = null;
          });
        }, time);
      } else {
        theTimer = "infinite";
      }
    }

    var killTimer = function() {
      if (theTimer != null) {
        if (theTimer != "infinite") {
          clearTimeout(theTimer);
        }
        theTimer = null;
      }
    }

    $elem.html("<strong>{0}</strong> {1}".format(title, txt));

    if (theTimer != null) {
      // we are already shown, kill timer and set again
      killTimer();
      startTimer();
    } else {
      $elem.addClass(mode).fadeIn(fadingTime, function() {
        // we finished fading in
        startTimer();
      });
    }

  }

  this.info = function(txt, time) {
    if (active) {
      pushMessage('alert-info', 'Info:', txt, time);
    }
  }
  this.success = function(txt, time) {
    if (active) {
      pushMessage('alert-success', 'Jawoll:', txt, time);
    }
  }
  this.warning = function(txt, time) {
    if (active) {
      pushMessage('alert-warning', 'Warnung:', txt, time);
    }
  }
  this.error = function(txt, time) {
    if (active) {
      pushMessage('alert-danger', 'Fehler:', txt, time);
    }
  }
}
