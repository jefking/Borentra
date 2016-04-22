namespace Borentra.DataAccessLayer
{
    using System;

    #region Action
    /// <summary>
    /// Action
    /// </summary>
    [Flags]
    public enum ActionFlags
    {
        Execute = 1,
        Load = 2
    }
    #endregion

    #region Privacy Level
    /// <summary>
    /// Privacy Level
    /// </summary>
    public enum PrivacyLevel
    {
        Unknown = 0,
        Public = 1,
        Community = 2,
        Friends = 3,
        Private = 4
    }
    #endregion

    #region Storage Accounts
    public enum StorageAccounts
    {
        Default = 0,
        Administrative1,
        Offsite1,
    }
    #endregion
}