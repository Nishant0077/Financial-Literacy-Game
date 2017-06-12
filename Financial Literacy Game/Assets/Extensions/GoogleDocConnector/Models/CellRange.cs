namespace SA.GoogleDoc {

using UnityEngine;
using System.Collections;


[System.Serializable]
public class CellRange  {

	public RanageDirection direction;

	public Cell StartCell;

	public bool useLinebreak = false;
	public int LineLength = 1;

	public CellRange() :this("A1") {

	}

	public CellRange(string StartCellKey) :this(StartCellKey, RanageDirection.Row) {

	}

	public CellRange(string StartCellKey, RanageDirection rangeDirection) {
		direction = rangeDirection;
		StartCell = new Cell (StartCellKey);
	}

}

}