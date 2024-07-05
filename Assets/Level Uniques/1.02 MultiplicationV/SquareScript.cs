using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquareScript : MonoBehaviour
{
    public bool isHighlighted = false;
    public Renderer squareRenderer;
    public int rowNum;
    public int columnNum;

    void Start()
    {
        squareRenderer = GetComponent<Renderer>();
        squareRenderer.material.color = Color.black;
    }

    public void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
        squareRenderer = GetComponent<Renderer>();
        squareRenderer.material.color = Color.green;
        squareRenderer.material.color = isHighlighted ? Color.yellow : Color.white;
    }
}