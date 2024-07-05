using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOnAnswerBox110 : MonoBehaviour
{
    private Renderer objectRenderer;
    public bool isInputActive = false;
    public CanvasGroup popUpCanvasGroup; // Reference to the pop-up canvas's CanvasGroup component
    private bool sceneComplete = false;

    public GameObject currentRowAnswers;
    private GameObject lastClickedObject;
    private int colliderInst;


    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        popUpCanvasGroup.alpha = 0f; // Set the pop-up canvas's alpha to 0 (fully transparent) initially
        popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas

        // Set the visibility of all child objects to false after generating the parent groups
        VisibilityController visibilityController = GetComponent<VisibilityController>();
    }

    void OnMouseDown()
    {
        if (sceneComplete != true)
        {
            isInputActive = !isInputActive;
            if (isInputActive)
            {
                objectRenderer.material.color = Color.gray;

                VisibilityController visibilityController = GetComponent<VisibilityController>();
                popUpCanvasGroup.alpha = 1f; // Set the pop-up canvas's alpha to 0 (fully transparent) initially
                popUpCanvasGroup.interactable = true; // Disable interactions with the pop-up canvas
                if (lastClickedObject != null)
                {
                    lastClickedObject.GetComponent<Renderer>().material.color = Color.white;
                    lastClickedObject = null;
                }

                visibilityController.SetChildObjectsVisibility(currentRowAnswers, true);
            }

            if (!isInputActive)
            {
                objectRenderer.material.color = Color.white;

                VisibilityController visibilityController = GetComponent<VisibilityController>();
                visibilityController.SetChildObjectsVisibility(currentRowAnswers, false);
                popUpCanvasGroup.alpha = 0f; // Set the pop-up canvas's alpha to 0 (fully transparent) initially
                popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas

                if (lastClickedObject != null)
                {
                    // Handle correct answer input
                    if (int.Parse(lastClickedObject.name) == NumberDisplay.answerInt)
                    {
                        sceneComplete = true;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneComplete = NumberDisplay.sceneComplete;
        if (sceneComplete == true)
            objectRenderer.material.color = Color.green;

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
                        hit.collider.GetComponent<Renderer>().material.color = Color.blue;

                        // Now you have a reference to the object that was clicked, and you can use it as needed.
                        // For example, you can change its color or perform other actions.
                    }
                }
            }
        }
    }
}