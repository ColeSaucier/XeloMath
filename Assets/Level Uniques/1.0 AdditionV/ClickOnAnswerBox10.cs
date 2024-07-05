using System;
using System.Reflection; // Add this using directive to access PropertyInfo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnAnswerBox10 : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup; // Reference to the pop-up canvas's CanvasGroup component
    public Button answerButton; // Reference to the object's Renderer component

    private bool isInputActive = false;
    private string userInput = "";
    public SceneCompleteMenu sceneCompleteScript;

    //Mobile Keyboard Enabling
    public Canvas mobileKeyboard;
    private bool mobileVersion = true;
    public TextMeshProUGUI KeyboardInputText;

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
        string answerString = AnswerCalc.correctAnswer.ToString();

        if (mobileVersion)
        {
            if (KeyboardInputText.text == answerString)
            {
                sceneCompleteScript.SceneComplete = true;
                answerButton.image.color = Color.green;
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
                answerButton.image.color = Color.green;
            }
        }

        // Close answerbox
        popUpCanvasGroup.alpha = 0f;
    }

    public void activateInput()
    {
        isInputActive = !isInputActive;

        if (mobileVersion == true)
        {
            mobileKeyboard.enabled = !mobileKeyboard.enabled;
        }

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