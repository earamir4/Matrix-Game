using UnityEngine;

public class GSFU_Runtime : MonoBehaviour
{
    public string playername;
       public string url;
    public string password;
    public string id;

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
    
    public void urlInput(string inputURL)
    {
        url = inputURL;
    }
    
    public void pwInput(string inputPW)
    {
        password = inputPW;
    }
    
    public void idInput(string inputID)
    {
        id = inputID;
    }

    public void ChangeName()
    {
        if (playername != null)
        {
            PlayerPrefs.SetString("Playername", playername);
            PlayerPrefs.SetString("URL", url);
            PlayerPrefs.SetString("Password",password);
            PlayerPrefs.SetString("ID", id);
            GSFU_Demo_Utils.namechange(playername);
        }
    }
}
