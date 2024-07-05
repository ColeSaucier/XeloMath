using UnityEngine;

public class CircleGridGenerator1    : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject circleRedPrefab;
    public int minRandomNumberOfCircles = 0;
    public int maxRandomNumberOfCircles = 50;
    public float circleSpacing = 1f;
    public float xOffset = -1.75f;
    public float yOffset = 0f;
    public float zRotationOffset = 0f;

    public static GameObject circleGroup;
    private bool isDragging;
    private Vector3 lastMousePosition;
    private Vector3 offset;

    public static int totalNumberOfCircles;
    public static int circlesInCompleteRows;
    public static int remainingCircles;

    void Start()
    {
        totalNumberOfCircles = Random.Range(minRandomNumberOfCircles, maxRandomNumberOfCircles + 1);
        int circlesPerColumn = 10;
        int fullColumns = totalNumberOfCircles / circlesPerColumn;
        remainingCircles = totalNumberOfCircles % circlesPerColumn;
        circlesInCompleteRows = fullColumns * circlesPerColumn;
        mainNumber2.primaryNum = totalNumberOfCircles;

        // Calculate the total number of columns based on full columns and remaining circles
        int totalColumns = remainingCircles > 0 ? fullColumns + 1 : fullColumns;

        // Create a parent object to group the circles
        circleGroup = new GameObject("CircleGroup1");

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

        Vector3 firstCirclePosition = circleGroup.transform.GetChild(0).transform.position;
        // Get the last child object's position
        Vector3 lastCirclePosition = circleGroup.transform.GetChild(circlesInCompleteRows - 1).transform.position;
        Vector3 delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        BoxCollider collider = circleGroup.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition; // Apply the X offset      + new Vector3(colliderYOffset, colliderXOffset, 0f)
        collider.size = new Vector3(totalColumns * circleSpacing, circlesPerColumn * circleSpacing, 1f);
        if (remainingCircles > 0)
            {
            collider.center = collider.center + new Vector3(circleSpacing / 2f, 0f, 0f);
            }

        circleGroup.transform.position = new Vector3(xOffset, yOffset, 0f);
        circleGroup.transform.rotation = Quaternion.Euler(0f, 0f, zRotationOffset);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == circleGroup.GetComponent<BoxCollider>())
            {
                isDragging = true;
                lastMousePosition = GetWorldMousePosition();
                offset = lastMousePosition - circleGroup.transform.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 targetPosition = currentMousePosition - offset;
            circleGroup.transform.position = targetPosition;
            lastMousePosition = currentMousePosition;
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}