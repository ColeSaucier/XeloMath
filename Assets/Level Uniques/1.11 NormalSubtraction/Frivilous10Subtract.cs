using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Frivilous10Subtract : MonoBehaviour
{
    public bool oneToggleYes = false;
    public TextMeshProUGUI tensOne;
    public GameObject panel;

    private void Start()
    {
    panel.SetActive(false);
    }

    // Start is called before the first frame update
    public void alterOneToggle()
    {
        oneToggleYes = !oneToggleYes;
        if (oneToggleYes == true)
        { 
            tensOne.text = "10";
            panel.SetActive(true);
        }

        else
        {
            tensOne.text = null;
            panel.SetActive(false);
        }

    }
}