using System.Web.Mvc;
using Atm.Application;
using Atm.Web.Models;

namespace Atm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAtmCardService _atmCardService;

        public HomeController(IAtmCardService atmCardService)
        {
            _atmCardService = atmCardService;
        }

        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var atmCard = _atmCardService.GetCardByNumber(viewModel.CardNumber);
                if (atmCard != null)
                {
                    if (!atmCard.User.IsLocked)
                    {
                        return RedirectToAction("Pin", new { cardNumber = viewModel.CardNumber });
                    }

                    ModelState.AddModelError("", "Your account has been locked, please contact your bank to unlock your account");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid card number");
                }
            }

            return View(viewModel);
        }

        public ActionResult Pin(string cardNumber)
        {
            if (!string.IsNullOrEmpty(cardNumber))
            {
                return View(new PinViewModel { CardNumber = cardNumber });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Pin(PinViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _atmCardService.ValidateAtmCardPin(viewModel.CardNumber, viewModel.Pin);
                if (user != null)
                {
                    Session["UserName"] = user.UserName;
                    Session["UserId"] = user.Id;
                    Session["CardNumber"] = viewModel.CardNumber;
                    return RedirectToAction("Index", "Operation");
                }

                ModelState.AddModelError("", "Invalid pin number");
            }

            return View(viewModel);
        }

        public ActionResult Logout()
        {
            if (IsValidUser())
            {
                Session["UserName"] = null;
                Session["UserId"] = null;
                Session["CardNumber"] = null;
            }

            return RedirectToAction("Index");
        }

        private bool IsValidUser()
        {
            return Session["UserId"] != null;
        }
    }
}