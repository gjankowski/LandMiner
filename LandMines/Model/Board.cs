using System;
using System.Collections.Generic;
namespace LandMines.Model
{
	public class Board
	{	
		public int ColumnsCount { get; private set; }

		public int RowsCount { get; private set; }
		
		public List<Field> Fields { get; private set; }
        public Board(int columnsCount, int rowsCount)
		{
			ColumnsCount = columnsCount;
			RowsCount = rowsCount;
		}
    }
}