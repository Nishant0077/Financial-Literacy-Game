using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SA.GoogleDoc;

public class DocDataRertivalExamle : MonoBehaviour {

	// Use this for initialization
	void Start () {


		string val;
		string DOC_NAME = "Google Doc Connector Example";


		//Using cell col / row indexes
		val = API.GetValue<string>(DOC_NAME, 1, 1);
		Debug.Log(val);

		//Using Cell class with col / row indexes
		Cell cell =  new Cell(1, 1);
		val = API.GetValue<string>(DOC_NAME, cell);
		Debug.Log(val);

		//Using Cell class with key representation
		cell =  new Cell("A1");
		val = API.GetValue<string>(DOC_NAME, cell);
		Debug.Log(val);



		List<int> array;
		CellRange range =  new CellRange();
		range.StartCell =  new Cell("C11");
		range.useLinebreak = true;
		range.LineLength = 3;

		array = API.GetList<int>(DOC_NAME, range);

		foreach (int data in array) {
			Debug.Log("List Data: " + data);
		}


	}



}
