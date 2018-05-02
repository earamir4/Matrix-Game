using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The <see cref="GameManager"/> gives feedback to the User whenever a solution is submitted.
/// <para>
///     The <see cref="GameManager"/> is also responsible for scene transitions.
/// </para>
/// <para>
///     Data is recorded to the Google Sheets for Unity (GSFU) system and the <see cref="MatrixLogger"/>.
/// </para>
/// </summary>
public class GameManager : MonoBehaviour
{
    #region UI
    public GameObject SubmissionResultPanel;
    public GameObject QuestionPanel;
    public Text QuestionText;
    public Text ResultText;
	public Image ResultPanelImage;
    #endregion

    #region Question and Answer Values
    public string Playername;

    public string QuestionString;
    public float MatrixValueA;
    public float MatrixValueB;
    public float MatrixValueC;
    public float MatrixValueD;

    private Matrix2x2 solutionMatrix;
    private float answertime;
    public string q;

    public const string MAIN_MENU_NAME = "MainMenu";
    #endregion

    /// <summary>
    /// Instantiates the question being asked.
    /// <para>
    ///     The <see cref="solutionMatrix"/> is based on four "matrix values".
    /// </para>
    /// </summary>
    void Start ()
    {
		if (QuestionText)
        {
            QuestionText.text = QuestionString;
        }

		solutionMatrix = new Matrix2x2(MatrixValueA, MatrixValueB, MatrixValueC, MatrixValueD);
	}

	/// <summary>
	/// Compares the answer <see cref="Matrix2x2"/> provided by the Player with the <see cref="solutionMatrix"/>.
	/// <para>
	///     The <see cref="ResultText"/> will display whether or not the Player got the right answer.
    ///     This information will be recorded in the Google Sheets for Unity system (GSFU) via <see cref="CloudConnectorCore"/>.
	/// </para>
	/// <para>
	///     Additional information can be sent to the <see cref="MatrixLogger"/> as well.
	/// </para>
	/// </summary>
	/// <param name="answerMatrix">The final answer the User submitted.</param>
	public void CheckAnswer(Matrix2x2 answerMatrix)
    {
        bool isCorrect = Matrix2x2.IsEqual(solutionMatrix, answerMatrix);

        if (isCorrect)
        {
            ResultText.text = "Correct!";
            answertime = Time.timeSinceLevelLoad;
            CloudConnectorCore.UpdateObjects("playerInfo", "name", Playername, q, answertime.ToString() , true);

            MatrixLogger.Add("Correct! The answer was:\n" + solutionMatrix.ToString());
            SubmissionResultPanel.SetActive(true);

            StopCoroutine("RemoveResultPanelAfterSomeSeconds");
            StartCoroutine("RemoveResultPanelAfterSomeSeconds");
		}
        else
        {
            MatrixLogger.Add("Incorrect! Your answer was:\n" + answerMatrix.ToString());

            ResultText.text = "Incorrect...";
            SubmissionResultPanel.SetActive(true);

            StopCoroutine("RemoveResultPanelAfterSomeSeconds");
            StartCoroutine("RemoveResultPanelAfterSomeSeconds");
        }
    }

    /// <summary>
    /// Restarts the current level.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
	public void GoToMainMenu()
	{
		SceneManager.LoadScene(MAIN_MENU_NAME);
	}

    /// <summary>
    /// Goes to the next level.
    /// <para>
    ///     If the Player is on the last level, then the game returns to the main menu.
    /// </para>
    /// </summary>
    public void NextScene()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel > SceneManager.sceneCountInBuildSettings)
        {
            GoToMainMenu();
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    /// <summary>
    /// TODO: Add method description and rename the method.
    /// </summary>
    /// <returns></returns>
	public IEnumerator RemoveResultPanelAfterSomeSeconds()
	{
		yield return new WaitForSeconds(4f);

		float elapsedTime = 0;

		while(elapsedTime < 2f)
		{
			yield return null;
			elapsedTime += Time.unscaledDeltaTime;
			ResultPanelImage.color = new Color(ResultPanelImage.color.r, ResultPanelImage.color.g, ResultPanelImage.color.b, (1-elapsedTime / 2f));
			ResultText.color = new Color(ResultText.color.r, ResultText.color.g, ResultText.color.b, (1 - elapsedTime / 2f));
		}

		SubmissionResultPanel.SetActive(false);
		ResultPanelImage.color = new Color(ResultPanelImage.color.r, ResultPanelImage.color.g, ResultPanelImage.color.b, 1f);
		ResultText.color = new Color(ResultText.color.r, ResultText.color.g, ResultText.color.b, 1f);
	}
}
