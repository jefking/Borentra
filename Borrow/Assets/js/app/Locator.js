ServiceLocator

  // Product API
  .addService(new Service("addProduct", "/api/Product/Save"))
  .addService(new Service("getProduct", "/api/Product/Get"))
  .addService(new Service("getProductPhotos", "/api/Product/GetImages"))
  .addService(new Service("addProductPhoto", "/api/Product/SaveImage"))
  .addService(new Service("addProductPhotoUrl", "/api/Product/SaveImagebyUrl"))
  .addService(new Service("getProducts", "/api/Product/Search"))
  .addService(new Service("getProductGuid", "/api/Product/CreateGuid"))
  .addService(new Service("getSuggestedMagicProducts", "http://borentraapi.herokuapp.com/mtg/get?callback=?"))
  .addService(new Service("getSuggestedPs3Products", "http://borentraapi.herokuapp.com/ps3/get?callback=?"))
  .addService(new Service("getSuggestedXbox360Products", "http://borentraapi.herokuapp.com/xbox360/get?callback=?"))
  .addService(new Service("getSuggestedHalloweenProducts", "http://borentraapi.herokuapp.com/halloween/get?callback=?"))
  .addService(new Service("getSuggestedPokemonProducts", "http://borentraapi.herokuapp.com/pokemon/get?callback=?"))
  .addService(new Service("getSuggestedToolsProducts", "http://borentraapi.herokuapp.com/tools/get?callback=?"))
  .addService(new Service("getSuggestedBooksProducts", "http://borentraapi.herokuapp.com/books/get?callback=?"))

// Profile API
  .addService(new Service("deleteProfile", "/api/User/Delete"))
  .addService(new Service("addProfile", "/api/User/SaveProfile"))
  .addService(new Service("getProfile", "/api/User/Get"))
  .addService(new Service("getMyProfile", "/api/User/My"))
  .addService(new Service("searchProfiles", "/api/User/Search", { method: "get" }))

// Borrow API
  .addService(new Service("addBorrowRequest", "/api/Borrow/Request"))
  .addService(new Service("addBorrowAccept", "/api/Borrow/Accept"))
  .addService(new Service("addBorrowReject", "/api/Borrow/Reject"))
  .addService(new Service("addBorrowReturn", "/api/Borrow/Return"))
  .addService(new Service("addBorrowDelete", "/api/Borrow/Delete"))

// Conversations API
  .addService(new Service("addConversationMessage", "/api/Conversation/Save"))
  .addService(new Service("getConversationMessages", "/api/Conversation/Search"))
  .addService(new Service("getItemActionComments", "/api/Conversation/ItemActionComments"))

// Trade API
  .addService(new Service("addTradeRequest", "/api/Trade/Request"))
  .addService(new Service("addTradeAccept", "/api/Trade/Accept"))
  .addService(new Service("addTradeReject", "/api/Trade/Reject"))

// Free API
  .addService(new Service("getFreeRequest", "/api/Free/Request"))
  .addService(new Service("getFreeAccept", "/api/Free/Accept"))
  .addService(new Service("getFreeDecline", "/api/Free/Decline"))
  .addService(new Service("getFreeCancel", "/api/Free/Cancel"))

// Item Request API
  .addService(new Service("addItemRequestSave", "/api/ItemRequest/Save"))
  .addService(new Service("getItemRequestSearch", "/api/ItemRequest/Search"))
  .addService(new Service("addItemRequestFulfill", "/api/ItemRequest/Fulfill"))
  .addService(new Service("getItemRequestFulfillDelete", "/api/ItemRequest/Delete"))
  .addService(new Service("getItemRequestFulfillAccept", "/api/ItemRequest/Accept"))
  .addService(new Service("getItemRequestFulfillDecline", "/api/ItemRequest/Decline"))
  .addService(new Service("addItemRequestPhotoUrl", "/api/ItemRequest/SaveImagebyUrl"))
  .addService(new Service("addItemRequestPhoto", "/api/ItemRequest/SaveImage"))
  .addService(new Service("getItemRequestPhotos", "/api/ItemRequest/GetImages"))

// Search API
  .addService(new Service("getPlatformSearch", "/api/Search/General", { method: "get" }))
  .addService(new Service("getSearchProfile", "/api/Search/profile", { method: "get" }))
  .addService(new Service("getSearchOffer", "/api/Search/offer", { method: "get" }))
  .addService(new Service("getSearchRequest", "/api/Search/request", { method: "get" }))
  .addService(new Service("getPublicImageSearch", "/api/Search/PublicImages"))

// Social API
  .addService(new Service("getSearchActivity", "/api/Social/SearchActivity"))
  .addService(new Service("getMyActivity", "/api/Social/MyActivity"))
  .addService(new Service("addSaveFavorite", "/api/Social/SaveFavorite"))
  .addService(new Service("getSearchFavorite", "/api/Social/Favorites"))
  .addService(new Service("addSaveRating", "/api/Social/SaveRating"))
  .addService(new Service("getSearchRating", "/api/Social/SearchRating"))
  .addService(new Service("addSaveComment", "/api/Social/SaveComment"))
  .addService(new Service("getSearchComment", "/api/Social/Comments"))
  .addService(new Service("addSaveTags", "/api/Social/SaveTags"))
  .addService(new Service("getNotifications", "/api/Social/Notifications"))
  .addService(new Service("addNotification", "/api/Social/SaveActivity"))

// Statistics
  .addService(new Service("getGlobalHeatMapStats", "/api/Statistics/MembersByCountry"))

// Company
  .addService(new Service("addRegisterCompany", "/api/compnany/Register"))

// Group
  .addService(new Service("addRegisterGroup", "/api/group/Register"))

// Rent
  .addService(new Service("addRentReturn", "/api/rent/return"))
  .addService(new Service("addRentRequest", "/api/rent/request"))
  .addService(new Service("addRentReject", "/api/rent/reject"))
  .addService(new Service("addRentDelete", "/api/rent/delete"))
  .addService(new Service("addRentAccept", "/api/rent/accept"))
;