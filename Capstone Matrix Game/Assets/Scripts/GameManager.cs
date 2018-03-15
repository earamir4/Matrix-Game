using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject QuestionPanel;
    public Text QuestionText;

    private Matrix2x2 SolutionMatrix;

	// Use this for initialization
	void Start ()
    {
        QuestionText.text = "Test message: Hello world!";

        SolutionMatrix = new Matrix2x2(2, 2, 2, 2);
	}

    /// <summary>
    /// Toggles the visibility of the <see cref="QuestionPanel"/>.
    /// </summary>
    public void ToggleQuestionPanel()
    {
        QuestionPanel.SetActive(!QuestionPanel.activeInHierarchy);
    }

    /// <summary>
    /// Toggles the visibility of the <see cref="OptionsPanel"/>.
    /// </summary>
    public void ToggleOptionsPanel()
    {
        OptionsPanel.SetActive(!OptionsPanel.activeInHierarchy);
    }

    /// <summary>
    /// Compares the answer <see cref="Matrix2x2"/> provided by the Player with the <see cref="SolutionMatrix"/>.
    /// <para>
    ///     The <see cref="QuestionText"/> will display whether or not the Player got the right answer.
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
            QuestionText.text = "Correct answer!";
            MatrixLogger.Add("Correct! The answer was:\n" + SolutionMatrix.ToString());
        }
        else
        {
            QuestionText.text = "Incorrect answer!";
            MatrixLogger.Add("Incorrect! Your answer was:\n" + answerMatrix.ToString());
        }
    }

    /// <summary>
    /// Restarts the current level.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
