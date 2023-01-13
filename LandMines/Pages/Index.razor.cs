using LandMines.Model;
using LandMines.BusinessLogic;
using Microsoft.AspNetCore.Components;

namespace LandMines.Pages
{
    public partial class Index
    {
        [Parameter]
        public string GameSummaryMessage { get; set; }
        private int counter;
        public string PlayerName;
        public string NumberOfMines;
        public string NumberOfLifes;
        public int NumberOfColumns;
        public int NumberOfRows;
        public int PlayerCurrentRow;
        public int PlayerCurrentColumn;
        [Inject]
        public GameEngine GameEngine { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PlayerName = "greg";
            NumberOfMines = "10";
            NumberOfLifes = "3";
            NumberOfColumns = 8;
            NumberOfRows = 8;

            StartNewGameClicked();
        }

        void StartNewGameClicked()
        {
            if(int.TryParse(NumberOfMines,out int noOfMines) &&
                int.TryParse(NumberOfLifes, out int noOfLifes))
            {
                if (GameEngine.Board == null)
                {
                    GameEngine.CreateBoard(NumberOfColumns, NumberOfRows);
                }
                GameEngine.RestartGame();                
                GameEngine.DeployLandMines(noOfMines);
                GameEngine.CreatePlayer(PlayerName, noOfLifes);

                GameEngine.MovePlayer(PlayerName);
                GameSummaryMessage = string.Empty;
            }
            
        }

        void ButtonClicked(Direction direction)
        {
            var player = GameEngine.MovePlayerToNewPosition(PlayerName, direction);
            if(player != null)
            {
                var currentField = GameEngine.GetFieldByIndex(player.CurrentPosition);
                if (currentField != null)
                {
                    PlayerCurrentColumn = currentField.ColumnIndex;
                    PlayerCurrentRow = GameEngine.Board.RowsCount - currentField.RowIndex;
                }
            }
            
            
            if (player?.LivesCount == 0)
            {
                GameSummaryMessage = $"Player {player.Name} lost the game in {player.MovesCount} steps.";
            }
            if (player != null && player.ReachedDestination)
            {
                GameSummaryMessage = $"Player {player.Name} won the game in {player.MovesCount} steps with {player.LivesCount} lives remaining";
            }

            this.StateHasChanged();
            //this.StateHasChanged();
            //counter += 1;
        }
    }
}
