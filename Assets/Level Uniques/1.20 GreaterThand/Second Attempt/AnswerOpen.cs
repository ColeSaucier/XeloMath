using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerOpen : MonoBehaviour
{
    public GameObject AnswerPanel;
    public AnswerManagerCircleGeneration answerManager;

    public void OpenAnswerPanel()
    {
        if(AnswerPanel != null)
        {
            AnswerPanel.SetActive(!AnswerPanel.activeSelf);

            if (AnswerPanel.activeSelf == false)
                answerManager.TestPlayerInput();
        }
    }
}
