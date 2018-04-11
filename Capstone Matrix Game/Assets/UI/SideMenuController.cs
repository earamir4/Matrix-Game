using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuController : MonoBehaviour
{
	public GameObject OptionsPanel;
	public GameObject LogPanel;
	public GameObject TemplatesPanel;
    public GameObject VectorPanel;

	public void SwapToOptionsPanel()
	{
        VectorPanel.SetActive(false);
        TemplatesPanel.SetActive(false);
		OptionsPanel.SetActive(true);
		LogPanel.SetActive(false);
	}

	public void SwapToLogPanel()
	{
        VectorPanel.SetActive(false);
        TemplatesPanel.SetActive(false);
		OptionsPanel.SetActive(false);
		LogPanel.SetActive(true);
	}

	public void SwapToTemplatesPanel()
	{
        VectorPanel.SetActive(false);
        TemplatesPanel.SetActive(true);
		OptionsPanel.SetActive(false);
		LogPanel.SetActive(false);
	}

    public void SwapToVectorPanel()
    {
        VectorPanel.SetActive(true);
        TemplatesPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        LogPanel.SetActive(false);
    }
}
