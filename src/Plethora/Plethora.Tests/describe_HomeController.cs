using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NSpec;
using Oak;
using Oak.Controllers;
using Plethora.Controllers;

namespace Plethora.Tests.describe_controllers
{
    public class describe_HomeController : nspec
    {
        private HomeController controller;
        private SeedController seed;

        private void before_each()
        {
            controller = new HomeController();

            seed = new SeedController();

            seed.PurgeDb();

            seed.All();
        }

        private void specify_listing_games()
        {
            new { Id = Guid.NewGuid(), Name = "Big magical game" }.InsertInto( "BoardGames" );

            var result = controller.Index() as ViewResult;

            var firstBoardGame = ( result.ViewBag.Games as IEnumerable<dynamic> ).First();

            ( firstBoardGame.Name as string ).should_be( "Big magical game" );
        }
    }
}