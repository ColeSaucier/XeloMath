using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FractionHandlerHard : MonoBehaviour
{
    public int numerator;
    public int denominator;
    public int multiplier;
    public int numeratorAns;
    public int denominatorAns;

    public TextMeshProUGUI numeratorText;
    public TextMeshProUGUI denominatorText;
    public TextMeshProUGUI numerator1Text;
    public TextMeshProUGUI denominator1Text;

    public TextMeshProUGUI keyboardNumerator;
    public TextMeshProUGUI keyboardDenominator;

    public AnswerManager415 answerManager;
    public GameObject ansPanel1;
    public GameObject ansPanel2;
    public GameObject keyPanel1;
    public GameObject keyPanel2;

    void Start()
    {
        // Generate random numerator
        numerator = Random.Range(1, 12);
        // Generate random denominator between numerator and 12
        denominator = Random.Range(numerator+1, 13);
        // Generate random multiplier between 1 and 10
        multiplier = Random.Range(2, 11);

        // Calculate Answers
        numeratorAns = numerator * multiplier;
        denominatorAns = denominator * multiplier;

        int randomNumeratorVsDenominator = Random.Range(0, 2);
        if (randomNumeratorVsDenominator == 0)
        {
            ansPanel1.SetActive(false);
            ansPanel2.SetActive(true);
            keyPanel1.SetActive(false);
            keyPanel2.SetActive(true);
            answerManager.secondInput = true;
            numerator1Text.text = numeratorAns.ToString();
            keyboardNumerator.text = numeratorAns.ToString();

            denominator1Text.text = "x";
            answerManager.numerator.text = numeratorAns.ToString();
        }
        else
        {
            numerator1Text.text = "x";
            denominator1Text.text = denominatorAns.ToString();
            keyboardDenominator.text = denominatorAns.ToString();
            answerManager.denominator.text = denominatorAns.ToString();
        }

        // Update texts to randomly generated vals
        numeratorText.text = numerator.ToString();
        denominatorText.text = denominator.ToString();
    }
}