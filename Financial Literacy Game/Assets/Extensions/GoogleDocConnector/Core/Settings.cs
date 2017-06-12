using UnityEngine;
using System.IO;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace SA.GoogleDoc {


	#if UNITY_EDITOR
	[InitializeOnLoad]
	#endif
	public class Settings : ScriptableObject {

		public const string VERSION_NUMBER = "2.0";

		private const string GDAssetName = "GoogleDocConnectorSettings";
		private const string GDAssetExtension = ".asset";

		public static string SERVICE_NAME = "GoogleDocConnector-v" + VERSION_NUMBER;

		public static string CLIENT_ID 		= "282525665220-5sc8be65ea34p0l0a3qs53krk2gf8tvk.apps.googleusercontent.com";
		public static string CLIENT_SECRET 	= "4rrrenfH5s2PcWp9mhucgyFB";
		public static string SCOPE 			= "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";
		public static string REDIRECT_URI 	= "urn:ietf:wg:oauth:2.0:oob";

		public static string GOOGLEDOC_URL_START = "https://docs.google.com/spreadsheets/d/";
		public static string GOOGLEDOC_URL_END = "#gid=";

		public static string SPREADSHEET_URL_START = "https://spreadsheets.google.com/tq?&tq=&key=";
		public static string SPREADSHEET_URL_END = "&gid=";

		public static string SCRIPT_URL_START = "https://script.google.com/macros/s/AKfycbwrDJWLwK7BuA0KCOap3uavVFdPpoqGoffJu9zxigOO-kuAUFw/exec?key=";
		public static string SCRIPT_URL_PARAM = "&sheetid=";

		public static string PATH_ADDING 	= "Resources/Cache/";

		public string CachePath = "Extensions/GoogleDocConnector";

		public List<DocTemplate> docs = new List<DocTemplate>();

		private static Settings instance = null;

		
		public static Settings Instance {
			
			get {
				if (instance == null) {
					instance = Resources.Load(GDAssetName) as Settings;

					if (instance == null) {
						
						// If not found, autocreate the asset object.
						instance = CreateInstance<Settings>();
						#if UNITY_EDITOR

						SA_FileStaticAPI.CreateFolder(SA_Config.SettingsPath);
						string fullPath = Path.Combine(Path.Combine("Assets", SA_Config.SettingsPath),
						                               GDAssetName + GDAssetExtension
						                               );
						AssetDatabase.CreateAsset(instance, fullPath);
						#endif
					}

				}
				return instance;
			}
		}

		public static string GetCachePath() {
				#if UNITY_EDITOR
				SA_FileStaticAPI.CreateFolder(Settings.Instance.CachePath + Settings.PATH_ADDING);
				#endif
				return Application.dataPath + "/" + Settings.Instance.CachePath + Settings.PATH_ADDING;
		}

		public static string GetCachePath(string docName) {
			return GetCachePath() + docName + ".txt";
		}

		public void AddDoc(DocTemplate tpl) {
			docs.Add(tpl);
		}
		public void RemoveDoc(DocTemplate tpl) {
			docs.Remove(tpl);
		}



		public int GetDocIndexByName(string name) {
			foreach(DocTemplate d in docs) {
	            if (d != null) {
	                if (name.Equals(d.name)) {
	                    int index = docs.IndexOf(d) + 1;
	                    return index;
	                }
	            }
			}

			return 0;
		}


			public int GetDocIndexByKey(string key) {
				foreach(DocTemplate d in docs) {
					if (d != null) {
						if (key.Equals(d.key)) {
							int index = docs.IndexOf(d) + 1;
							return index;
						}
					}
				}

				return 0;
			}




		public DocTemplate GetDocByName(string name) {
			
			foreach(DocTemplate d in docs) {
	            if (d != null) {
	                if(name.Equals(d.name)) {
					    return d;
				    }
	            }
			}

			return null;
		}

		public DocTemplate GetDocByKey(string key) {

			foreach(DocTemplate d in docs) {
				if (d != null) {
						if(key.Equals(d.key)) {
						return d;
					}
				}
			}

			return null;
		}


		public DocTemplate GetDocByIndex(int index) {
		
			if(index == 0) {
				return null;
			}

			index--;


			if(index >= docs.Count) {
				return null;
			} else {
				return docs[index];
			}
		}



		public string[] GetDocNames() {
			List<string> names =  new List<string>();
			names.Add("None");
			foreach(DocTemplate d in docs) {
	            if (d != null) {
	                names.Add(d.name);
	            }
			}
			
			return names.ToArray();
		}
	

		public static void OpenDocURL(DocTemplate doc, WorksheetTemplate worksheet = null) {
			int id = 0;
			if(worksheet != null) {
				id = worksheet.listId;
			}
			Application.OpenURL(Settings.GOOGLEDOC_URL_START + doc.key + Settings.GOOGLEDOC_URL_END + id.ToString());
		}


	}

}
