using Borentra.Models;
namespace Borentra.Web
{
    public partial class RssDescriptionTemplate
    {
        #region Properties
        public string Description
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        public string Link
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public Reference ReferenceType
        {
            get;
            set;
        }
        #endregion
    }
}