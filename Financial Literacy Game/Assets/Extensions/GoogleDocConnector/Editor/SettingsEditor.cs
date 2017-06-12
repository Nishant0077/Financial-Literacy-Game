using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SA.GoogleDoc {

	[InitializeOnLoad]
	[CustomEditor(typeof(Settings))]
	public class SettingsEditor : Editor {


		GUIContent SdkVersion   = new GUIContent("Plugin Version [?]", "This is Plugin version.  If you have problems or compliments please include this so we know exactly what version to look out for.");



		GUIContent docDisplayLabel = new GUIContent("Display Name [?]", "Wou will use this document name as data source");
		GUIContent docPublicUrlLabel = new GUIContent("Public Key [?]", "Document public key");

		GUIContent tableListNameLabel = new GUIContent ("Worksheet Name [?]", "Worksheet name");
		GUIContent tableListIdLabel = new GUIContent ("Worksheet ID [?]", "Worksheet public id");

		GUIContent cacheLocationLable = new GUIContent("Cache Localition [?]", "Path to cache files locaion relative to project folder");
				 
		private bool showDocs = true;
		private bool cacheSettingsOpen = true;
		
		public override void OnInspectorGUI() {

			GUI.changed = false;

			SA_EditorTool.BlockHeader("Google Doc Connector Settings");
			EditorGUILayout.Space();

			Documents();
			EditorGUILayout.Space();

			CacheSettings();
			AboutGUI();
		

			if(GUI.changed) {
				EditorUtility.SetDirty(Settings.Instance);
			}
		}


		private const string WORKSHEETS_LABEL = "worksheets_label";
		private const string WORKSHEETS_ID = "worksheets_id";
		private const string WORKSHEETS_NAME = "worksheets_name";
		private void Documents() {
		

			showDocs = EditorGUILayout.Foldout(showDocs, "Spreadsheets");
			if(showDocs) {
				if (Settings.Instance.docs.Count == 0) {
					EditorGUILayout.HelpBox("You haven't add any Spreadsheets yet", MessageType.Error);
				}

				foreach(DocTemplate doc in Settings.Instance.docs) {
					
					EditorGUI.indentLevel = 1;
					EditorGUILayout.BeginVertical (GUI.skin.box);

					EditorGUILayout.BeginHorizontal();

					doc.IsOpen = EditorGUILayout.Foldout(doc.IsOpen, doc.name);
					EditorGUILayout.Space();

					if (!doc.CreationTime.Equals(string.Empty)) {
						Color oldColor = GUI.color;
						GUI.color = Color.green;
						GUI.skin.label.alignment = TextAnchor.MiddleRight;
						EditorGUILayout.LabelField(doc.CreationTime, GUILayout.Width(120));
						GUI.color = oldColor;
					} else {
						Color oldColor = GUI.color;
						GUI.color = Color.red;
						GUI.skin.label.alignment = TextAnchor.MiddleRight;
						EditorGUILayout.LabelField("Cache not found. Press 'Update' button", GUILayout.Width(245));
						GUI.color = oldColor;
					}
					
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Space();

					if(doc.IsOpen) {

						EditorGUI.indentLevel = 2;

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(docDisplayLabel);

						string newDocName = EditorGUILayout.TextField(doc.name);
						if (newDocName != doc.name) {
							if (File.Exists(Settings.GetCachePath(doc.name))) {
								// Check for the file with the same name
								if (!File.Exists(Settings.GetCachePath(newDocName))) {
									File.Move(Settings.GetCachePath(doc.name), Settings.GetCachePath(newDocName));

								}

								AssetDatabase.Refresh();
							}

							doc.name = newDocName;
						}

						EditorGUILayout.EndHorizontal();


						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(docPublicUrlLabel);
						doc.key	 	= EditorGUILayout.TextField(doc.key);
						EditorGUILayout.EndHorizontal();

						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.Space();

						if (GUILayout.Button("Open", GUILayout.Width(100))) {
							Application.OpenURL(Settings.GOOGLEDOC_URL_START + doc.key + Settings.GOOGLEDOC_URL_END + "0");
						}

						if(GUILayout.Button("Update",  GUILayout.Width(100))) {

							#if UNITY_WEBPLAYER
							EditorUtility.DisplayDialog("Update Not Available", "Document cash update is not available under the Web Player platform. Since writing files is forbidden in web player platform. Switch to any other platform for updating documents cash. ", "Okay");
							#else

							API.RetivePublicSheetData(doc);
											

							return;
							#endif
						}

						if(GUILayout.Button("Remove",  GUILayout.Width(100))) {

							Settings.Instance.RemoveDoc(doc);
							File.Delete(Settings.GetCachePath(doc.name));

							AssetDatabase.Refresh();

							return;
						}
						
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();

						if (doc.Worksheets.Count > 0) {
							EditorGUILayout.BeginVertical();
							GUI.SetNextControlName(WORKSHEETS_LABEL);

							doc.IsTableListOpen = EditorGUILayout.Foldout(doc.IsTableListOpen, "Additional Worksheets");
							if (doc.IsTableListOpen) {
								foreach (WorksheetTemplate worksheet in doc.Worksheets){
									
									EditorGUI.indentLevel = 2;
									EditorGUILayout.Space();
									
									EditorGUI.indentLevel = 3;

									if (doc.WorksheetExist(worksheet.listName)){
										EditorGUILayout.HelpBox("Worksheet with such Name already exist", MessageType.Warning);
									}

									EditorGUILayout.BeginHorizontal();
									EditorGUILayout.LabelField(tableListNameLabel);
									GUI.SetNextControlName(WORKSHEETS_NAME);
									worksheet.listName = EditorGUILayout.TextField(worksheet.listName);
									if (worksheet.listId == 0 && doc.Worksheets.IndexOf(worksheet) == 0) {
										GUI.enabled = false;
									}
									EditorGUILayout.EndHorizontal();

									if (doc.WorksheetExist(worksheet.listId) && doc.Worksheets.IndexOf(worksheet) != 0) {
										EditorGUILayout.HelpBox("Worksheet with such Id already exist", MessageType.Warning);
									}

									EditorGUILayout.BeginHorizontal();
									EditorGUILayout.LabelField(tableListIdLabel);
									GUI.SetNextControlName(WORKSHEETS_ID);
									worksheet.listId = EditorGUILayout.IntField(worksheet.listId);
									EditorGUILayout.EndHorizontal();

									EditorGUILayout.BeginHorizontal();
									EditorGUILayout.Space();

									bool temp = GUI.enabled;
									GUI.enabled = !(doc.WorksheetExist(worksheet.listId) || doc.WorksheetExist(worksheet.listName));
									if(GUILayout.Button("Update Worksheet",  GUILayout.Width(150))) {
									#if UNITY_WEBPLAYER
										EditorUtility.DisplayDialog("Update Not Available", "Document cash update is not available under the Web Player platform. Since writing files is forbidden in web player platform. Switch to any other platform for updating documents cash. ", "Okay");
									#else
										float CurrentProgres = 0f;
										EditorUtility.DisplayProgressBar("Cache " + worksheet.listName + " Worksheet", doc.name, CurrentProgres);
										API.RetivePublicSheetData(doc, worksheet, true);

										CurrentProgres += 100.0f;
										EditorUtility.DisplayProgressBar("Cache " + worksheet.listName + " Worksheet", doc.name, CurrentProgres);									
										EditorUtility.ClearProgressBar();
									#endif
									}
									GUI.enabled = temp;

									if(GUILayout.Button("Remove Worksheet",  GUILayout.Width(150))) {
										if (GUI.GetNameOfFocusedControl().Equals(WORKSHEETS_NAME) || GUI.GetNameOfFocusedControl().Equals(WORKSHEETS_ID)) {
											GUI.FocusControl(WORKSHEETS_LABEL);
										}
										doc.RemoveWorksheet(worksheet);
										return;
									}
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.Space();
									GUI.enabled = true;
								}

								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.Space();
								if (GUILayout.Button ("Add New Worksheet", GUILayout.Width(150))) {
									doc.AddNewWorksheet();
									GUI.FocusControl(WORKSHEETS_LABEL);
								}
								EditorGUILayout.EndHorizontal();
								EditorGUILayout.Space();
							}
							EditorGUILayout.EndVertical();
						}
					}
					
					EditorGUILayout.EndVertical ();
					
				}


				EditorGUI.indentLevel = 0;
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.Space();

				if (Settings.Instance.docs.Count > 0) {
					if(GUILayout.Button("Update All",  GUILayout.Width(150))) {

						#if UNITY_WEBPLAYER
							EditorUtility.DisplayDialog("Update Not Available", "Document cash update is not available under the Web Player platform. Since writing files is forbidden in web player platform. Switch to any other platform for updating documents cash. ", "Okay");
						#else
						UpateAllDocs();
						#endif
					}
				}

				if(GUILayout.Button("Add New Spreadsheet",  GUILayout.Width(150))) {
					DocTemplate newDoc = new DocTemplate();
					newDoc.InitDoc();
					Settings.Instance.AddDoc(newDoc);
				}
				
				EditorGUILayout.EndHorizontal();

			}


		}

		public static void UpateAllDocs() {
			float CurrentProgres = 0f;
			float progressStep = 100f / Settings.Instance.docs.Count;

			
			foreach(DocTemplate doc in Settings.Instance.docs) {
				EditorUtility.DisplayProgressBar("Cache Public Documents", doc.name, CurrentProgres);
				API.RetivePublicSheetData(doc, false);
				
				CurrentProgres += progressStep;
				EditorUtility.DisplayProgressBar("Cache Public Documents", doc.name, CurrentProgres);
			}
			
			EditorUtility.ClearProgressBar();
		}
		
		private const string LOGIN_FIELD_NAME = "login_field";
		private const string PASS_FIELD_NAME = "pass_field";
		private void AuthOptions() {

			/*

			
			EditorGUILayout.HelpBox("Authorization", MessageType.None);
			EditorGUILayout.Space();

			
			ClienLogin = EditorGUILayout.Foldout(ClienLogin, "Authorizing  with Login");
			if(ClienLogin) {

				EditorGUI.BeginChangeCheck();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(LoginLabel);
				GUI.SetNextControlName(LOGIN_FIELD_NAME);
				GoogleDocEditorPrefs.login = EditorGUILayout.TextField(GoogleDocEditorPrefs.login);
				EditorGUILayout.EndHorizontal();
				
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(PassLabel);
				GUI.SetNextControlName(PASS_FIELD_NAME);
				GoogleDocEditorPrefs.pass = EditorGUILayout.PasswordField(GoogleDocEditorPrefs.pass);
				EditorGUILayout.EndHorizontal();
				
				if (EditorGUI.EndChangeCheck()){
					GDConnector.AuthFailed = false;
					Debug.Log("GDConnector.AuthFailed = false");
				}

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				if(GUILayout.Button("Test Auth",  GUILayout.Width(120))) {
					try {

						SpreadsheetsService service = new SpreadsheetsService(Settings.SERVICE_NAME);
						service.setUserCredentials(GoogleDocEditorPrefs.login, GoogleDocEditorPrefs.pass);
						string token = service.QueryClientLoginToken();

						EditorUtility.DisplayDialog("Auth Succeeded", "Auth Approved. Tocken: \n " + token, "Ok");

					} catch(System.Exception e) {
						EditorUtility.DisplayDialog("Auth Failed", e.Message, "Ok");
					}
				}

				EditorGUILayout.EndHorizontal();
				
			}

			OAuth = EditorGUILayout.Foldout(OAuth, "Authorizing with OAuth 2.0");
			if(OAuth) {

				EditorGUILayout.HelpBox("Wating Auth API fix, should appear in next versions", MessageType.None);

				GUI.enabled = false;

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(AccessCodeLabel);
				GoogleDocEditorPrefs.AccessCode = EditorGUILayout.TextField(GoogleDocEditorPrefs.AccessCode);
				EditorGUILayout.EndHorizontal();


				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				if(GUILayout.Button("Auth",  GUILayout.Width(80))) {
					OAuth2Parameters parameters = new OAuth2Parameters();
					parameters.ClientId 		= Settings.CLIENT_ID;
					parameters.ClientSecret 	= Settings.CLIENT_SECRET;
					parameters.RedirectUri		= Settings.REDIRECT_URI;
					parameters.Scope 			= Settings.SCOPE;
					string authorizationUrl 	= OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
					Application.OpenURL(authorizationUrl);
				}

				if(!GoogleDocEditorPrefs.AccessCode.Equals(string.Empty)) {
					if(GUILayout.Button("Test Access Code",  GUILayout.Width(120))) {



					}
				}

				EditorGUILayout.EndHorizontal();

				GUI.enabled = true;
			
			}


			EditorGUILayout.Space();

	*/
		}


		private bool ContainsNotCachedFiles() {
			string[] files = Directory.GetFiles (Settings.GetCachePath());

			foreach (string file in files) {
				string fileName = Path.GetFileName(file);
				fileName = fileName.Substring(0, fileName.IndexOf('.'));
				if (Settings.Instance.GetDocByName(fileName) == null) {
					return true;
				}
			}

			return false;
		}


		private bool IsCachePathWasFocused = false;
		private bool IsCachePathCorrect = false;
		private const string CACHE_PATH_FILED_NAME = "cache_path";
		private void CacheSettings() {

			SA_EditorTool.BlockHeader("Data Cache");

			if (Settings.Instance.CachePath.Equals ("Unset")) {
				Settings.Instance.CachePath = ""; //put default path here
				}

			cacheSettingsOpen = EditorGUILayout.Foldout(cacheSettingsOpen, "Cache Settings");
			if(cacheSettingsOpen) {


				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(cacheLocationLable);
				GUI.SetNextControlName(CACHE_PATH_FILED_NAME);
				Settings.Instance.CachePath = EditorGUILayout.TextField(Settings.Instance.CachePath);

				if(Settings.Instance.CachePath.Length == 0) {
					Settings.Instance.CachePath = Settings.Instance.CachePath + "/";
				} else {
					string last = Settings.Instance.CachePath.Substring(Settings.Instance.CachePath.Length-1, 1);
					if(!last.Equals("/")) {
						Settings.Instance.CachePath = Settings.Instance.CachePath + "/";
					}

				}

				if(GUI.GetNameOfFocusedControl().Equals(CACHE_PATH_FILED_NAME)) {
					IsCachePathWasFocused = true;
				} else {
					if(IsCachePathWasFocused) {
						IsCachePathWasFocused = false;
						
						if (Settings.Instance.CachePath.StartsWith("/")) {
							Settings.Instance.CachePath = Settings.Instance.CachePath.TrimStart('/');
						}					
					}
				}

				if (!Settings.Instance.CachePath.Equals("/")
				    && Directory.Exists(Settings.GetCachePath())){
					IsCachePathCorrect = true;
				} else{
					IsCachePathCorrect = false;
				}
				
				EditorGUILayout.EndHorizontal();
				if (!IsCachePathCorrect){
					EditorGUILayout.HelpBox("Path doesn't exist: " + Settings.GetCachePath(),
					                        MessageType.Error);
				}else{
					EditorGUILayout.HelpBox("Cache path: " + Settings.GetCachePath(),
					                        MessageType.Info);
					if (ContainsNotCachedFiles()) {
						EditorGUILayout.HelpBox("Cache folder contains unsed files", MessageType.Warning);
					}
				}
			}

		}

		private void AboutGUI() {
			SA_EditorTool.BlockHeader("About the Plugin");
			
			SA_EditorTool.SelectableLabelField(SdkVersion,   Settings.VERSION_NUMBER);
			SA_EditorTool.SupportMail();
			SA_EditorTool.DrawSALogo();

		}
		
		
		
	}

}

