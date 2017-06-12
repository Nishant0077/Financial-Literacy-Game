using UnityEngine;
using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using GDMiniJSON;

#if UNITY_EDITOR && !SA_DEBUG
using UnityEditor;
#endif


namespace SA.GoogleDoc {

	internal class Reader {
		public static Dictionary<string, SpreadsheetData> sheets =  new Dictionary<string, SpreadsheetData>();
		
		public static void RetivePublicSheetData(DocTemplate doc, bool drawProgressBar = true) {
			foreach (WorksheetTemplate worksheet in doc.Worksheets) {
				RetivePublicSheetData(doc, worksheet, true);
			}
		}
		
		public static void RetivePublicSheetData(DocTemplate doc, WorksheetTemplate worksheet, bool drawProgressBar = true) {
			#if !UNITY_WEBPLAYER


			SpreadsheetData spreadsheet;
			if (!sheets.ContainsKey(doc.name)) {
				spreadsheet = new SpreadsheetData ();
				sheets.Add(doc.name, spreadsheet);
			} else {
				spreadsheet = sheets[doc.name];
			}
			
			if (spreadsheet.worksheets.ContainsKey(worksheet.listId)) {
				spreadsheet.worksheets.Remove(worksheet.listId);
			}
			LoadSheetData (spreadsheet, worksheet.listId, doc, drawProgressBar);
			
			string JSON = string.Empty;
			JSON += Json.Serialize(sheets[doc.name].worksheets);
			
			// Add creation DateTime stamp to file
			DateTime date = DateTime.Now;
			CultureInfo culture = CultureInfo.CurrentUICulture;
			string creationTime = date.ToString ("dd MMMM, (HH:mm)\n", culture);
			
			//Rewrite txt file with updated data
			System.IO.File.WriteAllText(Settings.GetCachePath(doc.name), creationTime + JSON);
			doc.UpdateTime (creationTime);
			#endif
			
			#if UNITY_EDITOR && !SA_DEBUG
			AssetDatabase.Refresh ();
			
			
			if(drawProgressBar) {
				EditorUtility.ClearProgressBar();
			}
		
			#endif
		}
		
		public static void LoadSheetData(SpreadsheetData spreadsheetData, int tableListId, DocTemplate doc, bool drawProgressBar) {
			#if UNITY_EDITOR && !SA_DEBUG
			float p = 0f;
			
			if(drawProgressBar) {
				EditorUtility.DisplayProgressBar("Caching Document", doc.name + " worksheet: " + doc.GetWorksheetName(tableListId) , p);
			}
			
			//Set Plain Text in Sheets by ID
			WWW request = new WWW (Settings.SCRIPT_URL_START + doc.key + Settings.SCRIPT_URL_PARAM + tableListId);
			while(!request.isDone) {
				Thread.Sleep (100);
			}
			
			request = new WWW (Settings.SPREADSHEET_URL_START + doc.key + Settings.SPREADSHEET_URL_END + tableListId);
			while(!request.isDone) {
				Thread.Sleep (100);
				p+=0.1f;
				
				if(drawProgressBar) {
					EditorUtility.DisplayProgressBar("Caching Document", doc.name + " worksheet: " + doc.GetWorksheetName(tableListId) , p);
				}
			}
			
			
			
			if (request.error != null) {
				Debug.LogWarning("[GoogleDataAPI] " + request.error);
				
				return;
			}
			
			FillSpreadsheetWithData(spreadsheetData, tableListId, request.text);
			
			#endif
		}
		
		public static void RetiveSheetDataLocal(string docName) {
			string t = GetDocCache(docName);
			
			if (t != null) {
				// Offset for DateTime line
				int offset = t.IndexOf ("\n") + 1;
				string jsonStr = t.Substring (offset, t.Length - offset);
				
				SpreadsheetData spreadsheet = new SpreadsheetData ();
				Dictionary<string, object> worksheets = Json.Deserialize(jsonStr) as Dictionary<string, object>;
				
				Dictionary<int, Dictionary<string, string>> workSheets = new Dictionary<int, Dictionary<string, string>>();
				foreach(string key in worksheets.Keys) {
					Dictionary<string, object> lists = worksheets[key] as Dictionary<string, object>;
					
					int listId = System.Convert.ToInt32(key);
					Dictionary<string, string> cells = new Dictionary<string, string>();
					
					foreach(string cell in lists.Keys) {
						cells.Add(cell, lists[cell].ToString());
					}
					
					workSheets.Add(listId, cells);
				}
				spreadsheet.worksheets = workSheets;
				sheets.Add(docName, spreadsheet);
			} else {
				Debug.LogWarning("Such GoogleDocFile doesn't exist");
			}
		}

		private static Dictionary<string, string> DocsCacheData = new Dictionary<string, string>();
		private static string GetDocCache(string docName) {
			if(!DocsCacheData.ContainsKey(docName)) {
				TextAsset t = Resources.Load("Cache/" + docName) as TextAsset;
				DocsCacheData.Add(docName, t.text);
			} 

			return DocsCacheData[docName];
		}

		public static void SetDocCache(string docName, int tableListId, string data) {

			//Format string returned by WWW request
			//Just delete 'google.visualization.Query.setResponse(' at the beginning
			//and ');' at the end of this string
			string json = "";
			Match match = Regex.Match(data, @"\((.+)\)", RegexOptions.IgnoreCase);
			if(match.Success) {
				json = match.Groups[1].Value;
			}
			
			Dictionary<string, object> tableEntries = Json.Deserialize (json) as Dictionary<string, object>;
			if (tableEntries == null) {
				return;
			}

			SpreadsheetData spreadsheetData = new SpreadsheetData();
			FillSpreadsheetWithData(spreadsheetData, tableListId, data);

			if (DocsCacheData.ContainsKey(docName)) {
				Dictionary<string, object> worksheets = Json.Deserialize(DocsCacheData[docName]) as Dictionary<string, object>;
				if (worksheets.ContainsKey(tableListId.ToString())) {
					worksheets[tableListId.ToString()] = spreadsheetData.worksheets[tableListId];
				} else {
					worksheets.Add(tableListId.ToString(), spreadsheetData.worksheets[tableListId]);
				}
				DocsCacheData[docName] = Json.Serialize(worksheets);
			} else {
				Dictionary<string, object> worksheets = new Dictionary<string, object>();
				if (worksheets.ContainsKey(tableListId.ToString())) {
					worksheets[tableListId.ToString()] = spreadsheetData.worksheets[tableListId];
				} else {
					worksheets.Add(tableListId.ToString(), spreadsheetData.worksheets[tableListId]);
				}
				DocsCacheData.Add(docName, Json.Serialize(worksheets));
			}

		}

		private static void FillSpreadsheetWithData(SpreadsheetData spreadsheet, int tableListId, string data) {
			//Format string returned by WWW request
			//Just delete 'google.visualization.Query.setResponse(' at the beginning
			//and ');' at the end of this string
			string json = "";
			Match match = Regex.Match(data, @"\((.+)\)", RegexOptions.IgnoreCase);
			if(match.Success) {
				json = match.Groups[1].Value;
			}
			
			Dictionary<string, object> tableEntries = Json.Deserialize (json) as Dictionary<string, object>;
			if (tableEntries == null) {
				return;
			}
			
			int tableEntryIndex = 0;
			uint row = 1;
			uint column = 1;
			foreach (string k in tableEntries.Keys) {
				Dictionary<string, object> cells = tableEntries[k] as Dictionary<string, object>;
				if (cells != null) {
					
					foreach (string key in cells.Keys) {
						if (key.Equals("cols")) {
							// You can get access for table columns here						
						} else if (key.Equals("rows")) {
							List<object> rows = cells[key] as List<object>;
							
							foreach(object rk in rows) {
								Dictionary<string, object> rcells = rk as Dictionary<string, object>;
								
								foreach (string crk in rcells.Keys) {
									List<object> d = rcells[crk] as List<object>;
									
									foreach(object res in d) {
										
										Dictionary<string, object> resource = res as Dictionary<string, object>;
										if (resource != null) {
											foreach(string rkey in resource.Keys) {
												if (rkey == "v") {
													if (resource[rkey] == null) {
														spreadsheet.SetData(tableListId, column, row, "null");
													} else {
														spreadsheet.SetData(tableListId, column, row, resource[rkey].ToString());
													}
													row++;
												}
											}
										} else {
											spreadsheet.SetData(tableListId, column, row, "null");
											row++;
										}
									}
								}
								row = 1;
								column++;
							}
						}
					}
					tableEntryIndex++;
				}
			}
		}
		
	}

}
