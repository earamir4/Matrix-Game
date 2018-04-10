using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GSFU_Runtime : MonoBehaviour
{
    public string playername;

	void OnEnable()
	{
		// Suscribe for catching cloud responses.
		CloudConnectorCore.processedResponseCallback.AddListener(GSFU_Demo_Utils.ParseData);
	}
	
	void OnDisable()
	{
		// Remove listeners.
		CloudConnectorCore.processedResponseCallback.RemoveListener(GSFU_Demo_Utils.ParseData);
	}
	
    public void NameInput(string inputName)
    {
        playername = inputName;
    }

    public void ChangeName()
    {
        if (playername != null)
            PlayerPrefs.SetString("Playername", playername);
    }
}



