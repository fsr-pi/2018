$(function () {
  $('.pagebox').click(function () {
    $(this).select();
  });

  $('.pagebox').bind('keyup', function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    var pageBox = $(this);
    if (keycode == 13) {      
      if (validRange(pageBox.val(), pageBox.data("min"), pageBox.data("max"))) {
        var link = pageBox.data('url');
        link = link.replace('-1', pageBox.val());
        window.location = link;
      }
    }
    else if (keycode == 27) {      
      pageBox.val(pageBox.data('current'));
    }
  });
});

function validRange(str, min, max) {
  var intRegex = /^\d+$/;
  if (intRegex.test(str)) {//da li je upisan broj?
    var num = parseInt(str);
    if (num >= min && num <= max)
      return true;
    else
      return false;
  }
  else {
    return false;
  }
}