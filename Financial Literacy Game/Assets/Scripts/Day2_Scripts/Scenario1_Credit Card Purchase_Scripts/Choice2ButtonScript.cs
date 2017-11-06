using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice2ButtonScript : MonoBehaviour
{
    public CreditCardGameController cc;
    static int buttonID = 2;
    
    public void OnMouseDown()
    {
        cc.DoStuffWhenButtonIsClicked(buttonID);
    }
}