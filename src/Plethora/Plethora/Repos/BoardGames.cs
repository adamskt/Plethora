using Massive;
using Oak;

namespace Plethora.Repos
{
    public class BoardGames : DynamicRepository
    {
    }

    public class BoardGame : DynamicModel
    {
        public BoardGame()
        {
        }

        public BoardGame( object dto )
                : base( dto )
        {
        }
    }
}