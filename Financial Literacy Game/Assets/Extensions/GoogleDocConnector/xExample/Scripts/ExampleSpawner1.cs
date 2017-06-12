using UnityEngine;
using System.Collections;
using SA.GoogleDoc;

public class ExampleSpawner1 : MonoBehaviour {

	public int SpawnRange = 5;
	public float SpawnDelay  = 1f;

	public GameObject spawnPattern;
	private bool IsStarted = false;



	void OnGUI() {
		if(GUI.Button(new Rect(10, 10, 100, 40), "Spawner1") ) {

			StartSpawn();
		//	SA.GoogleDoc.r
		}

	}

	private void StartSpawn() {
		if(IsStarted) {
			return;
		}

		Spawn();
		IsStarted = true;
		InvokeRepeating("Spawn", SpawnDelay, SpawnDelay);

	}

	private void Spawn() {

		float x = Random.Range(-SpawnRange, SpawnRange);
		float y = Random.Range(-SpawnRange, SpawnRange);
		
		GameObject sp = Instantiate(spawnPattern) as GameObject;
		sp.transform.position = new Vector3(x, y, 0);
		sp.SetActive(true);

	}


}
