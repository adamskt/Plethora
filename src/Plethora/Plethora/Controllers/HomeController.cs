using System.Web.Mvc;
using Plethora.Repos;

namespace Plethora.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoardGames _BoardGames = new BoardGames();

        public ActionResult Index()
        {
            ViewBag.Games = _BoardGames.All();

            return View();
        }
    }
}