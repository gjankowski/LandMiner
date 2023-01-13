using System;
using System.Collections.Generic;

public class Board 
{
	private int _width;
	public int Width { 
		get {
			return _width; 
		} 
		set {
			_width = value;
		}

	private int _height;
    public int Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
        }
    }
	public List<Field> Fields { get; private set; }
    Public Board(int width, int height)
	{
		_width = width;
		_height = height;

		for(int i=0; i< _width*_height; i++)
		{
			Fields.Add(new Field
			{
				CellIndex = i,
				ColumnIndex = i,
				RowIndex = i,
				Color = ColorType
			}) ;
		}
	}
}
