namespace AuctionInterface_WPFApp
{
    using AuctionInterface_Sellers.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Auction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auction()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }

        public double Amount { get; set; }

        public Metal_Type Metal { get; set; }

        public double StartingPrice { get; set; }

        public double MinimunBidIncrease { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime ClosingTime { get; set; }

        public Auction_State State { get; set; }

        public int OwnerAccountId { get; set; }

        public int? WinnerAccountId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
