using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickOnAnswerBox0751 : MonoBehaviour
{
    public Button answerButton;
    public bool isInputActive = false;
    public CanvasGroup popUpCanvasGroup; // Reference to the pop-up canvas's CanvasGroup component

    //private GameObject userInput;
    public static bool firstAnswerCorrect = false;
    public static bool secondAnswerCorrect = false;
    private bool thirdAnswerCorrect = false;
    public SceneCompleteMenu sceneCompleteScript;
    private bool sceneComplete = false;

    public float spacing = 1f; // Spacing between shapes
    GameObject answer = null;

    private GameObject rowAnswerGroup1;
    private GameObject rowAnswerGroup2;
    private GameObject rowAnswerGroup3;
    private GameObject currentRowAnswers;
    private GameObject lastClickedObject;
    private int colliderInst;

    public GameObject rowAnswerGroupsParent;


    // Start is called before the first frame update
    void Start()
    {
        firstAnswerCorrect = false;
        secondAnswerCorrect = false;

        rowAnswerGroup1 = GenerateRowAnswerGroup1();
        rowAnswerGroup2 = GenerateRowAnswerGroup2();
        rowAnswerGroup3 = GenerateRowAnswerGroup3();

        // Set the visibility of all child objects to false after generating the parent groups
        VisibilityController visibilityController = GetComponent<VisibilityController>();
        visibilityController.SetChildObjectsVisibility(rowAnswerGroup1, false);
        visibilityController.SetChildObjectsVisibility(rowAnswerGroup2, false);
        visibilityController.SetChildObjectsVisibility(rowAnswerGroup3, false);

        // Reparent the rowAnswerGroup objects under the "RowAnswerGroupsParent"
        rowAnswerGroup1.transform.SetParent(rowAnswerGroupsParent.transform);
        rowAnswerGroup2.transform.SetParent(rowAnswerGroupsParent.transform);
        rowAnswerGroup3.transform.SetParent(rowAnswerGroupsParent.transform);

    }

    public void activateInput()
    {
        if (sceneComplete != true)
        {
            isInputActive = !isInputActive;
            if (isInputActive)
            {
                answerButton.image.color = Color.grey;

                VisibilityController visibilityController = GetComponent<VisibilityController>();
                popUpCanvasGroup.alpha = 1f; // Set the pop-up canvas's alpha to 0 (fully transparent) initially
                popUpCanvasGroup.interactable = true; // Disable interactions with the pop-up canvas
                if (lastClickedObject != null)
                {
                    lastClickedObject.GetComponent<Renderer>().material.color = Color.white;
                    lastClickedObject = null;
                }

                // Determine the lowest correct answer that is false
                if (!firstAnswerCorrect)
                {
                    // The first answer is incorrect
                    // You can add the code for handling this case here
                    ShapeGenerator1 shapeGenerator1 = FindObjectOfType<ShapeGenerator1>();
                    answer = shapeGenerator1.GetShapeAnswer();
                    currentRowAnswers = rowAnswerGroup1;
                    visibilityController.SetChildObjectsVisibility(currentRowAnswers, true);
                }
                else if (!secondAnswerCorrect)
                {
                    // The first answer is correct but the second answer is incorrect
                    // You can add the code for handling this case here
                    ShapeGenerator2 shapeGenerator2 = FindObjectOfType<ShapeGenerator2>();
                    answer = shapeGenerator2.GetShapeAnswer();
                    currentRowAnswers = rowAnswerGroup2;
                    visibilityController.SetChildObjectsVisibility(rowAnswerGroup2, true);
                }
                else if (!thirdAnswerCorrect)
                {
                    // The first and second answers are correct but the third answer is incorrect
                    // You can add the code for handling this case here
                    ShapeGenerator3 shapeGenerator3 = FindObjectOfType<ShapeGenerator3>();
                    answer = shapeGenerator3.GetShapeAnswer();
                    currentRowAnswers = rowAnswerGroup3;
                    visibilityController.SetChildObjectsVisibility(rowAnswerGroup3, true);
                }
                else
                {
                    // All answers are correct
                    // You can add the code for handling this case here
                }
            }

            if (!isInputActive)
            {
                answerButton.image.color = Color.white;

                VisibilityController visibilityController = GetComponent<VisibilityController>();
                visibilityController.SetChildObjectsVisibility(currentRowAnswers, false);
                popUpCanvasGroup.alpha = 0f; // Set the pop-up canvas's alpha to 0 (fully transparent) initially
                popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas

                if (lastClickedObject != null)
                {
                    // Handle correct answer input
                    if (lastClickedObject.name == answer.name)
                    {
                        // Determine the lowest correct answer that is false
                        if (!firstAnswerCorrect)
                        {
                            // The first answer is incorrect
                            // You can add the code for handling this case here
                            firstAnswerCorrect = true;
                            SetShapeAlpha(answer, 1f);
                        }
                        else if (!secondAnswerCorrect)
                        {
                            // The first answer is correct but the second answer is incorrect
                            // You can add the code for handling this case here
                            secondAnswerCorrect = true;
                            SetShapeAlpha(answer, 1f);
                        }
                        else if (!thirdAnswerCorrect)
                        {
                            // The first and second answers are correct but the third answer is incorrect
                            // You can add the code for handling this case here
                            thirdAnswerCorrect = true;
                            answerButton.image.color = Color.green;
                            sceneComplete = true;
                            sceneCompleteScript.SceneComplete = true;
                            SetShapeAlpha(answer, 1f);
                        }
                    }
                    else
                    {
                        Handheld.Vibrate();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse input click
        if (Input.GetMouseButtonDown(0))
        {
            if (isInputActive)
            {
                // Create a ray from the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Create a RaycastHit variable to store information about the hit
                RaycastHit hit;

                // Perform the raycast and check if it hits something
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.transform.IsChildOf(currentRowAnswers.transform))
                    {
                        if (lastClickedObject != null)
                        {
                            lastClickedObject.GetComponent<Renderer>().material.color = Color.white;
                            lastClickedObject = null;
                        }

                        // Check if the hit object has a Renderer component (to ensure it's a visible object)
                        Renderer renderer = hit.collider.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            // Store the last clicked object or reference to the object
                            lastClickedObject = hit.collider.gameObject;
                            hit.collider.GetComponent<Renderer>().material.color = new Color(210/255f, 0/255f, 0/255f);

                            // Now you have a reference to the object that was clicked, and you can use it as needed.
                            // For example, you can change its color or perform other actions.
                        }
                    }
                }
            }
        }
    }


    GameObject GenerateRowAnswerGroup1()
    {
        ShapeGenerator1 shapeGenerator1 = FindObjectOfType<ShapeGenerator1>();

        GameObject[] shapePrefabs = shapeGenerator1.shapePrefabs;
        int length = shapePrefabs.Length;

        // Create the parent group (RowAnswerGroup1)
        GameObject rowAnswerGroup = new GameObject("RowAnswerGroup1");
        rowAnswerGroup.transform.SetParent(transform); // Set the current object as the parent

        // Randomize the shape order
        int[] shapeIndices = GenerateRandomIndices(length);

        // Calculate the center offset
        float xOffset = -(((length - 1) / 2f) * spacing);
        float yOffset = Camera.main.ScreenToWorldPoint(new Vector3(240f, 240f, 240f)).y;

        // Generate child objects (shapes) and position them in RowAnswerGroup1
        for (int i = 0; i < length; i++)
        {
            int shapeIndex = shapeIndices[i];
            GameObject shapePrefab = shapePrefabs[shapeIndex];

            // Instantiate the shape prefab
            GameObject shapeObject = Instantiate(shapePrefab);
            shapeObject.GetComponent<Renderer>().sortingLayerName = "a bit more now";

            // Set the shape's parent as RowAnswerGroup1
            shapeObject.transform.SetParent(rowAnswerGroup.transform);

            // Position the shape along the x-axis with spacing
            float xPosition = xOffset + (i * spacing);
            float yPosition = yOffset / 2;

            shapeObject.transform.localPosition = new Vector3(xPosition, 0, 0f);

            // Add a BoxCollider to the shape object
            BoxCollider boxCollider = shapeObject.AddComponent<BoxCollider>();
            // Modify box collider properties if needed
        }

        // Return the created parent group GameObject
        return rowAnswerGroup;
    }

    GameObject GenerateRowAnswerGroup2()
    {
        ShapeGenerator2 shapeGenerator2 = FindObjectOfType<ShapeGenerator2>();

        GameObject[] shapePrefabs = shapeGenerator2.shapePrefabs;
        int length = shapePrefabs.Length;

        // Create the parent group (RowAnswerGroup2)
        GameObject rowAnswerGroup = new GameObject("RowAnswerGroup2");
        rowAnswerGroup.transform.SetParent(transform); // Set the current object as the parent

        // Randomize the shape order
        int[] shapeIndices = GenerateRandomIndices(length);

        // Calculate the center offset
        float xOffset = -(((length - 1) / 2f) * spacing);
        float yOffset = Camera.main.ScreenToWorldPoint(new Vector3(0f, 2.5f, 0f)).y;

        // Generate child objects (shapes) and position them in RowAnswerGroup2
        for (int i = 0; i < length; i++)
        {
            int shapeIndex = shapeIndices[i];
            GameObject shapePrefab = shapePrefabs[shapeIndex];

            // Instantiate the shape prefab
            GameObject shapeObject = Instantiate(shapePrefab);
            shapeObject.GetComponent<Renderer>().sortingLayerName = "a bit more now";

            // Set the shape's parent as RowAnswerGroup2
            shapeObject.transform.SetParent(rowAnswerGroup.transform);

            // Position the shape along the x-axis with spacing
            float xPosition = xOffset + (i * spacing);
            float yPosition = yOffset / 2;

            shapeObject.transform.localPosition = new Vector3(xPosition, 0f, 0f);

            // Add a BoxCollider to the shape object
            BoxCollider boxCollider = shapeObject.AddComponent<BoxCollider>();
            // Modify box collider properties if needed
        }

        // Return the created parent group GameObject
        return rowAnswerGroup;
    }

    GameObject GenerateRowAnswerGroup3()
    {
        ShapeGenerator3 shapeGenerator3 = FindObjectOfType<ShapeGenerator3>();

        GameObject[] shapePrefabs = shapeGenerator3.shapePrefabs;
        int length = shapePrefabs.Length;

        // Create the parent group (RowAnswerGroup3)
        GameObject rowAnswerGroup = new GameObject("RowAnswerGroup3");
        rowAnswerGroup.transform.SetParent(transform); // Set the current object as the parent

        // Randomize the shape order
        int[] shapeIndices = GenerateRandomIndices(length);

        // Calculate the center offset
        float xOffset = -(((length - 1) / 2f) * spacing);
        float yOffset = Camera.main.ScreenToWorldPoint(new Vector3(0f, 2.5f, 0f)).y;

        // Generate child objects (shapes) and position them in RowAnswerGroup3
        for (int i = 0; i < length; i++)
        {
            int shapeIndex = shapeIndices[i];
            GameObject shapePrefab = shapePrefabs[shapeIndex];

            // Instantiate the shape prefab
            GameObject shapeObject = Instantiate(shapePrefab);
            shapeObject.GetComponent<Renderer>().sortingLayerName = "a bit more now";

            // Set the shape's parent as RowAnswerGroup3
            shapeObject.transform.SetParent(rowAnswerGroup.transform);

            // Position the shape along the x-axis with spacing
            float xPosition = xOffset + (i * spacing);
            float yPosition = yOffset / 2;

            shapeObject.transform.localPosition = new Vector3(xPosition, 0f, 0f);

            // Add a BoxCollider to the shape object
            BoxCollider boxCollider = shapeObject.AddComponent<BoxCollider>();
            // Modify box collider properties if needed
        }

        // Return the created parent group GameObject
        return rowAnswerGroup;
    }

    private int[] GenerateRandomIndices(int length)
    {
        int[] indices = new int[length];
        for (int i = 0; i < length; i++)
        {
            indices[i] = i;
        }

        // Fisher-Yates shuffle algorithm to randomize the indices
        for (int i = 0; i < length - 1; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, length);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        return indices;
    }

    private void SetShapeAlpha(GameObject shape, float alpha)
    {
        Renderer shapeRenderer = shape.GetComponent<Renderer>();
        if (shapeRenderer != null)
        {
            Color shapeColor = shapeRenderer.material.color;
            shapeColor.a = alpha;
            shapeRenderer.material.color = shapeColor;
        }
    }
}