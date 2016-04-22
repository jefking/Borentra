namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Http;

    /// <summary>
    /// Item Request Controller
    /// </summary>
    public class ItemRequestController : ApiController
    {
        #region Members
        /// <summary>
        /// Item Request Core
        /// </summary>
        private readonly ItemRequestCore itemRequestCore = new ItemRequestCore();

        /// <summary>
        /// Image Core
        /// </summary>
        private readonly ImageCore imageCore = new ImageCore();
        #endregion

        #region Methods
        [HttpPost]
        [Authorize]
        public ItemRequest Save(ItemRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            request.UserIdentifier = User.Identifier();

            return itemRequestCore.Save(request);
        }

        [HttpPost]
        [Authorize]
        public ItemRequestFulfill Fulfill(ItemRequestFulfill fulfill)
        {
            if (null == fulfill)
            {
                throw new ArgumentNullException("fulfill");
            }

            if (Guid.Empty == fulfill.ItemRequestIdentifier)
            {
                throw new ArgumentException("Item Request Identifier");
            }

            fulfill.UserIdentifier = User.Identifier();

            return this.itemRequestCore.Save(fulfill);
        }

        //
        // GET: /api/ItemRequest/GetImages
        [HttpGet]
        public IEnumerable<ItemImage> GetImages(Guid itemIdentifier)
        {
            if (Guid.Empty == itemIdentifier)
            {
                throw new ArgumentException("itemIdentifier");
            }

            return this.imageCore.GetItemRequestImages(itemIdentifier);
        }

        [HttpGet]
        public IEnumerable<ItemRequest> Search()
        {
            return itemRequestCore.Search();
        }

        [HttpGet]
        [Authorize]
        public void Delete(Guid identifier)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            this.itemRequestCore.Delete(userId, identifier);
        }

        [HttpGet]
        [Authorize]
        public ItemRequestFulfill Decline(Guid identifier, string comment = null)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            return this.itemRequestCore.Decline(userId, identifier, comment);
        }

        [HttpGet]
        [Authorize]
        public ItemRequestFulfill Accept(Guid identifier, string comment = null)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            return this.itemRequestCore.Accept(userId, identifier, comment);
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

            return imageCore.SaveItemRequest(image, userId);
        }

        [HttpPost]
        [Authorize]
        public ItemImage SaveImagebyUrl(PublicItemRequestImage image)
        {
            if (null == image)
            {
                throw new ArgumentNullException("image");
            }

            if (Guid.Empty == image.ItemRequestIdentifier)
            {
                throw new ArgumentException("Identifer");
            }

            var userId = User.Identifier();

            var itemImage = new ItemRequestImageInput()
            {
                UserIdentifier = userId,
                ItemRequestIdentifier = image.ItemRequestIdentifier,
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

            var image = new ItemRequestImageInput()
            {
                UserIdentifier = userId,
                ItemRequestIdentifier = Guid.Parse(request.Form["Identifier"]),
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