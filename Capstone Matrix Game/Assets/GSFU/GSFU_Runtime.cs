using UnityEngine;

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

    public void SubmitInfo(string inputName, string password, string URL, string ID)
    {
        if (playername != null)
        {
            GSFU_Demo_Utils.createPlayer(playername);
        }
    }
}
