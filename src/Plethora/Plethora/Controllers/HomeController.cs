using System.Web.Mvc;
using JetBrains.Annotations;
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

        [HttpPost]
        public ActionResult Create( dynamic @params )
        {
            if ( string.IsNullOrEmpty( @params.Name ) )
            {
                ViewBag.Flash = "Name of game is required.";

                return View();
            }

            _BoardGames.Insert( @params );

            return null;
        }
    }
}