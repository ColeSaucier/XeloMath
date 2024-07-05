using UnityEngine;
using TMPro;

public class FractionMobileKeyboardController : MonoBehaviour
{
    public TextMeshProUGUI hour;
    public TextMeshProUGUI minutes;
    public bool secondInputBool;
    public AnswerManager15 script;

    // Function to add a number to the text
    public void NumberInput(int number)
    {
        secondInputBool = script.minuteInputBool;
        Vibrator.Vibrate(100);

        if (secondInputBool == true)
            minutes.text += number.ToString();
        else
            hour.text += number.ToString();
    }

    // Function to delete the last character in the text
    public void DeleteInput()
    {
        secondInputBool = script.minuteInputBool;
        Vibrator.Vibrate(100);

        if (secondInputBool == true)
        {
            if (minutes.text.Length > 0)
            {
                minutes.text = minutes.text.Substring(0, minutes.text.Length - 1);
            }
        }
        else
        {
            if (hour.text.Length > 0)
            {
                hour.text = hour.text.Substring(0, hour.text.Length - 1);
            }
        }
    }
}