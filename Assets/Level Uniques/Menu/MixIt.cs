using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MixIt : MonoBehaviour
{
    // Static variables
    public static List<string> mixList = new List<string>();
    public static bool mixBool;
    public HomescreenSceneManager homescreenSceneManager;

    void start()
    {
        mixBool = false;
    }
    public void CreateMix()
    {
        List<string> completedLevels = homescreenSceneManager.completedLevels;

        if (homescreenSceneManager.completedLevels.Count >= 4)
        {
            mixBool = true;
            // Get the last four elements from HomescreenSceneManager.completedLevels
            int startIndex = homescreenSceneManager.completedLevels.Count - 4;
            for (int i = startIndex; i < homescreenSceneManager.completedLevels.Count; i++)
            {
                mixList.Add(homescreenSceneManager.completedLevels[i]);
            }
        }
        else
        {
            Debug.LogError("You have not unlocked this");
        }
    }

    public void StartRandomMixLevel()
    {
        if (mixList.Count > 0)
        {
            // Generate a random index within the bounds of mixList
            int randomIndex = Random.Range(0, mixList.Count);

            // Access the random entry from mixList
            string nextScene = mixList[randomIndex];

            mixList.Remove(nextScene);

            SceneManager.LoadScene(nextScene);
        }
        else
        {
            //go back to menu
            SceneManager.LoadScene("Speed");
        }
    }

    public void StartMixIt()
    {
        CreateMix();
        StartRandomMixLevel();
    }
}