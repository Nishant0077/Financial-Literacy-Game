using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GDMiniJSON;

namespace SA.GoogleDoc {

	

	public static class API  {


		//--------------------------------------
		// Public Methods
		//--------------------------------------

		public static T GetValue<T> (string docName, int row, int col, int workSheetNumber = 0) {
			T value = default(T);

			if (!Reader.sheets.ContainsKey(docName)) {
				Reader.RetiveSheetDataLocal(docName);
			}
			
			if (Reader.sheets.ContainsKey(docName)) {
				SpreadsheetData spreadsheet = Reader.sheets[docName];
				if (spreadsheet != null) {
					string data = spreadsheet.GetData (workSheetNumber, row, col);

					try {
						value = (T)Convert.ChangeType (data, typeof(T));
					} catch (FormatException){
						return default(T);
					}
				}
			}

			return value;
		}

		public static T GetValue<T> (string docName, Cell cell, int workSheetNumber = 0) {
			return GetValue<T>(docName, cell.row, cell.col, workSheetNumber);
		}



		public static List<T> GetList<T>(string docName, CellRange range, string workSheetName) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			if (doc != null) {
				return  GetList<T> (docName, range, doc.GetWorksheetId(workSheetName));
			} else {
				return new List<T>();
			}
		}

		public static List<T> GetList<T>(string docName, CellRange range, int workSheetNumber = 0) {
			List<T> result = new List<T> ();

			string data = string.Empty;
			
			Cell cell =  new Cell(range.StartCell.col, range.StartCell.row);
			
			if (range.direction == RanageDirection.Row) {
				int lenght = 1;
				for (;;) {
					data = GetValue<string> (docName, cell, workSheetNumber);
					if (data.Equals (string.Empty)) {
						if (range.useLinebreak && lenght >= range.LineLength) {
							cell.col = range.StartCell.col;
							cell.row++;
							lenght = 1;
						} else {
							return result;
						}
					} else {
						if (data.Equals("null")){
							return result;
						} else {
							if (range.useLinebreak && lenght >= range.LineLength) {
								cell.col = range.StartCell.col;
								cell.row++;
								lenght = 1;
							} else {
								lenght++;
								cell.col++;
							}

							T value = default(T);
							try {
								value = (T)Convert.ChangeType (data, typeof(T));
							} catch (FormatException){
								value = default(T);
							}
							
							result.Add(value);
						}
					}
				}
			}
			else if (range.direction == RanageDirection.Collumn) {
				int lenght = 1;
				for(;;) {
					data = GetValue<string> (docName, cell, workSheetNumber);
					if (data.Equals (string.Empty)) {
						if (range.useLinebreak && lenght >= range.LineLength) {
							cell.row = range.StartCell.row;
							cell.col++;
							lenght = 1;
						} else {
							return result;
						}
					} else {
						if (data.Equals("null")){
							return result;
						} else {
							if (range.useLinebreak && lenght >= range.LineLength) {
								cell.row = range.StartCell.row;
								cell.col++;
								lenght = 1;
							} else {
								lenght++;
								cell.row++;
							}

							T value = default(T);
							try {
								value = (T)Convert.ChangeType (data, typeof(T));
							} catch (FormatException){
								value = default(T);
							}
							
							result.Add(value);
						}
					}
				}
			}

			return result;
		}

		public static T[] GetArray<T>(string docName, CellRange range, int workSheetNumber = 0) {
			return GetList<T> (docName, range, workSheetNumber).ToArray ();
		}

		public static T[] GetArray<T>(string docName, CellRange range, string workSheetName) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			List<T> result = new List<T> ();
			if (doc != null) {
				result = GetList<T> (docName, range, doc.GetWorksheetId(workSheetName));
			}
			return result.ToArray();
		}



		public static Dictionary<K, V> GetDictionary<K, V>(string docName, CellDictionaryRange dictionaryRange, string workSheetName) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);

			if (doc != null) {
				return  GetDictionary<K, V> (docName, dictionaryRange, doc.GetWorksheetId(workSheetName));
			} else {
				return new Dictionary<K, V>();
			}
		}

		public static Dictionary<K, V> GetDictionary<K, V>(string docName, CellDictionaryRange dictionaryRange, int workSheetNumber = 0) {
			Dictionary<K, V> result = new Dictionary<K, V> ();
			List<K> keys = GetList<K> (docName, dictionaryRange.cellRange, workSheetNumber);
			List<V> values = GetList<V> (docName, dictionaryRange.GetCellRangeWithOffset(), workSheetNumber);

			for (int i = 0; i < keys.Count; i++) {
				if (values.Count > i) {
					if (!result.ContainsKey(keys[i])) {
						result.Add(keys[i], values[i]);
					}
				} else {
					result.Add(keys[i], default(V));
				}
			}

			return result;
		}





		public static bool IsValueOfType<T>(string docName, int row, int col, int workSheetNumber = 0) {

			if (!Reader.sheets.ContainsKey(docName)) {
				Reader.RetiveSheetDataLocal(docName);
			}
			
			if (Reader.sheets.ContainsKey(docName)) {
				SpreadsheetData spreadsheet = Reader.sheets[docName];
				if (spreadsheet != null) {
					string data = spreadsheet.GetData (workSheetNumber, row, col);
					
					try {
						Convert.ChangeType (data, typeof(T));
						return true;
					} catch (FormatException){
						return false;
					}
				}
			}
			return false;

		}


		public static bool IsValueOfType<T>(string docName, Cell cell, int workSheetNumber = 0) {
			return IsValueOfType<T>(docName, cell.row, cell.col, workSheetNumber);
		}


		/*public static bool IsValueOfType(Type type, string docName, int row, int col, int workSheetNumber = 0) {
			return false;
		}*/


		public static Cell FindCellByContent(string docName, object content, int workSheetNumber = 0) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				// Begin search from cell A1
				Cell cell = new Cell (1, 1);
				string value = GetValue<string>(docName, cell, workSheetNumber);
				int nEmpty = 0;
				while (nEmpty < 2) {
					
					if (value.Equals(content.ToString())){
						return cell;
					}

					if (value.Equals(string.Empty)) {
						cell.col = 1;
						cell.row++;
						nEmpty++;
					} else {
						cell.col++;
						nEmpty = 0;
					}

					value = GetValue<string>(docName, cell, workSheetNumber);
				}
			}
			
			return null;
		}


		public static Cell FindCellByContent(string docName, object content, string workSheetName) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				return FindCellByContent(docName, content, doc.GetWorksheetId(workSheetName));
			}
			
			return null;
		}


		public static Cell FindCellByContent(string docName, string column, object content, int workSheetNumber = 0) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);

			if (doc != null) {
				// Begin search from cell with row = 1
				Cell cell = new Cell (column + "1");
				string value = GetValue<string>(docName, cell, workSheetNumber);
				while (!value.Equals(string.Empty)) {

					if (value.Equals(content.ToString())){
						return cell;
					}

					cell.row++;
					value = GetValue<string>(docName, cell, workSheetNumber);
				}
			}

			return null;
		}


		public static Cell FindCellByContent(string docName, string column, object content, string workSheetName) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				return FindCellByContent(docName, column, content, doc.GetWorksheetId(workSheetName));
			}
			
			return null;
		}
		
		
		public static Cell FindCellByContent(string docName, int row, object content, int workSheetNumber = 0) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				// Begin search from cell with collumn = 1
				Cell cell = new Cell (1, row);
				string value = GetValue<string>(docName, cell, workSheetNumber);
				while (!value.Equals(string.Empty)) {
					
					if (value.Equals(content.ToString())){
						return cell;
					}
					
					cell.col++;
					value = GetValue<string>(docName, cell, workSheetNumber);
				}
			}
			
			return null;
		}


		public static Cell FindCellByContent(string docName, int row, object content, string workSheetName) {
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				return FindCellByContent(docName, row, content, doc.GetWorksheetId(workSheetName));
			}
			
			return null;
		}


		public static Cell[] FindCellsByContent(string docName, object content, int workSheetNumber = 0) {
			List<Cell> cells = new List<Cell> ();
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				// Begin search from cell A1
				Cell cell = new Cell (1, 1);
				string value = GetValue<string>(docName, cell, workSheetNumber);
				int nEmpty = 0;
				while (nEmpty < 2) {
					
					if (value.Equals(content.ToString())){
						cells.Add(new Cell(cell.col, cell.row));
					}
					
					if (value.Equals(string.Empty)) {
						cell.col = 1;
						cell.row++;
						nEmpty++;
					} else {
						cell.col++;
						nEmpty = 0;
					}
					
					value = GetValue<string>(docName, cell, workSheetNumber);
				}
			}

			return cells.ToArray();
		}


		public static Cell[] FindCellsByContent(string docName, object content, string workSheetName) {
			List<Cell> cells = new List<Cell> ();
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				return FindCellsByContent(docName, content, doc.GetWorksheetId(workSheetName));
			}
			
			return cells.ToArray();
		}


		public static Cell[] FindCellsByContent(string docName, string column, object content, int workSheetNumber = 0) {
			List<Cell> cells = new List<Cell> ();
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				// Begin search from cell with row = 1
				Cell cell = new Cell (column + "1");
				string value = GetValue<string>(docName, cell, workSheetNumber);
				while (!value.Equals(string.Empty)) {
					
					if (value.Equals(content.ToString())){
						cells.Add(new Cell(cell.key));
					}
					
					cell.row++;
					value = GetValue<string>(docName, cell, workSheetNumber);
				}
			}

			return cells.ToArray ();
		}


		public static Cell[] FindCellsByContent(string docName, string column, object content, string workSheetName) {
			List<Cell> cells = new List<Cell> ();
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				return FindCellsByContent(docName, column, content, doc.GetWorksheetId(workSheetName));
			}
			
			return cells.ToArray();
		}


		public static Cell[] FindCellsByContent(string docName, int row, object content, int workSheetNumber = 0) {
			List<Cell> cells = new List<Cell> ();
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				// Begin search from cell with collumn = 1
				Cell cell = new Cell (1, row);
				string value = GetValue<string>(docName, cell, workSheetNumber);
				while (!value.Equals(string.Empty)) {
					
					if (value.Equals(content.ToString())){
						cells.Add(new Cell(cell.key));
					}
					
					cell.col++;
					value = GetValue<string>(docName, cell, workSheetNumber);
				}
			}
			
			return cells.ToArray();
			
		}

		
		public static Cell[] FindCellsByContent(string docName, int row, object content, string workSheetName) {
			List<Cell> cells = new List<Cell> ();
			DocTemplate doc = Settings.Instance.GetDocByName (docName);
			
			if (doc != null) {
				return FindCellsByContent(docName, row, content, doc.GetWorksheetId(workSheetName));
			}
			
			return cells.ToArray();
		}



		//--------------------------------------
		// Utils
		//--------------------------------------


		public static void RetivePublicSheetData(DocTemplate doc, bool drawProgressBar = true) {
			Reader.RetivePublicSheetData(doc, drawProgressBar);
		}

		public static void RetivePublicSheetData(DocTemplate doc, WorksheetTemplate worksheet, bool drawProgressBar = true) {
			Reader.RetivePublicSheetData(doc, worksheet, drawProgressBar);
		}
			
	}

}
