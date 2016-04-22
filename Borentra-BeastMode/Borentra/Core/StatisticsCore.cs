namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Admin;
    using Borentra.Models;
    using Borentra.Models.Admin;
    using Borentra.Models.DataTransferObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Statistics Core
    /// </summary>
    public class StatisticsCore
    {
        #region Methods
        public LeaderBoard LeaderBoard(Guid? callerId = null, byte? top = 10)
        {
            var sproc = new StatsLeaderBoard()
            {
                Top = top,
            };

            var board = new LeaderBoard()
            {
                World = sproc.CallObjects<Leader>(),
            };

            if (callerId.HasValue && Guid.Empty != callerId.Value)
            {
                sproc.CallerIdentifier = callerId.Value;
                board.Community = sproc.CallObjects<Leader>();
            }

            return board;
        }

        public IEnumerable<ItemShare> CommunityShares()
        {
            return new StatsCommunityShare().CallObjects<ItemShare>();
        }

        public IEnumerable<ItemRental> CommunityRents()
        {
            return new StatsCommunityRent().CallObjects<ItemRental>();
        }

        public IEnumerable<Trade> CommunityTrades()
        {
            var sproc = new StatsCommunityTrade();

            var data = sproc.CallObjects<ItemTradeDTO>();
            return from pt in data
                    group pt by pt.TradeIdentifier
                        into g
                    select new Trade(g);
        }

        public IEnumerable<ItemFree> CommunityFree()
        {
            return new StatsCommunityFree().CallObjects<ItemFree>();
        }

        public IEnumerable<LandingTheme> LandingPage(byte? days = 7)
        {
            var sproc = new StatsLandingConverstions()
            {
                Days = days ?? 7,
            };

            return sproc.CallObjects<LandingTheme>();
        }

        public Statistics<ItemCount> ItemGrowth(byte? days = null)
        {
            var proc = new StatsLatestItems()
            {
                Days = days ?? 14,
            };

            return new Statistics<ItemCount>()
            {
                Daily = proc.CallObjects<ItemCount>(),
                Monthly = new StatsItemsByMonth().CallObjects<ItemCount>(),
            };
        }

        public Statistics<DeviceCount> DeviceGrowth(byte? days = null)
        {
            var proc = new StatsLatestDevice()
            {
                Days = days ?? 14,
            };

            return new Statistics<DeviceCount>()
            {
                Daily = proc.CallObjects<DeviceCount>(),
                Monthly = new StatsDeviceByMonth().CallObjects<DeviceCount>(),
            };
        }

        public Statistics<CountByDate> UserGrowth(byte? days = null)
        {
            var proc = new StatsLatestUsers()
            {
                Days = days ?? 14,
            };

            return new Statistics<CountByDate>()
            {
                Daily = proc.CallObjects<CountByDate>(),
                Monthly = new StatsUsersByMonth().CallObjects<CountByDate>(),
            };
        }

        public int UserByMonth()
        {
            var userCount = 0;
            var users = new StatsUsersByMonth().CallObjects<ItemCount>();
            foreach (var user in users)
            {
                userCount += user.Count;
            }

            return userCount;
        }

        public int ItemsByMonth()
        {
            var itemCount = 0;
            var items = new StatsItemsByMonth().CallObjects<ItemCount>();
            foreach (var item in items)
            {
                itemCount += item.Count;
            }

            return itemCount;
        }
        public Totals Totals()
        {
            return new StatsTotals().CallObject<Totals>();
        }
        #endregion
    }
}