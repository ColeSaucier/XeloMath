using UnityEngine;

public class answerMatcher05 : MonoBehaviour
{
    public static int correctAnswer;

    void Start()
    {
        correctAnswer = NotRowObject.answer; // Access the answer variable using the class name
    }

    void Update()
    {
        // Check if the answer has changed and update correctAnswer accordingly
        if (correctAnswer != NotRowObject.answer)
        {
            correctAnswer = NotRowObject.answer;
        }
    }
}
