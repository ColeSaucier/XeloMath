using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberDisplay : MonoBehaviour
{
    public static int number1 = 5;   // First integer variable defaults
    public static int number2 = 7;   // Second integer variable
    public static int answerInt;
    public int test;
    public static bool sceneComplete = false;

    public TextMeshProUGUI textMesh1; // Reference to the first TextMesh component
    public TextMeshProUGUI textMesh2; // Reference to the second TextMesh component

    // Start is called before the first frame update
    void Start()
    {
        //Random number generation
        number1 = Random.Range(0, 51);
        number2 = Random.Range(0, 51);

        // Update the text of the TextMesh components to display the numbers
        UpdateTextMeshes();

        if (number1 < number2)
            answerInt = 1;
            test = 1;
        if (number1 == number2)
            answerInt = 2;
            test = 2;
        if (number1 > number2)
            answerInt = 3;
            test = 3;
    }

    // Helper method to update the text of the TextMesh components
    void UpdateTextMeshes()
    {
        textMesh1.text = number1.ToString();
        textMesh2.text = number2.ToString();
    }
}