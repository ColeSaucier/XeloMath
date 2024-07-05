using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCompleteReloader : MonoBehaviour
{
    public int ones = 7;
    public string numString;
    public TextMeshProUGUI textMesh1;
    public Button Button;

    // Start is called before the first frame update
    void Start()
    {
        textMesh1.text = ones.ToString();  
    }
    public void addone()
    {
        ones++;
        textMesh1.text = ones.ToString();

        if (ones == 10)
        {
            reLoadScene();
        }
    }
    // Update is called once per frame
    public void reLoadScene()
    {
        SceneManager.LoadScene("0.0 NumCountingOrdered 1", LoadSceneMode.Single);
    }
}
