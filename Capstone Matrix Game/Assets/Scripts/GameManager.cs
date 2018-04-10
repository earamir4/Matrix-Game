using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region GameManager Variables

    // UI
    public GameObject submissionResultPanel;
	public Text resultText;
	public Image resultPanelImage;

	// Question and answer values
	public string QuestionString;
    public float MatrixValueA;
    public float MatrixValueB;
    public float MatrixValueC;
    public float MatrixValueD;

    private Matrix2x2 SolutionMatrix;
    
    private float answertime;
    public string playername;

	public Text QuestionText;

	public static string NameOfMainMenuScene = "MainMenu";
    
    #endregion

    /// <summary>
    /// Instantiates the question being asked.
    /// <para>
    ///     The <see cref="SolutionMatrix"/> is based on four "matrix values".
    /// </para>
    /// </summary>
    void Start ()
    {
		if (QuestionText)
			QuestionText.text = QuestionString;
		SolutionMatrix = new Matrix2x2(MatrixValueA, MatrixValueB, MatrixValueC, MatrixValueD);
	}

	/// <summary>
	/// Compares the answer <see cref="Matrix2x2"/> provided by the Player with the <see cref="SolutionMatrix"/>.
	/// <para>
	///     The <see cref="resultText"/> will display whether or not the Player got the right answer.
	/// </para>
	/// <para>
	///     Additional information can be sent to the <see cref="MatrixLogger"/> as well.
	/// </para>
	/// </summary>
	/// <param name="answerMatrix">The final answer the User submitted.</param>
	public void CheckAnswer(Matrix2x2 answerMatrix)
    {
        bool isCorrect = Matrix2x2.IsEqual(SolutionMatrix, answerMatrix);

        if (isCorrect)
        {
            resultText.text = "Correct!";

			answertime = Time.timeSinceLevelLoad;
			CloudConnectorCore.UpdateObjects("playerInfo", "name", "Cameron Root", "q1", answertime.ToString() , true);

            MatrixLogger.Add("Correct! The answer was:\n" + SolutionMatrix.ToString());
            submissionResultPanel.SetActive(true);

            StopCoroutine("RemoveResultPanelAfterSomeSeconds");
            StartCoroutine("RemoveResultPanelAfterSomeSeconds");
		}
        else
        {
            MatrixLogger.Add("Incorrect! Your answer was:\n" + answerMatrix.ToString());

            resultText.text = "Incorrect...";
            submissionResultPanel.SetActive(true);

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

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(NameOfMainMenuScene);
	}

	public IEnumerator RemoveResultPanelAfterSomeSeconds()
	{
		yield return new WaitForSeconds(4f);

		float elapsedTime = 0;

		while(elapsedTime < 2f)
		{
			yield return null;
			elapsedTime += Time.unscaledDeltaTime;
			resultPanelImage.color = new Color(resultPanelImage.color.r, resultPanelImage.color.g, resultPanelImage.color.b, (1-elapsedTime / 2f));
			resultText.color = new Color(resultText.color.r, resultText.color.g, resultText.color.b, (1 - elapsedTime / 2f));
		}

		submissionResultPanel.SetActive(false);
		resultPanelImage.color = new Color(resultPanelImage.color.r, resultPanelImage.color.g, resultPanelImage.color.b, 1f);
		resultText.color = new Color(resultText.color.r, resultText.color.g, resultText.color.b, 1f);
	}
}
