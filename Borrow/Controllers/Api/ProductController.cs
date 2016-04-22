namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using Borentra.Models.DataTransferObjects;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Http;

    /// <summary>
    /// Product Controller
    /// </summary>
    public class ProductController : ApiController
    {
        #region Members
        /// <summary>
        /// Badge Core
        /// </summary>
        private readonly BadgeCore badgeCore = new BadgeCore();

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();

        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Image Core
        /// </summary>
        private readonly ImageCore imageCore = new ImageCore();

        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore emailCore = new EmailCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Rent Core
        /// </summary>
        private readonly RentCore rentCore = new RentCore();
        #endregion

        #region Methods
        //GET: /api/Product/CreateGuid

        [HttpGet]
        public GuidDTO CreateGuid()
        {
            return new GuidDTO()
            {
                Identifier = Guid.NewGuid()
            };
        }

        //
        // GET: /api/Product/Get
        [HttpGet]
        public IEnumerable<Item> Get(Guid? identifier = null, string s = null, string key = null)
        {
            var callerId = User.IdentifierSafe();

            if ((identifier.HasValue && Guid.Empty != identifier.Value)
                || !string.IsNullOrWhiteSpace(key))
            {
                var list = new List<Item>(1);
                list.Add(itemCore.GetItem(identifier, key, callerId));
                return list;
            }
            else
            {
                return itemCore.Search(null, s, null, null, callerId);
            }
        }

        //
        // GET: /api/Product/GetImages
        [HttpGet]
        public IEnumerable<ItemImage> GetImages(Guid itemIdentifier)
        {
            if (Guid.Empty == itemIdentifier)
            {
                throw new ArgumentException("itemIdentifier");
            }

            return this.imageCore.GetItemImages(itemIdentifier);
        }

        //
        // GET: /api/Product/My
        [HttpGet]
        [Authorize]
        public IEnumerable<Item> My()
        {
            return this.Get(User.Identifier(), null);
        }

        //
        // POST: /api/Product/Save
        [HttpPost]
        [Authorize]
        public Item Save(Item item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (string.IsNullOrWhiteSpace(item.Title) && !item.Delete)
            {
                throw new ArgumentException("Title");
            }

            var userId = User.Identifier();
            item.UserIdentifier = userId;

            item = itemCore.Save(item);

            return item;
        }

        //
        // GET: /api/Product/Search
        [HttpGet]
        [Authorize]
        public IEnumerable<Item> Search(Guid? user = null, string s = null, OfferType type = OfferType.Unknown)
        {
            var callerId = User.IdentifierSafe();

            return itemCore.Search(user, type, s, null, callerId);
        }

        #region Image
        //
        // POST: /api/Product/SaveImage
        [HttpPost]
        [Authorize]
        public ItemImage SaveImage(ItemImageMetaData image)
        {
            if (null == image)
            {
                throw new ArgumentNullException("image");
            }

            if (Guid.Empty == image.Identifier)
            {
                throw new ArgumentException("Identifer");
            }

            var userId = User.Identifier();

            return imageCore.SaveItem(image, userId);
        }

        [HttpPost]
        [Authorize]
        public ItemImage SaveImagebyUrl(PublicItemImage image)
        {
            if (null == image)
            {
                throw new ArgumentNullException("image");
            }

            if (Guid.Empty == image.ItemIdentifier)
            {
                throw new ArgumentException("Identifer");
            }

            var userId = User.Identifier();

            var itemImage = new ItemImageInput()
            {
                UserIdentifier = userId,
                ItemIdentifier = image.ItemIdentifier,
                FileName = image.Url,
            };

            using (var file = imageCore.Download(image.Url))
            {
                itemImage.ContentType = file.ContentType;
                using (var response = file.GetResponseStream())
                {
                    using (var ms = new MemoryStream())
                    {
                        response.CopyTo(ms);
                        itemImage.Contents = ms.ToArray();
                    }
                }
            }

            return this.imageCore.Save(itemImage);
        }

        //
        // POST: /api/Product/UploadImage
        [HttpPost]
        [Authorize]
        public ItemImage UploadImage()
        {
            var userId = User.Identifier();
            var request = HttpContext.Current.Request;

            var image = new ItemImageInput()
            {
                UserIdentifier = userId,
                ItemIdentifier = Guid.Parse(request.Form["Identifier"]),
            };

            if (request.Files.Count > 0)
            {
                //we are uploading the old way
                var file = request.Files[0];
                image.Contents = new byte[file.ContentLength];
                file.InputStream.Read(image.Contents, 0, file.ContentLength);
                image.ContentType = file.ContentType;
                image.FileName = file.FileName;
            }
            else if (request.ContentLength > 0)
            {
                // Using FileAPI the content is in Request.InputStream!!!!
                image.Contents = new byte[request.ContentLength];
                request.InputStream.Read(image.Contents, 0, request.ContentLength);
                image.FileName = request.Headers["X-File-Name"];
                image.ContentType = request.Headers["X-File-Type"];
            }

            image.FileSize = image.Contents != null ? image.Contents.Length : 0;

            return this.imageCore.Save(image);
        }
        #endregion
        #endregion
    }
}