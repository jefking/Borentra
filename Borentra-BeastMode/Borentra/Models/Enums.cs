namespace Borentra.Models
{
    #region Reference
    public enum Reference
    {
        None = 0,
        User = 1,
        Item = 2,
        ItemRequest = 3,
        UserStatus = 4,
        ItemImage = 5,
        TradeRequest = 6,
        ItemRequestImage = 7,
        Company = 8,
    }
    #endregion

    #region Borrow Status
    /// <summary>
    /// Borrow Status
    /// </summary>
    public enum BorrowStatus
    {
        Pending = 0,
        Accepted = 1,
        Returned = 2,
        Rejected = 3
    }
    #endregion

    #region Rental Status
    /// <summary>
    /// Rental Status
    /// </summary>
    public enum RentalStatus
    {
        Pending = 0,
        Accepted = 1,
        Returned = 2,
        Rejected = 3
    }
    #endregion

    #region Request Status
    public enum RequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
    #endregion

    #region Free Status
    /// <summary>
    /// Free Status
    /// </summary>
    public enum FreeStatus
    {
        Pending = 0,
        Accepted = 1,
        Declined = 2
    }
    #endregion

    #region User Context
    /// <summary>
    /// User Context
    /// </summary>
    public enum UserContext
    {
        Unknown = 0,
        Mine = 1,
        Friend = 2,
        Nearby = 3
    }
    #endregion

    #region Social Networks
    public enum SocialNetworks
    {
        Unknown = 0,
        Facebook = 1,
    }
    #endregion

    #region Offer Type
    public enum OfferType
    {
        Unknown = 0,
        Free = 1,
        Share = 2,
        Trade = 3,
        Rent = 4,
    }
    #endregion

    #region Rental Units
    public enum RentalUnit
    {
        Unknown = 0,
        PerDay = 1,
    }
    #endregion

    #region Mobile OS
    public enum MobileOS
    {
        Unknown = 0,
        iOS = 1,
        Android = 2,
        WindowsPhone = 3,
        Windows8 = 4,
        Blackberry = 5,
    }
    #endregion

    #region Market Tests
    public enum MarketTest
    {
        Unknown = 0,
        Group = 2,
        Company = 1
    }
    #endregion
}