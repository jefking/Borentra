namespace Borentra.Models
{
    using System;

    /// <summary>
    /// Image Result
    /// </summary>
    public class ImageResult
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ImageResult()
        {
        }

        /// <summary>
        /// Bing Image Result Wrapper
        /// </summary>
        /// <param name="result"></param>
        public ImageResult(Bing.ImageResult result)
        {
            if (null == result)
            {
                throw new ArgumentNullException("result");
            }

            this.Url = result.MediaUrl;
            if (null != result.Thumbnail)
            {
                this.ThumbnailUrl = result.Thumbnail.MediaUrl;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Thumbnail Url
        /// </summary>
        public string ThumbnailUrl
        {
            get;
            set;
        }
        #endregion
    }
}