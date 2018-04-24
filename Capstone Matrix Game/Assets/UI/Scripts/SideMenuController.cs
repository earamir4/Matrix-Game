using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuController : MonoBehaviour
{
    public GameObject TemplatesPanel;
    public GameObject VectorPanel;
    public GameObject popupMenu;

    public void SwapToTemplatesPanel()
    {
        TemplatesPanel.SetActive(true);
        VectorPanel.SetActive(false);
    }

    public void SwapToVectorPanel()
    {
        TemplatesPanel.SetActive(false);
        VectorPanel.SetActive(true);
    }

    public void ShowPopupMenu()
    {
        popupMenu.SetActive(true);
    }

    public void HidePopupMenu()
    {
        popupMenu.SetActive(false);
    }

}