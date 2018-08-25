using System.Web.Mvc;

namespace Atm.Web.Controllers
{
    public class OperationController : Controller
    {
        public ActionResult Index()
        {
            if (IsValidUser())
            {
                ViewBag.UserName = Session["UserName"].ToString();
                return View();
            }

            return View("Error");
        }

        private bool IsValidUser()
        {
            return Session["UserId"] != null;
        }
    }
}