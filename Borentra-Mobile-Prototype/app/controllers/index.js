var fb = require('facebook');
fb.appid = "315854161864272";
fb.permissions = ['email', 'user_about_me', 'user_location'];
fb.forceDialogAuth = true;
fb.addEventListener('login', function(e) {
  if (e.success) {

    $.index.close();
    var home = Alloy.createController('home').getView();
    home.open();
  } else if (e.error) {
    Ti.API.error("Authentication error");
  } else if (e.cancelled) {

    Ti.API.error("Cancelled login");
  }
});
fb.authorize();
//
//
// facebook.addEventListener('login', function(e) {
// if (e.success) {
// $.index.close();
// // var home = Alloy.createController('homescreen').getView();
// // home.open();
// return;
// } else if (e.error || e.cancelled) {
// Titanium.UI.createAlertDialog({
// title : 'CM',
// message : 'Error logging in. Please check your internet connection'
// }).show();
// return;
// }
// //Ti.API.info(Ti.Facebook.uid);
// });
//
$.index.open();
