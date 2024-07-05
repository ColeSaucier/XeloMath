using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager42 : MonoBehaviour
{
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;
    public bool isInputActive = false;
    public bool secondInput = false;

    public TwoSeparateObjects objectGenerator;
    public int copiedNumerator;
    public int copiedDenominator;

    public TextMeshProUGUI numerator;
    public TextMeshProUGUI denominator;
    
    public CanvasGroup popUpCanvasGroup;
    public Button Button;
    public string userInput;

    public GameObject ansPanel1;
    public GameObject ansPanel2;
    public GameObject keyPanel1;
    public GameObject keyPanel2;

    public Canvas mobileKeyboard;
    private bool mobileVersion = true;
    public TextMeshProUGUI keyboardNumerator;
    public TextMeshProUGUI keyboardDenominator;

    public void activateInput()
    {
        isInputActive = !isInputActive;

        if (isInputActive == true)
        { 
            // Reset answerbox input
            userInput = "";
            numerator.text = "";
            denominator.text = "";
            // Show answerbox
            popUpCanvasGroup.alpha = 1f;
        }
        else
        {
            // Close answerbox
            popUpCanvasGroup.alpha = 0f;
            secondInput = false;
            ansPanel1.SetActive(true);
            ansPanel2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputActive)
        {
            if (mobileVersion != true)
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
    }
    public void checkStringInput()
    {
        copiedNumerator = objectGenerator.numerator;
        copiedDenominator = objectGenerator.numberOfSides;

        if (mobileVersion)
        {
            if (secondInput == true)
            {
                if (keyboardNumerator.text == copiedNumerator.ToString() && keyboardDenominator.text == copiedDenominator.ToString()) 
                {
                    SceneComplete = true;
                    sceneCompleteScript.SceneComplete = true;
                    Button.image.color = Color.green;
                }
                else
                {
                    Handheld.Vibrate();
                    secondInput = false;
                    keyPanel1.SetActive(true);
                    keyPanel2.SetActive(false);
                    keyboardNumerator.text = "";
                    keyboardDenominator.text = "";
                }
            }
            else
            {
                secondInput = true;
                keyPanel1.SetActive(false);
                keyPanel2.SetActive(true);
            }
        }
        else
        {
            if (secondInput == true)
            {
                if (numerator.text == copiedNumerator.ToString() && denominator.text == copiedDenominator.ToString()) 
                {
                    SceneComplete = true;
                    sceneCompleteScript.SceneComplete = true;
                    Button.image.color = Color.green;
                    activateInput();
                }
                else
                    activateInput();
            }
            else
            {
                secondInput = true;
                userInput = "";
                ansPanel1.SetActive(false);
                ansPanel2.SetActive(true);
            }
        }
    }
}