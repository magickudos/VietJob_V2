using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using VietJob_V2.Models.Classes;

namespace VietJob_V2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult GetMatching(string cv, string jd)
        {
            /*try
            {
                CVMatcher matcher = new CVMatcher();
                matcher.Main();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(null, JsonRequestBehavior.AllowGet);*/
            
            var model = new Matching(cv, jd);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}