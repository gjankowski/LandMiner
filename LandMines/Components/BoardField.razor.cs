using LandMines.Model;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace LandMines.Components
{
    public partial class BoardField
    {
        private string backgroundColor = "DarkColor";

        private ColorType _colorType;
        [Parameter]
        public ColorType ColorType
        {
            get {
                return _colorType;
            } 
            set { 
            _colorType = value;
                if(_colorType == ColorType.light)
                    backgroundColor = "LightColor";
                else
                    backgroundColor = "DarkColor";
            } 
        }
        [Parameter]
        public int RowIndex { get; set; }
        [Parameter]
        public int ColumnIndex { get; set; }
        private string _iconUrl;
        [Parameter]
        public string IconUrl { 
            get { 
                return _iconUrl;
            }
            set {
                _iconUrl = value;
            } 
        }
        [Parameter]
        public int CellIndex { get; set; }
        [Parameter]
        public bool IsMineDeployed { get; set; }
        [Parameter]
        public string HideImage { get; set; }
        private bool _isMineExploded;
        [Parameter]
        public bool IsMineExploded { get; set; }
        [Parameter]
        public string BackgroundIcon { get; set; }
        [Parameter]
        public string CustomClass { get; set; }
        [Parameter]
        public FieldBackgroundAttributes Properties { get; set; } = new() {
            Position = "relative",
            Top = "0",
            Left = "0",
            Height = "100px",
            Width = "100px",
            Margin = "1px",
            Border = "1px solid",
            BackgroundColor = "darkgrey",
            BackgroundImage = string.Empty,
            BackgroundSize = "100px"
    };

    }
}
