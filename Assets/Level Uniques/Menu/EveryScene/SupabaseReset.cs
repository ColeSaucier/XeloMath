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

public class SupabaseReset : MonoBehaviour
{
    private Client supabase;

    private ActivityInsertDelayed activityInsertObject;

    private void Start()
    {
        // Initialize the Supabase client
        supabase = new Client("https://acpornqddkzqsdppbabw.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFjcG9ybnFkZGt6cXNkcHBiYWJ3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM2NTU5MzcsImV4cCI6MjAyOTIzMTkzN30.UQ73w2nx-UxmXhxBF2_jSTl19aZ1bjb9LjYY4eraMtY");
        if (supabase == null) {
            Debug.LogError("Supabase client is not initialized.");
            return;
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
}