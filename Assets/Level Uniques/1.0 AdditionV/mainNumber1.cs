using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mainNumber1 : MonoBehaviour
{
    public static int primaryNum = 0; // the variable to display
    public static int secondaryNum = 0;
    public TMP_Text mainNumber; // The TextMeshPro object to display
    public static bool primaryYes = true;

    // Start is called before the first frame update
    void Start()
    {
        primaryYes = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (primaryYes)
        {
            mainNumber.SetText(primaryNum.ToString());
        }
        
        if (!primaryYes)
        {
            mainNumber.SetText(secondaryNum.ToString());
        }
    }
}