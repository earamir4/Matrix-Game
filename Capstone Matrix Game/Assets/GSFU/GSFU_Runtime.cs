using UnityEngine;

public class GSFU_Runtime : MonoBehaviour
{
    void OnEnable()
    {
        // Suscribe for catching cloud responses.
        CloudConnectorCore.processedResponseCallback.AddListener(GSFU_Demo_Utils.ParseData);
    }

    /// <summary>
    /// Remove listeners from <see cref="GSFU_Demo_Utils"/>.
    /// </summary>
    void OnDisable()
    {
        CloudConnectorCore.processedResponseCallback.RemoveListener(GSFU_Demo_Utils.ParseData);
    }

    public void SubmitInfo(string inputName, string password, string URL, string ID)
    {
        if (!string.IsNullOrEmpty(inputName))
        {
            PlayerPrefs.SetString("Name", inputName);
            PlayerPrefs.SetString("URL", URL);
            PlayerPrefs.SetString("ID", ID);
            PlayerPrefs.SetString("Password", password);
            GSFU_Demo_Utils.createPlayer(inputName);
        }
    }
}
