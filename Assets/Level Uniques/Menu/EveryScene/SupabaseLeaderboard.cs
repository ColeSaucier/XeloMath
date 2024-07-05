using Supabase;
using System.Threading.Tasks;
using UnityEngine;
using Supabase.Gotrue;
using Supabase.Gotrue.Exceptions;
using TMPro;
using Client = Supabase.Client;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Postgrest.Models;

public class LeaderboardManager : MonoBehaviour
{
    private Client supabase;
    public TextMeshProUGUI[] nameTexts; // Array to hold TextMeshPro objects for display
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI[] durationTexts;
    public TextMeshProUGUI[] rankTexts;
    public TextMeshProUGUI[] ratingTexts;

    private SceneData sceneObject;
    private string scenejsonFilePath;
    private string sceneJsonString;
    private string filePath;
    private VariableData variableObject;
    public string variablejsonFilePath;
    private string variableJsonString;
    public SceneCompleteMenu sceneCompleteMenu_script;
    public string currentScene;

    public TextMeshProUGUI medianText;
    public TextMeshProUGUI percentileText;

    public Canvas leaderboard;
    public CanvasGroup leaderboardCanvasGroup;

    private ActivityInsertDelayed activityInsertObject;

    private void Start()
    {
        // Initialize the Supabase client
        supabase = new Client("https://acpornqddkzqsdppbabw.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFjcG9ybnFkZGt6cXNkcHBiYWJ3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM2NTU5MzcsImV4cCI6MjAyOTIzMTkzN30.UQ73w2nx-UxmXhxBF2_jSTl19aZ1bjb9LjYY4eraMtY");
        //retrieve data and create dataObjects
        sceneObject = new SceneData();
        scenejsonFilePath = sceneCompleteMenu_script.scenejsonFilePath;
        filePath = Path.Combine(Application.persistentDataPath, scenejsonFilePath);
        sceneJsonString = File.ReadAllText(filePath);
        sceneObject = JsonUtility.FromJson<SceneData>(sceneJsonString);
        variableObject = new VariableData();
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        variableJsonString = File.ReadAllText(filePath);
        variableObject = JsonUtility.FromJson<VariableData>(variableJsonString);
    }

    public async Task SceneCompleteInsert()
    {
        // Get the current scene
        currentScene = sceneCompleteMenu_script.currentScene;

        // Insert activity set into the database
        await FormatActivity_SetInsert(variableObject.setId, variableObject.user, currentScene, sceneCompleteMenu_script.rounded_time);
    }
    public async Task SceneCompleteSupabase()
    {
        // Get user rank
        int userRank = await GetUserRank(currentScene, variableObject.user);
        Debug.LogError($"User rank.{userRank}");

        // Get leaderboard data
        await GetLeaderboardData(userRank, currentScene);

        // Update median and percentile
        await UpdateMedianAndPercentile(currentScene, variableObject.user);

        highlightPlayerRowRed(userRank);
        leaderboardCanvasGroup.alpha = 1f;
    }

    public async Task<int> GetUserRank(string level, string username)
    {
        // Call the get_user_rank function using Supabase's RPC
        var rankResponse = await supabase.Rpc("get_user_rank", new Dictionary<string, object>
        {
            { "level_param", level },
            { "user_param", username }
        });

        if (rankResponse != null)
        {
            // Parse the rank from the response
            var rank = JsonConvert.DeserializeObject<int>(rankResponse.Content.ToString());
            return rank;
        }
        else
        {
            // Handle error
            Debug.LogError("Failed to retrieve user rank.");
            return -1; // Return a default value or handle the error case accordingly
        }
    }

    private async Task GetLeaderboardData(int number, string level)
    {
        if (number <= 6)
            number = 6;
        // Call the get_leaderboard_data function
        var baseResponse = await supabase.Rpc("get_leaderboard_data", new Dictionary<string, object>
        {
            { "level_param", level },
            { "rank_param", number }
        });

        levelText.text = currentScene;
        // Check if response is successful and contains data
        if (baseResponse != null)
        {
            // Parse the JSON string into a list of leaderboard entries
            var leaderboardEntries = JsonConvert.DeserializeObject<List<LeaderboardEntry>>(baseResponse.Content.ToString());

            // Loop through each leaderboard entry and update TextMeshPro objects
            for (int i = 0; i < leaderboardEntries.Count && i < nameTexts.Length; i++)
            {
                // Ensure the corresponding TextMeshPro object exists
                if (nameTexts[i] != null)
                {
                    // Update TextMeshPro text with leaderboard entry details
                    nameTexts[i].text = $"{leaderboardEntries[i].firstname1}";
                }
                // Update duration text
                if (durationTexts[i] != null)
                {
                    CalculateRatingsForLeaderboard(leaderboardEntries[i].duration1, ratingTexts[i]);
                    durationTexts[i].text = $"{leaderboardEntries[i].duration1}";
                }
                // Update rank text
                if (rankTexts[i] != null)
                {
                    rankTexts[i].text = $"{leaderboardEntries[i].rank1}";
                }
            }
        }
        else
        {
            // Handle error
            Debug.LogError("Failed to fetch leaderboard data.");
        }
    }

    public void CalculateRatingsForLeaderboard(decimal duration, TextMeshProUGUI TextMeshObject)
    {
        // Determine Rating
        string completionRating = "Standard";
        if (duration <= sceneObject.goldTime)
        {
            completionRating = "Epic";
            TextMeshObject.color = new Color32(255, 165, 0, 255); // Orange
        }
        if (duration <= sceneObject.perfTime)
        {
            completionRating = "Legendary";
            TextMeshObject.color = new Color32(128, 0, 128, 255); // Purple
        }
        TextMeshObject.text = completionRating;
    }

    private async Task UpdateMedianAndPercentile(string level, string username)
    {
        // Call the calculate_median and get_percentile functions using Supabase's RPC
        var medianResponse = await supabase.Rpc("calculate_median", new Dictionary<string, object>
        {
            { "level_param", level }
        });

        var percentileResponse = await supabase.Rpc("get_percentile", new Dictionary<string, object>
        {
            { "level_param", level },
            { "user_param", username }
        });

        // Check if responses are successful and contain data
        if (medianResponse != null && percentileResponse != null)
        {
            // Parse the JSON strings into numeric values
            var medianValue = JsonConvert.DeserializeObject<decimal>(medianResponse.Content.ToString());
            var percentileValue = JsonConvert.DeserializeObject<decimal>(percentileResponse.Content.ToString());

            // Update TextMeshPro objects with median and percentile values
            medianText.text = $"Median Speed: {medianValue}";
            percentileText.text = $"You're Top: {(float)Math.Round(percentileValue, 2)}%";
        }
        else
        {
            // Handle error
            Debug.LogError("Failed to update median and percentile values.");
        }
    }

    public async Task FormatActivity_SetInsert(int setId1, string user_name1, string level1, float duration1)
    {
        if (supabase == null) {
            Debug.LogError("Supabase client has not been initialized.");
            return;
        }

        try {
            var result = await supabase.Rpc("insert_activity_set", new Dictionary<string, object> {
                { "set_id_param", setId1 },
                { "user_name_param", user_name1 },
                { "level_param", level1 },
                { "duration_param", duration1 }
            });

            if (result != null) {
                Debug.LogError("Activity set inserted successfully");
            } else {
                Debug.LogError("Received null result without throwing an exception.");
                throw new InvalidOperationException("Supabase returned a null result unexpectedly.");
            }
        } catch (Exception ex) {
            Debug.LogError($"Failed to insert activity set due to an exception: {ex.Message}");
            //IMPO: Save activity to json object then file.

            activityInsertObject = new ActivityInsertDelayed();
            // Set ActivityInsert() values from args
            activityInsertObject.setId1 = setId1;
            activityInsertObject.user_name1 = user_name1;
            activityInsertObject.level1 = level1;
            activityInsertObject.duration1 = duration1;



            int i = 0;
            string currentFilePath = Path.Combine(Application.persistentDataPath, $"ActivityInsertDelayed{i}.json");//"ActivityInsert0.json"

            //file already exists, find empty value
            while (File.Exists(currentFilePath))
            {
                i++;
                currentFilePath = Path.Combine(Application.persistentDataPath, $"ActivityInsertDelayed{i}.json");
            }
            //file does not exist
            // Save to JSON file
            string json = JsonUtility.ToJson(activityInsertObject);
            File.WriteAllText(currentFilePath, json);

            // Handle error
            Debug.LogError("Failed to insert activity set. SAVED...");
            //Debug.LogError("currentFilePath: "  + currentFilePath);
        }
    }
    [Serializable]
    public class ActivityInsertDelayed
    {
        public int setId1;
        public string user_name1;
        public string level1;
        public float duration1;
    }

    public void highlightPlayerRowRed(int i)
    {
        i -= 1;
        if (i > 5)
            i = 5;

        if (nameTexts[i] != null)
        {
            nameTexts[i].color = new Color32(210, 0, 0, 255); // Change color to red
        }
        // Update duration text
        if (durationTexts[i] != null)
        {
            durationTexts[i].color = new Color32(210, 0, 0, 255); // Change color to red
        }
        // Update rank text
        if (rankTexts[i] != null)
        {
            rankTexts[i].color = new Color32(210, 0, 0, 255); // Change color to red
        }
    }



    // Class to represent a single leaderboard entry
    private class LeaderboardEntry
    {
        public string firstname1 { get; set; }
        public decimal duration1 { get; set; }
        public long rank1 { get; set; }
    }

    [Serializable]
    public class SceneData
    {
        public float bestTime;
        public string bestRating;
        public int goldTime;
        public int perfTime;
        public int numRepetitions;
    }
    [Serializable]
    public class VariableData
    {
        public string user;
        public string currentScene;
        public int counterScene;
        public float timeElapsed;
        public int setId;
    }
}


/*
using Supabase;
using System.Threading.Tasks;
using UnityEngine;
using Supabase.Gotrue;
using Supabase.Gotrue.Exceptions;
using TMPro;
using Client = Supabase.Client;
using System.Collections.Generic;
using Newtonsoft.Json;

public class FunctionResult
{
    public string Username { get; set; }
    public string Firstname1 { get; set; }
    public string Level1 { get; set; }
    public decimal Duration1 { get; set; }
    public long Rank1 { get; set; }
}

public class LeaderboardManager : MonoBehaviour
{
    private Client supabase;
    public string leaderboardDataString;

    private async void Start()
    {
        // Initialize the Supabase client
        supabase = new Client("https://acpornqddkzqsdppbabw.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFjcG9ybnFkZGt6cXNkcHBiYWJ3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM2NTU5MzcsImV4cCI6MjAyOTIzMTkzN30.UQ73w2nx-UxmXhxBF2_jSTl19aZ1bjb9LjYY4eraMtY");

        await GetLeaderboardData(6, "NumberCounting");
    }

    private async Task GetLeaderboardData(int number, string level)
    {
        // Call the get_leaderboard_data function
        var baseResponse = await supabase.Rpc("get_leaderboard_data", new Dictionary<string, object>
        {
            { "level_param", "NumberCounting" },
            { "rank_param", 6 }
        });
        
        var modeledResponse = new ModeledResponse<FunctionResult>(baseResponse, new JsonSerializerSettings());

        // Access the data
        foreach (var result in modeledResponse.Models)
        {
            Console.WriteLine($"Username: {result.Username}, Firstname1: {result.Firstname1}, Level1: {result.Level1}, Duration1: {result.Duration1}, Rank1: {result.Rank1}");
        }
        

        // Check if response is successful and contains data
        if (baseResponse != null)
        {
            // Convert response data to string (you may need to adjust this based on the actual response structure)
            leaderboardDataString = baseResponse.Content.ToString();

            // Log the data
            Debug.Log(leaderboardDataString);
        }
        else
        {
            // Handle error
            Debug.LogError("Failed to fetch leaderboard data.");
        }
    }
}
*/