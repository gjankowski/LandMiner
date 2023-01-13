using LandMines.Model;

namespace LandMines.BusinessLogic
{
    public class PlayerFactory
    {
        public Player CreateUser(string playerName, int livesCount, int initialPosition)
        {
            return new Player {
                Name= playerName,
                LivesCount = livesCount,
                MovesCount = 0,
                CurrentPosition = initialPosition,
                PlayerIcon = @"\images\player.png"
        };
        }
    }
}
