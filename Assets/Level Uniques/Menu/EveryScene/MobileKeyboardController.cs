using UnityEngine;
using TMPro;

public class MobileKeyboardController : MonoBehaviour
{
    public TextMeshProUGUI inputText;

    // Function to add a number to the text
    public void NumberInput(int number)
    {
        inputText.text += number.ToString();
        Vibrator.Vibrate(100);
    }

    // Function to delete the last character in the text
    public void DeleteInput()
    {
        if (inputText.text.Length > 0)
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
            Vibrator.Vibrate(100);
        }
    }
}
