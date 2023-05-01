using System.Diagnostics;
using System.EnterpriseServices;
using System.Web.Mvc;

namespace VietJob_V2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}