using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager415 : MonoBehaviour
{
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;
    public bool isInputActive = false;
    public bool secondInput = false;

    public FractionHandlerHard fractionHandler;
    public int copiedNumerator;
    public int copiedDenominator;

    public TextMeshProUGUI numerator;
    public TextMeshProUGUI denominator;
    
    public CanvasGroup popUpCanvasGroup;
    public Button Button;
    public string userInput;

    public Canvas mobileKeyboard;
    private bool mobileVersion = true;
    public TextMeshProUGUI keyboardNumerator;
    public TextMeshProUGUI keyboardDenominator;

    public void activateInput()
    {
        isInputActive = true;
        userInput = "";

        // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
        popUpCanvasGroup.alpha = 1f;
        popUpCanvasGroup.interactable = true; // Enable interactions with the pop-up canvas
        Button.interactable = false;
        // keyboard = TouchScreenKeyboard.Open(userInput, TouchScreenKeyboardType.Default);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputActive)
        {
            // Check for input and handle it
            if (Input.GetKeyDown(KeyCode.Return))
            {
                checkStringInput();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace) && userInput.Length > 0)
            {
                userInput = userInput.Substring(0, userInput.Length - 1);
            }
            else
            {
                userInput += Input.inputString;
            }

            if (secondInput == true)
                denominator.text = userInput;
            else
                numerator.text = userInput;
        }
    }
    public void checkStringInput()
    {
        copiedNumerator = fractionHandler.numeratorAns;
        copiedDenominator = fractionHandler.denominatorAns;

        if (mobileVersion)
        {
            if (keyboardNumerator.text == copiedNumerator.ToString() && keyboardDenominator.text == copiedDenominator.ToString()) 
            {
                SceneComplete = true;
                sceneCompleteScript.SceneComplete = true;
                Button.image.color = Color.green;
            }
            else
            {
                if (secondInput == true)
                    keyboardDenominator.text = "";
                else
                    keyboardNumerator.text = "";
                
                Handheld.Vibrate();
            }
        }
        else
        {
            if (numerator.text == copiedNumerator.ToString() && denominator.text == copiedDenominator.ToString()) 
            {
                SceneComplete = true;
                sceneCompleteScript.SceneComplete = true;
                Button.image.color = Color.green;
            }
        
            isInputActive = false;
            // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
            popUpCanvasGroup.alpha = 0f;
            Button.interactable = true;
        }
    }
}
