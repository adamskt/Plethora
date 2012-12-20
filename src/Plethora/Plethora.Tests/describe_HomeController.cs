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
        HomeController controller;
        SeedController seed;

        void before_each()
        {
            controller = new HomeController();

            seed = new SeedController();

            seed.PurgeDb();

            seed.All();
        }

        void specify_listing_games()
        {
            new { Name = "Big magical game" }.InsertInto( "BoardGames" );

            var result = controller.Index() as ViewResult;

            var firstBoardGame = Games().First();

            ( firstBoardGame.Name as string ).should_be( "Big magical game" );
        }

        void specify_new_game()
        {
            controller.Create( new { Name = "Some new game" } );

            var firstBoardGame = Games().First();

            ( firstBoardGame.Name as string ).should_be( "Some new game" );
        }

        void specify_invalid_game()
        {
            var result = controller.Create(new { Name = string.Empty }) as ViewResult;

            ( result.ViewBag.Flash as string ).should_be( "Name of game is required." );
        }



        IEnumerable<dynamic> Games()
        {
            return ( controller.Index() as ViewResult ).ViewBag.Games as IEnumerable<dynamic>;
        }
    }
}