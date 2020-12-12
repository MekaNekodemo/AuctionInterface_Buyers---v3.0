using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AuctionInterface_Sellers.Models;

namespace AuctionInterface_WPFApp
{
    /// <summary>
    /// Interaction logic for AuctionsWindow.xaml
    /// </summary>
    public partial class AuctionsWindow : Window
    {

        private readonly Account activeAccount;
        private Account auctioneer;
        private Auction currentlySelectedAuction;
        private readonly AuctionDB_Sets dataSets = new AuctionDB_Sets();
        public AuctionsWindow(Account account)
        {
            activeAccount = account;

            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            auctionsListView.DataContext = dataSets.Auctions.ToList();

            RefreshUI();

            System.Windows.Data.CollectionViewSource auctionDB_SetsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("auctionDB_SetsViewSource");
            // Load data by setting the CollectionViewSource.Source property:
            // auctionDB_SetsViewSource.Source = [generic data source]
        }

        private void auctionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshUI();
        }

        private void Button_PlaceBid_Click(object sender, RoutedEventArgs e)
        {

            RefreshUI();

            string bidValueAsString = TextBox_BidValue.Text;

            if (Int32.TryParse(bidValueAsString, out int result) == true)
            {
                decimal bidValue = Convert.ToDecimal(bidValueAsString);

                if (ValidateBid(bidValue) && IsLegalBiddingTime())
                {
                    Bid bid = new Bid()
                    {
                        OwnerAccountId = activeAccount.Id,
                        Amount = bidValue,
                        AuctionId = ((Auction)auctionsListView.SelectedValue).Id
                    };

                    dataSets.Bids.Add(bid);
                    dataSets.SaveChanges();
                    TextBox_BidValue.Text = "";
                    RefreshUI();
                }
                else
                {
                    MessageBox.Show($"Error: is your bid high enough?, is it a numerical value? is the auction open?");
                }
            }


        }

        private void RefreshUI()
        {

            currentlySelectedAuction = (Auction)auctionsListView.SelectedItem;

            auctioneer = GetAuctioneer(currentlySelectedAuction.OwnerAccountId);

            auctionsListView.DataContext = dataSets.Auctions.ToList();

            UpdateUI_AuctioneerContactInfo();
            UpdateUI_TimeLeftForAuction();
            UpdateUI_FromCurrentAuction();

            dataSets.SaveChanges();
        }

        private void UpdateUI_AuctioneerContactInfo()
        {
            TextBox_AuctioneerName.Text = auctioneer.Name;
            TextBox_Email.Text = auctioneer.Email;
            TextBox_Phone.Text = auctioneer.PhoneNumber;

        }
        private void UpdateUI_TimeLeftForAuction()
        {
            TimeSpan timeLeft = currentlySelectedAuction.ClosingTime.Subtract(DateTime.Now);

            if (timeLeft.TotalSeconds <= 0)
            {

                List<Auction> auctions = dataSets.Auctions.ToList();

                Auction currentAuction = (from auction in auctions
                                          where auction.Id == currentlySelectedAuction.Id
                                          select auction).First();

                currentAuction.State = Auction_State.Closed;

                dataSets.SaveChanges();



            }
            else
            {
                TextBox_DaysLeft.Text = timeLeft.Days.ToString();
                TextBox_HoursLeft.Text = timeLeft.Hours.ToString();
                TextBox_MinutesLeft.Text = timeLeft.Minutes.ToString();

            }


        }
        private void UpdateUI_FromCurrentAuction()
        {

            bidsOnCurrentAuctionListView.DataContext = LoadAuctionsBidList();
            TextBox_StartingPrice.Text = currentlySelectedAuction.StartingPrice.ToString();
            TextBox_MinimumBid.Text = currentlySelectedAuction.MinimunBidIncrease.ToString();
            TextBox_ClosingDate.Text = currentlySelectedAuction.ClosingTime.ToString();
        }
        private List<Bid> LoadAuctionsBidList()
        {

            List<Bid> currentAuctionsBids = freshSnapShotOfBids();

            if (currentAuctionsBids.Count > 1)
            {

                if (currentlySelectedAuction.State == Auction_State.Open)
                {
                    freshSnapShotOfBids().First().State = Bid_State.Leading;
                    dataSets.SaveChanges();
                    for (int i = 1; i < currentAuctionsBids.Count; i++)
                    {
                        freshSnapShotOfBids().ElementAt(i).State = Bid_State.Trailing;
                        dataSets.SaveChanges();
                    }

                    return freshSnapShotOfBids();
                }
                else 
                {

                    currentAuctionsBids[0].State = Bid_State.Won;
                    dataSets.SaveChanges();
                    for (int i = 1; i < currentAuctionsBids.Count; i++)
                    {
                        freshSnapShotOfBids().ElementAt(i).State = Bid_State.Lost;
                        dataSets.SaveChanges();
                    }

                    return freshSnapShotOfBids();
                }
               
            }
            else if (currentAuctionsBids.Count == 1)
            {

                if (currentlySelectedAuction.State == Auction_State.Closed)
                {
                    freshSnapShotOfBids().First().State = Bid_State.Won;
                    dataSets.SaveChanges();
                }
                dataSets.SaveChanges();
                return freshSnapShotOfBids();
            }
            else
            {
               
                return freshSnapShotOfBids();
            }


        }

        private List<Bid> freshSnapShotOfBids()
        {
            List<Bid> listOfBids = dataSets.Bids.ToList();

            List<Bid> listOfBidsOnCurrentAuction = (from bid in listOfBids
                                          where bid.AuctionId == currentlySelectedAuction.Id
                                          select bid).ToList();
            listOfBidsOnCurrentAuction.Sort();

            return listOfBidsOnCurrentAuction;
        }
        private bool ValidateBid(decimal bidValue)
        {

            if (currentlySelectedAuction.Bids.Count == 0)
            {
                if (bidValue >= Convert.ToDecimal(currentlySelectedAuction.StartingPrice + currentlySelectedAuction.MinimunBidIncrease))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (bidValue > (currentlySelectedAuction.Bids.First().Amount + Convert.ToDecimal(currentlySelectedAuction.MinimunBidIncrease)))
                {
                    return true;
                }
                return false;
            }

        }
        private bool IsLegalBiddingTime()
        {
            if (currentlySelectedAuction.State == AuctionInterface_Sellers.Models.Auction_State.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private Account GetAuctioneer(int auctioneersId)
        {
            List<Account> accounts = GetAllAccounts();


            Account currentAuctionsAccount = (from account in accounts
                                              where account.Id == auctioneersId
                                              select account).ToList().First();

            return currentAuctionsAccount;
        }
        private List<Account> GetAllAccounts()
        {
            return dataSets.Accounts.ToList();
        }



    }
}
