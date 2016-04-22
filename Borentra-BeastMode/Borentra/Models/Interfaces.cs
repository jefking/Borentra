namespace Borentra.Models
{
    using System;
    using System.Collections.Generic;

    #region IWebEntity
    public interface IWebEntity : IKey, ILocation, IDataContext, IIdentifier
    {
    }
    #endregion

    #region IIdentifier
    public interface IIdentifier
    {
        Guid Identifier
        {
            get;
        }
    }
    #endregion

    #region IUserIdentifier
    public interface IUserIdentifier
    {
        Guid UserIdentifier
        {
            get;
        }
    }
    #endregion

    #region IKey
    public interface IKey
    {
        string Key
        {
            get;
        }
    }
    #endregion

    #region ILocation
    public interface ILocation
    {
        string Location
        {
            get;
        }

        double Latitude
        {
            get;
        }

        double Longitude
        {
            get;
        }
    }
    #endregion

    #region IDataContext
    public interface IDataContext
    {
        bool IsMine
        {
            get;
        }

        bool IsFriend
        {
            get;
        }

        bool IsPublic
        {
            get;
        }
    }
    #endregion

    #region IContent
    public interface IContent
    {
        #region Properties
        string Title
        {
            get;
        }
        string Description
        {
            get;
        }
        #endregion
    }
    #endregion

    #region IDescriptive
    public interface IDescriptive : IContent
    {
        #region Properties
        string Tags
        {
            get;
        }
        IEnumerable<string> Categories
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region IRental
    public interface IRental
    {
        #region Properties
        decimal Price
        {
            get;
        }

        RentalUnit PerUnit
        {
            get;
        }
        #endregion
    }
    #endregion

    #region IFacebookIdentity
    public interface IFacebookIdentity
    {
        long FacebookId
        {
            get;
        }
    }
    #endregion

    #region IFacebookAccess
    public interface IFacebookAccess
    {
        /// <summary>
        /// Facebook Access Token
        /// </summary>
        string FacebookAccessToken
        {
            get;
        }

        DateTime FacebookTokenExpiration
        {
            get;
        }
    }
    #endregion

    #region IFacebookEntity
    public interface IFacebookEntity : IFacebookAccess, IFacebookIdentity
    {

    }
    #endregion

    #region IReferenceType
    public interface IReferenceType
    {
        Reference Type
        {
            get;
        }
    }
    #endregion

    #region IComment
    public interface IComment
    {
        #region Properties
        string Comment
        {
            get;
        }
        #endregion
    }
    #endregion
}