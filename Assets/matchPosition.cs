using UnityEngine;

public class MatchPosition : MonoBehaviour
{
    public Transform targetObject; // Reference to the object whose position you want to match

    private Transform thisTransform; // Reference to the Transform component of the current object

    private void Start()
    {
        thisTransform = transform; // Get the Transform component of the current object
    }

    private void Update()
    {
        // Match the position of the current object to the position of the target object
        thisTransform.position = targetObject.position;
    }
}
