using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDescription : MonoBehaviour
{
	public GameObject descriptionText;
	public GameObject hintsText;
	public Text buttonText;

	public void SwapStuff()
	{
		if (descriptionText.activeSelf)
		{
			descriptionText.SetActive(false);
			hintsText.SetActive(true);
			buttonText.text = "Back To Description";
        }
		else
		{
			descriptionText.SetActive(true);
			hintsText.SetActive(false);
			buttonText.text = "Hints";
		}
	}
}
