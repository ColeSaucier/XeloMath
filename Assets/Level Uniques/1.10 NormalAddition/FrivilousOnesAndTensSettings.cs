using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrivilousOnesAndTensSettings : MonoBehaviour
{
    public TMP_InputField frivilousOnes;
    public TMP_InputField frivilousTens;
    public TextMeshProUGUI ones;
    public TextMeshProUGUI tens;

    // Start is called before the first frame update
    void Start()
    {
        frivilousOnes.keyboardType = TouchScreenKeyboardType.NumberPad;
        frivilousTens.keyboardType = TouchScreenKeyboardType.NumberPad;
    }
}