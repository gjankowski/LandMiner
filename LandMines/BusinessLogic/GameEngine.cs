using LandMines.Model;
using System.Numerics;

namespace LandMines.BusinessLogic
{
    public class GameEngine
    {
        public Board? Board;
        public List<Field>? Fields;
        private List<Player> _players;
        private readonly PlayerFactory _playerFactory;

        public void CreateBoard(int columnsCount, int rowsCount)
        {
            Board = new Board(columnsCount, rowsCount);
            CreateBoardFields(Board);
        }
        public int BoardRowsNumber { 
            get {
                if (Board == null)
                    return 0;
                return Board.RowsCount; 
            } 
        }
        public int BoardColumnsNumber
        {
            get
            {
                if (Board == null)
                    return 0;
                return Board.RowsCount;
            }
        }
        public int DefaultPlayerPosition
        {
            get
            {
                return (BoardColumnsNumber * (BoardRowsNumber-1));
            }
        }
        public GameEngine(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _players = new List<Player>();
        }

        private int _livesCount;
        public void CreatePlayer(string playerName, int livesCount, int initialPosition = -1)
        {
            if(initialPosition == -1)
            {
                initialPosition = DefaultPlayerPosition;
            }
            _livesCount = livesCount;

            var existingPlayer = _players.FirstOrDefault(e => e.Name.Equals(playerName));
            if(existingPlayer != null)
            {
                existingPlayer.LivesCount = livesCount;
                existingPlayer.CurrentPosition = initialPosition;
                existingPlayer.ReachedDestination = false;
                existingPlayer.MovesCount = 0;
            }
            else
            {
                _players.Add(_playerFactory.CreateUser(playerName, livesCount, initialPosition));
            }                       
            if(Fields.Count < initialPosition)
                Fields[initialPosition].PlayerIcon = @"\images\player.png";
        }

        private void CreateBoardFields(Board board)
        {
            var columns = board.ColumnsCount;
            var rows = board.RowsCount;
            Fields = new List<Field>();
            var currentFieldColour = ColorType.dark;
            for (int i = 0; i < columns * rows; i++)
            {
                var totalNumberOfRows = rows;


                var currentColumnIndex = i % columns;
                var currentRowIndex = i / columns;

                if (currentColumnIndex == 0)
                {
                    if (totalNumberOfRows % 2 == 0 && currentRowIndex % 2 == 0)
                    {
                        currentFieldColour = ColorType.light;

                    }
                    else if (totalNumberOfRows % 2 == 0 && currentRowIndex % 2 > 0)
                    {
                        currentFieldColour = ColorType.dark;
                    }
                    if (totalNumberOfRows % 2 > 0 && currentRowIndex % 2 == 0)
                    {
                        currentFieldColour = ColorType.dark;

                    }
                    else if (totalNumberOfRows % 2 > 0 && currentRowIndex % 2 > 0)
                    {
                        currentFieldColour = ColorType.light;
                    }
                }
                else
                {
                    currentFieldColour = currentFieldColour == ColorType.dark ? ColorType.light : ColorType.dark;
                }

                var playerIcon = @"\images\transparent.png";
                if(i == DefaultPlayerPosition)
                {
                    playerIcon = @"\images\player.png";
                }

                Fields.Add(new Field
                {
                    FieldId = i,
                    ColumnIndex = currentColumnIndex,
                    RowIndex = currentRowIndex,
                    ColorType = currentFieldColour,
                    PlayerIcon = playerIcon,
                    BackgroundIcon = @"\images\transparent.png",
                    IsLandMineDeployed = false,
                    IsLandMineExploded = false
                });
            }
        }

        public Field? GetFieldByIndex(int fieldId)
        {
            return Fields?.FirstOrDefault(e=>e.FieldId == fieldId);
        }

        public Player? GetPlayerByName(string playerName)
        {
            return _players.FirstOrDefault(e => e.Name == playerName);
        }

        public Player? MovePlayerToNewPosition(string playerName, Direction direction)
        {
            var player = GetPlayerByName(playerName);           
            if (player == null || player.LivesCount == 0) return null;
            if (player.ReachedDestination) 
                return player;

            var playerPosition = player.CurrentPosition;
            var currentField = GetFieldByIndex(playerPosition);
            if (currentField == null) return player;

            
            if(direction == Direction.Up)
            {
                playerPosition -= BoardColumnsNumber;
            }
            if (direction == Direction.Down && (playerPosition + BoardColumnsNumber) < Fields.Count)
            {
                playerPosition += BoardColumnsNumber;
            }

            if(direction == Direction.Left && currentField.ColumnIndex > 0)
            {
                playerPosition -= 1;
            }

            if (direction == Direction.Right && currentField.ColumnIndex < (Board.ColumnsCount - 1))
            {
                playerPosition += 1;
            }

            return MovePlayer(playerName, playerPosition);
        }
        public Player? MovePlayer(string playerName, int fieldId = -1)
        {
            if(fieldId == -1)
            {
                fieldId = DefaultPlayerPosition;
            }
            var player = GetPlayerByName(playerName);
            if (player == null || player.LivesCount == 0) return null;
            var currentField = GetFieldByIndex(fieldId);
            if (currentField == null) return player;

            // update player position and move playerIcon
            // first remove player Icon from previous field
            var previousField = GetFieldByIndex(player.CurrentPosition);
            if(previousField != null)
                previousField.PlayerIcon = @"\images\transparent.png";
            player.CurrentPosition = fieldId;
            currentField.PlayerIcon = player.PlayerIcon;
            player.MovesCount++;

            if (currentField.IsLandMineDeployed && !currentField.IsLandMineExploded)
            {
                // Process code for explosion
                currentField.BackgroundIcon = @"\images\explosion.png";
                currentField.IsLandMineExploded = true;
                
                player.LivesCount -= 1;
                if(player.LivesCount == 0)
                {
                    //Game over no lives left.
                }
            }
            if(player.LivesCount>0 && fieldId < Board.ColumnsCount)
            {
                player.ReachedDestination = true;
            }
            return player;
        }

        public void DeployLandMines(int landMinesCount)
        {
            if (Fields == null || Fields.Count == 0) return;
            // populate landmines on the fields
            var landminedFieldIndexes = new List<int>();
            var rand = new Random();
            landminedFieldIndexes.AddRange(Enumerable.Range(0, Fields.Count - 1)
                               .OrderBy(i => rand.Next())
                               .Take(landMinesCount));

            if(landminedFieldIndexes.Any(e=>e == DefaultPlayerPosition))
            {
                landminedFieldIndexes.Remove(DefaultPlayerPosition);
                landminedFieldIndexes.AddRange(Enumerable.Range(1, DefaultPlayerPosition - 1)
                               .OrderBy(i => rand.Next())
                               .Take(1));
            }

            foreach (var index in landminedFieldIndexes)
            {
                Fields[index]._isLandMineDeployed = true;
            }
        }

        internal void RestartGame()
        {
            foreach (var field in Fields)
            {
                field.IsLandMineDeployed = false;
                field.IsLandMineExploded = false;
                field.BackgroundIcon = @"\images\transparent.png";
                field.PlayerIcon = @"\images\transparent.png";
            }
           
            foreach (var player in _players)
            {
                player.LivesCount = _livesCount;
            }
        }
    }
}
