namespace Borentra
{
    using System.Web;
    using System.Web.Optimization;

    /// <summary>
    /// Bundle Config
    /// </summary>
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                // Frameworks and Extension Libraries
                "~/Assets/bootstrap/js/bootstrap.js",
                "~/Assets/js/lib/knockout-2.3.0.js",
                "~/Assets/js/lib/underscore.js",
                "~/Assets/js/lib/Faker.js",
                "~/Assets/js/lib/moment.js",
                "~/Assets/js/lib/handlebars.js",

                "~/Assets/js/lib/load-image.js",

                "~/Assets/js/lib/bootstrap/bootstrap-modalmanager.js",
                "~/Assets/js/lib/bootstrap/bootstrap-modal.js",
                "~/Assets/js/lib/bootstrap/bootstrap-image-gallery.js",
                "~/Assets/js/lib/bootstrap/bootstrap-datepicker.js",

                "~/Assets/js/lib/twitter/typeahead.js",
                "~/Assets/js/lib/twitter/hogan.js",

                "~/Assets/js/lib/jquery/jquery.cookie.js",
                "~/Assets/js/lib/vendor/jquery.ui.widget.js",
                "~/Assets/js/lib/jquery/jquery.iframe-transport.js",
                "~/Assets/js/lib/jquery/jquery.fileupload.js",
                "~/Assets/js/lib/jquery/jquery.fileupload-fp.js",
                "~/Assets/js/lib/jquery/jquery.fileupload-ui.js",
                "~/Assets/js/lib/jquery/jquery.keybind.js",
                "~/Assets/js/lib/jquery/jquery.pubsub.js",
                "~/Assets/js/lib/jquery/jquery.dotdotdot.js",

                "~/Assets/js/lib/highcharts.js",
                "~/Assets/js/lib/ServiceLocator.js",
                "~/Assets/js/app/Locator.js",

                // Application Controllers 
                "~/Assets/js/app/item/ItemController.js",
                "~/Assets/js/app/item/ItemManagementController.js",

                "~/Assets/js/app/item/NewLendItemController.js",
                "~/Assets/js/app/item/NewFreeItemController.js",
                "~/Assets/js/app/item/ItemEditController.js",
                "~/Assets/js/app/item/UploadPhotosController.js",
                "~/Assets/js/app/item/GalleryController.js",
                "~/Assets/js/app/item/BorrowController.js",
                "~/Assets/js/app/item/BorrowAcceptController.js",
                "~/Assets/js/app/item/BorrowRejectController.js",
                "~/Assets/js/app/item/BorrowReturnController.js",
                "~/Assets/js/app/item/BorrowDeleteController.js",
                "~/Assets/js/app/item/RentController.js",
                "~/Assets/js/app/item/RentAcceptController.js",
                "~/Assets/js/app/item/RentRejectController.js",
                "~/Assets/js/app/item/RentReturnController.js",
                "~/Assets/js/app/item/RentDeleteController.js",
                "~/Assets/js/app/item/FreeController.js",
                "~/Assets/js/app/item/FreeDeleteController.js",
                "~/Assets/js/app/item/FreeRejectController.js",
                "~/Assets/js/app/item/FreeAcceptController.js",
                "~/Assets/js/app/item/TradeController.js",
                "~/Assets/js/app/item/TradeManagementController.js",
                "~/Assets/js/app/item/OfferConversationController.js",

                "~/Assets/js/app/profile/UserPlateController.js",
                "~/Assets/js/app/profile/EditProfileController.js",
                "~/Assets/js/app/profile/ProfileMessageController.js",

                //Item Request
                "~/Assets/js/app/item/ItemRequestController.js",
                "~/Assets/js/app/item/ItemRequestEditController.js",
                "~/Assets/js/app/item/ItemRequestFulfillController.js",
                "~/Assets/js/app/item/ItemRequestFulfillDeleteController.js",
                "~/Assets/js/app/item/ItemRequestFulfillDeclineController.js",
                "~/Assets/js/app/item/ItemRequestFulfillAcceptController.js",


                "~/Assets/js/app/conversation/ConversationController.js",
                "~/Assets/js/app/dashboard/NewsFeedController.js",
                "~/Assets/js/app/dashboard/MultiAddController.js",
                "~/Assets/js/app/dashboard/QuickAddController.js",

                // Search
                "~/Assets/js/app/search/OmniboxSearchController.js",


                // Companies
                "~/Assets/js/app/company/CompanyMastheadController.js",

                "~/Assets/js/app/ui/keybinds.js",

                "~/Assets/js/lib/raphael.js",
                "~/Assets/js/lib/world/MapView.js",
                "~/Assets/js/lib/world/World.js",

                "~/Assets/js/app/report/GlobalHeatMapController.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Assets/js/app/admin/UserGrowthController.js",
                "~/Assets/js/app/admin/DeviceGrowthController.js",
                "~/Assets/js/app/admin/ItemGrowthController.js"
            ));

        }
    }
}