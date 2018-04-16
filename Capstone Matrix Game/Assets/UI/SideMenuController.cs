using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuController : MonoBehaviour
{
	public GameObject LogPanel;
	public GameObject TemplatesPanel;
	public GameObject popupMenu;

	public void SwapToLogPanel()
	{
		TemplatesPanel.SetActive(false);
		LogPanel.SetActive(true);
	}

	public void SwapToTemplatesPanel()
	{
		TemplatesPanel.SetActive(true);
		LogPanel.SetActive(false);
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
