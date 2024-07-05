using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    // Method to set visibility of all child objects under the parentGroup
    public void SetChildObjectsVisibility(GameObject parentGroup, bool isVisible)
    {
        // Loop through all child objects of the parent group
        foreach (Transform child in parentGroup.transform)
        {
            // Check if the child has a renderer
            if (child.GetComponent<Renderer>() != null)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                // Set the renderer's enabled property based on the isVisible argument
                childRenderer.enabled = isVisible;
            }
            if (child.GetComponent<Collider>() != null)
            {
                Collider collider = child.GetComponent<Collider>();
                collider.enabled = isVisible;
            }
        }
    }
    // Not used yet in any code, deletable
    public void SetObjectVisibility(GameObject parent, bool isVisible)
    {
        // Check if the object has a renderer
        if (parent.GetComponent<Renderer>() != null)
        {
            Renderer objectRenderer = parent.GetComponent<Renderer>();
            // Set the renderer's enabled property based on the isVisible argument
            objectRenderer.enabled = isVisible;
        }

        if (parent.GetComponent<Collider>() != null)
        {
            Collider collider = parent.GetComponent<Collider>();
            collider.enabled = isVisible;
        }
    }

    public void SetChildObjectsSortOrder(GameObject parentGroup, int sortOrder)
    {
        // Loop through all child objects of the parent group
        foreach (Transform child in parentGroup.transform)
        {
            // Check if the child has a renderer
            if (child.GetComponent<Renderer>() != null)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                // Set the renderer's sorting order based on the sortOrder argument
                childRenderer.sortingOrder = sortOrder;
            }
        }
    }

    // Not used yet in any code, deletable
    public void SetObjectSortOrder(GameObject parent, int sortOrder)
    {
        // Check if the object has a renderer
        if (parent.GetComponent<Renderer>() != null)
        {
            Renderer objectRenderer = parent.GetComponent<Renderer>();
            // Set the renderer's sorting order based on the sortOrder argument
            objectRenderer.sortingOrder = sortOrder;
        }
    }
}