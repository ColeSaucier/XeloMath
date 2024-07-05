using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class DataCollector : MonoBehaviour
{
    public string variablejsonFilePath;
    private string variableJsonString;
    private VariableData variableObject;

    public SceneCompleteMenu sceneCompleteMenu_script;
    public ThumbsUpManager thumbsUp_script;
    public PlayerPreferenceManagement playerData_script;

    private void Start()
    {
        LoadVariableData();
    }

    private void LoadVariableData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        variableJsonString = File.ReadAllText(filePath);
        variableObject = JsonUtility.FromJson<VariableData>(variableJsonString);
    }

    //string query = FormatActivity_repsInsert();
    private string FormatActivity_repsInsert()
    {
        // This creates the VariableData object
        variableObject = new VariableData();
        LoadVariableData();

        // Get data values 
        string user = playerData_script.user;
        string level = SceneManager.GetActiveScene().name;
        float duration = sceneCompleteMenu_script.elapsedTimeFinal;
        int setId = variableObject.setId + 1;
        int thumbsUp = thumbsUp_script.thumbsUp;
        bool completedBool = sceneCompleteMenu_script.SceneWASCompleted;
        //bool timeEnabled = sceneCompleteMenu_script.timeEnabled;


        // Format the insert query
        string insertQuery = $"INSERT INTO Activity_reps (user, level, duration, thumbsUp, setId, completedBool) VALUES ('{user}', '{level}', {duration}, {thumbsUp}, {setId}, {completedBool})";

        return insertQuery;
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