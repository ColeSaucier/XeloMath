using UnityEngine;

public class ScreenModifierCalculator : MonoBehaviour
{
    private const float iPhone12Width = 1170f; // iPhone 12's width in points

    public float portraitModifier; // Modifier for portrait orientation
    public float landscapeModifier; // Modifier for landscape orientation
    public static bool isPortrait; // Boolean variable indicating if the screen is in portrait orientation

    private void Awake()
    {
        CalculateModifiers();
    }

    private void CalculateModifiers()
    {
        //float screenWidth = Screen.width;
        //float screenHeight = Screen.height;

        isPortrait = Screen.height > Screen.width;

        if (isPortrait)
        {
            // Portrait orientation
            portraitModifier = Screen.width / iPhone12Width;
            landscapeModifier = 0;
        }
        else
        {
            // Landscape orientation
            portraitModifier = 0;
            landscapeModifier = Screen.height / iPhone12Width;
        }
    }
}
