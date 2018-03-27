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
    public GameObject SettingsPanel;

    public GameObject PartOnePanel;
    public GameObject PartTwoPanel;
    public GameObject PartThreePanel;
    public GameObject PartFourPanel;

    public GameObject LoginPanel;
    public GameObject NamePanel;
    public InputField NameInputField;
    public GameObject LogoutPanel;

    public Text CurrentLevelText;

    public GameObject GSFUManager;

    private const string TEST_LEVEL = "FullMatrixUI";
    #endregion

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
        if (SettingsPanel.activeSelf)
            SettingsPanel.SetActive(false);
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
    /// Displays or hides the <see cref="SettingsPanel"/>.
    /// Hides the level select panel if it's active
    /// </summary>
    public void DisplaySettings()
    {
        if (LevelSelectPanel.activeSelf)
            LevelSelectPanel.SetActive(false);
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
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
        
        SettingsPanel.SetActive(false);
    }

    /// <summary>
    /// Moves on to the <see cref="LogoutPanel"/> if there is text to submit 
    /// </summary>
    public void SubmitName()
    {
        if (NameInputField.text != null)
        {
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

}
