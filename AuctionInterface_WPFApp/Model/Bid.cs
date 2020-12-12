namespace AuctionInterface_WPFApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid : IComparable<Bid>
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public AuctionInterface_Sellers.Models.Bid_State State { get; set; }

        public int OwnerAccountId { get; set; }

        public int AuctionId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Auction Auction { get; set; }

        public int CompareTo(Bid other)
        {
            if (this.Amount > other.Amount)
            {
                return -1;
            }
            else if (this.Amount == other.Amount)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
