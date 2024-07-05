using UnityEngine;

public class ShapeGenerator3 : MonoBehaviour
{
    public int cycleLength = 3; // Length of the repeating cycle
    public int totalLength = 10; // Total length of all shapes generated
    public int shapesPerRow = 5; // Number of shapes generated per row
    public float spacing = 1f; // Spacing between shapes
    public float scale = 1.2f;
    public float localScale = 1.3f;

    public float centerYOffset = 0f; // Offset to center the parent group on the y-axis

    public GameObject[] shapePrefabs; // Array of shape prefabs
    public GameObject questionMarkPrefab; // Prefab for the question mark object

    private GameObject shapeAnswer; // The chosen invisible shape

    public GameObject GetShapeAnswer()
    {
        return shapeAnswer;
    }

    private void Start()
    {
        GenerateShapes();
    }

    private void GenerateShapes()
    {
        int cycleCount = totalLength / cycleLength; // Number of times the cycle repeats
        int shapeCount = cycleCount * cycleLength; // Total number of shapes to generate

        // Create the parent group for the generated shapes
        GameObject parentGroup = new GameObject("ShapeGroup3");
        //parentGroup.transform.position = transform.position;

        Vector3 spawnPosition = Vector3.zero; // Starting position for shape spawning
        int shapeIndex = 0; // Index to iterate through the shape prefabs

        int invisibleShapeIndex = Random.Range(0, shapeCount); // Randomly choose the invisible shape

        for (int i = 0; i < shapeCount; i++)
        {
            // Instantiate the shape prefab at the spawn position relative to the parent group
            GameObject shape = Instantiate(shapePrefabs[shapeIndex], spawnPosition, Quaternion.identity, parentGroup.transform);

            // Check if the shape is the invisible one
            if (i == invisibleShapeIndex)
            {
                shapeAnswer = shape; // Record the invisible shape as shapeAnswer
                SetShapeAlpha(shape, 0f); // Make the shape invisible
            }

            // Move the spawn position based on spacing
            spawnPosition.x += spacing;

            // Check if the current row is complete
            if ((i + 1) % shapesPerRow == 0)
            {
                spawnPosition.x = 0f; // Reset the X position
                spawnPosition.y -= spacing; // Move to the next row
            }

            // Increment the shape index and reset it if it exceeds the number of shape prefabs
            shapeIndex++;
            if (shapeIndex >= shapePrefabs.Length)
                shapeIndex = 0;
        }

        // Instantiate the question mark prefab below the invisible shape
        Vector3 questionMarkPosition = shapeAnswer.transform.position;
        Instantiate(questionMarkPrefab, questionMarkPosition, Quaternion.identity, parentGroup.transform);

        /*
        // Apply the offset to center the parent group
        if (ScreenModifierCalculator.isPortrait)
        {
            scale = ScreenModifierCalculator.portraitModifier;
        }
        else
        {
            scale = ScreenModifierCalculator.landscapeModifier + 0.3f;
            centerYOffset += 0.75f;
        }
        */
        scale = 1f;
        parentGroup.transform.localScale = new Vector3(localScale,localScale,localScale) * scale;
        parentGroup.transform.position = new Vector3(-(((shapeCount - 0.95f) / 2) * spacing * localScale * scale), centerYOffset, 0f);
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