using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DataLoader : MonoBehaviour
{
	// public GameObject textGO;
	// public List<string> questions;
 //    public List<string> questionsID;
 //    public List<string> questionsPossibleAnswersStrs;

 //    public string currentQuestion, currentQuestionPAS;
 //    int currentQuestionInt = 0;

 //    void Start()
 //    {
 //    	string readFromFilePath = Application.streamingAssetsPath + "/data";

 //        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

 //        foreach(string line in fileLines)
 //        {
 //            string id = line.Substring(0, line.IndexOf(' '));
 //            string tempQuestion = line.Substring(id.Length + 3);
 //            string question = line.Substring(id.Length + 3);
 //            string questionsPossibleAnswersStr = line.Substring(id.Length +  question.Length + 7);

 //            questions.Add(question);
 //            questionsID.Add(id);
 //            questionsPossibleAnswersStrs.Add(questionsPossibleAnswersStr);
 //        }

 //    	textGO.GetComponent<TextMeshProUGUI>().text = questions[0];
 //    }

 //    void Update()
 //    {
 //        currentQuestion = questions[currentQuestionInt];
 //        currentQuestionPAS = questionsPossibleAnswersStrs[currentQuestionInt];
 //    }

 //    public void yesB()
 //    {
 //        currentQuestionInt++;
 //    }

    // use this code later


    public GameData data;

    public string file = "data";

    void Start()
    {
        // data.question1 = "is this person male or female ?";
        // data.question2 = "is this person alive ?";
        // data.question3 = "is this person real ?";
        // data.question4 = "";
        // data.question5 = "";
        // data.question6 = "";
        // data.question7 = "";
        // data.question8 = "";
        // data.question9 = "";
        // data.question10 = "";
        // data.question11 = "";
        // data.question12 = "";
        // data.question13 = "";
        // data.question14 = "";
        // data.question15 = "";
        // data.question16 = "";
        // data.question17 = "";
        // data.question18 = "";
        // data.question19 = "";
        // data.question20 = "";



        // Save();
        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);
    }

    public void Load()
    {
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    public void WriteToFile(string fileName, string json)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    public string ReadFromFile(string fileName)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;

        if(File.Exists(path))
        {
            using(StreamReader reader  = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File Not Found");
        }
        return "";
    }

    private string GetFilePath(string fileName)
    {
        return  Application.streamingAssetsPath + "/" + fileName;
    }
}