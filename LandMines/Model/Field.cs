using System;
using System.Diagnostics.Metrics;

namespace LandMines.Model
{
	public class Field
	{
		public int FieldId { get; set; }
		public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public string PlayerIcon { get; set; }
        public string BackgroundIcon { get; set; }
        public ColorType ColorType { get; set; }
        public bool IsLandMineExploded { get; set; }

        public bool _isLandMineDeployed;
		public bool IsLandMineDeployed { get {
				return _isLandMineDeployed;
			} set {
				_isLandMineDeployed = value;
				if (_isLandMineDeployed && !IsLandMineExploded)
				{
					// Exploding landmine
					BackgroundIcon = @"\images\explosion.png";
				}
				else
				{
                    BackgroundIcon = @"\images\transparent.png";
                }
			} 
		}
     
        public void ExecuteLogicOnEnteringField()
		{
			PlayerIcon = @"\images\player.png";
            if (IsLandMineDeployed)
            {
                IsLandMineExploded = true;
				BackgroundIcon = @"\images\explosion.png";

            }      
		}

        private void ExecuteLogicOnLeavingField(int currentCellIndex)
        {
            PlayerIcon = @"\images\transparent.png";
        }
    }
}