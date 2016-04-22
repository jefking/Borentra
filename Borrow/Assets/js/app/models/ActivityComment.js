define(function () {
  var ActivityComment = function (data) {
    this.Comment = ko.observable(data.Comment);
    this.OwnerKey = ko.observable(data.OwnerKey);
    this.OwnerPicture = ko.observable(data.OwnerPicture);
    this.OwnerName = ko.observable(data.OwnerName);
    this.CreatedOn = ko.observable(data.CreatedOn);
    this.IsMine = ko.observable(data.IsMine);
    return this;
  };


  return ActivityComment;
});
