using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FractionHandler : MonoBehaviour
{
    public int numerator;
    public int denominator;
    public int multiplier;
    public int numeratorAns;
    public int denominatorAns;

    public TextMeshProUGUI numeratorText;
    public TextMeshProUGUI denominatorText;
    public TextMeshProUGUI multiplier1;
    public TextMeshProUGUI multiplier2;
    void Start()
    {
        // Generate random numerator
        numerator = Random.Range(0, 12);
        // Generate random denominator between numerator and 12
        denominator = Random.Range(numerator+1, 13);
        // Generate random multiplier between 1 and 10
        multiplier = Random.Range(2, 11);

        // Calculate Answers
        numeratorAns = numerator * multiplier;
        denominatorAns = denominator * multiplier;

        // Update texts to randomly generated vals
        numeratorText.text = numerator.ToString();
        denominatorText.text = denominator.ToString();
        multiplier1.text = $"x{multiplier}";
        multiplier2.text = $"x{multiplier}";
    }
}