using System;
using System.Linq;
using System.Web.Mvc;
using Atm.Application;
using Atm.Core;
using Atm.Web.Models;

namespace Atm.Web.Controllers
{
    public class BalanceController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;        

        public BalanceController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            var loggedUser = IsValidUser();
            if (loggedUser != null)
            {
                var account = _accountService.Balance(loggedUser.Accounts.FirstOrDefault().Id);
                var balanceViewModel = new BalanceViewModel
                {
                    CardNumber = loggedUser.AtmCard.Number,
                    Date = DateTime.Now,
                    Amount = account.Amount
                };

                return View(balanceViewModel);
            }

            return View("Error");
        }

        protected User IsValidUser()
        {
            if (Session["UserId"] != null)
            {
                var userId = Convert.ToInt32(Session["UserId"]);
                return _userService.GetById(userId);
            }

            return null;
        }
    }
}