using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverTextScript : MonoBehaviour {

    public GameObject popUpMenu;

    public void OnMouseOver()
    {
        Debug.Log("Mouse over");
        popUpMenu.SetActive(true);
    }
    public void OnMouseExit()
    {
        Debug.Log("Mouse exit");
        popUpMenu.SetActive(false);
    }



}
