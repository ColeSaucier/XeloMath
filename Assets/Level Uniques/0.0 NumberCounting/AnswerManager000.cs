using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager000 : MonoBehaviour
{
    // References needed for answer button
    public Button Button;
    public string userInput = "";
    public bool isInputActive = false;
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup;
    public Canvas mobileKeyboard;
    private bool mobileVersion = true;
    public TextMeshProUGUI KeyboardInputText;

    public SceneCompleteMenu sceneCompleteScript;
    public RowObject AnswerHolder;

    // Scene Variables
    public string answerString;
    public bool SceneComplete;

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

    public void checkStringInput()
    {
        answerString = AnswerHolder.answer.ToString();

        if (mobileVersion)
        {
            if (KeyboardInputText.text == answerString)
            {
                SceneComplete = true;
                sceneCompleteScript.SceneComplete = true;
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
                SceneComplete = true;
                sceneCompleteScript.SceneComplete = true;
                Button.image.color = Color.green;
            }
        }

        // Close answerbox
        popUpCanvasGroup.alpha = 0f;
    }
    public void activateInput()
    {
        isInputActive = !isInputActive;

        /*
        if (mobileVersion == true)
        {
            mobileKeyboard.enabled = !mobileKeyboard.enabled;
        }
        */
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
}