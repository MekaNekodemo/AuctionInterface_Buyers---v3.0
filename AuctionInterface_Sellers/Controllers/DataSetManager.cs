
using AuctionInterface_Sellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionInterface_Sellers.Controllers
{
    public class DataSetManager
    {

        public Auction_DBEntities DataSets { get; set; }

        public DataSetManager(Auction_DBEntities dataSets)
        {
            this.DataSets = dataSets;
        }
        //--------------------------------- Account set manipulation below ---------------------------------//
        public List<Account> GetAccounts()
        {
            return DataSets.Accounts.ToList();
        }



        //--------------------------------- Auction set manipulation below ---------------------------------//
        public List<Auction> GetAuctions()
        {
            UpdateAuctionState();

            return DataSets.Auctions.ToList();
        }

        public List<Auction> AccountsAuctionsFromId(int accountId)
        {
            List<Auction> allAuctions = GetAuctions();
            List<Auction> foundAccountsAuctions = new List<Auction>();

            foreach (Auction auction in allAuctions)
            {
                if (auction.OwnerAccountId == accountId)
                {
                    foundAccountsAuctions.Add(auction);
                }
            }
            return foundAccountsAuctions;
        }

        private void UpdateAuctionState()
        {
            List<Auction> auctions = DataSets.Auctions.ToList();

            foreach (Auction auction in auctions)
            {
                if(DateTime.Now.CompareTo(auction.ClosingTime) >  0)
                {
                    auction.State = Auction_State.Closed;
                    DataSets.SaveChanges();
                }
            }

        }

        public Auction Create_NewAuctionWithId()
        {
            Auction freshAuction = DataSets.Auctions.Add(new Auction());
            DataSets.SaveChanges();
            return freshAuction;
        }

        //--------------------------------- Bid set manipulation below ---------------------------------//

        public List<Bid> GetAllBids()
        {
            return DataSets.Bids.ToList();
        }

    }
}