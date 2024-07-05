using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mainNumberDisplay : MonoBehaviour
{
    public static int primaryNum = 0; // the variable to display
    public TMP_Text mainNumber; // The TextMeshPro object to display

    // Start is called before the first frame update
    void Start()
    {
        primaryNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mainNumber.SetText(primaryNum.ToString());
    }
}