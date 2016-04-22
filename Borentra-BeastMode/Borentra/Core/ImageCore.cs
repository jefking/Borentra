namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataStore;
    using Borentra.Drawing;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Image Core
    /// </summary>
    public class ImageCore
    {
        #region Members
        /// <summary>
        /// Content Type
        /// </summary>
        private const string contentType = "image/jpeg";

        /// <summary>
        /// Thumbnail Name
        /// </summary>
        public const string ThumbnailName = "thumbnail";

        /// <summary>
        /// Original Name
        /// </summary>
        public const string OriginalName = "original";

        /// <summary>
        /// Large Name
        /// </summary>
        public const string LargeName = "large";
        #endregion

        #region Methods
        /// <summary>
        /// Save Image
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns>Item Image</returns>
        public ItemImage Save(ItemImageInput image)
        {
            var id = Guid.NewGuid();
            var virtualPath = string.Format("item/{0}_{1}.jpg", id, "{0}");
            var sproc = new GoodsSaveItemImage()
            {
                Identifier = id,
                ContentType = image.ContentType,
                FileName = image.FileName,
                FileSize = image.FileSize,
                ItemIdentifier = image.ItemIdentifier,
                UserIdentifier = image.UserIdentifier,
                Path = string.Format("/user/{0}", virtualPath),
            };

            var storedImage = sproc.CallObject<ItemImageInput>();

            var container = new BinaryContainer("user");
            container.Save(string.Format(virtualPath, OriginalName), image.Contents, image.ContentType);

            var thumbnail = this.Thumbnail(image.Contents, ImageFormat.Jpeg);

            var thumbnailPath = string.Format(virtualPath, ImageCore.ThumbnailName);
            container.Save(thumbnailPath, thumbnail, contentType);

            var large = this.Large(image.Contents, ImageFormat.Jpeg);

            var largePath = string.Format(virtualPath, ImageCore.LargeName);
            container.Save(largePath, large, contentType);

            var activity = new ActivityCore();
            activity.NewItemImage(storedImage);

            return new ItemImage()
            {
                VirtualPathFormat = string.Format("/user/{0}", thumbnailPath),
            };
        }

        public ItemImage Save(ItemRequestImageInput image)
        {
            var id = Guid.NewGuid();
            var virtualPath = string.Format("request/{0}_{1}.jpg", id, "{0}");
            var sproc = new GoodsSaveItemRequestImage()
            {
                Identifier = id,
                ContentType = image.ContentType,
                FileName = image.FileName,
                FileSize = image.FileSize,
                ItemRequestIdentifier = image.ItemRequestIdentifier,
                UserIdentifier = image.UserIdentifier,
                Path = string.Format("/user/{0}", virtualPath),
            };

            var storedImage = sproc.CallObject<ItemRequestImageInput>();

            var container = new BinaryContainer("user");
            container.Save(string.Format(virtualPath, OriginalName), image.Contents, image.ContentType);

            var thumbnail = this.Thumbnail(image.Contents, ImageFormat.Jpeg);

            var thumbnailPath = string.Format(virtualPath, ImageCore.ThumbnailName);
            container.Save(thumbnailPath, thumbnail, contentType);

            var large = this.Large(image.Contents, ImageFormat.Jpeg);

            var largePath = string.Format(virtualPath, ImageCore.LargeName);
            container.Save(largePath, large, contentType);

            var activity = new ActivityCore();
            activity.NewItemRequestImage(storedImage);

            return new ItemImage()
            {
                VirtualPathFormat = string.Format("/user/{0}", thumbnailPath),
            };
        }

        /// <summary>
        /// Crop Thumbnail
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="format">Format</param>
        /// <returns>Image Data</returns>
        public byte[] Thumbnail(byte[] data, ImageFormat format)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            var images = new Images();
            using (var stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                return images.ResizeByWidth(stream, format, 110).Data;
            }
        }

        /// <summary>
        /// Crop Large
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="format">Format</param>
        /// <returns>Image Data</returns>
        public byte[] Large(byte[] data, ImageFormat format)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            var images = new Images();
            using (var stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                return images.ResizeByWidth(stream, format, 960).Data;
            }
        }

        /// <summary>
        /// Save Item Image Meta Data
        /// </summary>
        /// <param name="image">Image</param>
        /// <param name="userId">User Identifier</param>
        /// <returns>Item Image</returns>
        public ItemImage SaveItem(ItemImageMetaData image, Guid userId)
        {
            if (null == image)
            {
                throw new ArgumentNullException("image");
            }

            if (Guid.Empty == image.Identifier)
            {
                throw new ArgumentException("Identifer");
            }

            if (Guid.Empty == userId)
            {
                throw new ArgumentException("Identifer");
            }

            var sproc = new GoodsSaveItemImage()
            {
                Delete = image.Delete,
                IsPrimary = image.IsPrimary,
                UserIdentifier = userId,
                Identifier = image.Identifier,
            };

            return sproc.CallObject<ItemImage>();
        }

        public ItemImage SaveItemRequest(ItemImageMetaData image, Guid userId)
        {
            if (null == image)
            {
                throw new ArgumentNullException("image");
            }

            if (Guid.Empty == image.Identifier)
            {
                throw new ArgumentException("Identifer");
            }

            if (Guid.Empty == userId)
            {
                throw new ArgumentException("Identifer");
            }

            var sproc = new GoodsSaveItemRequestImage()
            {
                Delete = image.Delete,
                IsPrimary = image.IsPrimary,
                UserIdentifier = userId,
                Identifier = image.Identifier,
            };

            return sproc.CallObject<ItemImage>();
        }

        /// <summary>
        /// Get Images
        /// </summary>
        /// <param name="itemIdentifier">Item Identifier</param>
        /// <returns>Item Images</returns>
        public IEnumerable<ItemImage> GetItemImages(Guid itemIdentifier)
        {
            if (Guid.Empty == itemIdentifier)
            {
                throw new ArgumentException("Item Identifier");
            }

            var sp = new GoodsSearchItemImage()
            {
                ItemIdentifier = itemIdentifier,
            };

            return sp.CallObjects<ItemImage>();
        }

        /// <summary>
        /// Get Wanted Images
        /// </summary>
        /// <param name="itemRequestIdentifier"></param>
        /// <returns></returns>
        public IEnumerable<ItemImage> GetItemRequestImages(Guid itemRequestIdentifier)
        {
            if (Guid.Empty == itemRequestIdentifier)
            {
                throw new ArgumentException("Item Request Identifier");
            }

            var sp = new GoodsSearchItemRequestImage()
            {
                ItemRequestIdentifier = itemRequestIdentifier,
            };

            return sp.CallObjects<ItemImage>();
        }

        /// <summary>
        /// Download
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Response</returns>
        public HttpWebResponse Download(string url)
        {
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            return (HttpWebResponse)httpWebRequest.GetResponse();
        }

        /// <summary>
        /// Thumnbnail on CDN
        /// </summary>
        /// <param name="imagePathFormat"></param>
        /// <returns></returns>
        public static string ThumbnailCdn(string imagePathFormat)
        {
            return string.IsNullOrWhiteSpace(imagePathFormat)
                ? null
                : Cdn(string.Format(imagePathFormat, ImageCore.ThumbnailName));
        }

        /// <summary>
        /// Thumbnail
        /// </summary>
        /// <param name="imagePathFormat"></param>
        /// <returns></returns>
        public static string Thumbnail(string imagePathFormat)
        {
            return string.IsNullOrWhiteSpace(imagePathFormat)
                ? null
                : string.Format(imagePathFormat, ImageCore.ThumbnailName);
        }

        /// <summary>
        /// Large on CDN
        /// </summary>
        /// <param name="imagePathFormat"></param>
        /// <returns></returns>
        public static string LargeCdn(string imagePathFormat)
        {
            return string.IsNullOrWhiteSpace(imagePathFormat)
                ? null
                : Cdn(string.Format(imagePathFormat, ImageCore.LargeName));
        }

        /// <summary>
        /// Large
        /// </summary>
        /// <param name="imagePathFormat"></param>
        /// <returns></returns>
        public static string Large(string imagePathFormat)
        {
            return string.IsNullOrWhiteSpace(imagePathFormat)
                ? null
                : string.Format(imagePathFormat, ImageCore.LargeName);
        }

        /// <summary>
        /// Original
        /// </summary>
        /// <param name="imagePathFormat"></param>
        /// <returns></returns>
        public static string Original(string imagePathFormat)
        {
            return string.IsNullOrWhiteSpace(imagePathFormat)
                ? null
                : string.Format(imagePathFormat, ImageCore.OriginalName);
        }

        /// <summary>
        /// Original
        /// </summary>
        /// <param name="imagePathFormat"></param>
        /// <returns></returns>
        public static string OriginalCdn(string imagePathFormat)
        {
            return string.IsNullOrWhiteSpace(imagePathFormat)
                ? null
                : Cdn(string.Format(imagePathFormat, ImageCore.OriginalName));
        }

        public static string Cdn(string relative)
        {
            return string.Format("http://cdn.borentra.com{0}", relative);
        }
        #endregion
    }
}