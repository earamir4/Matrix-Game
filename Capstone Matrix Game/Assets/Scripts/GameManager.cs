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
    public GameObject OptionsPanel;
    public GameObject QuestionPanel;
    public GameObject LogPanel;

    // Question and answer values
    public string QuestionString;
    public float MatrixValueA;
    public float MatrixValueB;
    public float MatrixValueC;
    public float MatrixValueD;

    // Private variables
    private Text QuestionText;
    private Matrix2x2 SolutionMatrix;
    
    private float answertime;
    public string playername;
    
    #endregion

    /// <summary>
    /// Instantiates the question being asked.
    /// <para>
    ///     The <see cref="SolutionMatrix"/> is based on four "matrix values".
    /// </para>
    /// </summary>
    void Start ()
    {
        SolutionMatrix = new Matrix2x2(MatrixValueA, MatrixValueB, MatrixValueC, MatrixValueD);

        if (QuestionText != null)
        {
            QuestionText = QuestionPanel.GetComponentInChildren<Text>();
            QuestionText.text = QuestionString;
        }

        SolutionMatrix = new Matrix2x2(MatrixValueA, MatrixValueB, MatrixValueC, MatrixValueD);
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

		if (LogPanel == null)
		{
			return;
		}

        if (!LogPanel.activeInHierarchy)
        {
            LogPanel.SetActive(true);
        }

        if (isCorrect)
        {
            QuestionText.text = "Correct!";
            MatrixLogger.Add("Correct! The answer was:\n" + SolutionMatrix.ToString());
	    
	    CloudConnectorCore.UpdateObjects("playerInfo", "name", playername, "q1", answertime.ToString() , true);
        }
        else
        {
            QuestionText.text = "Incorrect...";
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
    
    public void Update()
    {
        answertime = Time.timeSinceLevelLoad;
    }
}
