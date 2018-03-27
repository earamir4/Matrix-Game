using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_Script : MonoBehaviour
{

    public GameObject SideMenu;
    public GameObject MatrixRenderer;
    public GameObject WorkspacePanel;
    public GameObject SideMenuHideShowButton;

    // Use this for initialization
    void Start()
    {

    }

    public void ShowHideMenu()
    {
        if (SideMenu.activeSelf == false)
        {
            SideMenu.SetActive(true);
            SideMenuHideShowButton.GetComponentInChildren<Text>().text = "Hide >";
        }
        else if (SideMenu.activeSelf == true)
        {
            SideMenu.SetActive(false);
            SideMenuHideShowButton.GetComponentInChildren<Text>().text = "< Show";
        }

    }
}
