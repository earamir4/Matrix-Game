using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuController : MonoBehaviour
{
	public GameObject OptionsPanel;
	public GameObject LogPanel;
	public GameObject TemplatesPanel;

	public void SwapToOptionsPanel()
	{
		TemplatesPanel.SetActive(false);
		OptionsPanel.SetActive(true);
		LogPanel.SetActive(false);
	}

	public void SwapToLogPanel()
	{
		TemplatesPanel.SetActive(false);
		OptionsPanel.SetActive(false);
		LogPanel.SetActive(true);
	}

	public void SwapToTemplatesPanel()
	{
		TemplatesPanel.SetActive(true);
		OptionsPanel.SetActive(false);
		LogPanel.SetActive(false);
	}
}
