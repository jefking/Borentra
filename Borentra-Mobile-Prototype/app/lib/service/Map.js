var Service = require("service/Service");
var Locator = require("service/Locator");

Map = function() {
  
  var apiUrl = "http://www.borentra.com";
  // add services
   Locator
    // Product API
    .addService(new Service("addProduct", apiUrl + "/api/Product/Save"))
    .addService(new Service("updateProduct", apiUrl + "/api/Product/Update", { method: "post" }))
    .addService(new Service("getProduct", apiUrl + "/api/Product/Get"))
    .addService(new Service("getProductPhotos", apiUrl + "/api/Product/GetImages"))
    .addService(new Service("addProductPhoto", apiUrl + "/api/Product/SaveImage"))
    .addService(new Service("addProductPhotoUrl", apiUrl + "/api/Product/SaveImagebyUrl"))
    .addService(new Service("getProducts", apiUrl + "/api/Product/Search"))

  // Profile API
    .addService(new Service("addProfile", apiUrl + "/api/User/SaveProfile"))
    .addService(new Service("getProfile", apiUrl + "/api/User/Get"))
    .addService(new Service("searchProfiles", apiUrl + "/api/User/Search", { method: "get" }))

  // Borrow API
    .addService(new Service("addBorrowRequest", apiUrl + "/api/Borrow/Request"))
    .addService(new Service("addBorrowAccept", apiUrl + "/api/Borrow/Accept"))
    .addService(new Service("addBorrowReject", apiUrl + "/api/Borrow/Reject"))
    .addService(new Service("addBorrowReturn", apiUrl + "/api/Borrow/Return"))
    .addService(new Service("addBorrowDelete", apiUrl + "/api/Borrow/Delete"))

  // Conversations API
    .addService(new Service("addConversationMessage", apiUrl + "/api/Conversation/Save"))
    .addService(new Service("getConversationMessages", apiUrl + "/api/Conversation/Search"))
    .addService(new Service("getItemActionComments", apiUrl + "/api/Conversation/ItemActionComments"))

  // Trade API
    .addService(new Service("addTradeRequest", apiUrl + "/api/Trade/Request"))
    .addService(new Service("addTradeAccept", apiUrl + "/api/Trade/Accept"))
    .addService(new Service("addTradeReject", apiUrl + "/api/Trade/Reject"))

  // Free API
    .addService(new Service("getFreeRequest", apiUrl + "/api/Free/Request"))
    .addService(new Service("getFreeAccept", apiUrl + "/api/Free/Accept"))
    .addService(new Service("getFreeDecline", apiUrl + "/api/Free/Decline"))
    .addService(new Service("getFreeCancel", apiUrl + "/api/Free/Cancel"))

  // Item Request API
    .addService(new Service("addItemRequestSave", apiUrl + "/api/ItemRequest/Save"))
    .addService(new Service("getItemRequestSearch", apiUrl + "/api/ItemRequest/Search"))
    .addService(new Service("addItemRequestFulfill", apiUrl + "/api/ItemRequest/Fulfill"))
    .addService(new Service("getItemRequestFulfillDelete", apiUrl + "/api/ItemRequest/Delete"))
    .addService(new Service("getItemRequestFulfillAccept", apiUrl + "/api/ItemRequest/Accept"))
    .addService(new Service("getItemRequestFulfillDecline", apiUrl + "/api/ItemRequest/Decline"))
    .addService(new Service("addItemRequestPhotoUrl", apiUrl + "/api/ItemRequest/SaveImagebyUrl"))
    .addService(new Service("addItemRequestPhoto", apiUrl + "/api/ItemRequest/SaveImage"))

  // Public Image Search API
    .addService(new Service("getPublicImageSearch", apiUrl + "/api/PublicImageSearch/Search"))

  // Social API
    .addService(new Service("getSearchActivity", apiUrl + "/api/MobileSocial/SearchActivity"))
    .addService(new Service("getMyActivity", apiUrl + "/api/Social/MyActivity"))
    .addService(new Service("addSaveFavorite", apiUrl + "/api/Social/SaveFavorite"))
    .addService(new Service("getSearchFavorite", apiUrl + "/api/Social/Favorites"))
    .addService(new Service("addSaveRating", apiUrl + "/api/Social/SaveRating"))
    .addService(new Service("getSearchRating", apiUrl + "/api/Social/SearchRating"))
    .addService(new Service("addSaveComment", apiUrl + "/api/Social/SaveComment"))
    .addService(new Service("getSearchComment", apiUrl + "/api/Social/Comments"))

  // Admin
    .addService(new Service("getGlobalHeatMapStats", apiUrl + "/api/Admin/GlobalStats"));


  return Locator;
};

module.exports = Map;
