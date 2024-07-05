using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRowObject : MonoBehaviour
{
    public GameObject boxPrefab;

    public int numInteractableLevel = 0;
    public int minRandomNumberOfBoxes = 1;
    public int maxRandomNumberOfBoxes = 9;
    public static int answer; // Declare the answer variable

    public float minX = 0;
    public float maxX = 100;
    public float minY = 0;
    public float maxY = 100;

    public Transform xCenterObject;
    public Transform yCenterObject; // Reference to the center object (e.g., Main Camera)

    int numberOfBoxes;

    void Start()
    {
        GameObject parentObject = new GameObject("CirclesGroup"); // Create the parent object for circles

        // Generate the number of boxes randomly
        numberOfBoxes = Random.Range(minRandomNumberOfBoxes, maxRandomNumberOfBoxes + 1);
        answer = numInteractableLevel + numberOfBoxes; // Assign value to answer variable

        for (int i = 0; i < numberOfBoxes; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Instantiate(boxPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity, parentObject.transform);
        }
    }
}