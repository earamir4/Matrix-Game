using UnityEngine;

public class GSFU_Runtime : MonoBehaviour
{
    public string playername;
    public string url;
    public string password;
    public string id;
    /*
    private void Update()
    {
        playername = PlayerPrefs.GetString("Playername");
        url = PlayerPrefs.GetString("URL");
        password = PlayerPrefs.GetString("Password");
        id = PlayerPrefs.GetString("ID");

    }
    */
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
        PlayerPrefs.SetString("Name", playername);

    }

    public void URLInput(string inputURL)
    {
        url = inputURL;
        PlayerPrefs.SetString("URL", url);
    }

    public void PWInput(string inputPW)
    {
        password = inputPW;
        PlayerPrefs.SetString("Password", url);
    }

    public void IDInput(string inputID)
    {
        id = inputID;
        PlayerPrefs.SetString("ID", url);
    }

    public void SubmitInfo(string inputName, string password, string URL, string ID)
    {
        if (playername != null)
        {
            GSFU_Demo_Utils.createPlayer(playername);
        }
    }
}
