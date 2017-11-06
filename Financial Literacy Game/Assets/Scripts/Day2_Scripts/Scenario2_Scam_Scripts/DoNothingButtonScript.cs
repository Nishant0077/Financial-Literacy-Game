using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNothingButtonScript : MonoBehaviour {

    public ScamController sc;
    static int buttonID = 2;

    public void OnMouseDown()
    {
        sc.DoStuffWhenAButtonIsClicked(buttonID);
    }
}
