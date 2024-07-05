using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager13 : MonoBehaviour
{
	public static int number1 = 5;
    public static int number2 = 7;
    public static int number3 = 3;
    public static int number4 = 4;

	public string answerString;
    //public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;

    public TextMeshProUGUI tens1;
    public TextMeshProUGUI ones1;
    public TextMeshProUGUI tens2;
    public TextMeshProUGUI ones2;

    //public TextMeshPro playerInput;

    // References needed for answer button
    public Button Button;
    public string userInput = "";
    private bool isInputActive = false;

    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup; // Reference to the pop-up canvas's CanvasGroup component


    //Mobile Keyboard Enabling
    public Canvas mobileKeyboard;
    private bool mobileVersion = true;
    public TextMeshProUGUI KeyboardInputText;

    // Start is called before the first frame update
    void Start()
    {
        int number1sum = Random.Range(10, 51);
        int number2sum = Random.Range(0, number1sum);
        number1 = number1sum / 10;
        number2 = number1sum % 10;
        number3 = number2sum / 10;
        number4 = number2sum % 10;

        tens1.text = number1.ToString();
        ones1.text = number2.ToString();
        tens2.text = number3.ToString();
        ones2.text = number4.ToString();

        int result = number1sum - number2sum;
        answerString = result.ToString();
    }

    public void Update()
    {
        if (isInputActive)
        {
            // Real Keyboard Usage
            if (mobileVersion != true)
            {
                // Check for input and handle it
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    checkStringInput();
                    isInputActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.Backspace) && userInput.Length > 0)
                {
                    userInput = userInput.Substring(0, userInput.Length - 1);
                }
                else
                {
                    userInput += Input.inputString;
                }
                inputText.text = userInput;
            }
        }
    }

    public void activateInput()
    {
        isInputActive = !isInputActive;

        if (isInputActive == true)
        { 
            // Reset answerbox input
            userInput = "";
            inputText.text = "";
            // Show answerbox
            popUpCanvasGroup.alpha = 1f;
        }
        else
        {
            // Close answerbox
            popUpCanvasGroup.alpha = 0f;
            checkStringInput();
        }
    }
    public void checkStringInput()
    {
        if (mobileVersion)
        {
            if (KeyboardInputText.text == answerString)
            {
                sceneCompleteScript.SceneComplete = true;
                Button.image.color = Color.green;
            }
            else
            {
                Handheld.Vibrate();
            }
            // Reset input
            KeyboardInputText.text = "";
        }
        else
        {
            if (inputText.text == answerString)
            {
                sceneCompleteScript.SceneComplete = true;
                Button.image.color = Color.green;
            }
        }

        // Close answerbox
        popUpCanvasGroup.alpha = 0f;
    }
}