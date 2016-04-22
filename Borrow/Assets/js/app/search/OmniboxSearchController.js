(function ($) {

    var context = $(".omnibox-search-component");

    if (!context.length) { return false; }
    var userType = 1;
    var itemType = 2;
    var requestType = 3;
    var companyType = 8;

    context.find(".typeahead").typeahead([{
        header: "<div class='page-header'><h4><span class='glyphicon glyphicon-list'></span> Search Results</h4></div>",
        name: 'items',
        remote: {
            "url": ServiceLocator.getService("getPlatformSearch").getURI() + '?term=%QUERY',
            cache: true,
            beforeSend: function (jqxhr, settings) {
                console.log("Before sent request");

                self.UILoading(true);
            },
            filter: function (results) {
                self.UILoading(false);
                return results;
            }
        },
        valueKey: 'Key',
        template: [
          '<div class="media-object">',
            '<a class="pull-left" href="#">',
            '<img class="media-object" src="{{Thumbnail}}" height="50" width="50" alt="{{Title}}">',
            '</a>',
            '<div class="media-body">',
              '<h5 class="media-heading">{{Title}}</h5>',
              '<p class="text-muted">&nbsp;</p>',
            '</div>',
          '</div>'
        ].join(''),
        engine: Hogan,
        limit: 5
    }]).on("typeahead:opened", function (event, selected, source) {
        self.UILoading(false);
    }).on("typeahead:closed", function () {
        self.UILoading(false);
    }).on("typeahead:selected", function (event, selected, source) {
        var url = "/search";
        switch (selected.Type) {
            case userType:
                url = "/profile/";
                break;
            case companyType:
                url = "/company/";
                break;
            case itemType:
                url = "/offer/";
                break;
            case requestType:
                url = "/wanted/";
                break;
        }
        window.location.href = url + selected.Key;
    }).on("typeahead:autocompleted", function (event, selected, source) {
    });

    context.find("form").submit(function (event) {
        event.preventDefault();
        var searchTerm = context.find("input.typeahead").val();

        var url = "/search"
        switch (selected.Type) {
            case userType:
                url += "/member";
                break;
            case companyType:
                url += "/company";
                break;
            case itemType:
                url += "/offer";
                break;
            case requestType:
                url += "/wanted";
                break;
        }

        window.location.href = url + "?s=" + searchTerm + "&c=organic";
    });

    var self = {
        UILoading: ko.observable(false),
        results: ko.observableArray([])
    };

    ko.applyBindings(self, context[0]);

    var Initialize = function () {
        self.UILoading(false);
    };

    Initialize();

})(jQuery);