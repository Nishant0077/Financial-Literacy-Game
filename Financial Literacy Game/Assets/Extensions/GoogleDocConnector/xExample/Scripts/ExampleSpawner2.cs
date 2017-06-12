using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SA.GoogleDoc;

public class ExampleSpawner2 : MonoBehaviour {

	public Vector2 StartPos =  new Vector2(0, 0);
	public int[] SpawnArray;



	public GameObject spawnPattern;
	private bool IsStarted = false;

	void OnGUI() {
		if(GUI.Button(new Rect(10, 60, 100, 40), "Spawner2") ) {
			StartSpawn();
		}
	}


	private void StartSpawn() {
		if(IsStarted) {
			return;
		}
	
		IsStarted = false;

		float spawnPos =  StartPos.y;
		foreach(int count in SpawnArray) {
			for(int i = 0; i < count; i++) {
				float y = spawnPos;
				float x = StartPos.x + i;

				
				GameObject sp = Instantiate(spawnPattern) as GameObject;
				sp.transform.position = new Vector3(x, y, 0);
				sp.SetActive(true);
			}
			spawnPos--;
		}
	}
}
