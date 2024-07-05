using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneToggle : MonoBehaviour
{
    public bool oneToggleYes = false;
    public TextMeshProUGUI tensOne;

    // Start is called before the first frame update
    public void alterOneToggle()
    {
        oneToggleYes = !oneToggleYes;
        if (oneToggleYes == true)
        {
            tensOne.text = "1";
        }
        else
            tensOne.text = null;

    }
}
