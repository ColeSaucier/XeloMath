using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Reflection;

public class SceneCompleteMenu : MonoBehaviour
{
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public TextMeshProUGUI ratingText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI besttimeText;
    //public GameObject SceneCompleteCanvas;
    public TextMeshProUGUI repCounter;
    public CanvasGroup sceneCanvasGroup;
    public CanvasGroup buttonCanvasGroup;
    public string completionRating = "";

    public bool SceneComplete = false;
    public bool SceneWASCompleted = false;
    public float startTime;

    //For Data Collection/Saving
    private SceneData sceneObject;
    public string scenejsonFilePath;
    private string sceneJsonString;
    private VariableData variableObject;
    public string variablejsonFilePath;
    private string variableJsonString;
    private AllSceneRatingsData allSceneRatingObject;
    public string allSceneRatingsjsonFilePath;
    private string allSceneRatingsJsonString;
    private string filePath; 
    public string completedLevelTextFilePath;//"1.01 SubtractionV"
    List<string> levelOrder = new List<string> {"NumberCounting", "NumberCountingScattered", "BasicAdditionV", "BasicSubtractionV", "ShapePatterns", "SmallerOrBigger", "PlaceValues", "Clock", "AdditionV", "AdditionFunctionBox", "SubtractionFunctionBox", "MultiplicationV", "DivisionV", "NormalAddition", "NormalSubtraction", "LongMultiplication", "FractionFromShape", "FractionEqualize", "FractionEqualizeHard", "LongDivision"};
    public string currentScene;// = text.gameObject.name;

    public GameObject barHolder;
    public Image beatScoreBar;
    public float repPaceTime;
    public float totalTimeToBeatScore;
    public float elapsedTime;
    public float elapsedTimeFinal;

    public GameObject improveAnimation1;
    public GameObject improveAnimation2;
    public GameObject correctAnimation;
    public LeaderboardManager leaderboardScript;
    public float rounded_time;
    public bool leaderboardEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        sceneCanvasGroup.interactable = false;

        //retrieve data and create dataObject
        sceneObject = new SceneData();
        LoadSceneData();

        //retrieve data and create dataObject
        variableObject = new VariableData();
        LoadVariableData();

        //Updates repCounter, current scene, and starts counter
        repCounter.text = $"{variableObject.counterScene}/{sceneObject.numRepetitions}";
        currentScene = SceneManager.GetActiveScene().name;
        variableObject.currentScene = currentScene;
        SaveVariableData();

        //Debug.LogError("sceneObject.bestTime: " + sceneObject.bestTime);
        
        //Vars for beatScoreBar
        startTime = Time.time;
        totalTimeToBeatScore = sceneObject.bestTime - variableObject.timeElapsed;
        repPaceTime = totalTimeToBeatScore / (sceneObject.numRepetitions - variableObject.counterScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneComplete)
        {
            // Used in Data collector, to determine if scene was completed
            SceneWASCompleted = true;

            SceneComplete = false;
            barHolder.SetActive(false);
            sceneComplete();
        }
        else
        {
            // checks if variable 
            elapsedTime = Time.time - startTime;
            // beatScoreBar Updating
            if (elapsedTime > totalTimeToBeatScore)
                {
                beatScoreBar.color = Color.red;
                beatScoreBar.fillAmount = (float)(0.1);
                }
            else if (elapsedTime > repPaceTime)
               beatScoreBar.color = Color.yellow;
            else
                beatScoreBar.fillAmount = (float)(0.1 + 0.9 * ((repPaceTime - elapsedTime) / repPaceTime));
        }

    }

    void sceneComplete()
    {
        //Record the duration of one scene repetition, cumalatively into variableObject
        //Raise rep counter
        elapsedTimeFinal = Time.time - startTime;
        variableObject.timeElapsed += elapsedTimeFinal;//(int)
        //variableObject.counterScene = 0;
        variableObject.counterScene++;
        repCounter.text = $"{variableObject.counterScene}/{sceneObject.numRepetitions}";

        //Determine if Reps are completed or to restart
        if (variableObject.counterScene != sceneObject.numRepetitions)
        {
            //saveData
            SaveVariableData();
            string thisScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(thisScene);
        }
        if (variableObject.counterScene == sceneObject.numRepetitions)
        {
            RepsCompleted();
        }
    }

    async void RepsCompleted()
    {
        float current_time = variableObject.timeElapsed;
        rounded_time = (float)Math.Round(current_time, 2);
       
        // Record time in scene json
        if (current_time < sceneObject.bestTime)
        {
            sceneObject.bestTime = current_time;
            timeText.color = Color.blue;
            improveAnimation1.SetActive(true);
            improveAnimation2.SetActive(true);
        } 
        else
            correctAnimation.SetActive(true);

        // Determine Rating (+record it)
        completionRating = "Standard";
        if (sceneObject.bestTime <= sceneObject.goldTime)
        {
            completionRating = "Epic";
            ratingText.color = new Color32(255, 165, 0, 255); // Orange
        }
        if (sceneObject.bestTime <= sceneObject.perfTime)
        {
            completionRating = "Legendary";
            ratingText.color = new Color32(128, 0, 128, 255); // Purple
        }

        UpdateValue_AllSceneRatings(currentScene, completionRating);

        //Assigns repetitions menu complete variables
        sceneObject.bestRating = completionRating;
        ratingText.text = completionRating; 
        float minutes = variableObject.timeElapsed / 60; //sceneObject.bestTime
        float seconds = variableObject.timeElapsed % 60;
        string timeString = $"{(int)minutes}min:{Math.Round(seconds, 2)}sec";
        timeText.text = timeString;

        minutes = sceneObject.bestTime / 60; //sceneObject.bestTime
        seconds = sceneObject.bestTime % 60;
        timeString = $"{(int)minutes}:{Math.Round(seconds, 2)}";
        besttimeText.text = timeString;

        //Reset variableObject
        variableObject.counterScene = 0;
        variableObject.timeElapsed = 0;
        variableObject.setId = variableObject.setId + 1;

        //saveData
        SaveVariableData(); 
        SaveSceneData();

        //save completed string
        filePath = Path.Combine(Application.persistentDataPath, completedLevelTextFilePath);
        AddCompletedScene(currentScene);
        //Debug.LogError("This didnt work");

        //Introduce the scene canvas
        sceneCanvasGroup.alpha = 1f;
        sceneCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;
        //SceneCompleteCanvas.SetActive(true);

        if (leaderboardEnabled)
        {
            await leaderboardScript.SceneCompleteInsert();
            await leaderboardScript.SceneCompleteSupabase();
        }
        else
        {
            await leaderboardScript.SceneCompleteInsert();
            leaderboardScript.leaderboard.gameObject.SetActive(false);
        }
    }

    public void RestartScene()
    {
        //Reset variableObject
        variableObject.counterScene = 0;
        variableObject.timeElapsed = 0;
        SaveVariableData();

        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }
    public void GoToMenu()
    {
        variableObject = new VariableData();
        LoadVariableData();
        //Reset variableObject
        variableObject.counterScene = 0;
        variableObject.timeElapsed = 0;
        SaveVariableData();
        SceneManager.LoadScene("Menu");
    }

    public void BeginNextScene()
    {
        if (MixIt.mixBool)
        {
            //Same as MixIt.StartRandomMixLevel() (In home screen)
            if (MixIt.mixList.Count > 0)
            {
                // Generate a random index within the bounds of mixList
                int randomIndex = UnityEngine.Random.Range(0, MixIt.mixList.Count);

                // Access the random entry from mixList
                string nextScene = MixIt.mixList[randomIndex];

                MixIt.mixList.Remove(nextScene);

                SceneManager.LoadScene(nextScene);
            }
            else
            {
                //go back to menu
                SceneManager.LoadScene("Speed");
            }
        }
        else
        {
            //determine next scene
            string thisScene = SceneManager.GetActiveScene().name;
            int index = levelOrder.IndexOf(thisScene);
            string nextScene = null;

            // Check if the string was found and has a next element
            if (index != -1 && index < levelOrder.Count - 1)
            {
                // Get the next string in the list
                nextScene = levelOrder[index + 1];
            }

            SceneManager.LoadScene(nextScene);
        }
    }

    void AddCompletedScene(string sceneName)
    {
        //Adds the scene name to .txt list, after reps completed
        List<string> completedLevelTextList = GetStringListFromFile();
        completedLevelTextList.Add(sceneName);
        File.WriteAllLines(filePath, completedLevelTextList);
    }

    List<string> GetStringListFromFile()
    {
        if (File.Exists(filePath))
        {
            return new List<string>(File.ReadAllLines(filePath));
        }
        return new List<string>();
    }

    public void HideShowTimeTextsSC(bool enabled)
    {
        //Alter persistent data
        if (enabled)
        {
            // Show the TextMeshPro objects
            besttimeText.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        }
        else
        {
            // Hide the TextMeshPro objects
            besttimeText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
        }
    }
    public void HideShowPaceBarSC(bool enabled)
    {
        if (enabled)
        {
            // Show the pace bar
            beatScoreBar.gameObject.SetActive(true);
        }
        else
        {
            // Hide the pace bar
            beatScoreBar.gameObject.SetActive(false);
        }
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
    public void LoadSceneData()
    {
        filePath = Path.Combine(Application.persistentDataPath, scenejsonFilePath);
        sceneJsonString = File.ReadAllText(filePath);
        sceneObject = JsonUtility.FromJson<SceneData>(sceneJsonString);
    }
    public void SaveSceneData()
    {
        filePath = Path.Combine(Application.persistentDataPath, scenejsonFilePath);
        string sceneJsonString = JsonUtility.ToJson(sceneObject);
        File.WriteAllText(filePath, sceneJsonString);
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
    public void LoadVariableData()
    {
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        variableJsonString = File.ReadAllText(filePath);
        variableObject = JsonUtility.FromJson<VariableData>(variableJsonString);
    }
    public void SaveVariableData()
    {
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        string variableJsonString = JsonUtility.ToJson(variableObject);
        File.WriteAllText(filePath, variableJsonString);
    }

    [Serializable]
    public class AllSceneRatingsData
    {
        public string NumberCounting;
        public string NumberCountingScattered;
        public string BasicAddition;
        public string BasicSubtraction;
        public string ShapePatterns;
        public string SmallerOrBigger;
        public string Clock;
        public string PlaceValues;
        public string AdditionV;
        public string AdditionFunctionBox;
        public string SubtractionFunctionBox;
        public string NormalAddition;
        public string NormalSubtraction;
        public string MultiplicationV;
        public string DivisionV;
        public string LongMultiplication;
        public string FractionFromShape;
        public string FractionEqualize;
        public string FractionEqualizeHard;
        public string LongDivision;
    }
    
    //For AllSceneRatingsData
    void UpdateValue_AllSceneRatings(string key, string rating)
    {
        // Convert rating to number
        string valueRating;
        valueRating = "1";
        if (rating == "Epic")
            valueRating = "2";
        if (rating == "Legendary")
            valueRating = "3";

        //retrieve data and create dataObject
        allSceneRatingObject = new AllSceneRatingsData();
        filePath = Path.Combine(Application.persistentDataPath, allSceneRatingsjsonFilePath); // Combine with the Assets folder
        allSceneRatingsJsonString = File.ReadAllText(filePath);
        allSceneRatingObject = JsonUtility.FromJson<AllSceneRatingsData>(allSceneRatingsJsonString);

        Dictionary<string, string> allSceneRatingDictionary = new Dictionary<string, string>
        {
            { "NumberCounting", allSceneRatingObject.NumberCounting},
            { "NumberCountingScattered", allSceneRatingObject.NumberCountingScattered},
            { "BasicAdditionV", allSceneRatingObject.BasicAddition},
            { "BasicSubtractionV", allSceneRatingObject.BasicSubtraction},
            { "ShapePatterns", allSceneRatingObject.ShapePatterns},
            { "SmallerOrBigger", allSceneRatingObject.SmallerOrBigger},
            { "Clock", allSceneRatingObject.Clock},
            { "PlaceValues", allSceneRatingObject.PlaceValues},
            { "AdditionV", allSceneRatingObject.AdditionV},
            { "AdditionFunctionBox", allSceneRatingObject.AdditionFunctionBox},
            { "SubtractionFunctionBox", allSceneRatingObject.SubtractionFunctionBox},
            { "NormalAddition", allSceneRatingObject.NormalAddition},
            { "NormalSubtraction", allSceneRatingObject.NormalSubtraction},
            { "MultiplicationV", allSceneRatingObject.MultiplicationV},
            { "DivisionV", allSceneRatingObject.DivisionV},
            { "LongMultiplication", allSceneRatingObject.LongMultiplication},
            { "FractionFromShape", allSceneRatingObject.FractionFromShape},
            { "FractionEqualize", allSceneRatingObject.FractionEqualize},
            { "FractionEqualizeHard", allSceneRatingObject.FractionEqualizeHard},
            { "LongDivision", allSceneRatingObject.LongDivision}
        };

        allSceneRatingDictionary[key] = valueRating;
        ConvertDictionaryToJson__SaveIt(allSceneRatingDictionary);
    }

    public void ConvertDictionaryToJson__SaveIt(Dictionary<string, string> dictionary)
    {
        allSceneRatingObject.NumberCounting = dictionary["NumberCounting"];
        allSceneRatingObject.NumberCountingScattered = dictionary["NumberCountingScattered"];
        allSceneRatingObject.BasicAddition = dictionary["BasicAdditionV"];
        allSceneRatingObject.BasicSubtraction = dictionary["BasicSubtractionV"];
        allSceneRatingObject.ShapePatterns = dictionary["ShapePatterns"];
        allSceneRatingObject.SmallerOrBigger = dictionary["SmallerOrBigger"];
        allSceneRatingObject.Clock = dictionary["Clock"];
        allSceneRatingObject.PlaceValues = dictionary["PlaceValues"];
        allSceneRatingObject.AdditionV = dictionary["AdditionV"];
        allSceneRatingObject.AdditionFunctionBox = dictionary["AdditionFunctionBox"];
        allSceneRatingObject.SubtractionFunctionBox = dictionary["SubtractionFunctionBox"];
        allSceneRatingObject.NormalAddition = dictionary["NormalAddition"];
        allSceneRatingObject.NormalSubtraction = dictionary["NormalSubtraction"];
        allSceneRatingObject.MultiplicationV = dictionary["MultiplicationV"];
        allSceneRatingObject.DivisionV = dictionary["DivisionV"];
        allSceneRatingObject.LongMultiplication = dictionary["LongMultiplication"];
        allSceneRatingObject.FractionFromShape = dictionary["FractionFromShape"];
        allSceneRatingObject.FractionEqualize = dictionary["FractionEqualize"];
        allSceneRatingObject.FractionEqualizeHard = dictionary["FractionEqualizeHard"];
        allSceneRatingObject.LongDivision = dictionary["LongDivision"];

        //saveData
        filePath = Path.Combine(Application.persistentDataPath, allSceneRatingsjsonFilePath);
        string allSceneRatingsJsonString = JsonUtility.ToJson(allSceneRatingObject);
        File.WriteAllText(filePath, allSceneRatingsJsonString);
    }
}
