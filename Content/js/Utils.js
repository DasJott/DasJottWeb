function Utils() {}

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
