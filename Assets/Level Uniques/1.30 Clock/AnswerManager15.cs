using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager15 : MonoBehaviour
{
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;
    public bool isInputActive = false;
    public bool minuteInputBool = false;

    public int copiedHour;
    public int copiedMinute;
    public string copiedMinuteString;

    public TextMeshProUGUI hour;
    public TextMeshProUGUI minutes;
    public CanvasGroup popUpCanvasGroup;
    public ClockGenerator clockGenerator;
    public Button Button;

    public string userInput;

    public Canvas mobileKeyboard;
    private bool mobileVersion = true;
    public TextMeshProUGUI KeyboardInputHour;
    public TextMeshProUGUI KeyboardInputMinute;
    
    void Start()
    {
        copiedHour = clockGenerator.randomHour;
        copiedMinute = clockGenerator.randomMinute * 5;
        if (copiedMinute < 10)
            copiedMinuteString = $"0{copiedMinute}";
        else
            copiedMinuteString = copiedMinute.ToString();
    }
    public void activateInput()
    {
        isInputActive = !isInputActive;

        if (isInputActive == true)
        { 
            // Reset answerbox input
            userInput = "";
            minutes.text = "";
            hour.text = "";
            // Show answerbox
            popUpCanvasGroup.alpha = 1f;
        }
        else
        {
            // Close answerbox
            popUpCanvasGroup.alpha = 0f;
            minuteInputBool = false;
            //checkStringInput();
        }
    }
    public void checkStringInput()
    {
        if (mobileVersion)
        {
            if (minuteInputBool == true)
            {
                if (KeyboardInputHour.text == copiedHour.ToString() && KeyboardInputMinute.text == copiedMinuteString) 
                {
                    SceneComplete = true;
                    sceneCompleteScript.SceneComplete = true;
                    Button.image.color = Color.green;
                    minuteInputBool = false;
                }
                else
                {
                    Handheld.Vibrate();
                    minuteInputBool = false;
                    KeyboardInputHour.text = "";
                    KeyboardInputMinute.text = "";
                }
            }

            else
            {
                minuteInputBool = true;
            }
        }
        else
        {
            if (minuteInputBool == true)
            {
                if (hour.text == copiedHour.ToString() && minutes.text == copiedMinuteString) 
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
                minuteInputBool = true;
                userInput = "";
            }
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

                if (minuteInputBool == true)
                    minutes.text = userInput;
                else
                    hour.text = userInput;
            }
        }
    }
}
