using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject LevelSelectPanel;
    public GameObject SettingsPanel;

    public GameObject PartOnePanel;
    public GameObject PartTwoPanel;
    public GameObject PartThreePanel;
    public GameObject PartFourPanel;

    private const string TEST_LEVEL = "FullMatrixUI";
    #endregion

    #region Scene Management
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
    /// Displays the <see cref="LevelSelectPanel"/>.
    /// </summary>
    public void DisplayLevelSelect()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
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
    /// Displays the <see cref="SettingsPanel"/>.
    /// </summary>
    public void DisplaySettings()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    /// <summary>
    /// Displays the <see cref="MainMenuPanel"/>, hiding all other panels.
    /// </summary>
    public void ReturnToMain()
    {
        MainMenuPanel.SetActive(true);

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
    #endregion
}
