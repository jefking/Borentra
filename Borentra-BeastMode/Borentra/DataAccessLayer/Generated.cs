namespace Borentra.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Class that Represents Admin.AddToProfileStats Stored Procedure
    /// </summary>
	public partial class AdminAddToProfileStats : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.AddToProfileStats";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminAddToProfileStats.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.Archive Stored Procedure
    /// </summary>
	public partial class AdminArchive : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.Archive";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminArchive.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.Borrow Stored Procedure
    /// </summary>
	public partial class GoodsBorrow : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.Borrow";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsBorrow.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OnParameter = "@On";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OnParameter, DbType.DateTime)]
		public DateTime? On
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UntilParameter = "@Until";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UntilParameter, DbType.DateTime)]
		public DateTime? Until
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.BorrowAccept Stored Procedure
    /// </summary>
	public partial class GoodsBorrowAccept : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.BorrowAccept";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsBorrowAccept.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OnParameter = "@On";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OnParameter, DbType.DateTime)]
		public DateTime? On
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UntilParameter = "@Until";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UntilParameter, DbType.DateTime)]
		public DateTime? Until
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.BorrowDelete Stored Procedure
    /// </summary>
	public partial class GoodsBorrowDelete : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.BorrowDelete";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsBorrowDelete.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.BorrowReject Stored Procedure
    /// </summary>
	public partial class GoodsBorrowReject : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.BorrowReject";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsBorrowReject.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.BorrowReturn Stored Procedure
    /// </summary>
	public partial class GoodsBorrowReturn : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.BorrowReturn";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsBorrowReturn.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReturnedOnParameter = "@ReturnedOn";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReturnedOnParameter, DbType.DateTime)]
		public DateTime? ReturnedOn
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.CommunityFree Stored Procedure
    /// </summary>
	public partial class StatsCommunityFree : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.CommunityFree";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsCommunityFree.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.CommunityRent Stored Procedure
    /// </summary>
	public partial class StatsCommunityRent : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.CommunityRent";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsCommunityRent.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.CommunityShare Stored Procedure
    /// </summary>
	public partial class StatsCommunityShare : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.CommunityShare";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsCommunityShare.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.CommunityTrade Stored Procedure
    /// </summary>
	public partial class StatsCommunityTrade : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.CommunityTrade";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsCommunityTrade.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.CountryPercentages Stored Procedure
    /// </summary>
	public partial class StatsCountryPercentages : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.CountryPercentages";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsCountryPercentages.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Social.CreateBadge Stored Procedure
    /// </summary>
	public partial class SocialCreateBadge : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.CreateBadge";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialCreateBadge.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DescriptionParameter = "@Description";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DescriptionParameter, DbType.String)]
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IconNameParameter = "@IconName";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IconNameParameter, DbType.String)]
		public string IconName
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Byte)]
		public byte? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PointsParameter = "@Points";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PointsParameter, DbType.Byte)]
		public byte? Points
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TitleParameter = "@Title";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TitleParameter, DbType.String)]
		public string Title
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.CreateBadges Stored Procedure
    /// </summary>
	public partial class SocialCreateBadges : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.CreateBadges";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialCreateBadges.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.Dashboard Stored Procedure
    /// </summary>
	public partial class StatsDashboard : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.Dashboard";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsDashboard.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.DeleteProfile Stored Procedure
    /// </summary>
	public partial class UserDeleteProfile : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.DeleteProfile";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserDeleteProfile.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.DeviceByMonth Stored Procedure
    /// </summary>
	public partial class StatsDeviceByMonth : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.DeviceByMonth";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsDeviceByMonth.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.FindItem Stored Procedure
    /// </summary>
	public partial class AdminFindItem : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.FindItem";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminFindItem.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.FindItemRequest Stored Procedure
    /// </summary>
	public partial class AdminFindItemRequest : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.FindItemRequest";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminFindItemRequest.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.FindProfile Stored Procedure
    /// </summary>
	public partial class AdminFindProfile : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.FindProfile";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminFindProfile.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.FulfillAccept Stored Procedure
    /// </summary>
	public partial class GoodsFulfillAccept : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.FulfillAccept";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsFulfillAccept.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.FulfillDecline Stored Procedure
    /// </summary>
	public partial class GoodsFulfillDecline : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.FulfillDecline";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsFulfillDecline.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.FulfillDelete Stored Procedure
    /// </summary>
	public partial class GoodsFulfillDelete : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.FulfillDelete";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsFulfillDelete.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GenerateBadges Stored Procedure
    /// </summary>
	public partial class AdminGenerateBadges : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GenerateBadges";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGenerateBadges.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GenerateForProfile Stored Procedure
    /// </summary>
	public partial class AdminGenerateForProfile : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GenerateForProfile";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGenerateForProfile.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GenerateSocialData Stored Procedure
    /// </summary>
	public partial class AdminGenerateSocialData : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GenerateSocialData";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGenerateSocialData.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string MinutesAgoParameter = "@MinutesAgo";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(MinutesAgoParameter, DbType.Int16)]
		public short? MinutesAgo
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Device.GetDevice Stored Procedure
    /// </summary>
	public partial class DeviceGetDevice : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Device.GetDevice";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return DeviceGetDevice.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.GetItem Stored Procedure
    /// </summary>
	public partial class GoodsGetItem : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.GetItem";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsGetItem.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyParameter = "@Key";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyParameter, DbType.String)]
		public string Key
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.GetItemRequest Stored Procedure
    /// </summary>
	public partial class GoodsGetItemRequest : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.GetItemRequest";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsGetItemRequest.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyParameter = "@Key";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyParameter, DbType.String)]
		public string Key
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GetItemRequestUsers Stored Procedure
    /// </summary>
	public partial class AdminGetItemRequestUsers : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GetItemRequestUsers";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGetItemRequestUsers.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GetItemUsers Stored Procedure
    /// </summary>
	public partial class AdminGetItemUsers : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GetItemUsers";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGetItemUsers.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.GetProfile Stored Procedure
    /// </summary>
	public partial class UserGetProfile : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.GetProfile";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserGetProfile.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyParameter = "@Key";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyParameter, DbType.String)]
		public string Key
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GetProfileUsers Stored Procedure
    /// </summary>
	public partial class AdminGetProfileUsers : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GetProfileUsers";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGetProfileUsers.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.GetSearchableContent Stored Procedure
    /// </summary>
	public partial class AdminGetSearchableContent : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.GetSearchableContent";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminGetSearchableContent.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Device.InvalidateDeviceKey Stored Procedure
    /// </summary>
	public partial class DeviceInvalidateDeviceKey : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Device.InvalidateDeviceKey";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return DeviceInvalidateDeviceKey.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.ItemOwnerChange Stored Procedure
    /// </summary>
	public partial class GoodsItemOwnerChange : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.ItemOwnerChange";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsItemOwnerChange.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.ItemRequestToItem Stored Procedure
    /// </summary>
	public partial class GoodsItemRequestToItem : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.ItemRequestToItem";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsItemRequestToItem.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemRequestIdentifierParameter = "@ItemRequestIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemRequestIdentifierParameter, DbType.Guid)]
		public Guid? ItemRequestIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.ItemsByMonth Stored Procedure
    /// </summary>
	public partial class StatsItemsByMonth : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.ItemsByMonth";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsItemsByMonth.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.LandingConverstions Stored Procedure
    /// </summary>
	public partial class StatsLandingConverstions : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.LandingConverstions";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsLandingConverstions.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DaysParameter = "@Days";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DaysParameter, DbType.Byte)]
		public byte? Days
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.LatestDevice Stored Procedure
    /// </summary>
	public partial class StatsLatestDevice : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.LatestDevice";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsLatestDevice.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DaysParameter = "@Days";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DaysParameter, DbType.Byte)]
		public byte? Days
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.LatestItems Stored Procedure
    /// </summary>
	public partial class StatsLatestItems : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.LatestItems";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsLatestItems.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DaysParameter = "@Days";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DaysParameter, DbType.Byte)]
		public byte? Days
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.LatestUsers Stored Procedure
    /// </summary>
	public partial class StatsLatestUsers : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.LatestUsers";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsLatestUsers.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DaysParameter = "@Days";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DaysParameter, DbType.Byte)]
		public byte? Days
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.LeaderBoard Stored Procedure
    /// </summary>
	public partial class StatsLeaderBoard : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.LeaderBoard";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsLeaderBoard.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.LocationByIpRange Stored Procedure
    /// </summary>
	public partial class AdminLocationByIpRange : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.LocationByIpRange";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminLocationByIpRange.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Geo.LocationByIpRange Stored Procedure
    /// </summary>
	public partial class GeoLocationByIpRange : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Geo.LocationByIpRange";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GeoLocationByIpRange.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.MyHistory Stored Procedure
    /// </summary>
	public partial class GoodsMyHistory : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.MyHistory";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsMyHistory.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.RecentItemShare Stored Procedure
    /// </summary>
	public partial class GoodsRecentItemShare : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.RecentItemShare";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsRecentItemShare.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.RentAccept Stored Procedure
    /// </summary>
	public partial class GoodsRentAccept : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.RentAccept";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsRentAccept.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.RentDelete Stored Procedure
    /// </summary>
	public partial class GoodsRentDelete : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.RentDelete";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsRentDelete.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.RentReject Stored Procedure
    /// </summary>
	public partial class GoodsRentReject : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.RentReject";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsRentReject.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.RentRequest Stored Procedure
    /// </summary>
	public partial class GoodsRentRequest : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.RentRequest";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsRentRequest.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OnParameter = "@On";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OnParameter, DbType.DateTime)]
		public DateTime? On
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UntilParameter = "@Until";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UntilParameter, DbType.DateTime)]
		public DateTime? Until
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.RentReturn Stored Procedure
    /// </summary>
	public partial class GoodsRentReturn : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.RentReturn";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsRentReturn.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReturnedOnParameter = "@ReturnedOn";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReturnedOnParameter, DbType.DateTime)]
		public DateTime? ReturnedOn
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.SaveActivity Stored Procedure
    /// </summary>
	public partial class UserSaveActivity : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.SaveActivity";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserSaveActivity.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TextParameter = "@Text";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TextParameter, DbType.String)]
		public string Text
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TypeParameter = "@Type";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TypeParameter, DbType.Byte)]
		public byte? Type
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveComment Stored Procedure
    /// </summary>
	public partial class SocialSaveComment : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveComment";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveComment.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Company.SaveCompany Stored Procedure
    /// </summary>
	public partial class CompanySaveCompany : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Company.SaveCompany";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return CompanySaveCompany.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string BannerPathParameter = "@BannerPath";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(BannerPathParameter, DbType.String)]
		public string BannerPath
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DescriptionParameter = "@Description";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DescriptionParameter, DbType.String)]
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LogoPathParameter = "@LogoPath";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LogoPathParameter, DbType.String)]
		public string LogoPath
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PhoneNumberParameter = "@PhoneNumber";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PhoneNumberParameter, DbType.String)]
		public string PhoneNumber
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string WebsiteUrlParameter = "@WebsiteUrl";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(WebsiteUrlParameter, DbType.String)]
		public string WebsiteUrl
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Company.SaveCompanyAdministrator Stored Procedure
    /// </summary>
	public partial class CompanySaveCompanyAdministrator : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Company.SaveCompanyAdministrator";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return CompanySaveCompanyAdministrator.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CompanyIdentifierParameter = "@CompanyIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CompanyIdentifierParameter, DbType.Guid)]
		public Guid? CompanyIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Company.SaveCompanyItemRent Stored Procedure
    /// </summary>
	public partial class CompanySaveCompanyItemRent : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Company.SaveCompanyItemRent";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return CompanySaveCompanyItemRent.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemRentIdentifierParameter = "@ItemRentIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemRentIdentifierParameter, DbType.Guid)]
		public Guid? ItemRentIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string QuantityParameter = "@Quantity";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(QuantityParameter, DbType.Byte)]
		public byte? Quantity
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Test.SaveCompanySignUp Stored Procedure
    /// </summary>
	public partial class TestSaveCompanySignUp : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Test.SaveCompanySignUp";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return TestSaveCompanySignUp.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string EmailParameter = "@Email";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(EmailParameter, DbType.String)]
		public string Email
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string NameParameter = "@Name";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(NameParameter, DbType.String)]
		public string Name
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveConnection Stored Procedure
    /// </summary>
	public partial class SocialSaveConnection : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveConnection";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveConnection.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ConnectionIdentifierParameter = "@ConnectionIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ConnectionIdentifierParameter, DbType.Guid)]
		public Guid? ConnectionIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string NetworkTypeParameter = "@NetworkType";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(NetworkTypeParameter, DbType.Byte)]
		public byte? NetworkType
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OwnerIdentifierParameter = "@OwnerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OwnerIdentifierParameter, DbType.Guid)]
		public Guid? OwnerIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveContact Stored Procedure
    /// </summary>
	public partial class SocialSaveContact : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveContact";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveContact.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string EmailParameter = "@Email";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(EmailParameter, DbType.String)]
		public string Email
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FirstNameParameter = "@FirstName";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FirstNameParameter, DbType.String)]
		public string FirstName
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string InviteParameter = "@Invite";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(InviteParameter, DbType.Boolean)]
		public bool? Invite
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LastNameParameter = "@LastName";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LastNameParameter, DbType.String)]
		public string LastName
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PhoneNumberParameter = "@PhoneNumber";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PhoneNumberParameter, DbType.String)]
		public string PhoneNumber
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveConversation Stored Procedure
    /// </summary>
	public partial class SocialSaveConversation : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveConversation";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveConversation.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ParentConversationIdentifierParameter = "@ParentConversationIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ParentConversationIdentifierParameter, DbType.Guid)]
		public Guid? ParentConversationIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReadParameter = "@Read";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReadParameter, DbType.Boolean)]
		public bool? Read
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ToUserIdentifierParameter = "@ToUserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ToUserIdentifierParameter, DbType.Guid)]
		public Guid? ToUserIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Device.SaveDevice Stored Procedure
    /// </summary>
	public partial class DeviceSaveDevice : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Device.SaveDevice";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return DeviceSaveDevice.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string AmplitudeParameter = "@Amplitude";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(AmplitudeParameter, DbType.Int32)]
		public int? Amplitude
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string AngularFrequencyParameter = "@AngularFrequency";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(AngularFrequencyParameter, DbType.Int32)]
		public int? AngularFrequency
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeviceIdentifierParameter = "@DeviceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeviceIdentifierParameter, DbType.Guid)]
		public Guid? DeviceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookAccessTokenParameter = "@FacebookAccessToken";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookAccessTokenParameter, DbType.String)]
		public string FacebookAccessToken
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookIdParameter = "@FacebookId";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookIdParameter, DbType.Int64)]
		public long? FacebookId
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookIsValidatedParameter = "@FacebookIsValidated";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookIsValidatedParameter, DbType.Boolean)]
		public bool? FacebookIsValidated
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookTokenExpirationParameter = "@FacebookTokenExpiration";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookTokenExpirationParameter, DbType.DateTime)]
		public DateTime? FacebookTokenExpiration
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IpAddressParameter = "@IpAddress";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IpAddressParameter, DbType.String)]
		public string IpAddress
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyExpiresOnParameter = "@KeyExpiresOn";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyExpiresOnParameter, DbType.DateTime)]
		public DateTime? KeyExpiresOn
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LastValidatedOnParameter = "@LastValidatedOn";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LastValidatedOnParameter, DbType.DateTime)]
		public DateTime? LastValidatedOn
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OsParameter = "@Os";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OsParameter, DbType.Byte)]
		public byte? Os
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PhaseShiftParameter = "@PhaseShift";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PhaseShiftParameter, DbType.Int32)]
		public int? PhaseShift
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string VerticalOffsetParameter = "@VerticalOffset";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(VerticalOffsetParameter, DbType.Int32)]
		public int? VerticalOffset
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveFacebookConnection Stored Procedure
    /// </summary>
	public partial class SocialSaveFacebookConnection : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveFacebookConnection";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveFacebookConnection.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookIdParameter = "@FacebookId";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookIdParameter, DbType.Int64)]
		public long? FacebookId
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OwnerIdentifierParameter = "@OwnerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OwnerIdentifierParameter, DbType.Guid)]
		public Guid? OwnerIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveFavorite Stored Procedure
    /// </summary>
	public partial class SocialSaveFavorite : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveFavorite";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveFavorite.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Test.SaveGroupSignUp Stored Procedure
    /// </summary>
	public partial class TestSaveGroupSignUp : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Test.SaveGroupSignUp";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return TestSaveGroupSignUp.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string EmailParameter = "@Email";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(EmailParameter, DbType.String)]
		public string Email
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string NameParameter = "@Name";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(NameParameter, DbType.String)]
		public string Name
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItem Stored Procedure
    /// </summary>
	public partial class GoodsSaveItem : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItem";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItem.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DescriptionParameter = "@Description";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DescriptionParameter, DbType.String)]
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FreePrivacyLevelParameter = "@FreePrivacyLevel";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FreePrivacyLevelParameter, DbType.Byte)]
		public byte? FreePrivacyLevel
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RentPrivacyLevelParameter = "@RentPrivacyLevel";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RentPrivacyLevelParameter, DbType.Byte)]
		public byte? RentPrivacyLevel
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string SharePrivacyLevelParameter = "@SharePrivacyLevel";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(SharePrivacyLevelParameter, DbType.Byte)]
		public byte? SharePrivacyLevel
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TitleParameter = "@Title";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TitleParameter, DbType.String)]
		public string Title
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TradePrivacyLevelParameter = "@TradePrivacyLevel";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TradePrivacyLevelParameter, DbType.Byte)]
		public byte? TradePrivacyLevel
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemActionComment Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemActionComment : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemActionComment";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemActionComment.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemActionIdentifierParameter = "@ItemActionIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemActionIdentifierParameter, DbType.Guid)]
		public Guid? ItemActionIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemFree Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemFree : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemFree";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemFree.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string StatusParameter = "@Status";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(StatusParameter, DbType.Byte)]
		public byte? Status
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemImage Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemImage : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemImage";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemImage.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ContentTypeParameter = "@ContentType";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ContentTypeParameter, DbType.String)]
		public string ContentType
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FileNameParameter = "@FileName";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FileNameParameter, DbType.String)]
		public string FileName
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FileSizeParameter = "@FileSize";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FileSizeParameter, DbType.Int32)]
		public int? FileSize
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IsPrimaryParameter = "@IsPrimary";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IsPrimaryParameter, DbType.Boolean)]
		public bool? IsPrimary
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PathParameter = "@Path";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PathParameter, DbType.String)]
		public string Path
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemRent Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemRent : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemRent";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemRent.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PerUnitParameter = "@PerUnit";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PerUnitParameter, DbType.Byte)]
		public byte? PerUnit
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PriceParameter = "@Price";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PriceParameter, DbType.Currency)]
		public decimal? Price
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemRenting Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemRenting : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemRenting";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemRenting.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OnParameter = "@On";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OnParameter, DbType.DateTime)]
		public DateTime? On
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReturnedOnParameter = "@ReturnedOn";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReturnedOnParameter, DbType.DateTime)]
		public DateTime? ReturnedOn
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string StatusParameter = "@Status";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(StatusParameter, DbType.Byte)]
		public byte? Status
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UntilParameter = "@Until";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UntilParameter, DbType.DateTime)]
		public DateTime? Until
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemRequest Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemRequest : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemRequest";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemRequest.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DescriptionParameter = "@Description";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DescriptionParameter, DbType.String)]
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ForFreeParameter = "@ForFree";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ForFreeParameter, DbType.Boolean)]
		public bool? ForFree
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ForRentParameter = "@ForRent";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ForRentParameter, DbType.Boolean)]
		public bool? ForRent
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ForShareParameter = "@ForShare";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ForShareParameter, DbType.Boolean)]
		public bool? ForShare
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ForTradeParameter = "@ForTrade";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ForTradeParameter, DbType.Boolean)]
		public bool? ForTrade
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TitleParameter = "@Title";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TitleParameter, DbType.String)]
		public string Title
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemRequestFulfill Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemRequestFulfill : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemRequestFulfill";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemRequestFulfill.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemRequestIdentifierParameter = "@ItemRequestIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemRequestIdentifierParameter, DbType.Guid)]
		public Guid? ItemRequestIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string StatusParameter = "@Status";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(StatusParameter, DbType.Byte)]
		public byte? Status
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string WillGiveParameter = "@WillGive";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(WillGiveParameter, DbType.Boolean)]
		public bool? WillGive
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string WillRentParameter = "@WillRent";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(WillRentParameter, DbType.Boolean)]
		public bool? WillRent
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string WillShareParameter = "@WillShare";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(WillShareParameter, DbType.Boolean)]
		public bool? WillShare
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string WillTradeParameter = "@WillTrade";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(WillTradeParameter, DbType.Boolean)]
		public bool? WillTrade
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemRequestImage Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemRequestImage : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemRequestImage";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemRequestImage.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ContentTypeParameter = "@ContentType";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ContentTypeParameter, DbType.String)]
		public string ContentType
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FileNameParameter = "@FileName";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FileNameParameter, DbType.String)]
		public string FileName
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FileSizeParameter = "@FileSize";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FileSizeParameter, DbType.Int32)]
		public int? FileSize
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IsPrimaryParameter = "@IsPrimary";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IsPrimaryParameter, DbType.Boolean)]
		public bool? IsPrimary
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemRequestIdentifierParameter = "@ItemRequestIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemRequestIdentifierParameter, DbType.Guid)]
		public Guid? ItemRequestIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PathParameter = "@Path";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PathParameter, DbType.String)]
		public string Path
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SaveItemShare Stored Procedure
    /// </summary>
	public partial class GoodsSaveItemShare : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SaveItemShare";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSaveItemShare.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CommentParameter = "@Comment";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CommentParameter, DbType.String)]
		public string Comment
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OnParameter = "@On";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OnParameter, DbType.DateTime)]
		public DateTime? On
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReturnedOnParameter = "@ReturnedOn";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReturnedOnParameter, DbType.DateTime)]
		public DateTime? ReturnedOn
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string StatusParameter = "@Status";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(StatusParameter, DbType.Byte)]
		public byte? Status
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UntilParameter = "@Until";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UntilParameter, DbType.DateTime)]
		public DateTime? Until
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.SaveProfile Stored Procedure
    /// </summary>
	public partial class UserSaveProfile : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.SaveProfile";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserSaveProfile.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DisplayNameParameter = "@DisplayName";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DisplayNameParameter, DbType.String)]
		public string DisplayName
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string EmailParameter = "@Email";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(EmailParameter, DbType.String)]
		public string Email
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookAccessTokenParameter = "@FacebookAccessToken";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookAccessTokenParameter, DbType.String)]
		public string FacebookAccessToken
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookIdParameter = "@FacebookId";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookIdParameter, DbType.Int64)]
		public long? FacebookId
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FacebookTokenExpirationParameter = "@FacebookTokenExpiration";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FacebookTokenExpirationParameter, DbType.DateTime)]
		public DateTime? FacebookTokenExpiration
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentityProviderParameter = "@IdentityProvider";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentityProviderParameter, DbType.String)]
		public string IdentityProvider
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IpAddressParameter = "@IpAddress";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IpAddressParameter, DbType.String)]
		public string IpAddress
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LatitudeParameter = "@Latitude";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LatitudeParameter, DbType.Double)]
		public double? Latitude
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LocationParameter = "@Location";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LocationParameter, DbType.String)]
		public string Location
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LongitudeParameter = "@Longitude";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LongitudeParameter, DbType.Double)]
		public double? Longitude
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string PrivacyLevelParameter = "@PrivacyLevel";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(PrivacyLevelParameter, DbType.Byte)]
		public byte? PrivacyLevel
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string SearchRadiusParameter = "@SearchRadius";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(SearchRadiusParameter, DbType.Int32)]
		public int? SearchRadius
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string StatusParameter = "@Status";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(StatusParameter, DbType.String)]
		public string Status
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.SaveProfileArchive Stored Procedure
    /// </summary>
	public partial class UserSaveProfileArchive : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.SaveProfileArchive";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserSaveProfileArchive.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string LandingThemeParameter = "@LandingTheme";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(LandingThemeParameter, DbType.String)]
		public string LandingTheme
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveRating Stored Procedure
    /// </summary>
	public partial class SocialSaveRating : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveRating";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveRating.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string DeleteParameter = "@Delete";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(DeleteParameter, DbType.Boolean)]
		public bool? Delete
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RatingParameter = "@Rating";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RatingParameter, DbType.Byte)]
		public byte? Rating
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SaveTags Stored Procedure
    /// </summary>
	public partial class SocialSaveTags : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SaveTags";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSaveTags.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TagsParameter = "@Tags";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TagsParameter, DbType.String)]
		public string Tags
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.SearchActivity Stored Procedure
    /// </summary>
	public partial class UserSearchActivity : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.SearchActivity";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserSearchActivity.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string MaximumParameter = "@Maximum";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(MaximumParameter, DbType.Byte)]
		public byte? Maximum
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchBadgeInformation Stored Procedure
    /// </summary>
	public partial class SocialSearchBadgeInformation : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchBadgeInformation";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchBadgeInformation.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchBadges Stored Procedure
    /// </summary>
	public partial class SocialSearchBadges : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchBadges";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchBadges.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchComment Stored Procedure
    /// </summary>
	public partial class SocialSearchComment : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchComment";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchComment.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Company.SearchCompany Stored Procedure
    /// </summary>
	public partial class CompanySearchCompany : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Company.SearchCompany";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return CompanySearchCompany.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyParameter = "@Key";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyParameter, DbType.String)]
		public string Key
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchConnection Stored Procedure
    /// </summary>
	public partial class SocialSearchConnection : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchConnection";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchConnection.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ConnectionIdentifierParameter = "@ConnectionIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ConnectionIdentifierParameter, DbType.Guid)]
		public Guid? ConnectionIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OwnerIdentifierParameter = "@OwnerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OwnerIdentifierParameter, DbType.Guid)]
		public Guid? OwnerIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchConversation Stored Procedure
    /// </summary>
	public partial class SocialSearchConversation : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchConversation";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchConversation.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchFavorite Stored Procedure
    /// </summary>
	public partial class SocialSearchFavorite : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchFavorite";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchFavorite.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchForRss Stored Procedure
    /// </summary>
	public partial class GoodsSearchForRss : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchForRss";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchForRss.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ResultTypeParameter = "@ResultType";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ResultTypeParameter, DbType.Byte)]
		public byte? ResultType
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string WithImagesParameter = "@WithImages";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(WithImagesParameter, DbType.Boolean)]
		public bool? WithImages
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItem Stored Procedure
    /// </summary>
	public partial class GoodsSearchItem : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItem";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItem.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string FilterFriendsOnlyParameter = "@FilterFriendsOnly";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(FilterFriendsOnlyParameter, DbType.Boolean)]
		public bool? FilterFriendsOnly
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeywordParameter = "@Keyword";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeywordParameter, DbType.String)]
		public string Keyword
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ShareTypeParameter = "@ShareType";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ShareTypeParameter, DbType.Byte)]
		public byte? ShareType
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemActionComment Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemActionComment : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemActionComment";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemActionComment.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemActionIdentifierParameter = "@ItemActionIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemActionIdentifierParameter, DbType.Guid)]
		public Guid? ItemActionIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemFree Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemFree : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemFree";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemFree.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OwnerIdentifierParameter = "@OwnerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OwnerIdentifierParameter, DbType.Guid)]
		public Guid? OwnerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RequesterIdentifierParameter = "@RequesterIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RequesterIdentifierParameter, DbType.Guid)]
		public Guid? RequesterIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemImage Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemImage : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemImage";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemImage.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemRent Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemRent : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemRent";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemRent.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemRenting Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemRenting : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemRenting";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemRenting.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OwnerIdentifierParameter = "@OwnerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OwnerIdentifierParameter, DbType.Guid)]
		public Guid? OwnerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RequesterIdentifierParameter = "@RequesterIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RequesterIdentifierParameter, DbType.Guid)]
		public Guid? RequesterIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemRequest Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemRequest : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemRequest";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemRequest.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyParameter = "@Key";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyParameter, DbType.String)]
		public string Key
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeywordParameter = "@Keyword";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeywordParameter, DbType.String)]
		public string Keyword
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemRequestFulfill Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemRequestFulfill : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemRequestFulfill";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemRequestFulfill.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemRequestImage Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemRequestImage : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemRequestImage";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemRequestImage.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemRequestIdentifierParameter = "@ItemRequestIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemRequestIdentifierParameter, DbType.Guid)]
		public Guid? ItemRequestIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemShare Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemShare : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemShare";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemShare.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifierParameter = "@ItemIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifierParameter, DbType.Guid)]
		public Guid? ItemIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string OwnerIdentifierParameter = "@OwnerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(OwnerIdentifierParameter, DbType.Guid)]
		public Guid? OwnerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RequesterIdentifierParameter = "@RequesterIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RequesterIdentifierParameter, DbType.Guid)]
		public Guid? RequesterIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchItemTrade Stored Procedure
    /// </summary>
	public partial class GoodsSearchItemTrade : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchItemTrade";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchItemTrade.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReceiverIdentifierParameter = "@ReceiverIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReceiverIdentifierParameter, DbType.Guid)]
		public Guid? ReceiverIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RequesterIdentifierParameter = "@RequesterIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RequesterIdentifierParameter, DbType.Guid)]
		public Guid? RequesterIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TradeIdentifierParameter = "@TradeIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TradeIdentifierParameter, DbType.Guid)]
		public Guid? TradeIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.SearchProfile Stored Procedure
    /// </summary>
	public partial class UserSearchProfile : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.SearchProfile";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserSearchProfile.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string CallerIdentifierParameter = "@CallerIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(CallerIdentifierParameter, DbType.Guid)]
		public Guid? CallerIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ConnectionIdentifierParameter = "@ConnectionIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ConnectionIdentifierParameter, DbType.Guid)]
		public Guid? ConnectionIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeyParameter = "@Key";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeyParameter, DbType.String)]
		public string Key
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string KeywordParameter = "@Keyword";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(KeywordParameter, DbType.String)]
		public string Keyword
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RadiusParameter = "@Radius";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RadiusParameter, DbType.Int32)]
		public int? Radius
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Int16)]
		public short? Top
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.SearchRating Stored Procedure
    /// </summary>
	public partial class SocialSearchRating : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.SearchRating";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialSearchRating.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReferenceIdentifierParameter = "@ReferenceIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReferenceIdentifierParameter, DbType.Guid)]
		public Guid? ReferenceIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Test.SearchSignUp Stored Procedure
    /// </summary>
	public partial class TestSearchSignUp : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Test.SearchSignUp";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return TestSearchSignUp.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.SearchTrade Stored Procedure
    /// </summary>
	public partial class GoodsSearchTrade : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.SearchTrade";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsSearchTrade.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ReceiverIdentifierParameter = "@ReceiverIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ReceiverIdentifierParameter, DbType.Guid)]
		public Guid? ReceiverIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string RequesterIdentifierParameter = "@RequesterIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(RequesterIdentifierParameter, DbType.Guid)]
		public Guid? RequesterIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents User.SearchUserActivity Stored Procedure
    /// </summary>
	public partial class UserSearchUserActivity : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "User.SearchUserActivity";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return UserSearchUserActivity.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TopParameter = "@Top";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TopParameter, DbType.Byte)]
		public byte? Top
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.SetTopGeoLocation Stored Procedure
    /// </summary>
	public partial class AdminSetTopGeoLocation : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.SetTopGeoLocation";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminSetTopGeoLocation.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Admin.SetTopLocation Stored Procedure
    /// </summary>
	public partial class AdminSetTopLocation : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Admin.SetTopLocation";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return AdminSetTopLocation.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.Totals Stored Procedure
    /// </summary>
	public partial class StatsTotals : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.Totals";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsTotals.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.TradeAccept Stored Procedure
    /// </summary>
	public partial class GoodsTradeAccept : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.TradeAccept";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsTradeAccept.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TradeIdentifierParameter = "@TradeIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TradeIdentifierParameter, DbType.Guid)]
		public Guid? TradeIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.TradeDelete Stored Procedure
    /// </summary>
	public partial class GoodsTradeDelete : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.TradeDelete";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsTradeDelete.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string IdentifierParameter = "@Identifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(IdentifierParameter, DbType.Guid)]
		public Guid? Identifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.TradeReject Stored Procedure
    /// </summary>
	public partial class GoodsTradeReject : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.TradeReject";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsTradeReject.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string TradeIdentifierParameter = "@TradeIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(TradeIdentifierParameter, DbType.Guid)]
		public Guid? TradeIdentifier
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Goods.TradeRequest Stored Procedure
    /// </summary>
	public partial class GoodsTradeRequest : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Goods.TradeRequest";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return GoodsTradeRequest.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string ItemIdentifiersParameter = "@ItemIdentifiers";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(ItemIdentifiersParameter, DbType.String)]
		public string ItemIdentifiers
		{
			get;
			set;
		}

        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Social.UserNotifications Stored Procedure
    /// </summary>
	public partial class SocialUserNotifications : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Social.UserNotifications";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return SocialUserNotifications.StoredProcName;
			}
		}

		#region Parameters
        /// <summary>
        /// Named Parameter Value
        /// </summary>
		public const string UserIdentifierParameter = "@UserIdentifier";
		
        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
		[DataMapper(UserIdentifierParameter, DbType.Guid)]
		public Guid? UserIdentifier
		{
			get;
			set;
		}

		#endregion
	}

    /// <summary>
    /// Class that Represents Stats.UsersByMonth Stored Procedure
    /// </summary>
	public partial class StatsUsersByMonth : IStoredProc
	{
		#region Members
        /// <summary>
        /// Stored Proc Name
        /// </summary>
		private const string StoredProcName = "Stats.UsersByMonth";
		#endregion

        /// <summary>
        /// Gets Stored Proc name with Schema
        /// </summary>
		public string StoredProc
		{
			get
			{
				return StatsUsersByMonth.StoredProcName;
			}
		}

		#region Parameters
		#endregion
	}

}