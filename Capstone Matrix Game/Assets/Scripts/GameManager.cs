using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text messageText;

    private Matrix2x2 solutionMatrix;

	// Use this for initialization
	void Start ()
    {
        messageText.text = "Test message: Hello world!";

        solutionMatrix = new Matrix2x2(2, 2, 2, 2);
	}
	
	// Update is called once per frame
	void Update ()
    {
		//
	}

    /// <summary>
    /// Compares the answer <see cref="Matrix2x2"/> provided by the Player with the <see cref="solutionMatrix"/>.
    /// <para>
    ///     The <see cref="messageText"/> will display whether or not the Player got the right answer.
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
            messageText.text = "Correct answer!";
            MatrixLogger.Add("Correct! The answer was:\n" + solutionMatrix.ToString());
        }
        else
        {
            messageText.text = "Incorrect answer!";
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
