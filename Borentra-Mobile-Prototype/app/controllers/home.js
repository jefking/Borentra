var ServiceLocator = require("service/Map")();
var moment = require("support/moment");

var view = $.viewNewsFeed;

var data = [];
var defaultFontSize = Ti.Platform.name === 'android' ? 16 : 14;

var url = "http://borentra.com/api/MobileSocial/SearchActivity?userId=9782A9F2-B83F-49E7-967C-80360C71278B";


console.log(ServiceLocator);

function formatDate() {
  var date = new Date();
  var datestr = date.getMonth() + '/' + date.getDate() + '/' + date.getFullYear();
  if (date.getHours() >= 12) {
    datestr += ' ' + (date.getHours() == 12 ? date.getHours() : date.getHours() - 12) + ':' + date.getMinutes() + ' PM';
  } else {
    datestr += ' ' + date.getHours() + ':' + date.getMinutes() + ' AM';
  }
  return datestr;
}

var params = {
  userId : "9782A9F2-B83F-49E7-967C-80360C71278B"
};
var data = [];

var tableView = Ti.UI.createTableView({
  data : data
});

view.add(tableView);

var border = Ti.UI.createView({
  backgroundColor : "#576c89",
  height : 2,
  bottom : 0
});

var tableHeader = Ti.UI.createView({
  backgroundColor : "#e2e7ed",
  width : 320,
  height : 60
});

// fake it til ya make it..  create a 2 pixel
// bottom border
tableHeader.add(border);

var arrow = Ti.UI.createView({
  backgroundImage : "/images/whiteArrow.png",
  width : 23,
  height : 60,
  bottom : 10,
  left : 20
});

var statusLabel = Ti.UI.createLabel({
  text : "Pull to reload",
  left : 55,
  width : 200,
  bottom : 30,
  height : "auto",
  color : "#576c89",
  textAlign : "center",
  font : {
    fontSize : 13,
    fontWeight : "bold"
  },
  shadowColor : "#999",
  shadowOffset : {
    x : 0,
    y : 1
  }
});

var lastUpdatedLabel = Ti.UI.createLabel({
  text : "Last Updated: " + formatDate(),
  left : 55,
  width : 200,
  bottom : 15,
  height : "auto",
  color : "#576c89",
  textAlign : "center",
  font : {
    fontSize : 12
  },
  shadowColor : "#999",
  shadowOffset : {
    x : 0,
    y : 1
  }
});

var actInd = Titanium.UI.createActivityIndicator({
  left : 20,
  bottom : 13,
  width : 30,
  height : 30
});

tableHeader.add(arrow);
tableHeader.add(statusLabel);
tableHeader.add(lastUpdatedLabel);
tableHeader.add(actInd);

tableView.headerPullView = tableHeader;

var pulling = false;
var reloading = false;

function beginReloading() {
  // just mock out the reload
  setTimeout(endReloading, 2000);
}

function endReloading() {
  // simulate loading

  // when you're done, just reset
  tableView.setContentInsets({
    top : 0
  }, {
    animated : true
  });
  reloading = false;
  lastUpdatedLabel.text = "Last Updated: " + formatDate();
  statusLabel.text = "Pull down to refresh...";
  actInd.hide();
  arrow.show();
}

tableView.addEventListener('scroll', function(e) {
  var offset = e.contentOffset.y;
  if (offset <= -65.0 && !pulling && !reloading) {
    var t = Ti.UI.create2DMatrix();
    t = t.rotate(-180);
    pulling = true;
    arrow.animate({
      transform : t,
      duration : 180
    });
    statusLabel.text = "Release to refresh...";
  } else if (pulling && (offset > -65.0 && offset < 0) && !reloading) {
    pulling = false;
    var t = Ti.UI.create2DMatrix();
    arrow.animate({
      transform : t,
      duration : 180
    });
    statusLabel.text = "Pull down to refresh...";
  }
});

var event1 = 'dragEnd';
if (Ti.version >= '3.0.0') {
  event1 = 'dragend';
}

tableView.addEventListener(event1, function(e) {
  if (pulling && !reloading) {
    reloading = true;
    pulling = false;
    arrow.hide();
    actInd.show();
    statusLabel.text = "Reloading...";
    tableView.setContentInsets({
      top : 60
    }, {
      animated : true
    });
    arrow.transform = Ti.UI.create2DMatrix();
    beginReloading();
  }
});

ServiceLocator.getService("getSearchActivity").invoke(params, function(err, response) {

  for (var i = 0; i < response.length; i++) {

    var row = Ti.UI.createTableViewRow({
      className : 'forumEvent', // used to improve table performance
      selectedBackgroundColor : 'white',
      rowIndex : i, // custom property, useful for determining the row during events
      height : 110
    });

    var imageAvatar = Ti.UI.createImageView({
      image : response[i].UserPicture,
      left : 10,
      top : 5,
      width : 50,
      height : 50
    });
    row.add(imageAvatar);

    var labelUserName = Ti.UI.createLabel({
      color : '#576996',
      font : {
        fontFamily : 'Arial',
        fontSize : defaultFontSize + 6,
        fontWeight : 'bold'
      },
      text : response[i].UserDisplayName,
      left : 70,
      top : 6,
      width : 200,
      height : 30
    });
    row.add(labelUserName);

    var labelDetails = Ti.UI.createLabel({
      color : '#222',
      font : {
        fontFamily : 'Arial',
        fontSize : defaultFontSize + 2,
        fontWeight : 'normal'
      },
      text : response[i].Text,
      left : 70,
      top : 44,
      width : 360
    });
    row.add(labelDetails);

    var imageCalendar = Ti.UI.createImageView({
      image : 'icons/love.png',
      left : 70,
      bottom : 2,
      width : 32,
      height : 32
    });
    row.add(imageCalendar);

    var labelDate = Ti.UI.createLabel({
      color : '#999',
      font : {
        fontFamily : 'Arial',
        fontSize : defaultFontSize,
        fontWeight : 'normal'
      },
      text : 'On ' + moment.utc(response[i].ModifiedOn).fromNow(),
      left : 105,
      bottom : 10,
      width : 200,
      height : 20
    });
    row.add(labelDate);

    data.push(row);
  }

  tableView.data = data;

});