namespace SA.GoogleDoc {

[System.Serializable]
public class CellDictionaryRange
{
	public CellRange cellRange;

	public int rowShift = 0;
	public int columnShift = 0;

	public CellDictionaryRange () : this(new CellRange(), 0, 0)	{

	}

	public CellDictionaryRange (CellRange range, int colShiftValue, int rowShiftValue)	{
		cellRange = range;
		columnShift = colShiftValue;
		rowShift = rowShiftValue;
	}


	public CellRange GetCellRangeWithOffset() {
		CellRange newCellRange = new CellRange ();
		newCellRange.useLinebreak = cellRange.useLinebreak;
		newCellRange.LineLength = cellRange.LineLength;
		newCellRange.direction = cellRange.direction;
		newCellRange.StartCell.col = cellRange.StartCell.col + columnShift;
		newCellRange.StartCell.row = cellRange.StartCell.row + rowShift;

		return newCellRange;
	}
}
}
