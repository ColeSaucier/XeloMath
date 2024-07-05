using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    private Button currentlyHighlightedButton;

    // Add your highlighting logic here
    public void HighlightButton(Button buttonToHighlight)
    {
        if (currentlyHighlightedButton != null)
        {
            // Unhighlight the previously highlighted button
            currentlyHighlightedButton.image.color = Color.white;
        }

        // Highlight the new button
        buttonToHighlight.image.color = Color.gray;
        currentlyHighlightedButton = buttonToHighlight;
    }
}
