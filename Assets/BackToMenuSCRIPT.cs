using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuSCRIPT : MonoBehaviour
{
    // Update is called once per frame
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
