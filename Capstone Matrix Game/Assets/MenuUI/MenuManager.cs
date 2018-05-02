using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// <see cref="MenuManager"/> handles the Main Menu UI logic.
/// <para>
///     Functions under the Scene Management section handle transitions to levels
///     or exiting the game.
/// </para>
/// <para>
///     Functions under the Panel Visibility section toggle panels being active/inactive.
/// </para>
/// </summary>
/// <remarks>
///     Author: Erick Ramirez Cordero
///     Date:   2/18/2018
/// </remarks>
public class MenuManager : MonoBehaviour
{
    #region Panel Variables
    public GameObject MainMenuPanel;
    public GameObject MainMenuButtons;
    public GameObject LevelSelectPanel;
    public GameObject CreditsPanel;

    public GameObject PartOnePanel;
    public GameObject PartTwoPanel;
    public GameObject PartThreePanel;
    public GameObject PartFourPanel;

    public GameObject LoginPanel;
    public GameObject NamePanel;
    public InputField NameInputField;
    public InputField PasswordInputField;
    public InputField URLInputField;
    public InputField WebsiteInputField;
    public GameObject LogoutPanel;
    public Text userName;

    public Text CurrentLevelText;

    public GameObject GSFUManager;
    private GameObject GSFU_Clone;

    private const string TEST_LEVEL = "Demo Level";
    #endregion

    public void Start()
    {
        if (GameObject.Find("GSFUManager") == null)
        {
            GSFU_Clone = Instantiate(GSFUManager);
            DontDestroyOnLoad(GSFU_Clone);
        }
    }

    public void UpdateUsername(string playerName)
    {
        GSFU_Clone.GetComponent<GSFU_Runtime>().NameInput(playerName);
        NameInputField.GetComponent<Text>().text = playerName;
        
    }
    public void UpdatePassword(string password)
    {
        GSFU_Clone.GetComponent<GSFU_Runtime>().PWInput(password);
        PasswordInputField.GetComponent<Text>().text = password;
    }
    public void UpdateURL(string URL)
    {
        GSFU_Clone.GetComponent<GSFU_Runtime>().URLInput(URL);
        URLInputField.GetComponent<Text>().text = URL;
    }
    public void UpdateID(string ID)
    {
        GSFU_Clone.GetComponent<GSFU_Runtime>().IDInput(ID);
        WebsiteInputField.GetComponent<Text>().text = ID;
    }


    #region Scene Management
    /// <summary>
    /// Updates the text based on the current level chosen
    /// </summary>
    public void UpdateLevelText(string textName)
    {
        CurrentLevelText.text = "Current Level: " + textName;
    }

    /// <summary>
    /// Loads scene based on scene index.
    /// </summary>
    /// <param name="index"></param>
    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }


    /// <summary>
    /// Loads scene based on name.
    /// </summary>
    /// <param name="name"></param>
    public void LoadSceneName(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    /// <summary>
    /// Loads the level denoted by the constant <see cref="TEST_LEVEL"/>.
    /// <para>
    ///     Warning: Only to be used for testing purposes!
    /// </para>
    /// </summary>
    public void LoadTestLevel()
    {
        SceneManager.LoadScene(TEST_LEVEL);
    }
    #endregion

    #region Panel Visibility
    /// <summary>
    /// Displays or hides the <see cref="LevelSelectPanel"/>.
    /// Hides the setting panel if active
    /// </summary>
    public void DisplayLevelSelect()
    {
        if (CreditsPanel.activeSelf)
            CreditsPanel.SetActive(false);
        LevelSelectPanel.SetActive(!LevelSelectPanel.activeSelf);
    }

    /// <summary>
    /// Displays <see cref="PartOnePanel"/> in the level select section.
    /// </summary>
    public void DisplayPartOnePanel()
    {
        PartOnePanel.SetActive(true);
        PartTwoPanel.SetActive(false);
        PartThreePanel.SetActive(false);
        PartFourPanel.SetActive(false);
    }

    /// <summary>
    /// Displays <see cref="PartTwoPanel"/> in the level select section.
    /// </summary>
    public void DisplayPartTwoPanel()
    {
        PartOnePanel.SetActive(false);
        PartTwoPanel.SetActive(true);
        PartThreePanel.SetActive(false);
        PartFourPanel.SetActive(false);
    }

    /// <summary>
    /// Displays <see cref="PartThreePanel"/> in the level select section.
    /// </summary>
    public void DisplayPartThreePanel()
    {
        PartOnePanel.SetActive(false);
        PartTwoPanel.SetActive(false);
        PartThreePanel.SetActive(true);
        PartFourPanel.SetActive(false);
    }

    /// <summary>
    /// Displays <see cref="PartFourPanel"/> in the level select section.
    /// </summary>
    public void DisplayPartFourPanel()
    {
        PartOnePanel.SetActive(false);
        PartTwoPanel.SetActive(false);
        PartThreePanel.SetActive(false);
        PartFourPanel.SetActive(true);
    }

    /// <summary>
    /// Displays or hides the <see cref="CreditsPanel"/>.
    /// Hides the level select panel if it's active.
    /// </summary>
    public void DisplayCredits()
    {
        if (LevelSelectPanel.activeSelf)
            LevelSelectPanel.SetActive(false);
        CreditsPanel.SetActive(!CreditsPanel.activeSelf);
    }

    /// <summary>
    /// Displays the <see cref="MainMenuPanel"/>, hiding all other panels.
    /// </summary>
    public void ReturnToMain()
    {
        MainMenuButtons.SetActive(true);

        if (LevelSelectPanel.activeInHierarchy)
        {
            LevelSelectPanel.SetActive(false);
            PartOnePanel.SetActive(false);
            PartTwoPanel.SetActive(false);
            PartThreePanel.SetActive(false);
            PartFourPanel.SetActive(false);
        }

        CreditsPanel.SetActive(false);
    }

    /// <summary>
    /// Moves on to the <see cref="LogoutPanel"/> if there is text to submit 
    /// </summary>
    public void SubmitGSFUInfo()
    {
        if (!string.IsNullOrEmpty(NameInputField.text))
        {
            GSFU_Clone.GetComponent<GSFU_Runtime>().SubmitInfo(NameInputField.GetComponent<Text>().text, PasswordInputField.GetComponent<Text>().text, URLInputField.GetComponent<Text>().text, WebsiteInputField.GetComponent<Text>().text);
            NamePanel.SetActive(false);
            LogoutPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Handles the rest of the GSFU panels: 
    ///     - Login changes to Submit
    ///     - Name cancels back into Login
    ///     - Logout changes to Login
    /// </summary>
    public void DisplayLogin_Logout()
    {
        if (LoginPanel.activeSelf == true)
        {
            NamePanel.SetActive(true);
            LoginPanel.SetActive(false);
        }

        //Called when user cancels submitting a name, which sends it back to the login panel
        else if (NamePanel.activeSelf == true)
        {
            LoginPanel.SetActive(true);
            NamePanel.SetActive(false);
        }

        else if (LogoutPanel.activeSelf == true)
        {
            LoginPanel.SetActive(true);
            LogoutPanel.SetActive(false);
        }
    }
    #endregion
    /*
    private void Update()
    {
        //PlayerPrefs.SetString("Playername", NameInputField.text);
        PlayerPrefs.SetString("Playername", NameInputField.text);
        PlayerPrefs.SetString("URL", URLInputField.text);
        PlayerPrefs.SetString("Password", PasswordInputField.text);
        PlayerPrefs.SetString("ID", WebsiteInputField.text);
    }
    */

}
