using UnityEngine;

public class RowAnswerGroupGenerator : MonoBehaviour
{
    public float spacing = 1f; // Spacing between shapes

    void Start()
    {
        GameObject rowAnswerGroup1 = GenerateRowAnswerGroup1();
        GameObject rowAnswerGroup2 = GenerateRowAnswerGroup2();
        GameObject rowAnswerGroup3 = GenerateRowAnswerGroup3();

        // Set the visibility of all child objects to false after generating the parent groups
        VisibilityController visibilityController = GetComponent<VisibilityController>();
        visibilityController.SetChildObjectsVisibility(rowAnswerGroup1, false);
        visibilityController.SetChildObjectsVisibility(rowAnswerGroup2, false);
        visibilityController.SetChildObjectsVisibility(rowAnswerGroup3, false);
    }

    private GameObject GenerateRowAnswerGroup1()
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
        float yOffset = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;

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

            shapeObject.transform.localPosition = new Vector3(xPosition, yPosition, 0f);

            // Add a BoxCollider to the shape object
            BoxCollider boxCollider = shapeObject.AddComponent<BoxCollider>();
            // Modify box collider properties if needed
        }

        // Return the created parent group GameObject
        return rowAnswerGroup;
    }

    private GameObject GenerateRowAnswerGroup2()
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
        float yOffset = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;

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

            shapeObject.transform.localPosition = new Vector3(xPosition, yPosition, 0f);

            // Add a BoxCollider to the shape object
            BoxCollider boxCollider = shapeObject.AddComponent<BoxCollider>();
            // Modify box collider properties if needed
        }

        // Return the created parent group GameObject
        return rowAnswerGroup;
    }

    private GameObject GenerateRowAnswerGroup3()
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
        float yOffset = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;

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

            shapeObject.transform.localPosition = new Vector3(xPosition, yPosition, 0f);

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
            int randomIndex = Random.Range(i, length);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        return indices;
    }
}