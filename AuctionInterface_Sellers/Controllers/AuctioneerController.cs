using AuctionInterface_Sellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace AuctionInterface_Sellers.Controllers
{
    public class AuctioneerController : Controller
    {
        private readonly DataSetManager dataSetManager = new DataSetManager(new Auction_DBEntities());

        [HttpPost]
        public ActionResult SellersPage(string email)
        {
            List<Account> accounts = dataSetManager.GetAccounts();
            List<Auction> allAuctions = new List<Auction>();
            List<Bid> allBids = dataSetManager.GetAllBids();

            try
            {
                Account foundAccount = (from account in accounts
                                        where account.Email == email
                                        select account).ToList().ElementAt(0);

                allAuctions = dataSetManager.GetAuctions();

                ViewBag.AllAuctions = allAuctions;
                ViewBag.Account = foundAccount;
                ViewBag.AllBids = allBids;


                return View();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show($"No Account with the Email: {email} ");
                return Redirect("/");
            }

        }


        public ActionResult AuctionForm(int auctioneerId)
        {
            ViewBag.auctioneerId = auctioneerId;

            return View();
        }

        public ActionResult CreateAuction(int auctioneerId,
            Metal_Type metal_Type,
            double amount,
            double startingPrice,
            double minimumBidIncrement,
            DateTime closingTime,
            string endHour,
            string endMinute
            )
        {

            // TODO Error Handeling - Try parse and/or input validation.

            closingTime = closingTime.AddHours(double.Parse(endHour)).AddMinutes(double.Parse(endMinute));

            Auction auction = new Auction();

            auction.OwnerAccountId = auctioneerId;
            auction.Amount = amount;
            auction.Metal = metal_Type;
            auction.StartingPrice = startingPrice;
            auction.MinimunBidIncrease = minimumBidIncrement;
            auction.StartTime = DateTime.Now;
            auction.ClosingTime = closingTime;
            auction.State = Auction_State.Open;

            dataSetManager.DataSets.Auctions.Add(auction);
            dataSetManager.DataSets.SaveChanges();


            return Redirect("/");
        }

       
    }
}