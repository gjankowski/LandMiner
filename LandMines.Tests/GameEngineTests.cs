using LandMines.BusinessLogic;

namespace LandMines.Tests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        [DataRow(8, 8, 64)]
        [DataRow(9, 7, 63)]
        [DataRow(7, 6, 42)]
        public void creating_new_board_generate_fields_for_all_board_fields(int noOfBoardColumns, int noOfBoardRows, int expectedNoOfFields)
        {
            var playerFactory = new PlayerFactory();
            var gameEngine = new GameEngine(playerFactory);


            gameEngine.CreateBoard(noOfBoardColumns, noOfBoardRows);
            var actuallNoOfFields = gameEngine.Fields?.Count;

            Assert.IsTrue(actuallNoOfFields != null, "Creating Boards resulted with creation of board fields");

            Assert.IsTrue(expectedNoOfFields == actuallNoOfFields, "Created expected number of fields representing each board field");
        }

        [TestMethod]
        [DataRow("user1",3,0, "user1",3,0)]
        [DataRow("user2",5,1, "user2",5,1)]
        [DataRow("user3",10,2, "user3",10,2)]
        public void playerFactory_return_player_object_with_playername_passed_into_createuser_method(string initialPlayerName, int lifesCount, int initialPosition, 
                    string expecedPlayerName, int expectedLifesCout, int expectedInitialPosition)
        {
            var playerFactory = new PlayerFactory();
            var player = playerFactory.CreateUser(initialPlayerName, lifesCount, initialPosition);
            var actualUserName = player.Name;
            var actuallifesCount = player.LivesCount;
            var actualinitialPosition = player.CurrentPosition;

            Assert.IsTrue(player != null, "Creating player resulted with creation of Player object");

            Assert.IsTrue(expecedPlayerName == actualUserName, "Player object contains expected playerName value");
            Assert.IsTrue(expectedLifesCout == actuallifesCount, "Player object contains expected number of lifes");
            Assert.IsTrue(expectedInitialPosition == actualinitialPosition, "Player object contains expected player initial position");
        }
    }
}