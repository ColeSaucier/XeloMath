using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickHandler : MonoBehaviour
{
    public Color tintColor = Color.blue; // Set your desired tint color in the Inspector
    public int num1 = 0;
    public int num2 = 0;

    public Canvas canvas;
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject clickedObject = hitInfo.collider.gameObject;
                Debug.Log("Clicked object name: " + clickedObject.name);

                Renderer renderer = clickedObject.GetComponent<Renderer>(); // Assuming your objects have a Renderer component

                string objectName = clickedObject.name;
                char num1 = objectName[0]; // Extracted from the object's name
                char num2 = objectName[2]; // Extracted from the object's name

                reset_colors();
                change_colors(num1, num2);
                update_additional_nums(num1, num2);

                if (renderer != null)
                {
                    Material material = renderer.material;
                    material.color = tintColor; // Change the material's tint color
                }
            }
        }
    }

    public void change_colors(char num1, char num2)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            string childName = child.name;
            char Cnum1 = childName[0]; // Extracted from the object's name
            char Cnum2 = childName[2];

            if (Cnum1 <= num1 & Cnum2 <= num2)
            {
                Renderer renderer = child.GetComponent<Renderer>(); // Assuming your objects have a Renderer component

                if (renderer != null)
                {
                    Material material = renderer.material;
                    material.color = tintColor; // Change the material's tint color
                }
            }
        }
    }
    public void reset_colors()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Renderer renderer = child.GetComponent<Renderer>(); // Assuming your objects have a Renderer component

            if (renderer != null)
            {
                Material material = renderer.material;
                material.color = Color.white; // Change the material's tint color
            }
        }
    }
    public void update_additional_nums(char num1, char num2)
    {
        TextMeshProUGUI[] textElements = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        int num1Int = int.Parse(num1.ToString()); // Convert num1 to an integer
        int num2Int = int.Parse(num2.ToString()) + 1;

        for (int i = 0; i < textElements.Length; i++)
        {
            if (i == 0)
            {
                textElements[0].text = num2Int.ToString();
            }
            else if (i <= num1Int)
            {
                if (num2Int == 10)
                    textElements[i].text = num2Int.ToString();
                else
                    textElements[i].text = num2Int.ToString()+"+";
            }

            else
            {
                textElements[i].text = "";
            }
        }
    }
}
