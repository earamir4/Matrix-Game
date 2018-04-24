using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoAreaController : MonoBehaviour
{
	public GameObject levelInfoPanel;
	public GameObject levelDescriptionPanel;
	public GameObject levelHintsPanel;

	public void ToggleShowingLevelInfo()
	{
		levelInfoPanel.SetActive(!levelInfoPanel.activeSelf);
    }

	public void ShowLevelDescription()
	{
		levelDescriptionPanel.SetActive(true);
		levelHintsPanel.SetActive(false);
    }

	public void ShowLevelHints()
	{
		levelDescriptionPanel.SetActive(false);
		levelHintsPanel.SetActive(true);
	}
}
