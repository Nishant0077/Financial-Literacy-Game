using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice1ButtonScript : MonoBehaviour {

    public CreditCardGameController cc;
    static int buttonID = 1;    

    public void OnMouseDown()
    {
        cc.DoStuffWhenButtonIsClicked(buttonID);
    }
}
