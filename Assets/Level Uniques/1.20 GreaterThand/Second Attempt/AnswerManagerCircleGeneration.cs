using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerManagerCircleGeneration : MonoBehaviour
{
    /// GreatorThand Variables
    public static int number1 = 5;
    public static int number2 = 7;

    public static int answerInt;
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;

    public Button Button;
    public TextMeshPro textMesh1;
    public TextMeshPro textMesh2;
    public TextMeshPro playerInput;



    /// Circle array generation
    public GameObject circlePrefab;
    public GameObject circleRedPrefab;
    public float circleSpacing = 1f;
    public float xOffset = -1.75f;
    public float yOffset = 0f;
    public float zRotationOffset = 0f;
    public int circlesPerColumn = 10;

    public GameObject circleGroup;
    public GameObject circleGroup1;
    private Vector3 offset;

    public int totalNumberOfCircles;
    public int circlesInCompleteRows;
    public int remainingCircles;

    ///Circle array 1 generation 
    public float xOffset1 = -1.75f;
    public float yOffset1 = 0f;
    public float zRotationOffset1 = 0f;

    // Start is called before the first frame update
    void Start()
    {
    /// GreatorThand Generation
        number1 = Random.Range(0, 51);
        number2 = Random.Range(0, 51);
        
        textMesh1.text = number1.ToString();
        textMesh2.text = number2.ToString();

        if (number1 < number2)
            answerInt = 1;
        if (number1 == number2)
            answerInt = 2;
        if (number1 > number2)
            answerInt = 3;

    /// Circle generation (0)
        totalNumberOfCircles = number1;
        int fullColumns = totalNumberOfCircles / circlesPerColumn;
        remainingCircles = totalNumberOfCircles % circlesPerColumn;
        circlesInCompleteRows = fullColumns * circlesPerColumn;

        // Calculate the total number of columns based on full columns and remaining circles
        int totalColumns = remainingCircles > 0 ? fullColumns + 1 : fullColumns;

        // Create a parent object to group the circles
        circleGroup = new GameObject("CircleGroup");

        // Calculate the center position of the grid
        Vector3 centerPosition = new Vector3((totalColumns - 1) * circleSpacing / 2f, 0f, 0f);

        // Spawn the circles in a grid pattern
        for (int column = 0; column < totalColumns; column++)
        {
            int circlesInColumn = column < fullColumns ? circlesPerColumn : remainingCircles;
            bool useRedPrefab = column >= fullColumns;

            for (int row = 0; row < circlesInColumn; row++)
            {
                // Calculate the position of each circle
                float xPosition = column * circleSpacing;
                float yPosition = row * circleSpacing;
                Vector3 position = centerPosition + new Vector3(xPosition, yPosition, 0f);

                // Instantiate the circle using the appropriate prefab
                GameObject circlePrefabToUse = useRedPrefab ? circleRedPrefab : circlePrefab;
                GameObject circle = Instantiate(circlePrefabToUse, position, Quaternion.identity);
                circle.transform.SetParent(circleGroup.transform);
            }
        }

        circleGroup.transform.position = new Vector3(xOffset, yOffset, 0f);
        circleGroup.transform.rotation = Quaternion.Euler(0f, 0f, zRotationOffset);
        
        /// Circle generation 1
        totalNumberOfCircles = number2;
        fullColumns = totalNumberOfCircles / circlesPerColumn;
        remainingCircles = totalNumberOfCircles % circlesPerColumn;
        circlesInCompleteRows = fullColumns * circlesPerColumn;

        // Calculate the total number of columns based on full columns and remaining circles
        totalColumns = remainingCircles > 0 ? fullColumns + 1 : fullColumns;

        // Create a parent object to group the circles
        circleGroup1 = new GameObject("CircleGroup1");

        // Calculate the center position of the grid
        centerPosition = new Vector3((totalColumns - 1) * circleSpacing / 2f, 0f, 0f);

        // Spawn the circles in a grid pattern
        for (int column = 0; column < totalColumns; column++)
        {
            int circlesInColumn = column < fullColumns ? circlesPerColumn : remainingCircles;
            bool useRedPrefab = column >= fullColumns;

            for (int row = 0; row < circlesInColumn; row++)
            {
                // Calculate the position of each circle
                float xPosition = column * circleSpacing;
                float yPosition = row * circleSpacing;
                Vector3 position = centerPosition + new Vector3(xPosition, yPosition, 0f);

                // Instantiate the circle using the appropriate prefab
                GameObject circlePrefabToUse = useRedPrefab ? circleRedPrefab : circlePrefab;
                GameObject circle = Instantiate(circlePrefabToUse, position, Quaternion.identity);
                circle.transform.SetParent(circleGroup1.transform);
            }
        }

        circleGroup1.transform.position = new Vector3(xOffset1, yOffset1, 0f);
        circleGroup1.transform.rotation = Quaternion.Euler(0f, 0f, zRotationOffset1);
    }

    public void TestPlayerInput()
    {
        if (int.Parse(playerInput.text) == answerInt)
        {
            SceneComplete = true;
            sceneCompleteScript.SceneComplete = true;
            Button.image.color = Color.green;
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public void ChangePlayerInput(int numberInput)
    {
        playerInput.text = numberInput.ToString();
    }
}
