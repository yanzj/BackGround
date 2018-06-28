using System.Web.Mvc;

namespace LtePlatform.Areas.TestPage.Controllers
{
    public class AngularTestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

    public class CoffeeScriptController : Controller
    {
        // GET: TestPage/CoffeeScript
        public ActionResult Hotseat()
        {
            return View();
        }
    }

    public class QUnitTestController : Controller
    {
        // GET: TestPage/QUnitTest
        public ActionResult Index()
        {
            return View();
        }

    }
}