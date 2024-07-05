using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockGenerator : MonoBehaviour
{
    public Transform centerCircle;
    public Transform locationObjects;
    public int randomHour;
    public int randomMinute;
    public SpriteRenderer hourHandSprite;
    public SpriteRenderer minuteHandSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Generate random hour and minute values between 1 and 12
        randomHour = Random.Range(1, 13);
        randomMinute = Random.Range(0, 12);

        // Calculate the angles for the hour and minute hands
        float hourAngle = 90f - (30 * randomHour);
        float minuteAngle = 90f - (30 * randomMinute);

        // Calculate the average position of the center circle and the random number object
        Vector3 hourAveragePosition = (1.2f * centerCircle.position + 0.8f * locationObjects.Find(randomHour.ToString()).position) / 2f;
        Vector3 minuteAveragePosition;
        if (randomMinute == 0)
            minuteAveragePosition = (centerCircle.position + locationObjects.Find("12").position) / 2f;
        else
            minuteAveragePosition = (centerCircle.position + locationObjects.Find(randomMinute.ToString()).position) / 2f;
        
        // Create and position the hour hand sprite
        CreateAndPositionHand(hourHandSprite, hourAveragePosition, hourAngle);

        // Create and position the minute hand sprite
        CreateAndPositionHand(minuteHandSprite, minuteAveragePosition, minuteAngle);
    }

    // Function to create and position a hand sprite
    void CreateAndPositionHand(SpriteRenderer handSprite, Vector3 position, float angle)
    {
        if (handSprite != null)
        {
            // Instantiate a new sprite object
            SpriteRenderer newHand = Instantiate(handSprite);

            // Set its position and rotation based on the calculated values
            newHand.transform.position = position;
            newHand.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            Debug.LogError("Hand sprite is not assigned.");
        }
    }
}


