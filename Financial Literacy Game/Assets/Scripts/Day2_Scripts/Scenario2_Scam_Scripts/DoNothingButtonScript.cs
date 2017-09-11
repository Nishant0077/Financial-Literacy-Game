using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNothingButtonScript : MonoBehaviour {

    public void OnMouseDown()
    {
        ScamController.b.gameObject.SetActive(true);
    }
}
