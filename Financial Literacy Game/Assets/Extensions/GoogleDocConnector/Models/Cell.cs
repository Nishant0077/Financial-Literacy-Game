using UnityEngine;
using System;
using System.Collections;


namespace SA.GoogleDoc {

[System.Serializable]
public class Cell  {

	[SerializeField]
	private int _col = 1;

	[SerializeField]
	private int _row = 1;

	[SerializeField]
	private string _key = "A1";



	public Cell() {

	}

	public Cell(int colIndex, int rowIndex) {
		_col = colIndex;
		_row = rowIndex;

		GenerateKey ();
	}

	public Cell(string CellKey) {
		key = CellKey;
	}

	
	public int row {
		get {
			return _row;
		}
		set {
			_row = value;
			GenerateKey ();
		}
	}


	public int col {
		get {
			return _col;
		}
		set {
			_col = value;
			GenerateKey ();
		}
	}


	public string key {
		get {
			return _key;
		}
		set {
			_key = value;
			int startIndex = _key.IndexOfAny("123456789".ToCharArray());
			string columnStr;
			int row = 0;
			try{
				columnStr = _key.Substring(0, startIndex);
				row = Int32.Parse(_key.Substring(startIndex));
			} catch (Exception) {
				columnStr = string.Empty;
				row = 0;
			}
			
			columnStr = columnStr.ToUpperInvariant ();
			int column = 0;
			for (int i = 0; i < columnStr.Length; i++) {
				column *= 26;
				column += (columnStr[i] - 'A' + 1);
			}

			this._col = column != 0 ? column : this._col;
			this._row = row != 0 ? row : this._row;
		}
	}


	private void GenerateKey() {
		char ch = (char)('A' + _col - 1);
		_key = ch + _row.ToString();
	}

}

}

