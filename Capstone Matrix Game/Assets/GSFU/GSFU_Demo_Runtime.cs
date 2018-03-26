using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GSFU_Demo_Runtime : MonoBehaviour
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
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 20, 140, 25), "Create Table"))
			GSFU_Demo_Utils.CreatePlayerTable(true);


		


        if (GUI.Button(new Rect(20, 300, 140, 25), "Test"))
            GSFU_Demo_Utils.namechange(playername);
    }
	
	
}



