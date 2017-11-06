using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScamButton : MonoBehaviour {

    public ScamController sc;
    static int buttonID = 1;

    public void OnMouseDown()
    {
        sc.DoStuffWhenAButtonIsClicked(buttonID);
    }
}
