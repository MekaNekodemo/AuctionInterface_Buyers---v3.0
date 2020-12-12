namespace AuctionInterface_WPFApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AuctionDB_Sets : DbContext
    {
        public AuctionDB_Sets()
            : base("name=AuctionDB_Sets")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.OwnerAccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Auctions)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.OwnerAccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Auctions1)
                .WithOptional(e => e.Account1)
                .HasForeignKey(e => e.WinnerAccountId);

            modelBuilder.Entity<Auction>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.Auction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bid>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);
        }
    }
}
