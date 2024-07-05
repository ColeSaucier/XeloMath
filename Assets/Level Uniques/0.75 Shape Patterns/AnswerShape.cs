using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerShape : MonoBehaviour
{
    public ShapeGenerator1 shapeGenerator;

    void Start()
    {
        shapeGenerator = FindObjectOfType<ShapeGenerator1>();
        
        if (shapeGenerator != null)
        {
            GameObject correctAnswer = shapeGenerator.GetShapeAnswer();
            // Do something with the shapeAnswer GameObject
        }
    }
}