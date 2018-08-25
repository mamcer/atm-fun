using System;
using System.Linq;
using System.Web.Mvc;
using Atm.Application;
using Atm.Core;
using Atm.Web.Models;

namespace Atm.Web.Controllers
{
    public class WithdrawalController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public WithdrawalController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            var loggedUser = IsValidUser();
            if (loggedUser != null)
            {
                return View(new WithdrawalActionViewModel());
            }

            return View("Error");
        }

        [HttpPost]
        public ActionResult Index(WithdrawalActionViewModel viewModel)
        {
            var loggedUser = IsValidUser();
            if (ModelState.IsValid)
            {
                if (loggedUser != null)
                {
                    var accountId = loggedUser.Accounts.FirstOrDefault().Id;
                    var amount = viewModel.Amount;
                    if (_accountService.HasEnoughFunds(accountId, amount))
                    {
                        var account = _accountService.Withdraw(accountId, amount);
                        if (account != null)
                        {
                            var withdrawalResult = new WithdrawalResultViewModel
                            {
                                CardNumber = loggedUser.AtmCard.Number,
                                Date = DateTime.Now,
                                AmountWithdraw = amount,
                                Balance = account.Amount
                            };
                            return View("Result", withdrawalResult);
                        }

                        return View("Error");
                    }

                    ModelState.AddModelError(string.Empty, "You don't have enough funds in this account for this transaction");
                }
                else
                {
                    return View("Error");
                }

                return View(viewModel);
            }

            return View(viewModel);
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