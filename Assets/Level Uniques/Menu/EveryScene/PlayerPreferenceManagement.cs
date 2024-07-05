using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerPreferenceManagement : MonoBehaviour
{
    public string playerjsonFilePath;
    private string playerjsonString;
    private PlayerData playerObject;
    //public Image paceBarImage;

    public Toggle paceBarToggle;
    public Toggle leaderboardToggle;
    public Toggle timeToggle;

    public InputField userField;
    public string user;

    private bool settingsViewable = false;
    public CanvasGroup playerSettingsCanvasGroup;

    public SceneCompleteMenu sceneCompleteMenu_script;

    private void Start()
    {
        playerObject = new PlayerData();
        LoadPlayerData();

        // Ensures all toggles reflect persistent player settings (on start)
        if (paceBarToggle != null)
        {
            paceBarToggle.isOn = playerObject.timeEnabled;
        }
        if (leaderboardToggle != null)
        {
            leaderboardToggle.isOn = playerObject.leaderboardEnabled;
        }
        if (timeToggle != null)
        {
            timeToggle.isOn = playerObject.timeEnabledNotPace;
        }

        if (playerObject.timeEnabled)
        {
            // Show the pace bar
            //paceBarImage.gameObject.SetActive(true);
        }
        else
        {
            // Hide the pace bar
            sceneCompleteMenu_script.HideShowPaceBarSC(false);
        }

        if (playerObject.timeEnabledNotPace)
        {

        }
        else
        {
            // Hide the TextMeshPro objects
            sceneCompleteMenu_script.HideShowTimeTextsSC(false);
        }

        if (playerObject.leaderboardEnabled)
        {

        }
        else
        {
            // Hide the TextMeshPro objects
            sceneCompleteMenu_script.leaderboardEnabled = false;
        }
    }

    private void LoadPlayerData()
    {
        //Load the data
        string filePath = Path.Combine(Application.persistentDataPath, playerjsonFilePath);
        playerjsonString = File.ReadAllText(filePath);
        playerObject = JsonUtility.FromJson<PlayerData>(playerjsonString);
    }

    private void SavePlayerData()
    {
        //save the data
        string filePath = Path.Combine(Application.persistentDataPath, playerjsonFilePath);
        playerjsonString = JsonUtility.ToJson(playerObject);
        File.WriteAllText(filePath, playerjsonString);
    }

    public void HideShowPaceBar()
    {
        if (paceBarToggle.isOn)
        {
            //Alter persistent data
            playerObject.timeEnabled = true;
            SavePlayerData();
            // Show the pace bar
            sceneCompleteMenu_script.HideShowPaceBarSC(true);
            //paceBarImage.gameObject.SetActive(true);
        }
        else
        {
            //Alter persistent data
            playerObject.timeEnabled = false;
            SavePlayerData();
            // Hide the pace bar
            sceneCompleteMenu_script.HideShowPaceBarSC(false);
            //paceBarImage.gameObject.SetActive(false);
        }
    }

    public void HideShowTimeTexts()
    {
        //Alter persistent data
        if (timeToggle.isOn)
        {
            playerObject.timeEnabledNotPace = true;
            SavePlayerData();
            sceneCompleteMenu_script.HideShowTimeTextsSC(true);
        }
        else
        {
            playerObject.timeEnabledNotPace = false;
            SavePlayerData();
            sceneCompleteMenu_script.HideShowTimeTextsSC(false);
        }
    }

    public void HideShowLeaderboard()
    {
        //Alter persistent data
        if (leaderboardToggle.isOn)
        {
            playerObject.leaderboardEnabled = true;
            SavePlayerData();
            sceneCompleteMenu_script.leaderboardEnabled = true;
        }
        else
        {
            playerObject.leaderboardEnabled = false;
            SavePlayerData();
            sceneCompleteMenu_script.leaderboardEnabled = false;
        }
    }
    //NOT USED CURRENTLY
    public void UpdateUser()
    {
        if (userField != null)
        {
            playerObject.user = userField.text;
            SavePlayerData();
        }
    }

    public void HidePlayerSettingsCanvas()
    {
        settingsViewable = false;
        if (playerSettingsCanvasGroup != null)
        {
            playerSettingsCanvasGroup.alpha = 0f;
            playerSettingsCanvasGroup.interactable = false;
            playerSettingsCanvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowPlayerSettingsCanvas()
    {
        if (!settingsViewable) //false
        {
            settingsViewable = true;
            if (playerSettingsCanvasGroup != null)
            {
                playerSettingsCanvasGroup.alpha = 1f;
                playerSettingsCanvasGroup.interactable = true;
                playerSettingsCanvasGroup.blocksRaycasts = true;
            }
        }
        else //true
        {
            HidePlayerSettingsCanvas();
        }
    }

    [Serializable]
    public class PlayerData
    {
        public string user;
        public string menuText;
        public int gemTotal;
        public bool timeEnabled;
        public bool timeEnabledNotPace;
        public bool leaderboardEnabled;
    }
}