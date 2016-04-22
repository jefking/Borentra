namespace Borentra.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Xml;

    /// <summary>
    /// RSS Result
    /// </summary>
    public class RssResult : ActionResult
    {
        #region Members
        /// <summary>
        /// Items
        /// </summary>
        private readonly List<IRss> items;

        /// <summary>
        /// RSS Title
        /// </summary>
        private readonly string title;

        /// <summary>
        /// RSS Description
        /// </summary>
        private readonly string description;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialises the RssResult
        /// </summary>
        /// <param name="items">The items to be added to the rss feed.</param>
        /// <param name="title">The title of the rss feed.</param>
        /// <param name="description">A short description about the rss feed.</param>
        public RssResult(IEnumerable<IRss> items, string title, string description)
        {
            this.items = new List<IRss>(items);
            this.title = title;
            this.description = description;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Execute Result
        /// </summary>
        /// <param name="context">Context</param>
        public override void ExecuteResult(ControllerContext context)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.Entitize;

            context.HttpContext.Response.ContentType = "text/xml";
            using (var writer = XmlWriter.Create(context.HttpContext.Response.OutputStream, settings))
            {
                // Begin structure
                writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");
                writer.WriteStartElement("channel");

                writer.WriteElementString("title", title);
                writer.WriteElementString("description", description);

                var copyright = string.Format("Copyright {0:yyyy}, Borentra Services Inc. The contents of this feed are available for non-commercial use only.", DateTime.UtcNow);

                writer.WriteElementString("copyright", copyright);
                writer.WriteElementString("language", "en");
                writer.WriteElementString("link", "http://www.borentra.com");
                writer.WriteStartElement("image");
                writer.WriteElementString("url", "http://cdn.borentra.com/assets/img/ui/logo-borentra-250X250.png");
                writer.WriteElementString("link", "http://www.borentra.com");
                writer.WriteElementString("width", "144");
                writer.WriteElementString("height", "144");
                writer.WriteElementString("title", title);
                writer.WriteEndElement();

                // Individual items
                items.ForEach(item =>
                {
                    var guid = string.Format("http://www.borentra.com{0}", item.Link);
                    var link = string.Format("{0}?utm_source=feed&utm_campaign=borentra&utm_medium=rss", guid);
                    writer.WriteStartElement("item");
                    writer.WriteStartElement("title");
                    switch (item.Type)
                    {
                        case Models.Reference.ItemRequest:
                            writer.WriteCData(string.Format("WANTED: {0}", item.Title));
                            break;
                        default:
                            writer.WriteCData(item.Title);
                            break;
                    }
                    writer.WriteEndElement();
                    if (!string.IsNullOrWhiteSpace(item.Description)
                        || !string.IsNullOrWhiteSpace(item.Image))
                    {
                        var template = new RssDescriptionTemplate()
                        {
                            Description = item.Description,
                            Image = item.Image,
                            Link = link,
                            Title = item.Title,
                            ReferenceType = item.Type,
                        };

                        writer.WriteStartElement("description");
                        writer.WriteCData(template.TransformText());
                        writer.WriteEndElement();
                    }
                    writer.WriteElementString("pubDate", item.PublishedOn.ToRFC822Format());
                    writer.WriteElementString("link", link);
                    writer.WriteElementString("guid", guid);
                    item.SetCategories();
                    var categories = item.Categories;
                    if (null != categories)
                    {
                        foreach (var category in categories)
                        {
                            writer.WriteElementString("category", category);
                        }
                    }
                    writer.WriteEndElement();
                });

                // End structure
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}