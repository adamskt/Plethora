using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NSpec;
using Oak;
using Oak.Controllers;
using Plethora.Controllers;

// ReSharper disable CheckNamespace,InconsistentNaming
namespace Plethora.Tests.describe_controllers

{
    
    public class describe_HomeController : nspec

    {
        HomeController _Controller;
        SeedController _Seed;

        void before_each()
        {
            _Controller = new HomeController();

            _Seed = new SeedController();

            _Seed.PurgeDb();

            _Seed.All();
        }

        void specify_listing_games()
        {
            new { Name = "Big magical game" }.InsertInto( "BoardGames" );

            var result = _Controller.Index() as ViewResult;

            var firstBoardGame = Games().First();

            ( firstBoardGame.Name as string ).should_be( "Big magical game" );
        }

        void specify_new_game()
        {
            _Controller.Create( new { Name = "Some new game" } );

            var firstBoardGame = Games().First();

            ( firstBoardGame.Name as string ).should_be( "Some new game" );
        }

        void specify_invalid_game()
        {
            var result = _Controller.Create(new { Name = string.Empty }) as ViewResult;

            ( result.ViewBag.Flash as string ).should_be( "Name of game is required." );
        }

        void specify_new_game_has_created_on_date()
        {
            _Controller.Create(new { Name = "Some new game" });

            var firstGame = Games().First();

            ( firstGame.CreatedOn is DateTime ? (DateTime) firstGame.CreatedOn : new DateTime() ).should_be_greater_than( DateTime.Now.AddSeconds( -15 ) );
        }


        IEnumerable<dynamic> Games()
        {
            return ( _Controller.Index() as ViewResult ).ViewBag.Games as IEnumerable<dynamic>;
        }
    }
}

// ReSharper restore CheckNamespace,InconsistentNaming