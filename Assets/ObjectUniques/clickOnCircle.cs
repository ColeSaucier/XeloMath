using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clickOnCircle : MonoBehaviour
{
    int yesNo = 0; // Declare the variable outside of any method to make it accessible to the entire class
    Renderer objectRenderer; // Reference to the object's Renderer component

    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); // Get the Renderer component
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        if (yesNo == 0)
        {
            // Code to execute if yesNo is equal to 0
            mainNumberDisplay.primaryNum++;
            yesNo = 1; // Update the value of yesNo
            objectRenderer.material.color = Color.red; // Change the object's color to red
        }
        else
        {
            // Code to execute if yesNo is not equal to 0
            mainNumberDisplay.primaryNum--;
            yesNo = 0; // Update the value of yesNo
            objectRenderer.material.color = Color.white; // Change the object's color to white
        }
    }
}
