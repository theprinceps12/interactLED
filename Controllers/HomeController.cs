using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InteractLED.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetCurrentState()
        {
            string filePath = Server.MapPath("~/SettingFiles");
            string togglePath = Path.Combine(filePath, ConfigurationManager.AppSettings["ToggleFileName"]);
            string blinkPath = Path.Combine(filePath, ConfigurationManager.AppSettings["BlinkFileName"]);
            string result = string.Empty;
            string toggle = string.Empty;
            string blink = string.Empty;
            try
            {
                toggle = System.IO.File.ReadAllText(togglePath);
                blink = System.IO.File.ReadAllText(blinkPath);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(new { Toggle = toggle, Blink = blink, Msg = result }, JsonRequestBehavior.AllowGet);
        }

        public string GetToggleState()
        {
            string togglePath = Path.Combine(Server.MapPath("~/SettingFiles"), ConfigurationManager.AppSettings["ToggleFileName"]);
            string result = string.Empty;
            try
            {
                result = System.IO.File.ReadAllText(togglePath);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public JsonResult ToggleLED(string toggleVal)
        {
            string togglePath = Path.Combine(Server.MapPath("~/SettingFiles"), ConfigurationManager.AppSettings["ToggleFileName"]);
            string result = string.Empty;
            try
            {
                System.IO.File.WriteAllText(togglePath, toggleVal);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(new { Msg = result });
        }

        [HttpPost]
        public JsonResult BlinkLED(string blinkVal)
        {
            string blinkPath = Path.Combine(Server.MapPath("~/SettingFiles"), ConfigurationManager.AppSettings["BlinkFileName"]);
            string result = string.Empty;
            try
            {
                System.IO.File.WriteAllText(blinkPath, blinkVal);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(new { Msg = result });
        }
    }
}