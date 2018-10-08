using Microsoft.AspNetCore.Mvc;
using Demo.AspNetCore.Mvc.Http2.Mvc.ActionConstraints;

namespace Demo.AspNetCore.Mvc.Http2.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult ConditionalRendering()
        {
            return View();
        }

        public IActionResult ViewDiscovery()
        {
            return View();
        }

        [Http2Only]
        [ActionName("ActionSelection")]
        public IActionResult Http2OnlyAction()
        {
            ViewBag.ProtocolAbbreviation = "H2";

            return View("ActionSelection");
        }

        [NonHttp2]
        [ActionName("ActionSelection")]
        public IActionResult NonHttp2Action()
        {
            ViewBag.ProtocolAbbreviation = "H1";

            return View("ActionSelection");
        }
    }
}
