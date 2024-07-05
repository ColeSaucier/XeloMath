using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;        
using UnityEngine.SceneManagement;
using System.IO;
using System.Reflection;

public class DetermineCompletedLevels : MonoBehaviour
{
    public List<string> completedLevels;// = new List<string> { "0.0 NumCountingOrdered", "0.5 NumCountingRandom", "0.75 ShapePattern", "1.0 AdditionV", "1.02 MultiplicationV"};
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public Button Button6;
    public Button Button7;
    public Button Button8;
    public Button Button9;
    public Button Button10;
    public Button Button11;
    public Button Button12;
    public Button Button13;
    public Button Button14;
    public Button Button15;
    public Button Button16;
    public Button Button17;
    public Button Button18;
    public Button Button19;
    public Button Button20;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;
    public TextMeshProUGUI text6;
    public TextMeshProUGUI text7;
    public TextMeshProUGUI text8;
    public TextMeshProUGUI text9;
    public TextMeshProUGUI text10;
    public TextMeshProUGUI text11;
    public TextMeshProUGUI text12;
    public TextMeshProUGUI text13;
    public TextMeshProUGUI text14;
    public TextMeshProUGUI text15;
    public TextMeshProUGUI text16;
    public TextMeshProUGUI text17;
    public TextMeshProUGUI text18;
    public TextMeshProUGUI text19;
    public TextMeshProUGUI text20;

    public GameObject menu;
    public GameObject loadingInterface;
    public Image progressBar;

    public Dictionary<string, string> allSceneRatingDictionary;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private string filePath;
    public string completedLevelTextFilePath;
    private string ratingString;

    public string allSceneRatingsjsonFilePath;
    private string allSceneRatingsJsonString;
    private AllSceneRatingsData allSceneRatingObject;
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, completedLevelTextFilePath);
        completedLevels = GetUniqueValuesFromFile();
        //Debug.Log("completedLevels: " + string.Join(", ", completedLevels.ToArray()));

        //retrieve data and create dataObject + UpdateValue_AllSceneRatings()
        allSceneRatingObject = new AllSceneRatingsData();
        filePath = Path.Combine(Application.persistentDataPath, allSceneRatingsjsonFilePath); // Combine with the Assets folder
        allSceneRatingsJsonString = File.ReadAllText(filePath);
        allSceneRatingObject = JsonUtility.FromJson<AllSceneRatingsData>(allSceneRatingsJsonString);
        
        allSceneRatingDictionary = new Dictionary<string, string>
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
        DeactivateButtons();
    }

    void DeactivateButtons()
    {
        //DeactivateButtonIfNotInList(Button1);
        DeactivateButtonIfNotInList(Button2);
        DeactivateButtonIfNotInList(Button3);
        DeactivateButtonIfNotInList(Button4);
        DeactivateButtonIfNotInList(Button5);
        //DeactivateButtonIfNotInList(Button6);
        DeactivateButtonIfNotInList(Button7);
        DeactivateButtonIfNotInList(Button8);
        DeactivateButtonIfNotInList(Button9);
        DeactivateButtonIfNotInList(Button10);
        DeactivateButtonIfNotInList(Button11);
        //DeactivateButtonIfNotInList(Button12);
        DeactivateButtonIfNotInList(Button13);
        DeactivateButtonIfNotInList(Button14);
        DeactivateButtonIfNotInList(Button15);
        //DeactivateButtonIfNotInList(Button16);
        DeactivateButtonIfNotInList(Button17);
        DeactivateButtonIfNotInList(Button18);
        DeactivateButtonIfNotInList(Button19);
        DeactivateButtonIfNotInList(Button20);

        UpdateTextRatings(text1);
        UpdateTextRatings(text2);
        UpdateTextRatings(text3);
        UpdateTextRatings(text4);
        UpdateTextRatings(text5);
        UpdateTextRatings(text6);
        UpdateTextRatings(text7);
        UpdateTextRatings(text8);
        UpdateTextRatings(text9);
        UpdateTextRatings(text10);
        UpdateTextRatings(text11);
        UpdateTextRatings(text12);
        UpdateTextRatings(text13);
        UpdateTextRatings(text14);
        UpdateTextRatings(text15);
        UpdateTextRatings(text16);
        UpdateTextRatings(text17);
        UpdateTextRatings(text18);
        UpdateTextRatings(text19);
        UpdateTextRatings(text20);
    }

    public void DeactivateButtonIfNotInList(Button button)
    {
        //Debug.Log("The name of this GameObject is: " + button.gameObject.name);
        //Debug.Log("completedLevels " + string.Join(", ", completedLevels.ToArray()));
        // Check if the button's name is not in the completedLevels list
        if (!completedLevels.Contains(button.gameObject.name))
        {
            // If not in the list, deactivate the button
            //button.interactable = false;
            button.gameObject.SetActive(false);
        }
    }
    
    public void UpdateTextRatings(TextMeshProUGUI textUGUI)
    {
        ratingString = textUGUI.gameObject.name;
        // text.text = GetPropertyValue(allSceneRatingObject, ratingString);
        textUGUI.text = allSceneRatingDictionary[ratingString];
    }

    public void StartAnyScene(Button button)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(button.gameObject.name));
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        float progressValue = 0;
        for(int i=0; i<scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                progressValue += scenesToLoad[i].progress;
                // Error below, because no progressbar set in the level Menu
                progressBar.fillAmount = progressValue / scenesToLoad.Count;
                yield return null;
            }
        }
    }

    List<string> GetStringListFromFile()
    {
        if (File.Exists(filePath))
        {
            return new List<string>(File.ReadAllLines(filePath));
        }
        return new List<string>();
    }

    List<string> GetUniqueValuesFromFile()
    {
        List<string> allValues = GetStringListFromFile();

        // Use a HashSet to efficiently remove duplicates
        HashSet<string> uniqueSet = new HashSet<string>(allValues);

        // Convert back to a List if needed
        List<string> completedLevels = new List<string>(uniqueSet);

        return completedLevels;
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
}