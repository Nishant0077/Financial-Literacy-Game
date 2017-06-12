using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SA.GoogleDoc;

public class LocalizationFromDocExample : MonoBehaviour {

	public GUIStyle GUITextStyle;


	private const string DOC_NAME = "Google Doc Connector Example";
	private const string LOCALIZATION_WORK_SHEET_NAME = "Localization";



	private List<string> LangCodes;
	private Dictionary<string, Dictionary<string, string>> languages =  new Dictionary<string, Dictionary<string, string>>();


	void Awake() {
		//Getting avaliable languages codes:
		CellRange range = new CellRange("B1");
		LangCodes = API.GetList<string>(DOC_NAME, range, LOCALIZATION_WORK_SHEET_NAME);


		//creating range of keys (tcokens);
		CellRange keysRange = new CellRange("A2", RanageDirection.Collumn);

		foreach(string lang in LangCodes) {

			//creating dictionary range for current language
			//the row shift is 0
			//the coll shoft is lang index in LangCodes list
			CellDictionaryRange dict = new CellDictionaryRange(keysRange, LangCodes.IndexOf(lang) + 1, 0);


			//filling languages dictionary
			languages.Add(lang, API.GetDictionary<string, string>(DOC_NAME, dict, LOCALIZATION_WORK_SHEET_NAME));
			
		}


		//Setting defult language code and retriving all avaliables tockens
		//this section is only for demo GUI redner

		tokens = API.GetList<string>(DOC_NAME, keysRange, LOCALIZATION_WORK_SHEET_NAME);
		currentLangCode = LangCodes[0];

	}


	private List<string> tokens;
	private string currentLangCode;
	void OnGUI() {

		int x = 10;
		int y = 10;
		foreach(string lang in LangCodes) {
			if(GUI.Button( new Rect(x, y, 140, 60), lang)) {
				currentLangCode = lang;
			}
			x += 170;
		}

		x = 10;
		y = 100;
		foreach(string token in tokens) {
			GUI.Label(new Rect(x, y, 140, 20),"Token: " + token, GUITextStyle);
			GUI.Label(new Rect(x + 300, y, 140, 20),"Localized String: " + GetLocalizedString(currentLangCode, token), GUITextStyle);
			y += 25;
		}
	}



	private string GetLocalizedString(string langCode, string tocken) {
		if(languages.ContainsKey(langCode)) {
			if(languages[langCode].ContainsKey(tocken)) {
				return languages[langCode][tocken];
			}
		}

		return string.Empty;
	}

}
