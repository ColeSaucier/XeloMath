using UnityEngine;
using System.IO;

public class ThumbsUpManager : MonoBehaviour
{
    public int thumbsUp = 0; // Public int for thumbsUp

    public void SetThumbsUpPositive()
    {
        thumbsUp = 1;
    }

    public void SetThumbsUpNegative()
    {
        thumbsUp = -1;
    }
}