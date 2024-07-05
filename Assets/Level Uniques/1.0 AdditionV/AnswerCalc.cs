using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCalc : MonoBehaviour
{
    public static int correctAnswer;

    void Start()
    {
        correctAnswer = mainNumber1.primaryNum + mainNumber2.primaryNum; // Access the answer variable using the class name
    }
}
