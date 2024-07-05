using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CircleGridGeneratorSmall : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject circleRedPrefab;
    public GameObject secondaryNumDisplay;
    public int minRandomNumberOfCircles = 0;
    public int maxRandomNumberOfCircles = 50;
    public float circleSpacing = 1f;
    public float xOffset = -1.75f;
    public float yOffset = 0f;
    public float zRotationOffset = 0f;
    public int circlesPerColumn = 10;

    public GameObject circleGroup;
    public GameObject COMBINEDcircleGroup;
    private bool isDragging;
    private Vector3 lastMousePosition;
    private Vector3 offset;

    public bool collided = false;
    public bool collided1 = false;
    public CircleGridGenerator1 scriptReference;

    public int totalNumberOfCircles;
    public int circlesInCompleteRows;
    public int remainingCircles;
    public int generatedCircles;
    public int COMBINEDcirclesInCompleteRows;
    public int COMBINEDremainingCircles;
    public int additiveRemainingCircles;
    public int additiveCirclesInCompleteRows;
    private bool isDraggingComb;
    public Vector3 colliderBoundsPreset;


    void Start()
    {
        totalNumberOfCircles = Random.Range(minRandomNumberOfCircles, maxRandomNumberOfCircles + 1);
        mainNumber1.primaryNum = totalNumberOfCircles;

        // Create a parent object to group the circles
        circleGroup = new GameObject("CircleGroup");

        // Calculate the center position of the grid
        Vector3 centerPosition = new Vector3(0f, 0f, 0f);

        for (int num = 0; num < totalNumberOfCircles; num++)
        {
            // Calculate the position of each circle
            float yPosition = num * circleSpacing;
            Vector3 position = centerPosition + new Vector3(0f, yPosition, 0f);

            GameObject circle = Instantiate(circlePrefab, position, Quaternion.identity);
            circle.transform.SetParent(circleGroup.transform);
        }

        Vector3 firstCirclePosition = circleGroup.transform.GetChild(0).transform.position;
        // Get the last child object's position
        Vector3 lastCirclePosition = circleGroup.transform.GetChild(totalNumberOfCircles).transform.position;
        Vector3 delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        BoxCollider collider = circleGroup.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition;
        collider.size = new Vector3(circleSpacing, circlesPerColumn * circleSpacing, 1f);

        collider.center = collider.center + new Vector3(circleSpacing / 2f, 0f, 0f);

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

            if (COMBINEDcircleGroup != null)
            { 
                if (Physics.Raycast(ray, out hit) && hit.collider == COMBINEDcircleGroup.GetComponent<BoxCollider>())
                {
                    isDraggingComb = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            // Check for overlap between circleGroup and circleGroup1 colliders
            GameObject circleGroup1 = CircleGridGenerator1.circleGroup;
            if (circleGroup.GetComponent<BoxCollider>().bounds.Intersects(circleGroup1.GetComponent<BoxCollider>().bounds))
            {
                // The colliders of circleGroup and circleGroup1 are overlapping.
                // You can add code here to handle the collision.
                collided = true;
                VisibilityController visibilityController = GetComponent<VisibilityController>();
                visibilityController.SetChildObjectsVisibility(circleGroup, false);
                visibilityController.SetChildObjectsVisibility(circleGroup1, false);
                if (COMBINEDcircleGroup != null)
                {
                    visibilityController.SetChildObjectsVisibility(COMBINEDcircleGroup, true);
                    BoxCollider combinedCollider = COMBINEDcircleGroup.GetComponent<BoxCollider>();
                    Vector3 newColliderSize = combinedCollider.size;
                    newColliderSize.x = colliderBoundsPreset.x; // Assign the desired x size
                    combinedCollider.size = newColliderSize;
                }
                mainNumber1.primaryYes = false;
                mainNumber2.primaryYes = false;
                //visibilityController.SetObjectVisibility(secondaryNumDisplay, false);
            }

            // Check for overlap between circleGroup and circleGroup1 colliders
            //GameObject circleGroup1 = CircleGridGenerator1.circleGroup;
            if (isDraggingComb == true)
            {
                VisibilityController visibilityController = GetComponent<VisibilityController>();
                visibilityController.SetChildObjectsVisibility(circleGroup, true);
                visibilityController.SetChildObjectsVisibility(circleGroup1, true);
                if (COMBINEDcircleGroup != null)
                {
                    visibilityController.SetChildObjectsVisibility(COMBINEDcircleGroup, false);

                    BoxCollider combinedCollider = COMBINEDcircleGroup.GetComponent<BoxCollider>();
                    Vector3 newColliderSize = combinedCollider.size;
                    newColliderSize.x = 0; // Assign the desired x size
                    combinedCollider.size = newColliderSize;
                }
                mainNumber1.primaryYes = true;
                mainNumber2.primaryYes = true;
                isDraggingComb = false;
                //visibilityController.SetObjectVisibility(secondaryNumDisplay, false);
            }
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 targetPosition = currentMousePosition - offset;
            circleGroup.transform.position = targetPosition;
            lastMousePosition = currentMousePosition;
        }
        if (collided && !collided1)
        {
            // Get Values from CircleGen 
            additiveRemainingCircles = CircleGridGenerator1.remainingCircles;
            additiveCirclesInCompleteRows = CircleGridGenerator1.circlesInCompleteRows;

            mainNumber1.secondaryNum = circlesInCompleteRows + additiveCirclesInCompleteRows;
            mainNumber2.secondaryNum = remainingCircles + additiveRemainingCircles;
            HandleCollision(circlesInCompleteRows, remainingCircles, additiveRemainingCircles, additiveCirclesInCompleteRows);
            collided1 = true;
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void HandleCollision(int circlesInCompleteRows, int remainingCircles, int additiveRemainingCircles, int additiveCirclesInCompleteRows)
    {
        // Your collision handling code goes here
        // You can access circlesInCompleteRows and remainingCircles as needed
        // For example:
        COMBINEDcirclesInCompleteRows = circlesInCompleteRows + additiveCirclesInCompleteRows;
        COMBINEDremainingCircles = remainingCircles + additiveRemainingCircles;

        int fullColumns = (COMBINEDcirclesInCompleteRows + COMBINEDremainingCircles) / circlesPerColumn;

        // Calculate the total number of columns based on full columns and remaining circles
        int totalColumnsremaining = COMBINEDremainingCircles / circlesPerColumn;
        int totalColumnscomplete = fullColumns - totalColumnsremaining;
        int totalColumns = totalColumnscomplete + totalColumnsremaining;

        // for grid? Calculate the center position of the grid
        if ((COMBINEDremainingCircles % circlesPerColumn) > 0)
        {
            if (COMBINEDremainingCircles < 10)
            {
                totalColumns = COMBINEDremainingCircles > 0 ? fullColumns + 1 : fullColumns;
            }
            else if (COMBINEDremainingCircles < 20)
            {
                totalColumns = COMBINEDremainingCircles > 10 ? fullColumns + 1 : fullColumns;
            }
        }
        else
        {
            totalColumns = fullColumns;
        }

        // Create a parent object to group the circles
        COMBINEDcircleGroup = new GameObject("COMBINEDCircleGroup");

        // Calculate the center position of the grid
        Vector3 centerPosition = new Vector3((totalColumns - 1) * circleSpacing / 2f, 0f, 0f);

        //int generatedCircles = 0; // Initialize the generatedCircles variable

        // Spawn the circles in a grid pattern
        for (int column = 0; column < totalColumns; column++)
        {
            int circlesInColumn = column < fullColumns ? circlesPerColumn : (COMBINEDremainingCircles % circlesPerColumn);

            bool useRedPrefab = column >= totalColumnscomplete; // Initialize the useRedPrefab variable

            generatedCircles += circlesInColumn;

            for (int row = 0; row < circlesInColumn; row++)
            {
                // Calculate the position of each circle
                float xPosition = column * circleSpacing;
                float yPosition = row * circleSpacing;
                Vector3 position = centerPosition + new Vector3(xPosition, yPosition, 0f);

                // Instantiate the circle using the appropriate prefab
                GameObject circlePrefabToUse = useRedPrefab ? circleRedPrefab : circlePrefab;
                GameObject circle = Instantiate(circlePrefabToUse, position, Quaternion.identity);
                circle.transform.SetParent(COMBINEDcircleGroup.transform);
            }
        }

        Vector3 firstCirclePosition = COMBINEDcircleGroup.transform.GetChild(0).transform.position;
        // Get the last child object's position
        Vector3 lastCirclePosition = COMBINEDcircleGroup.transform.GetChild(COMBINEDcirclesInCompleteRows - 1).transform.position;
        Vector3 delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        BoxCollider collider = COMBINEDcircleGroup.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition;
        collider.size = new Vector3((totalColumnsremaining + totalColumnscomplete) * circleSpacing, circlesPerColumn * circleSpacing, 1f);
        colliderBoundsPreset = collider.size;

        COMBINEDcircleGroup.transform.position = new Vector3(xOffset, yOffset, 0f);
        COMBINEDcircleGroup.transform.rotation = Quaternion.Euler(0f, 0f, zRotationOffset);
    }
}
