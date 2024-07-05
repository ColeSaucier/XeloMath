using UnityEngine;
using TMPro;

public class FractionMobileKeyboardController01 : MonoBehaviour
{
    public TextMeshProUGUI numerator;
    public TextMeshProUGUI denominator;
    public bool secondInputBool;
    public AnswerManager41 script;

    // Function to add a number to the text
    public void NumberInput(int number)
    {
        secondInputBool = script.secondInput;
        Vibrator.Vibrate(100);

        if (secondInputBool == true)
            denominator.text += number.ToString();
        else
            numerator.text += number.ToString();
    }

    // Function to delete the last character in the text
    public void DeleteInput()
    {
        secondInputBool = script.secondInput;
        Vibrator.Vibrate(100);

        if (secondInputBool == true)
        {
            if (denominator.text.Length > 0)
            {
                denominator.text = denominator.text.Substring(0, denominator.text.Length - 1);
            }
        }
        else
        {
            if (numerator.text.Length > 0)
            {
                numerator.text = numerator.text.Substring(0, numerator.text.Length - 1);
            }
        }
    }
}