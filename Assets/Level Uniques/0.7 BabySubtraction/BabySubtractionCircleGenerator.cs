using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BabySubtractionCircleGenerator : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public GameObject subtractPrefabToInstantiate;
    public int minObjects = 1;
    public int maxObjects = 10;
    public float spacing = 1.0f;
    public Vector3 initialSpawnPosition = new Vector3(-1, 0, 0);
    public TextMeshPro equation;
    public int resultObjects;

    private bool isDragging;
    private Vector3 lastMousePosition;
    private Vector3 offset;
    public GameObject parentObject;

    public bool collided = false;
    public bool undoCombination;

    private bool isDragging2;
    public Vector3 initialSpawnPosition2 = new Vector3(1, 0, 0);
    public GameObject parentObject2;

    public Vector3 combinedSpawnPosition = new Vector3(0, 0, 0);
    public GameObject combinedObject;

    void Start()
    {
        // Create an empty parent object to hold the generated prefabs
        parentObject = new GameObject("GeneratedPrefabs1");
        parentObject2 = new GameObject("GeneratedPrefabs2");
        combinedObject = new GameObject("CombinedPrefabs");

        // Generate a random number of prefabs
        int numberOfObjects = Random.Range(minObjects, maxObjects + 1);
        int numberOfObjects2 = Random.Range(0, numberOfObjects + 1);
        resultObjects = numberOfObjects - numberOfObjects2;
        equation.text = $"{numberOfObjects}-{numberOfObjects2}";

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Generate a random position with variable spacing
            Vector3 spawnPosition = initialSpawnPosition + new Vector3(0, -spacing, 0) * i;

            // Instantiate the prefab at the generated position
            GameObject newPrefab = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

            // Set the parent of the instantiated prefab to the parentObject
            newPrefab.transform.parent = parentObject.transform;
        }

        Vector3 firstCirclePosition = parentObject.transform.GetChild(0).transform.position;
        // Get the last child object's position
        Vector3 lastCirclePosition = parentObject.transform.GetChild(numberOfObjects - 1).transform.position;
        Vector3 delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        BoxCollider collider = parentObject.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition;
        collider.size = new Vector3(0.5f, numberOfObjects * spacing, 1f);

        for (int i = 0; i < numberOfObjects2; i++)
        {
            // Generate a random position with variable spacing
            Vector3 spawnPosition = initialSpawnPosition2 + new Vector3(0, -spacing, 0) * i;

            // Instantiate the prefab at the generated position
            GameObject newPrefab = Instantiate(subtractPrefabToInstantiate, spawnPosition, Quaternion.identity);

            // Set the parent of the instantiated prefab to the parentObject
            newPrefab.transform.parent = parentObject2.transform;
        }

        firstCirclePosition = parentObject2.transform.GetChild(0).transform.position;
        // Get the last child object's position
        lastCirclePosition = parentObject2.transform.GetChild(numberOfObjects2 - 1).transform.position;
        delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        collider = parentObject2.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition;
        collider.size = new Vector3(0.5f, numberOfObjects2 * spacing, 1f);

        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject correctPrefab;
            if (i >= resultObjects)
                correctPrefab = subtractPrefabToInstantiate;
            else
                correctPrefab = prefabToInstantiate;

            if (i < 10)
            {
                // Generate a random position with variable spacing
                Vector3 spawnPosition = combinedSpawnPosition + new Vector3(0, -spacing, 0) * i;
                // Instantiate the prefab at the generated position
                GameObject newPrefab = Instantiate(correctPrefab, spawnPosition, Quaternion.identity);
                // Set the parent of the instantiated prefab to the parentObject
                newPrefab.transform.parent = combinedObject.transform;
            }
            else
            {
                // Generate a random position with variable spacing
                Vector3 spawnPosition = combinedSpawnPosition + new Vector3(spacing, -spacing * (i-10), 0);
                // Instantiate the prefab at the generated position
                GameObject newPrefab = Instantiate(correctPrefab, spawnPosition, Quaternion.identity);
                // Set the parent of the instantiated prefab to the parentObject
                newPrefab.transform.parent = combinedObject.transform;
            }
        }

        firstCirclePosition = combinedObject.transform.GetChild(0).transform.position;
        
        // Get the last child object's position
        lastCirclePosition = combinedObject.transform.GetChild(numberOfObjects - 1).transform.position;
        Vector3 neededAdjustment = Vector3.zero;
        if (numberOfObjects < 10)
        {
            neededAdjustment = new Vector3(0, 0, 0);
        }
        else
        {
            float tenDifference = numberOfObjects % 10;
            tenDifference = 10 - tenDifference;
            neededAdjustment = new Vector3(0, -spacing * tenDifference/2, 0);
        }
        delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        collider = combinedObject.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition + neededAdjustment;
        collider.size = new Vector3(0.5f, 10 * spacing, 1f);
        combinedObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == parentObject.GetComponent<BoxCollider>())
            {
                isDragging = true;
                lastMousePosition = GetWorldMousePosition();
                offset = lastMousePosition - parentObject.transform.position;
            }
            if (Physics.Raycast(ray, out hit) && hit.collider == parentObject2.GetComponent<BoxCollider>())
            {
                isDragging2 = true;
                lastMousePosition = GetWorldMousePosition();
                offset = lastMousePosition - parentObject2.transform.position;
            }
            if (Physics.Raycast(ray, out hit) && hit.collider == combinedObject.GetComponent<BoxCollider>())
            {
                undoCombination = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isDragging2 = false;
            // Check for overlap between circleGroup and circleGroup1 colliders
            if (parentObject.GetComponent<BoxCollider>().bounds.Intersects(parentObject2.GetComponent<BoxCollider>().bounds))
            {
                parentObject.SetActive(false);
                parentObject2.SetActive(false);
                combinedObject.SetActive(true);
            }
            if (undoCombination)
            {
                undoCombination = false;
                parentObject.SetActive(true);
                parentObject2.SetActive(true);
                combinedObject.SetActive(false);
            }
        }
        if (isDragging)
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 targetPosition = currentMousePosition - offset;
            parentObject.transform.position = targetPosition;
            lastMousePosition = currentMousePosition;
        }
        if (isDragging2)
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 targetPosition = currentMousePosition - offset;
            parentObject2.transform.position = targetPosition;
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
